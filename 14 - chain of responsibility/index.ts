console.log('TypeScript Chain of Responsibility pattern');

type Order = {
  isAuthenticated: boolean;
  product: string;
  payment: number;
};

class ProductDatabase {
  static readonly products: [string, number][] = [
    ['High-End Gaming Rig', 1800],
    ['Budget Office Laptop', 550],
    ['4K Monitor', 400],
  ];
}

class OrderService {
  private handler: OrderHandler;

  constructor(handler: OrderHandler) {
    this.handler = handler;
  }

  order(order: Order): boolean {
    console.log(order, 'has been placed');

    if (this.handler.handle(order)) {
      console.log(`All is well, the order of ${order.product} is proceeding`);
      return true;
    }

    console.log(`Order of ${order.product} failed`);
    return false;
  }
}

abstract class OrderHandler {
  private next: OrderHandler | null = null;

  setNextHandler(next: OrderHandler): OrderHandler {
    this.next = next;
    return next;
  }

  abstract handle(order: Order): boolean;

  protected nextHandler(order: Order): boolean {
    if (this.next === null) return true;
    return this.next.handle(order);
  }
}

class AuthHandler extends OrderHandler {
  handle(order: Order): boolean {
    if (!order.isAuthenticated) {
      console.log('User is not authenticated');
      return false;
    }

    console.log('User is authenticated');
    return this.nextHandler(order);
  }
}

class ProductHandler extends OrderHandler {
  handle(order: Order): boolean {
    const isProductAvailable = ProductDatabase.products.some(
      ([product]) => product === order.product,
    );
    if (!isProductAvailable) {
      console.log('Product is not available');
      return false;
    }

    console.log('Product is available');
    return this.nextHandler(order);
  }
}

class PaymentHandler extends OrderHandler {
  handle(order: Order): boolean {
    const product = ProductDatabase.products.find(
      ([product]) => product === order.product,
    );
    if (order.payment < product![1]) {
      console.log('Insufficient payment');
      return false;
    }

    console.log('Sufficient payment');
    return this.nextHandler(order);
  }
}

const handler = new AuthHandler();
handler
  .setNextHandler(new ProductHandler())
  .setNextHandler(new PaymentHandler());

const orderService = new OrderService(handler);
orderService.order({
  isAuthenticated: true,
  product: 'High-End Gaming Rig',
  payment: 2000,
});
orderService.order({
  isAuthenticated: true,
  product: 'Budget Office Laptop',
  payment: 350,
});
orderService.order({
  isAuthenticated: true,
  product: 'Mid-Level Smartphone',
  payment: 300,
});
orderService.order({
  isAuthenticated: false,
  product: '4K Monitor',
  payment: 600,
});
