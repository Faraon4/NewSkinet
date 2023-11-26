import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent {
@Input() checkoutForm?: FormGroup; // use it as inout such way that we are able to send to checkout.cmp.ts


constructor(private accountService: AccountService, private toastr: ToastrService) {}



// We add the ability to save the upadted data after changing the data in address form
// reset() method --> need to have in side () this statemenet because otherwise , the form is reseted and no information theer is written
saveUserAddress(){
  this.accountService.updateUserAddress(this.checkoutForm?.get('addressForm')?.value).subscribe({
    next: () => {
      this.toastr.success('Address saved');
      this.checkoutForm?.get('addressForm')?.reset(this.checkoutForm?.get('addressForm')?.value);
  }
  })
}
}
