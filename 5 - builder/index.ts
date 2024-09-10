console.log('TypeScript Builder pattern');

class PC {
  case?: string;
  psu?: string;
  motherboard?: string;
  cpu?: string;
  gpu?: string;
  ram?: string;
  rom?: string[];
  cpuCooling?: string;
  caseCooling?: string[];

  getConfiguration(): string {
    const components: [string, string | string[] | undefined][] = [
      ['Case', this.case],
      ['PSU', this.psu],
      ['Motherboard', this.motherboard],
      ['CPU', this.cpu],
      ['GPU', this.gpu],
      ['RAM', this.ram],
      ['CPU Cooling', this.cpuCooling],
      ['ROM', this.rom?.length ? this.rom.join('\n  - ') : 'Not specified'],
      [
        'Case Cooling',
        this.caseCooling?.length
          ? this.caseCooling.join('\n  - ')
          : 'Not specified',
      ],
    ];

    return components
      .map(([label, value]) => `${label}: ${value || 'Not specified'}`)
      .join('\n');
  }
}

interface IPCBuilder {
  reset(): this;
  addCase(pcCase: string): this;
  addPSU(psu: string): this;
  addMotherboard(motherboard: string): this;
  addCPU(cpu: string): this;
  addGPU(gpu: string): this;
  addRAM(ram: string): this;
  addROM(rom: string[]): this;
  addCPUCooling(cpuCooling: string): this;
  addCaseCooling(caseCooling: string[]): this;
}

class PCBuilder implements IPCBuilder {
  private pc: PC = new PC();

  reset(): this {
    this.pc = new PC();
    return this;
  }

  addCase(pcCase: string): this {
    this.pc.case = pcCase;
    return this;
  }

  addPSU(psu: string): this {
    this.pc.psu = psu;
    return this;
  }

  addMotherboard(motherboard: string): this {
    this.pc.motherboard = motherboard;
    return this;
  }

  addCPU(cpu: string): this {
    this.pc.cpu = cpu;
    return this;
  }

  addGPU(gpu: string): this {
    this.pc.gpu = gpu;
    return this;
  }

  addRAM(ram: string): this {
    this.pc.ram = ram;
    return this;
  }

  addROM(rom: string[]): this {
    this.pc.rom = rom;
    return this;
  }

  addCPUCooling(cpuCooling: string): this {
    this.pc.cpuCooling = cpuCooling;
    return this;
  }

  addCaseCooling(caseCooling: string[]): this {
    this.pc.caseCooling = caseCooling;
    return this;
  }

  build(): PC {
    return this.pc;
  }
}

class PCDirector {
  static makeMyPC(builder: PCBuilder): void {
    builder
      .addCase('AeroCool Aero One')
      .addPSU('be quiet! System Power 9 600W')
      .addMotherboard('Asus TUF B450M PRO-II')
      .addCPU('AMD Ryzen 5500')
      .addGPU('Asus Dual GTX 1060 6GB')
      .addRAM('Kingston Fury Beast DDR4 2x8GB')
      .addROM([
        'Samsung 980 500GB',
        'TEAM EX2 1TB',
        'TEAM GX1 240GB',
        'WDC 640GB',
      ])
      .addCPUCooling('DeepCool Gammaxx GTE V2')
      .addCaseCooling(['DeepCool RF120 3 in 1']);
  }

  static makeAnotherRandomGPTPC(builder: PCBuilder): void {
    builder
      .addCase('NZXT H510')
      .addPSU('Corsair RM750x 750W')
      .addMotherboard('MSI MAG B550 TOMAHAWK')
      .addCPU('Intel Core i5-13600K')
      .addGPU('NVIDIA GeForce RTX 4070')
      .addRAM('G.Skill Ripjaws V DDR4 2x16GB')
      .addROM(['Crucial P5 Plus 1TB', 'Seagate Barracuda 2TB'])
      .addCPUCooling('Noctua NH-U12S')
      .addCaseCooling(['Noctua NF-A12x25', 'Corsair LL120']);
  }
}

const builder = new PCBuilder();

PCDirector.makeMyPC(builder);
const myPC = builder.build();
console.log('My PC Configuration:');
console.log(myPC.getConfiguration());

builder.reset();

PCDirector.makeAnotherRandomGPTPC(builder);
const anotherPC = builder.build();
console.log('-'.repeat(20));
console.log('Another Random PC Configuration:');
console.log(anotherPC.getConfiguration());
