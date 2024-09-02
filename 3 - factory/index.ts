console.log('TypeScript Factory pattern');

interface IGPU {
  startArtifacting(): void;
  burn(): void;
}

class AsusGPU implements IGPU {
  startArtifacting() {
    console.log('Asus GPU started artifacting');
  }
  burn() {
    console.log('Asus GPU is burning');
  }
}

class GigabyteGPU implements IGPU {
  startArtifacting() {
    console.log('Gigabyte GPU started artifacting');
  }
  burn() {
    console.log('Gigabyte GPU is burning');
  }
}

abstract class Manufacturer {
  abstract createGPU(): IGPU;
}

class Asus extends Manufacturer {
  createGPU(): IGPU {
    const gpu = new AsusGPU();
    gpu.startArtifacting();
    return gpu;
  }
}

class Gigabyte extends Manufacturer {
  createGPU(): IGPU {
    const gpu = new GigabyteGPU();
    gpu.startArtifacting();
    return gpu;
  }
}

const asus = new Asus();
const asusGPU = asus.createGPU();
asusGPU.burn();

const gigabyte = new Gigabyte();
const gigabyteGPU = gigabyte.createGPU();
gigabyteGPU.burn();
