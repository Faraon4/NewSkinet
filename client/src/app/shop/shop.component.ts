import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
products: Product[] = [];
brands: Brand[] = []


// Be carefully from where do we import the type , there are many places from where we can import it , we need exactly our interface from shared module
types: Type[] = []

brandIdSelected = 0;
typeIdSelected = 0;

constructor(private shopService: ShopService) {}

  //Here comes the power of the ts
  // in the get we can specify what exactlywe want to get
  // type safety , intelisses, autocomplete -> we are very specific what we want 
  // response.data => data is the data from the API that we get from paginated result 

  // This comments were deleted, please if you want to see more info where it was , look in the repo
  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected).subscribe({ //Order in () is important , is has to be as in the service
      next : response => this.products= response.data,
      error: error =>  console.log(error)
    })
  }

  getBrands(){
    this.shopService.getBrands().subscribe({
      next : response => this.brands= [{id: 0, name: 'All'}, ...response], // We adding all the brands that we get as array ...reponse -> and add a new element that is All with id 0
      error: error =>  console.log(error)
    })
  }

  getTypes(){
    this.shopService.getTypes().subscribe({
      next : response => this.types= [{id: 0, name: 'All'}, ...response], // The same as upper
      error: error =>  console.log(error)
    })
  }


  onBrandSelected(brandId:  number) {
    this.brandIdSelected = brandId;
    this.getProducts(); // we call this to get the updated list of items after we pass the brandId parameter
  }

  onTypeSelected(typeId:  number) {
    this.typeIdSelected = typeId;
    this.getProducts(); // we call this to get the updated list of items after we pass the typeId parameter
  }

}
