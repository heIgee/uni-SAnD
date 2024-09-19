console.log('TypeScript Object Pool pattern');

class DbConnectionPool {
  private readonly poolSize: number;
  private readonly pool: DbConnection[] = [];
  private activeConnections: number = 0;
  private readonly connectionString: string;

  constructor(poolSize: number, connectionString: string) {
    this.poolSize = poolSize;
    this.connectionString = connectionString;
  }

  public async getConnection(): Promise<DbConnection> {
    if (this.pool.length > 0) {
      console.log('Taken from pool');
      return this.pool.pop()!;
    }

    if (this.activeConnections < this.poolSize) {
      this.activeConnections++;
      console.log('Created new');
      return new DbConnection(this.connectionString);
    }

    console.log('Waiting for available connection');
    return new Promise((resolve) => {
      const interval = setInterval(() => {
        if (this.pool.length > 0) {
          clearInterval(interval);
          console.log('Taken from pool (after wait)');
          resolve(this.pool.pop()!);
        }
      }, 20);
    });
  }

  public returnConnection(connection: DbConnection): void {
    this.pool.push(connection);
    console.log('Connection returned');
  }
}

class DbConnection {
  private static connectionCounter = 0;
  private readonly connectionId: number;

  constructor(connectionString: string) {
    this.connectionId = ++DbConnection.connectionCounter;
    console.log(`Initialized connection with ${connectionString}`);
  }

  public async query(): Promise<void> {
    console.log(`Connection #${this.connectionId} performs some query`);
  }

  public async open(): Promise<void> {
    console.log(`Connection #${this.connectionId} opened`);
  }

  public async close(): Promise<void> {
    console.log(`Connection #${this.connectionId} closed`);
  }

  public dispose(): void {
    this.close();
  }
}

const connectionPool = new DbConnectionPool(5, 'some-conn-string');

const tasks: Promise<void>[] = [];

for (let i = 0; i < 10; i++) {
  tasks.push(
    (async () => {
      const connection = await connectionPool.getConnection();
      try {
        await connection.open();
        await connection.query();
        await new Promise((resolve) => setTimeout(resolve, 100));
      } finally {
        connectionPool.returnConnection(connection);
      }
    })(),
  );
}

await Promise.all(tasks);

export {};
