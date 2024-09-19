using System.Collections.Concurrent;

Console.WriteLine("C# Object Pool pattern");

var connectionPool = new DbConnectionPool(5, "some-conn-string");

var tasks = new Task[10];

for (int i = 0; i < tasks.Length; i++)
{
  int taskId = i;
  tasks[i] = Task.Run(() =>
  {
    var connection = connectionPool.GetConnection();
    try
    {
      connection.Open();
      connection.Query();
      Thread.Sleep(100);
    }
    finally
    {
      connectionPool.ReturnConnection(connection);
    }
  });
}

Task.WaitAll(tasks);

class DbConnectionPool
{
  private readonly int _poolSize;
  private readonly ConcurrentBag<DbConnection> _pool = [];
  private readonly string _connectionString;
  private int _activeConnections = 0;

  public DbConnectionPool(int poolSize, string connectionString)
  {
    _poolSize = poolSize;
    _connectionString = connectionString;
  }

  public DbConnection GetConnection()
  {
    if (_pool.TryTake(out var connection))
    {
      Console.WriteLine("Taken from pool");
      return connection;
    }

    if (Interlocked.Increment(ref _activeConnections) <= _poolSize)
    {
      Console.WriteLine("Created new");
      return new DbConnection(_connectionString);
    }

    Interlocked.Decrement(ref _activeConnections);
    Console.WriteLine("Waiting for available connection");

    var spinWait = new SpinWait();
    while (!_pool.TryTake(out connection))
    {
      spinWait.SpinOnce();
    }

    Console.WriteLine("Taken from pool (after wait)");
    return connection;
  }

  public void ReturnConnection(DbConnection connection)
  {
    _pool.Add(connection);
    Console.WriteLine("Connection returned");
  }

  public class DbConnection : IDisposable
  {
    private static int _connectionCounter = 0;
    private readonly int _connectionId;

    public DbConnection(string connectionString)
    {
      _connectionId = Interlocked.Increment(ref _connectionCounter);
      _ = connectionString;
    }

    public void Query() => Console.WriteLine($"Connection #{_connectionId} performs some query");
    public void Open() => Console.WriteLine($"Connection #{_connectionId} opened");
    public void Close() => Console.WriteLine($"Connection #{_connectionId} closed");
    public void Dispose()
    {
      Close();
      GC.SuppressFinalize(this);
    }
  }
}
