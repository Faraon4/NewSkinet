import { Component } from '@angular/core';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  // Adding the ctor and make the service to be public, such way we may use the service in the html
  constructor(public basketService: BasketService) {}

}
