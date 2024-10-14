console.log('TypeScript Proxy pattern');

type LaptopInfo = {
  id: string;
  brand: string;
  model: string;
  price: number;
  cpu?: string;
  gpu?: string;
  ram?: [number, string];
  storage?: [number, string][];
  display?: string;
  weight?: string;
  battery?: string;
  os?: string;
};

class LaptopInfoApi {
  static getFullInfo(id: string): LaptopInfo {
    return {
      id,
      brand: 'HP',
      model: 'Pavilion 15-eh3017ua',
      price: 800,
      cpu: 'AMD Ryzen 5 7530U (2.0-4.5 GHz)',
      gpu: 'AMD Radeon Graphics',
      ram: [16, 'DDR4'],
      storage: [[512, 'SSD NVMe']],
      display: '15.6" FHD IPS',
      weight: '1.74 kg',
      battery: '3-cell, 41 Wh (50% in 45 min)',
      os: 'FreeDOS 3.0',
    };
  }

  static getPartialInfo(id: string): LaptopInfo {
    return {
      id,
      brand: 'HP',
      model: 'Pavilion 15-eh3017ua',
      price: 800,
    };
  }
}

class LaptopInfoService {
  getInfo(id: string): LaptopInfo {
    console.log('Old service requesting full info');
    return LaptopInfoApi.getFullInfo(id);
  }
}

class LaptopInfoProxy extends LaptopInfoService {
  getInfo(id: string): LaptopInfo {
    console.log('Proxy service requesting partial info');
    return LaptopInfoApi.getPartialInfo(id);
  }
}

interface IComponent {
  render(): void;
}

class DetailsComponent implements IComponent {
  constructor(private service: LaptopInfoService) {}

  render(): void {
    const info = this.service.getInfo(crypto.randomUUID());
    console.log(`${this.constructor.name} rendering`, info);
  }
}

class CatalogComponent implements IComponent {
  constructor(private service: LaptopInfoService) {}

  render(): void {
    const info = this.service.getInfo(crypto.randomUUID());
    console.log(`${this.constructor.name} rendering`, info);
  }
}

const oldService = new LaptopInfoService();
const proxyService = new LaptopInfoProxy();

const details = new DetailsComponent(oldService);
const catalog = new CatalogComponent(proxyService);

details.render();
catalog.render();
