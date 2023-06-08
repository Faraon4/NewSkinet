import { Component, Input } from '@angular/core';
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
}
