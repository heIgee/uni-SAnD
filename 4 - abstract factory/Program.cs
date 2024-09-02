Console.WriteLine("C# Abstract Factory pattern");

var asus = new Asus();
List<IComponent> asusComponents = [
  asus.CreateGPU(),
  asus.CreateMonitor()
];
asusComponents.ForEach(c => c.Fail());

var gigabyte = new Gigabyte();
List<IComponent> gigabyteComponents = [
  gigabyte.CreateGPU(),
  gigabyte.CreateMonitor()
];
gigabyteComponents.ForEach(c => c.Fail());

interface IComponent
{
  void Fail();
}

interface IGPU : IComponent
{
  void StartArtifacting();
  void Burn();
}

class AsusGPU : IGPU
{
  public void Fail() => Burn();
  public void StartArtifacting()
  {
    Console.WriteLine("Asus GPU started artifacting");
  }
  public void Burn()
  {
    Console.WriteLine("Asus GPU is burning");
  }
}

class GigabyteGPU : IGPU
{
  public void Fail() => Burn();
  public void StartArtifacting()
  {
    Console.WriteLine("Gigabyte GPU started artifacting");
  }
  public void Burn()
  {
    Console.WriteLine("Gigabyte GPU is burning");
  }
}

interface IMonitor : IComponent
{
  void PixelStruck();
  void Flicker();
}

class AsusMonitor : IMonitor
{
  public void Fail() => Flicker();
  public void PixelStruck()
  {
    Console.WriteLine("Asus monitor got some stuck pixels");
  }
  public void Flicker()
  {
    Console.WriteLine("Asus monitor is flickering");
  }
}

class GigabyteMonitor : IMonitor
{
  public void Fail() => Flicker();
  public void PixelStruck()
  {
    Console.WriteLine("Gigabyte monitor got some stuck pixels");
  }
  public void Flicker()
  {
    Console.WriteLine("Gigabyte monitor is flickering");
  }
}

abstract class Manufacturer
{
  abstract public IGPU CreateGPU();
  abstract public IMonitor CreateMonitor();
}

class Asus : Manufacturer
{
  public override IGPU CreateGPU()
  {
    var gpu = new AsusGPU();
    gpu.StartArtifacting();
    return gpu;
  }
  public override IMonitor CreateMonitor()
  {
    var moinitor = new AsusMonitor();
    moinitor.PixelStruck();
    return moinitor;
  }
}

class Gigabyte : Manufacturer
{
  public override IGPU CreateGPU()
  {
    var gpu = new GigabyteGPU();
    gpu.StartArtifacting();
    return gpu;
  }
  public override IMonitor CreateMonitor()
  {
    var moinitor = new GigabyteMonitor();
    moinitor.PixelStruck();
    return moinitor;
  }
}
