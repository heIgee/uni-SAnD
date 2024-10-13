Console.WriteLine("C# Flyweight pattern");

var factory = new LaptopModelFactory();

var latitude7490 = factory.GetLaptopModel(new(
  "Latitude 7490",
  "Dell",
  "Windows 10",
  2018,
  14.0f
));

var nice7490 = new Laptop(
  latitude7490,
  "1920x1080 Anti-Glare",
  "Intel Core i7-8650U",
  new(16, "DDR4"),
  [new(512, "SSD")],
  "Intel UHD Graphics 620",
  60
);

var fakeLatitude7490WhereOnlyModelNameMatters = factory.GetLaptopModel(new(
  "Latitude 7490",
  "Asus",
  "Windows 12",
  2026,
  16.8f
));

var lame7490 = new Laptop(
  fakeLatitude7490WhereOnlyModelNameMatters,
  "1366x768 Anti-Glare",
  "Intel Core i5-8350U",
  new(8, "DDR4"),
  [new(256, "SSD")],
  "Intel UHD Graphics 620",
  42
);

Console.WriteLine(nice7490);
Console.WriteLine(lame7490);
Console.WriteLine($"Are laptop models the same object? {ReferenceEquals(nice7490.Model, lame7490.Model)}");

class LaptopModelFactory
{
  private readonly Dictionary<string, LaptopModel> laptopModels = [];

  public LaptopModel GetLaptopModel(LaptopModel modelToCopy)
  {
    if (!laptopModels.TryGetValue(modelToCopy.Name, out var model))
    {
      model = modelToCopy with { };
      laptopModels.Add(modelToCopy.Name, model);
    }
    return model;
  }
}

record LaptopModel(
  string Name,
  string Brand,
  string OperatingSystem,
  int YearReleased,
  float ScreenSize
);

record Laptop(
  LaptopModel Model,
  string Display,
  string Processor,
  Tuple<int, string> Ram,
  List<Tuple<int, string>> Storage,
  string Gpu,
  int BatteryCapacity
);
