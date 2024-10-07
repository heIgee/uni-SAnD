console.log('TypeScript Composite pattern');

interface IPcComponent {
  getTotalPower(): number;
}

class PcLeaf implements IPcComponent {
  constructor(public name: string, private power: number) {}

  getTotalPower(): number {
    return this.power;
  }
}

class PcComposite implements IPcComponent {
  constructor(public name: string, private components: IPcComponent[]) {}

  getTotalPower(): number {
    return this.components.reduce((acc, cur) => acc + cur.getTotalPower(), 0);
  }
}

// prettier-ignore
const pc = new PcComposite('Personal Computer', [
  new PcComposite('CPU', [
    new PcLeaf('Core0', 10),
    new PcLeaf('Core1', 10),
    new PcComposite('Cache', [
      new PcLeaf('L3', 5),
      new PcLeaf('L2', 3),
      new PcLeaf('L1', 2),
    ]),
    new PcLeaf('IMC', 8),
    new PcLeaf('Bus Interface', 2),
    new PcLeaf('Power Delivery System', 12),
  ]),

  new PcComposite('GPU', [
    new PcComposite('Shaders', [
      new PcLeaf('Shader0', 25),
      new PcLeaf('Shader1', 25),
      new PcLeaf('Shader2', 25),
      new PcLeaf('Shader3', 25),
    ]),
    new PcLeaf('VRAM', 15),
    new PcLeaf('Memory Controller', 10),
    new PcLeaf('Rasterizers', 20),
    new PcLeaf('Display Controller', 5),
    new PcLeaf('PCIe Interface', 3),
  ]),

  new PcComposite('RAM', [
    new PcLeaf('Stick0', 8), 
    new PcLeaf('Stick1', 8),
  ]),

  new PcComposite('Storage', [
    new PcLeaf('SSD', 5), 
    new PcLeaf('HDD', 10),
  ]),

  new PcComposite('Motherboard', [
    new PcLeaf('Chipset', 15),
    new PcLeaf('Voltage Regulators', 5),
  ]),

  new PcLeaf('PSU', 50),
  new PcComposite('Cooling', [
    new PcLeaf('CPU Fan0', 5),
    new PcLeaf('Case Fan0', 3),
    new PcLeaf('Case Fan1', 3),
  ]),
]);

console.log(`Total PC power consumption is: ${pc.getTotalPower()}W`);
