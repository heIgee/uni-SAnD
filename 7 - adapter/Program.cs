Console.WriteLine("C# Adapter pattern");

var usb = new USB(5);
Console.WriteLine($"We have {usb}");

var dcAdapter = new USBToDCAdapter(usb);
DCSocket.Plug(dcAdapter.Adapt());

interface ICable
{
  string Type { get; }
  double Voltage { get; }
  string ToString();
}

class USB(double voltage) : ICable
{
  public string Type => "USB";
  public double Voltage { get; } = voltage;

  public override string ToString()
  {
    return $"{Type} {Voltage}V cable";
  }
}

class DC(double voltage) : ICable
{
  public string Type => "DC";
  public double Voltage { get; } = voltage;

  public override string ToString()
  {
    return $"{Type} {Voltage}V cable";
  }
}

class DCSocket
{
  public static void Plug(DC cable)
  {
    Console.WriteLine($"{cable} is being plugged into DC socket");
  }
}

class USBToDCAdapter(USB usb) : ICable
{
  private readonly DC dc = new(usb.Voltage * 2.4);
  public string Type => dc.Type;
  public double Voltage => dc.Voltage;

  public override string ToString()
  {
    return dc.ToString();
  }

  public DC Adapt()
  {
    Console.WriteLine($"Adapter is adapting {usb} into {dc}");
    return dc;
  }
}

