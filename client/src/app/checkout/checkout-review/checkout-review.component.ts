import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss']
})
export class CheckoutReviewComponent {
  @Input() appStepper?: CdkStepper;
  constructor(private basketService: BasketService, private toastr: ToastrService) {}

  createPaymentIntent(){
    this.basketService.createPaymentIntent().subscribe({
      next: () => {
       // this.toastr.success('Payment created with success'); --> this was used for tests only to inform us
        this.appStepper?.next();
    },
      error: error => this.toastr.error(error.message)
    })
  }

}
