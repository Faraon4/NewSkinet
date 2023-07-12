import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product?: Product;
  quantity = 1
  quantityInBasket = 0
  constructor(private shopService: ShopService, private activatedRoot: ActivatedRoute, private bcService: BreadcrumbService, private basketService: BasketService) {

    this.bcService.set('@productDetails', ' ')
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    const id = this.activatedRoot.snapshot.paramMap.get('id')
    if(id) this.shopService.getProduct(+id).subscribe({
      next: product => 
      {
        this.product = product,
        this.bcService.set('@productDetails', product.name) // This we take from shop-routing models
        this.basketService.basketSource$.pipe(take(1)).subscribe({
          next: basket => {
                                                          // + is showing that we are looking for integer, not for string
            const item = basket?.items.find(x => x.id === +id);
            if (item) {
              this.quantity = item.quantity;
              this.quantityInBasket = item.quantity;
            }
          }
        })
      },
      error: error => console.log(error)
    })
  }


  incrementQuantity() {
    this.quantity ++;
  }

  decrementQuantity() {
    this.quantity--;
  }

  updateBasket(){
    if (this.product) {
      if(this.quantity > this.quantityInBasket) {
        const itemsToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemsToAdd;
        this.basketService.addItemToBasket(this.product, itemsToAdd);
      } else{
        const itemsToRemove = this.quantityInBasket - this.quantity;
        this.basketService.removeItemFromBasket(this.product.id, itemsToRemove);
      }
    }
  }

  get buttonText() {
    return this.quantityInBasket === 0 ? 'Add to basket' : 'Update the basket'
  }

}
