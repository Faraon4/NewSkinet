import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { Basket } from 'src/app/shared/models/basket';
import { Address } from 'src/app/shared/models/user';
import { NavigationExtras, Router } from '@angular/router';
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';
import { firstValueFrom } from 'rxjs';
import { OrderToCreate } from 'src/app/shared/models/oreder';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit{
@Input() checkoutForm? : FormGroup;
@ViewChild('cardNumber') cardNumberElement?: ElementRef;
@ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
@ViewChild('cardCvc') cardCvcElement?: ElementRef;
stripe: Stripe | null = null;
cardNumber?: StripeCardNumberElement;
cardExpiry?: StripeCardExpiryElement;
cardCvc?: StripeCardCvcElement;
cardNumberComplete = false;
cardExpiryComplete = false;
cardCvcComplete = false;
cardErrors: any;
loading = false;


constructor(private basketService: BasketService, 
            private checkoutService: CheckoutService, 
            private toastr: ToastrService,
            private router: Router){}



  ngOnInit(): void {
    // we get method from stripeJS ; and between () is the publishableKey
    loadStripe("pk_test_51OHaHOFoa3I1sw1FIYpfBXaUJWCBRzrOpkO9kxO6p0nGVwmERqjl1HnUh8dr59GjCXpJmG2FgQfHNfZ1rFMfZLOI000KbOwCbz").then(stripe => {
      this.stripe = stripe;
      const elements = stripe?.elements();
      if (elements) {
        this.cardNumber = elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        // In case that there is an error , we want to display the message
        this.cardNumber.on('change', event => {
          this.cardNumberComplete = event.complete;
          if(event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        // In case that there is an error , we want to display the message
        this.cardExpiry.on('change', event => {
          this.cardExpiryComplete = event.complete;
          if(event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        // In case that there is an error , we want to display the message
        this.cardCvc.on('change', event => {
          this.cardCvcComplete = event.complete;
          if(event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })
      }
    })
  }

    // we can use this get method inside out butthon to decide rather to disable or not
  get paymentFormComplete( ) {
    return this.checkoutForm?.get('paymentForm')?.valid && this.cardNumberComplete && this.cardExpiryComplete && this.cardCvcComplete
  }


  async submitOrder() {
    this.loading = true;
    const basket = this.basketService.getCurrentBasketValue();
    if(!basket) throw new Error('cannot get basket'); // implement the checking because when we want to delete basket , we cannot send a null element(live with deleting the basket)
    try {
      const createdOrder = await this.createsOrder(basket);
      const paymentResult = await this.confirmPaymentWithStripe(basket);

      // In case the payment was succesfull we display a green toastr
      if (paymentResult.paymentIntent) {
        this.basketService.deleteBasket(basket); // deleteBasket() method -> deletes the basket from redis and localy
        const navigationExtras: NavigationExtras = {state: createdOrder};
        this.router.navigate(['checkout/success'], navigationExtras);
      } 
      else {
        // In case the payment was declines we display an error toastr
        this.toastr.error(paymentResult.error.message);
      }

    } catch (error: any) {
      console.log(error);
      this.toastr.error(error.message)
    } finally {
      this.loading = false;
    }
    
  }
  private confirmPaymentWithStripe(basket: Basket | null) {
    if (!basket) throw new Error('Basket is null');
    const result = this.stripe?.confirmCardPayment(basket.clientSecret!, {
      payment_method: {
        card: this.cardNumber!,
        billing_details: {
          name: this.checkoutForm?.get('paymentForm')?.get('nameOnCard')?.value
        }
      }
    });
    if(!result) throw new Error('Problem attempting payment with stripe');
    return result
  }
  
  
  
  
  private async createsOrder(basket: Basket | null) {
    if (!basket) throw new Error('Basket is null');
    const orderToCreate = this.getOrderToCreate(basket);
    return firstValueFrom(this.checkoutService.createOrder(orderToCreate)); // return a promise ??

  }




  // We update to return specific smth otherwise , we get error in the create order line(119)
  private getOrderToCreate(basket: Basket): OrderToCreate {
    const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;
    if(!deliveryMethodId || !shipToAddress) throw new Error('Problem with basket');

    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethodId,
      shipToAddress: shipToAddress
    }

  }
}
