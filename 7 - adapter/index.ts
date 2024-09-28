console.log('TypeScript Adapter pattern');

interface ICable {
  type: string;
  voltage: number;
  toString(): string;
}

class USB implements ICable {
  constructor(public readonly voltage: number) {}

  get type(): string {
    return 'USB';
  }

  toString(): string {
    return `${this.type} ${this.voltage}V cable`;
  }
}

class DC implements ICable {
  constructor(public readonly voltage: number) {}

  get type(): string {
    return 'DC';
  }

  toString(): string {
    return `${this.type} ${this.voltage}V cable`;
  }
}

class DCSocket {
  static plug(cable: DC): void {
    console.log(`${cable} is being plugged into DC socket`);
  }
}

class USBToDCAdapter implements ICable {
  private dc: DC;

  constructor(usb: USB) {
    this.dc = new DC(usb.voltage * 2.4);
  }

  get type(): string {
    return this.dc.type;
  }

  get voltage(): number {
    return this.dc.voltage;
  }

  toString(): string {
    return this.dc.toString();
  }

  adapt(): DC {
    console.log(`Adapter is adapting ${usb} into ${this.dc}`);
    return this.dc;
  }
}

const usb = new USB(5);
console.log(`We have ${usb}`);

const dcAdapter = new USBToDCAdapter(usb);
DCSocket.plug(dcAdapter.adapt());
