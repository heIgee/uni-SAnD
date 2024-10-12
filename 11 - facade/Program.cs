Console.WriteLine("C# Facade pattern");

var orderService = new CustomerOrderService(isTrustedCustomer: true);
orderService.Purchase("High-end PC", 1400, "NovaPost", "Wall St. 69");

class CustomerOrderService(bool isTrustedCustomer)
{
  public void Purchase(string product, double price, string postalOperator, string address)
  {
    var finalPrice = price;
    if (isTrustedCustomer)
    {
      finalPrice *= 0.9;
      Console.WriteLine($"Is trusted customer - price reduced by ${price - finalPrice}");
    }

    Console.WriteLine($"Purchase of {product} valued at ${finalPrice} to be shipped via {postalOperator} is initiated");

    var paymentService = new PaymentService("LiqPay");
    paymentService.Transfer(finalPrice);

    var taxService = new TaxService("Ukraine");
    var taxAmount = taxService.CalculateVAT(finalPrice);
    taxService.PayTaxes(taxAmount);

    var buildService = new ProductBuildService(product);
    var builtProduct = buildService.Build();

    var postalService = new PostalService(postalOperator);
    postalService.Send(builtProduct, address);
  }
}

class PaymentService(string provider)
{
  public void Transfer(double amount)
  {
    Console.WriteLine($"${amount} is transferred via {provider} payment service");
  }
}

class TaxService(string government)
{
  public double CalculateVAT(double price) => government switch
  {
    "Ukraine" => price * 0.2,
    "Hungary" => price * 0.27,
    "California" => price * 0.0725,
    _ => 0
  };

  public void PayTaxes(double amount)
  {
    Console.WriteLine($"${amount} of taxes is payed to the {government} government");
  }
}

class ProductBuildService(string product)
{
  public string Build()
  {
    Console.WriteLine($"Building {product} is in progress");
    return $"[Newly built {product}]";
  }
}

class PostalService(string оperator)
{
  public void Send(string package, string address)
  {
    Console.WriteLine($"Package {package} is being sent by {оperator} postal service to {address}");
  }
}


