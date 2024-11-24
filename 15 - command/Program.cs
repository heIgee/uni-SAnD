Console.WriteLine("C# Command pattern");

var receiver = new Receiver();
var command = new RealCommand(receiver, new("[crucial data]"));
var invoker = new Invoker(command);
invoker.ExecuteCommand();


record Args(
  string Data
);

interface ICommand
{
  void Execute();
}

class RealCommand(Receiver receiver, Args args) : ICommand
{
  public void Execute()
  {
    receiver.BusinessLogic(args);
  }
}

class Receiver
{
  public void BusinessLogic(Args args)
  {
    Console.WriteLine($"Evaluating {args}");
  }
}

class Invoker(ICommand command)
{
  public ICommand Command { get; set; } = command;

  public void ExecuteCommand()
  {
    Command.Execute();
  }
}