import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { OrdersComponent } from './orders/orders.component';
import { OrderDetailedComponent } from '../order-detailed/order-detailed.component';


const routes: Routes = [
{path: '', component: OrdersComponent},
{path: ':id', component: OrderDetailedComponent, data: {breadcrumb: {alis: 'OrderDetailed'}}}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ],
  exports: [RouterModule]
})
export class OrdersRoutingModule { }