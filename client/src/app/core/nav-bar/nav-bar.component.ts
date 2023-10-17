import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  // we are declaring it public , such way we'll be able to use it outside
 constructor(public basketService: BasketService, public accountService: AccountService) {} // because we made the accountService public, we can use it dirrectly in the html template


 // this is a way to show all items in the basket -> even if we have 2 or more of the same kind

 getCount(items: BasketItem[]) {
  return items.reduce((sum, item) => sum + item.quantity, 0);
 }
}
