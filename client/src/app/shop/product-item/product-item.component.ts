import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
@Input() product?: Product; // This is how we dedclare a child component and show that is is wating for something
                            // It is important that in the html we put the product like this -> [product] -> showing that we are waiting for something to be passed
                            //in combination with ngFor , we are passing individual product from products

constructor(private basketService: BasketService) {}

addItemToBasket() { 
  // we added at the beginning this.product && -> because it is possible that it will be undefinied and if the users will clock the button to add nothing , it will do nothing
  this.product && this.basketService.addItemToBasket(this.product);
}
}
