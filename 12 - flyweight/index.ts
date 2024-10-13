console.log('TypeScript Flyweight pattern');

class LaptopModelFactory {
  private laptopModels: Map<string, LaptopModel> = new Map();

  getLaptopModel(modelToCopy: LaptopModel): LaptopModel {
    let model = this.laptopModels.get(modelToCopy.name);
    if (!model) {
      model = { ...modelToCopy };
      this.laptopModels.set(modelToCopy.name, model);
    }
    return model;
  }
}

type LaptopModel = {
  name: string;
  brand: string;
  operatingSystem: string;
  yearReleased: number;
  screenSize: number;
};

type Laptop = {
  model: LaptopModel;
  display: string;
  processor: string;
  ram: [number, string];
  storage: [number, string][];
  gpu: string;
  batteryCapacity: number;
};

const factory = new LaptopModelFactory();

const latitude7490 = factory.getLaptopModel({
  name: 'Latitude 7490',
  brand: 'Dell',
  operatingSystem: 'Windows 10',
  yearReleased: 2018,
  screenSize: 14.0,
});

const nice7490: Laptop = {
  model: latitude7490,
  display: '1920x1080 Anti-Glare',
  processor: 'Intel Core i7-8650U',
  ram: [16, 'DDR4'],
  storage: [[512, 'NVMe']],
  gpu: 'Intel UHD Graphics 620',
  batteryCapacity: 60,
};

const fakeLatitude7490WhereOnlyModelNameMatters = factory.getLaptopModel({
  name: 'Latitude 7490',
  brand: 'Asus',
  operatingSystem: 'Windows 12',
  yearReleased: 2026,
  screenSize: 16.8,
});

const lame7490: Laptop = {
  model: fakeLatitude7490WhereOnlyModelNameMatters,
  display: '1366x768 Anti-Glare',
  processor: 'Intel Core i5-8350U',
  ram: [8, 'DDR4'],
  storage: [[256, 'SSD']],
  gpu: 'Intel UHD Graphics 620',
  batteryCapacity: 42,
};

console.log(nice7490);
console.log(lame7490);
console.log(
  `Are laptop models the same object? ${nice7490.model === lame7490.model}`,
);
