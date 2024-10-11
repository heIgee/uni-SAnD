console.log('TypeScript Decorator pattern');

interface IComputerDelivery {
  send(): void;
}

class PostComputerDelivery implements IComputerDelivery {
  send(): void {
    console.log('Computer is being sent by post');
  }
}

abstract class PresentDecorator implements IComputerDelivery {
  constructor(private readonly delivery: IComputerDelivery) {}

  send(): void {
    this.delivery.send();
  }
}

class KeyboardPresentDecorator extends PresentDecorator {
  send(): void {
    super.send();
    console.log('Keyboard present is attached to the package');
  }
}

class MousePresentDecorator extends PresentDecorator {
  send(): void {
    super.send();
    console.log('Mouse present is attached to the package');
  }
}

const delivery = new PostComputerDelivery();
const deliveryWithKeyboard = new KeyboardPresentDecorator(delivery);
const deliveryWithKeyboardAndMouse = new MousePresentDecorator(
  deliveryWithKeyboard,
);
deliveryWithKeyboardAndMouse.send();
