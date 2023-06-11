import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef; // For accessing #search from the Search in the html
products: Product[] = [];
brands: Brand[] = []


// Be carefully from where do we import the type , there are many places from where we can import it , we need exactly our interface from shared module
types: Type[] = []

shopParams = new ShopParams;

sortOptions = [
  {name: 'Alphabetical', value: 'name'},
  {name: 'Price: Low to High', value: 'priceAsc'},
  {name: 'Price: High to Low', value: 'priceDesc'},
]

totalCount = 0;

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
    this.shopService.getProducts(this.shopParams).subscribe({ //Order in () is important , is has to be as in the service
      next : response => { // reponse is what we get back from the API -> in postman what we see 
        this.products= response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count
      },
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts(); // we call this to get the updated list of items after we pass the brandId parameter
  }

  onTypeSelected(typeId:  number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts(); // we call this to get the updated list of items after we pass the typeId parameter
  }

  onSortSelected(event: any){
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }

  onPageChanged(event: any){
    if(this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }


  onSearch() {
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value='';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
