console.log('TypeScript Singleton pattern');

class DbConnectionManager {
  private static _instance: DbConnectionManager;
  private static _instanceCounter = 0;
  private readonly _instanceId: number;

  private constructor() {
    DbConnectionManager._instanceCounter++;
    this._instanceId = DbConnectionManager._instanceCounter;
  }

  public static instance(): DbConnectionManager {
    if (!DbConnectionManager._instance) {
      DbConnectionManager._instance = new DbConnectionManager();
    }
    return DbConnectionManager._instance;
  }

  public get message(): string {
    return `I am instance #${this._instanceId}`;
  }

  public createConnection(connectionString: string): DbConnection {
    return new DbConnection(connectionString, this._instanceId);
  }
}

class DbConnection {
  private static connectionCounter = 0;
  private readonly connectionId: number;

  constructor(
    private connectionString: string,
    private contextCounter: number,
  ) {
    DbConnection.connectionCounter++;
    this.connectionId = DbConnection.connectionCounter;
    this.initialize(connectionString);
  }

  private initialize(connectionString: string) {}

  public open() {
    console.log(
      `Instance #${this.contextCounter} connection #${this.connectionId} opened`,
    );
  }

  public close() {
    console.log(
      `Instance #${this.contextCounter} connection #${this.connectionId} closed`,
    );
  }
}

const tasks: Promise<void>[] = [];

for (let i = 0; i < 10; i++) {
  tasks.push(
    new Promise<void>((resolve) => {
      const dbManager = DbConnectionManager.instance();
      console.log(dbManager.message);
      resolve();
    }),
  );
}

await Promise.all(tasks);

tasks.length = 0;

for (let i = 0; i < 10; i++) {
  tasks.push(
    new Promise<void>((resolve) => {
      const dbManager = DbConnectionManager.instance();
      const connection = dbManager.createConnection('some conn string');
      connection.open();
      // some operations
      connection.close();
      resolve();
    }),
  );
}

await Promise.all(tasks);

export {};
