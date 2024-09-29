console.log('TypeScript Bridge pattern');

abstract class FrontEnd {
  constructor(protected backEnd: BackEnd) {}

  get isLoggedIn(): boolean {
    return this.backEnd.isAuthenticated;
  }

  login(): void {
    this.backEnd.authenticate();
  }

  logout(): void {
    this.backEnd.invalidateSession();
  }

  abstract showAllTasks(): void;
  abstract editTask(): void;
  abstract removeTask(): void;
}

class React extends FrontEnd {
  showAllTasks(): void {
    console.log('Trying to show all tasks');
    const tasks = this.backEnd.findAllTasks();
    console.log(`Tasks ${tasks} are shown on the UI`);
  }

  editTask(): void {
    console.log('Trying to edit some task');
    if (!this.isLoggedIn) return;
    const updatedTask = this.backEnd.updateTask();
    if (updatedTask === null) return;
    console.log(`Task ${updatedTask} is changed on the UI`);
  }

  removeTask(): void {
    console.log('Trying to delete some task');
    if (!this.isLoggedIn) return;
    const deletedTask = this.backEnd.deleteTask();
    console.log(`Task ${deletedTask} is removed from the UI`);
  }
}

abstract class BackEnd {
  protected _isAuthenticated = false;

  get isAuthenticated(): boolean {
    return this._isAuthenticated;
  }

  authenticate(): void {
    console.log('User has been authenticated');
    this._isAuthenticated = true;
  }

  invalidateSession(): void {
    console.log('Session has been invalidated');
    this._isAuthenticated = false;
  }

  abstract findAllTasks(): string;
  abstract findTaskById(): string | null;
  abstract updateTask(): string | null;
  abstract deleteTask(): string;
}

class Nest extends BackEnd {
  get isAuthenticated(): boolean {
    console.log(
      this._isAuthenticated
        ? 'User is authenticated, permission granted'
        : 'User is not authenticated, forbidden',
    );
    return this._isAuthenticated;
  }

  findAllTasks(): string {
    console.log('All tasks are retrieved from the DB');
    return '[All the tasks]';
  }

  findTaskById(): string | null {
    const isFound = Math.random() > 0.5;
    console.log(
      isFound ? 'Task is found in the DB' : 'Task is not found in the DB',
    );
    return isFound ? '[Specific task]' : null;
  }

  updateTask(): string | null {
    const task = this.findTaskById();
    console.log(task ? `Updating the task ${task}` : 'Cannot update the task');
    return task ? '[Updated task]' : null;
  }

  deleteTask(): string {
    console.log('Task is deleted from the DB');
    return '[Deleted task]';
  }
}

const nest: BackEnd = new Nest();
const react: FrontEnd = new React(nest);

react.login();
console.log('---');

react.showAllTasks();
console.log('---');

react.editTask();
console.log('---');

react.removeTask();
console.log('---');

react.logout();
console.log('---');

react.removeTask();
console.log('---');
