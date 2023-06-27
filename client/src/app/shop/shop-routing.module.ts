import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopComponent } from './shop.component';


// This is used for LAzy Loading
// Instead of loading everything when we start our application, we are spliting it , and the routes that are used fir shop componenet will be loaded when we access it

const routes: Routes = [
  {path: '', component: ShopComponent}, // we do not use the shop/ as path because this path is written in the shopModule where we call this module
  {path: ':id', component: ProductDetailsComponent, data: {breadcrumb: {alias: 'productDetails'}}}, // this is used in the head componenet created in module 12 -> for display the route to each element from the shop
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes) //forChild() -> used because it is child of the app-routing
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule { }
