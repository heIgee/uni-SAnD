Console.WriteLine("C# Singleton pattern");

var tasks = new Task[10];

for (int i = 0; i < tasks.Length; i++)
{
  tasks[i] = Task.Run(() =>
  {
    var dbManager = DbConnectionManager.Instance;
    Console.WriteLine(dbManager.Message);
  });
}

Task.WaitAll(tasks);

for (int i = 0; i < tasks.Length; i++)
{
  tasks[i] = Task.Run(() =>
  {
    var dbManager = DbConnectionManager.Instance;
    using var connection = dbManager.CreateConnection("some conn string");
    connection.Open();
    // some operations
  });
}

Task.WaitAll(tasks);

class DbConnectionManager
{
  private DbConnectionManager()
  {
    _instanceCounter++;
    _instanceId = _instanceCounter;
  }

  private static int _instanceCounter = 0;
  private readonly int _instanceId;
  private static readonly Lazy<DbConnectionManager> _instance
    = new(() => new DbConnectionManager());

  public static DbConnectionManager Instance => _instance.Value;
  public string Message => $"I am instance #{_instanceId}";

  public DbConnection CreateConnection(string connectionString)
  {
    return new DbConnection(connectionString, _instanceId);
  }

  public class DbConnection : IDisposable
  {
    private readonly int _contextCounter;
    private static int _connectionCounter = 0;
    private readonly int _connectionId;

    public DbConnection(string connectionString, int contextCounter)
    {
      _connectionCounter++;
      _connectionId = _connectionCounter;
      _contextCounter = contextCounter;
      Initialize(connectionString);
    }

    private void Initialize(string connectionString) { }
    public void Open() => Console.WriteLine(
      $"Instance #{_contextCounter} connection #{_connectionId} opened"
    );
    public void Close() => Console.WriteLine(
      $"Instance #{_contextCounter} connection #{_connectionId} closed"
    );
    public void Dispose()
    {
      Close();
      GC.SuppressFinalize(this);
    }
  }
}