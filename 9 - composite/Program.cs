Console.WriteLine("C# Composite pattern");

var pc = new PcComposite("Personal Computer", [
  new PcComposite("CPU", [
    new PcLeaf("Core0", 10),
    new PcLeaf("Core1", 10),
    new PcComposite("Cache", [
      new PcLeaf("L3", 5),
      new PcLeaf("L2", 3),
      new PcLeaf("L1", 2)
    ]),
    new PcLeaf("IMC", 8),
    new PcLeaf("Bus Interface", 2),
    new PcLeaf("Power Delivery System", 12)
  ]),

  new PcComposite("GPU", [
    new PcComposite("Shaders", [
      new PcLeaf("Shader0", 25),
      new PcLeaf("Shader1", 25),
      new PcLeaf("Shader2", 25),
      new PcLeaf("Shader3", 25),
    ]),
    new PcLeaf("VRAM", 15),
    new PcLeaf("Memory Controller", 10),
    new PcLeaf("Rasterizers", 20),
    new PcLeaf("Display Controller", 5),
    new PcLeaf("PCIe Interface", 3)
  ]),

  new PcComposite("RAM", [
    new PcLeaf("Stick0", 8),
    new PcLeaf("Stick1", 8)
  ]),

  new PcComposite("Storage", [
    new PcLeaf("SSD", 5),
    new PcLeaf("HDD", 10)
  ]),

  new PcComposite("Motherboard", [
    new PcLeaf("Chipset", 15),
    new PcLeaf("Voltage Regulators", 5)
  ]),

  new PcLeaf("PSU", 50),
  new PcComposite("Cooling", [
    new PcLeaf("CPU Fan0", 5),
    new PcLeaf("Case Fan0", 3),
    new PcLeaf("Case Fan1", 3)
  ])
]);

Console.WriteLine($"Total PC power consumption is: {pc.GetTotalPower()}W");

interface IPcComponent
{
  int GetTotalPower();
}

class PcLeaf(string name, int power) : IPcComponent
{
  public string Name => name;
  public int GetTotalPower()
  {
    return power;
  }
}

class PcComposite(string name, List<IPcComponent> components) : IPcComponent
{
  public string Name => name;
  public int GetTotalPower()
  {
    return components.Aggregate(0, (acc, cur) => acc + cur.GetTotalPower());
  }
}
