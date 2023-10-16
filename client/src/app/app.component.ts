import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Skinet'

  constructor(private basketService: BasketService, private accountService: AccountService){}
  // account service is added here because we want to load the user if it is logged in

  ngOnInit(): void {
   this.loadBasek();
   this.loadCurrentUser();
  }


  loadBasek() {
    const basketId = localStorage.getItem('basket_id');

    if(basketId) this.accountService.loadCurrentUser(basketId).subscribe();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');

    if(token) this.accountService.loadCurrentUser(token).subscribe();
  }


}

