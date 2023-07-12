import * as cuid from 'cuid';

export interface BasketItem {
    id: number
    productName: string
    price: number
    quantity: number
    pictureUrl: string
    brand: string
    type: string
  }



export interface Basket {
    id: string
    items: BasketItem[]
  }

export class Basket implements Basket {
    id = cuid(); // give us a unique string id
    items: BasketItem[] = [];
}
  

// We will use it to calculate the total of the basket
export interface BasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}