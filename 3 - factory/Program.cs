Console.WriteLine("C# Factory pattern");

var asus = new Asus();
IGPU asusGPU = asus.CreateGPU();
asusGPU.Burn();

var gigabyte = new Gigabyte();
IGPU gigabyteGPU = gigabyte.CreateGPU();
gigabyteGPU.Burn();

interface IGPU
{
  void StartArtifacting();
  void Burn();
}

class AsusGPU : IGPU
{
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
  public void StartArtifacting()
  {
    Console.WriteLine("Gigabyte GPU started artifacting");
  }
  public void Burn()
  {
    Console.WriteLine("Gigabyte GPU is burning");
  }
}

abstract class Manufacturer
{
  abstract public IGPU CreateGPU();
}

class Asus : Manufacturer
{
  public override IGPU CreateGPU()
  {
    var gpu = new AsusGPU();
    gpu.StartArtifacting();
    return gpu;
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
}
