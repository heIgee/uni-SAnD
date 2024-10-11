Console.WriteLine("C# Decorator pattern");

var delivery = new PostComputerDelivery();
var deliveryWithKeyboard = new KeyboardPresentDecorator(delivery);
var deliveryWithKeyboardAndMouse = new MousePresentDecorator(deliveryWithKeyboard);
deliveryWithKeyboardAndMouse.Send();

interface IComputerDelivery
{
  void Send();
}

class PostComputerDelivery : IComputerDelivery
{
  public void Send()
  {
    Console.WriteLine("Computer is being sent by post");
  }
}

abstract class PresentDecorator(IComputerDelivery delivery) : IComputerDelivery
{
  private readonly IComputerDelivery delivery = delivery;

  public virtual void Send()
  {
    delivery.Send();
  }
}

class KeyboardPresentDecorator(IComputerDelivery delivery) : PresentDecorator(delivery)
{
  public override void Send()
  {
    base.Send();
    Console.WriteLine("Keyboard present is attached to the package");
  }
}

class MousePresentDecorator(IComputerDelivery delivery) : PresentDecorator(delivery)
{
  public override void Send()
  {
    base.Send();
    Console.WriteLine("Mouse present is attached to the package");
  }
}