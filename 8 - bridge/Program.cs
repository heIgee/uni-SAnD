Console.WriteLine("C# Bridge pattern");

BackEnd aspNet = new AspNet();
FrontEnd blazor = new Blazor(aspNet);

blazor.Login();
Console.WriteLine("---");

blazor.ShowAllTasks();
Console.WriteLine("---");

blazor.EditTask();
Console.WriteLine("---");

blazor.RemoveTask();
Console.WriteLine("---");

blazor.Logout();
Console.WriteLine("---");

blazor.RemoveTask();
Console.WriteLine("---");

abstract class FrontEnd(BackEnd backEnd)
{
  protected BackEnd backEnd = backEnd;
  public virtual bool IsLoggedIn => backEnd.IsAuthenticated;

  public virtual void Login() => backEnd.Authenticate();
  public virtual void Logout() => backEnd.InvalidateSession();

  public abstract void ShowAllTasks();
  public abstract void EditTask();
  public abstract void RemoveTask();
}

class Blazor(BackEnd backEnd) : FrontEnd(backEnd)
{
  public override void ShowAllTasks()
  {
    Console.WriteLine("Trying to show all tasks");
    var tasks = backEnd.FindAllTasks();
    Console.WriteLine($"Tasks {tasks} are shown on the UI");
  }

  public override void EditTask()
  {
    Console.WriteLine("Trying to edit some task");
    if (!IsLoggedIn) return;
    var updatedTask = backEnd.UpdateTask();
    if (updatedTask is null) return;
    Console.WriteLine($"Task {updatedTask} is changed on the UI");
  }

  public override void RemoveTask()
  {
    Console.WriteLine("Trying to delete some task");
    if (!IsLoggedIn) return;
    var deletedTask = backEnd.DeleteTask();
    Console.WriteLine($"Tasks {deletedTask} is removed from the UI");
  }
}

abstract class BackEnd
{
  public virtual bool IsAuthenticated { get; protected set; } = false;

  public virtual void Authenticate()
  {
    Console.WriteLine("User has been authenticated");
    IsAuthenticated = true;
  }

  public virtual void InvalidateSession()
  {
    Console.WriteLine("Session has been invalidated");
    IsAuthenticated = false;
  }

  public abstract string FindAllTasks();
  public abstract string? FindTaskById();
  public abstract string? UpdateTask();
  public abstract string DeleteTask();
}

class AspNet : BackEnd
{
  protected bool _isAuthenticated = false;
  public override bool IsAuthenticated
  {
    get
    {
      Console.WriteLine(_isAuthenticated
        ? "User is authenticated, permission granted"
        : "User is not authenticated, forbidden");
      return _isAuthenticated;
    }
    protected set => _isAuthenticated = value;
  }

  public override string FindAllTasks()
  {
    Console.WriteLine("All tasks are retrieved from the DB");
    return "[All the tasks]";
  }

  public override string? FindTaskById()
  {
    bool isFound = Random.Shared.NextSingle() > 0.5;
    Console.WriteLine(isFound
    ? "Task is found in the DB"
    : "Task is not found in the DB");
    return isFound ? "[Specific task]" : null;
  }

  public override string? UpdateTask()
  {
    var task = FindTaskById();
    Console.WriteLine(task is not null
    ? $"Updating the task {task}"
    : $"Cannot update the task");
    return task is not null ? "[Updated task]" : null;
  }

  public override string DeleteTask()
  {
    Console.WriteLine("Task is deleted from the DB");
    return "[Deleted task]";
  }
}
