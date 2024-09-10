Console.WriteLine("C# Builder pattern");

var builder = new PCBuilder();

PCDirector.MakeMyPC(builder);
var myPC = builder.Build();
Console.WriteLine("My PC Configuration:");
Console.WriteLine(myPC.GetConfiguration());

builder.Reset();

PCDirector.MakeAnotherRandomGPTPC(builder);
var anotherPC = builder.Build();
Console.WriteLine(new string('-', 20));
Console.WriteLine("Another Random PC Configuration:");
Console.WriteLine(anotherPC.GetConfiguration());

class PC
{
  public string? Case { get; set; }
  public string? PSU { get; set; }
  public string? Motherboard { get; set; }
  public string? CPU { get; set; }
  public string? GPU { get; set; }
  public string? RAM { get; set; }
  public string[]? ROM { get; set; }
  public string? CPUCooling { get; set; }
  public string[]? CaseCooling { get; set; }

  public string GetConfiguration()
  {
    var components = new[]
    {
      ("Case", Case),
      ("PSU", PSU),
      ("Motherboard", Motherboard),
      ("CPU", CPU),
      ("GPU", GPU),
      ("RAM", RAM),
      ("CPU Cooling", CPUCooling),
      ("ROM", ROM?.Length > 0 ? string.Join("\n  - ", ROM) : "Not specified"),
      ("Case Cooling", CaseCooling?.Length > 0 ? string.Join("\n  - ", CaseCooling) : "Not specified")
    };
    return string.Join("\n", components.Select(
      c => $"{c.Item1}: {(string.IsNullOrEmpty(c.Item2) ? "Not specified" : c.Item2)}")
    );
  }
}

interface IPCBuilder
{
  IPCBuilder Reset();
  IPCBuilder AddCase(string сase);
  IPCBuilder AddPSU(string psu);
  IPCBuilder AddMotherboard(string motherboard);
  IPCBuilder AddCPU(string cpu);
  IPCBuilder AddGPU(string gpu);
  IPCBuilder AddRAM(string ram);
  IPCBuilder AddROM(string[] rom);
  IPCBuilder AddCPUCooling(string cpuCooling);
  IPCBuilder AddCaseCooling(string[] cpuCooling);
}

class PCBuilder : IPCBuilder
{
  private PC pc = new();

  public IPCBuilder Reset()
  {
    pc = new PC();
    return this;
  }

  public IPCBuilder AddCase(string сase)
  {
    pc.Case = сase;
    return this;
  }

  public IPCBuilder AddPSU(string psu)
  {
    pc.PSU = psu;
    return this;
  }

  public IPCBuilder AddMotherboard(string motherboard)
  {
    pc.Motherboard = motherboard;
    return this;
  }

  public IPCBuilder AddCPU(string cpu)
  {
    pc.CPU = cpu;
    return this;
  }

  public IPCBuilder AddGPU(string gpu)
  {
    pc.GPU = gpu;
    return this;
  }

  public IPCBuilder AddRAM(string ram)
  {
    pc.RAM = ram;
    return this;
  }

  public IPCBuilder AddROM(string[] rom)
  {
    pc.ROM = rom;
    return this;
  }

  public IPCBuilder AddCPUCooling(string cpuCooling)
  {
    pc.CPUCooling = cpuCooling;
    return this;
  }

  public IPCBuilder AddCaseCooling(string[] caseCooling)
  {
    pc.CaseCooling = caseCooling;
    return this;
  }

  public PC Build()
  {
    return pc;
  }
}

class PCDirector
{
  public static void MakeMyPC(PCBuilder builder)
  {
    builder.AddCase("AeroCool Aero One")
      .AddPSU("be quiet! System Power 9 600W")
      .AddMotherboard("Asus TUF B450M PRO-II")
      .AddCPU("AMD Ryzen 5500")
      .AddGPU("Asus Dual GTX 1060 6GB")
      .AddRAM("Kingston Fury Beast DDR4 2x8GB")
      .AddROM(["Samsung 980 500GB", "TEAM EX2 1TB", "TEAM GX1 240GB", "WDC 640GB"])
      .AddCPUCooling("DeepCool Gammaxx GTE V2")
      .AddCaseCooling(["DeepCool RF120 3 in 1"]);
  }

  public static void MakeAnotherRandomGPTPC(PCBuilder builder)
  {
    builder.AddCase("NZXT H510")
      .AddPSU("Corsair RM750x 750W")
      .AddMotherboard("MSI MAG B550 TOMAHAWK")
      .AddCPU("Intel Core i5-13600K")
      .AddGPU("NVIDIA GeForce RTX 4070")
      .AddRAM("G.Skill Ripjaws V DDR4 2x16GB")
      .AddROM(["Crucial P5 Plus 1TB", "Seagate Barracuda 2TB"])
      .AddCPUCooling("Noctua NH-U12S")
      .AddCaseCooling(["Noctua NF-A12x25", "Corsair LL120"]);
  }
}