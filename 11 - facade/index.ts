console.log('TypeScript Facade Pattern');

class CustomerOrderService {
  constructor(private isTrustedCustomer: boolean) {}

  purchase(
    product: string,
    price: number,
    postalOperator: string,
    address: string,
  ): void {
    let finalPrice = price;
    if (this.isTrustedCustomer) {
      finalPrice *= 0.9;
      console.log(
        `Is trusted customer - price reduced by $${price - finalPrice}`,
      );
    }

    console.log(
      `Purchase of ${product} valued at $${finalPrice} to be shipped via ${postalOperator} is initiated`,
    );

    const paymentService = new PaymentService('PayPal');
    paymentService.transfer(finalPrice);

    const taxService = new TaxService('Hungary');
    const taxAmount = taxService.calculateVAT(finalPrice);
    taxService.payTaxes(taxAmount);

    const buildService = new ProductBuildService(product);
    const builtProduct = buildService.build();

    const postalService = new PostalService(postalOperator);
    postalService.send(builtProduct, address);
  }
}

class PaymentService {
  constructor(private provider: string) {}

  transfer(amount: number): void {
    console.log(
      `$${amount} is transferred via ${this.provider} payment service`,
    );
  }
}

class TaxService {
  constructor(private government: string) {}

  calculateVAT(price: number): number {
    switch (this.government) {
      case 'Ukraine':
        return price * 0.2;
      case 'Hungary':
        return price * 0.27;
      case 'California':
        return price * 0.0725;
      default:
        return 0;
    }
  }

  payTaxes(amount: number): void {
    console.log(
      `$${amount | 0} of taxes is paid to the ${this.government} government`,
    );
  }
}

class ProductBuildService {
  constructor(private product: string) {}

  build(): string {
    console.log(`Building ${this.product} is in progress`);
    return `[Newly built ${this.product}]`;
  }
}

class PostalService {
  constructor(private operator: string) {}

  send(pаckage: string, address: string): void {
    console.log(
      `Package ${pаckage} is being sent by ${this.operator} postal service to ${address}`,
    );
  }
}

const orderService = new CustomerOrderService(true);
orderService.purchase('High-end PC', 1400, 'MagyarPosta', 'Wall St. 69');
