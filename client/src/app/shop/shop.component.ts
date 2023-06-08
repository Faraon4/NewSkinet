import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
products: Product[] = [];

constructor(private shopService: ShopService) {}

  //Here comes the power of the ts
  // in the get we can specify what exactlywe want to get
  // type safety , intelisses, autocomplete -> we are very specific what we want 
  // response.data => data is the data from the API that we get from paginated result 

  // This comments were deleted, please if you want to see more info where it was , look in the repo
  ngOnInit(): void {
    this.shopService.getPRoducts().subscribe({
      next : response => this.products= response.data,
      error: error =>  console.log(error)
    })
  }

}
