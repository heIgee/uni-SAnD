console.log('TypeScript Prototype pattern');

interface IShipment {
  clone(): IShipment;
  getSummary(): string;
}

interface IDestination {
  country: string;
  city: string;
}

abstract class Shipment implements IShipment {
  constructor(protected destination: IDestination, protected weight: number) {}
  abstract clone(): IShipment;
  abstract getSummary(): string;
}

class ExpressShipment extends Shipment {
  constructor(
    destination: IDestination,
    weight: number,
    private priority: number,
  ) {
    super(destination, weight);
  }

  clone(): ExpressShipment {
    // god damn funky way
    // prettier-ignore
    return new ExpressShipment(
      ...(Object.entries(this).map(
        ([k, v]) => structuredClone(v)
      ) as [any, any, any]),
    );

    // return structuredClone(this); // does not work

    // sane way
    // return new ExpressShipment(
    //   structuredClone(this.destination),
    //   this.weight,
    //   this.priority,
    // );
  }

  getSummary(): string {
    return `Express Shipment to ${this.destination.city} (${this.destination.country}) weighing ${this.weight}kg with priority <${this.priority}>`;
  }
}

const duisburgShipment = new ExpressShipment(
  { country: 'Germany', city: 'Duisburg' },
  32000,
  2,
);
console.log(duisburgShipment.getSummary());

const clonedShipment = duisburgShipment.clone();
console.log(clonedShipment.getSummary());

console.log({ 'objects are equal': duisburgShipment === clonedShipment });
console.log({
  'their destinations are equal':
    // @ts-ignore-next-line // access modifiers are just letters
    duisburgShipment.destination === clonedShipment.destination,
});
