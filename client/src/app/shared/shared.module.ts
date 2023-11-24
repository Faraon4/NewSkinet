import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component'
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { OrderTotalsComponent } from './order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';
import { StepperComponent } from './components/stepper/stepper.component';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';




@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    TextInputComponent,
    StepperComponent,
    BasketSummaryComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(), // use for root , because we want to use it as singleton,
    CarouselModule.forRoot(),
    ReactiveFormsModule, // we import it here in shared module , because we want to lazy-load it, next step is to import shred modules in the account module,
    BsDropdownModule.forRoot(), // user for the dropdown in the nav bar --login/logout part,
    CdkStepperModule,
    RouterModule // We add the module now, because we create a basket summary that is used in the basket itself and in the review checkout
  ],
  exports: [
   
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    OrderTotalsComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    TextInputComponent,
    StepperComponent, // need it at the checkoutmodule
    CdkStepperModule,
    BasketSummaryComponent // exporting it , we can use the app-bsk-sum tag in the html template
  ]
})
export class SharedModule { }
