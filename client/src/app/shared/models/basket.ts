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
    id: string;
    items: BasketItem[];
    //Must be carefully with this type of properties---> in this case it should be the same as in the CustomerBasketDto.cs. Types should be the same correct as in the dto class
    clientSecret?: string;
    paymentIntentId?: string;
    deliveryMethodId?:number;
    shippingPrice: number;
  }

export class Basket implements Basket {
    id = cuid(); // give us a unique string id
    items: BasketItem[] = [];
    shippingPrice = 0;
}
  

// We will use it to calculate the total of the basket
export interface BasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}