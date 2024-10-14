Console.WriteLine("C# Proxy pattern");

var oldService = new LaptopInfoService();
var proxyService = new LaptopInfoProxy();

var details = new DetailsComponent(oldService);
var catalog = new CatalogComponent(proxyService);

details.Render();
catalog.Render();

record LaptopInfo(
  string Id,
  string Brand,
  string Model,
  float Price,
  string? Cpu = null,
  string? Gpu = null,
  Tuple<int, string>? Ram = null,
  List<Tuple<int, string>>? Storage = null,
  string? Display = null,
  string? Weight = null,
  string? Battery = null,
  string? Os = null
);

class LaptopInfoApi
{
  public static LaptopInfo GetFullInfo(string id) =>
    new(id, "HP", "Pavilion 15-eh3017ua", 800,
      "AMD Ryzen 5 7530U (2.0-4.5 GHz)",
      "AMD Radeon Graphics",
      new(16, "DDR4"),
      [new(512, "SSD NVMe")],
      "15.6\" FHD IPS",
      "1.74 kg",
      "3-cell, 41 Wh (50% in 45 min)",
      "FreeDOS 3.0"
    );

  public static LaptopInfo GetPartialInfo(string id) =>
    new(id, "HP", "Pavilion 15-eh3017ua", 800);
}

class LaptopInfoService
{
  public virtual LaptopInfo GetInfo(string id)
  {
    Console.WriteLine("Old service requesting full info");
    return LaptopInfoApi.GetFullInfo(id);
  }
}

class LaptopInfoProxy : LaptopInfoService
{
  public override LaptopInfo GetInfo(string id)
  {
    Console.WriteLine("Proxy service requesting partial info");
    return LaptopInfoApi.GetPartialInfo(id);
  }
}

interface IComponent
{
  void Render();
}

class DetailsComponent(LaptopInfoService service) : IComponent
{
  public void Render()
  {
    var info = service.GetInfo(Guid.NewGuid().ToString());
    Console.WriteLine($"{GetType()} rendering {info}");
  }
}

class CatalogComponent(LaptopInfoService service) : IComponent
{
  public void Render()
  {
    var info = service.GetInfo(Guid.NewGuid().ToString());
    Console.WriteLine($"{GetType()} rendering {info}");
  }
}
