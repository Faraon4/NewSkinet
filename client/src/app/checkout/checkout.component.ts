import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent {

    constructor(private fb: FormBuilder) {}

    // it will be used to track the checkout at every level
    // every new group will be used in the necesary html modules to check and to purside
    checkoutForm = this.fb.group({
      addressForm: this.fb.group({
        firstName:['', Validators.required],
        lastName:['', Validators.required],
        street:['', Validators.required],
        city:['', Validators.required],
        state:['', Validators.required],
        zipcode:['', Validators.required]
      }),
      deliveryForm : this.fb.group({
        deliveryMethod: ['', Validators.required]
      }),
      paymentForm: this.fb.group({
        nameOnCard: ['', Validators.required]
      })
    })
}
