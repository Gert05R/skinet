import { v4 as uuidv4} from 'uuid'

export interface IBasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export interface IBasket {
  items: IBasketItem[];
  id: string;
  clientSecret?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
  shippingPrice?: number;
}

export class Basket implements IBasket {
  //id = 'guid';
  //whenever we create a new isntance of the basket, it's going to have an unique identifier
  id= uuidv4();
  items: IBasketItem[] = [];
}

export interface IBasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
