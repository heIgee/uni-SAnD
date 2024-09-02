console.log('TypeScript Abstract Factory pattern');

interface IComponent {
  fail(): void;
}

interface IGPU extends IComponent {
  startArtifacting(): void;
  burn(): void;
}

class AsusGPU implements IGPU {
  fail() {
    this.burn();
  }
  startArtifacting() {
    console.log('Asus GPU started artifacting');
  }
  burn() {
    console.log('Asus GPU is burning');
  }
}

class GigabyteGPU implements IGPU {
  fail() {
    this.burn();
  }
  startArtifacting() {
    console.log('Gigabyte GPU started artifacting');
  }
  burn() {
    console.log('Gigabyte GPU is burning');
  }
}

interface IMonitor extends IComponent {
  pixelStruck(): void;
  flicker(): void;
}

class AsusMonitor implements IMonitor {
  fail() {
    this.flicker();
  }
  pixelStruck() {
    console.log('Asus monitor got some stuck pixels');
  }
  flicker() {
    console.log('Asus monitor is flickering');
  }
}

class GigabyteMonitor implements IMonitor {
  fail() {
    this.flicker();
  }
  pixelStruck() {
    console.log('Gigabyte monitor got some stuck pixels');
  }
  flicker() {
    console.log('Gigabyte monitor is flickering');
  }
}

abstract class Manufacturer {
  abstract createGPU(): IGPU;
  abstract createMonitor(): IMonitor;
}

class Asus extends Manufacturer {
  createGPU(): IGPU {
    const gpu = new AsusGPU();
    gpu.startArtifacting();
    return gpu;
  }
  createMonitor(): IMonitor {
    const monitor = new AsusMonitor();
    monitor.pixelStruck();
    return monitor;
  }
}

class Gigabyte extends Manufacturer {
  createGPU(): IGPU {
    const gpu = new GigabyteGPU();
    gpu.startArtifacting();
    return gpu;
  }
  createMonitor(): IMonitor {
    const monitor = new GigabyteMonitor();
    monitor.pixelStruck();
    return monitor;
  }
}

const asus = new Asus();
const asusComponentsDamnPrettier: IComponent[] = [
  asus.createGPU(),
  asus.createMonitor(),
];
asusComponentsDamnPrettier.forEach((c) => c.fail());

const gigabyte = new Gigabyte();
const gigabyteComponents: IComponent[] = [
  gigabyte.createGPU(),
  gigabyte.createMonitor(),
];
gigabyteComponents.forEach((c) => c.fail());
