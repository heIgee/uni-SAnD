using System.Text.Json;

Console.WriteLine("C# Prototype pattern");

var duisburgShipment = new ExpressShipment(
    new Destination { Country = "Germany", City = "Duisburg" },
    32000,
    2
);
Console.WriteLine(duisburgShipment.GetSummary());

var clonedShipment = duisburgShipment.Clone();
Console.WriteLine(clonedShipment.GetSummary());

Console.WriteLine($"Objects are equal: {duisburgShipment == clonedShipment}");
Console.WriteLine($"Their destinations are equal: {duisburgShipment.Destination == clonedShipment.Destination}");

interface IShipment
{
  IShipment Clone();
  string GetSummary();
}

class Destination
{
  public string Country { get; set; } = "";
  public string City { get; set; } = "";
  public Destination() { }
  public Destination(Destination other)
  {
    Country = other.Country;
    City = other.City;
  }
}

abstract class Shipment : IShipment
{
  protected readonly Destination destination;
  protected readonly double weight;

  public Destination Destination => destination;

  protected Shipment(Destination destination, double weight)
  {
    this.destination = destination;
    this.weight = weight;
  }

  public abstract IShipment Clone();
  public abstract string GetSummary();
}

class ExpressShipment : Shipment
{
  private readonly int priority;

  public ExpressShipment(Destination destination, double weight, int priority)
    : base(destination, weight)
  {
    this.priority = priority;
  }
  public ExpressShipment(ExpressShipment other)
    : this(new Destination(other.destination), other.weight, other.priority) { }

  public override ExpressShipment Clone()
  {
    return new ExpressShipment(this);
  }
  public override string GetSummary()
  {
    return $"Express Shipment to {destination.City} ({destination.Country}) weighing {weight}kg with priority <{priority}>";
  }
}

