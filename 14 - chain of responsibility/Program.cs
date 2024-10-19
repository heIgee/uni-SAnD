Console.WriteLine("C# Chain of Responsibility pattern");

var handler = new AuthHandler();
handler
  .SetNextHandler(new ProductHandler())
  .SetNextHandler(new PaymentHandler());

var orderService = new OrderService(handler);
orderService.Order(new(true, "High-End Gaming Rig", 2000));
orderService.Order(new(true, "Budget Office Laptop", 350));
orderService.Order(new(true, "Mid-Level Smartphone", 00));
orderService.Order(new(false, "4K Monitor", 600));

record Order(
  bool IsAuthenticated,
  string Product,
  float Payment
);

static class ProductDatabase
{
  public static readonly List<(string, float)> products = [
    new("High-End Gaming Rig", 1800),
    new("Budget Office Laptop", 550),
    new("4K Monitor", 400),
  ];
}

class OrderService(OrderHandler handler)
{
  public bool Order(Order order)
  {
    Console.WriteLine($"{order} has been placed");

    if (handler.Handle(order))
    {
      Console.WriteLine($"All is well, the order of {order.Product} is proceeding");
      return true;
    }

    Console.WriteLine($"Order of {order.Product} failed");
    return false;
  }
}

abstract class OrderHandler
{
  private OrderHandler? next;

  public OrderHandler SetNextHandler(OrderHandler next)
  {
    this.next = next;
    return next;
  }

  public abstract bool Handle(Order order);

  protected bool Next(Order order)
  {
    if (next is null) return true;
    return next.Handle(order);
  }
}

class AuthHandler : OrderHandler
{
  public override bool Handle(Order order)
  {
    if (!order.IsAuthenticated)
    {
      Console.WriteLine("User is not authenticated");
      return false;
    }

    Console.WriteLine("User is authenticated");
    return Next(order);
  }
}

class ProductHandler : OrderHandler
{
  public override bool Handle(Order order)
  {
    var isProductAvailable = ProductDatabase.products
        .Any(p => p.Item1.Equals(order.Product));
    if (!isProductAvailable)
    {
      Console.WriteLine("Product is not available");
      return false;
    }

    Console.WriteLine("Product is available");
    return Next(order);
  }
}

class PaymentHandler : OrderHandler
{
  public override bool Handle(Order order)
  {
    var product = ProductDatabase.products.Find(p => p.Item1.Equals(order.Product));
    if (order.Payment < product.Item2)
    {
      Console.WriteLine("Insufficient payment");
      return false;
    }

    Console.WriteLine("Sufficient payment");
    return Next(order);
  }
}