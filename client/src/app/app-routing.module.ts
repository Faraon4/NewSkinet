import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { authGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path: 'test-error', component: TestErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule)}, // we are calling the shopModule where we have the routes
  {path: 'basket', loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule)}, // We are calling the basket module
  {path: 'orders', loadChildren: () => import('./orders/orders.module').then(mod => mod.OrdersModule), data: {breadcrumb: 'Orders'} },
  {
    path: 'checkout', 
    canActivate: [authGuard], // add our guard 
    loadChildren: () => import('./checkout/checkout.module').then(m => m.CheckoutModule)}, // We are calling the basket module


  {path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule)}, // We are calling the basket module
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
