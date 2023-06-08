import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';

@Injectable({
  providedIn: 'root' //  we can change here the module where we want to put
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

  getProducts(brandId?: number, typeId?: number){
    
    // we need to create this because of the query string that our controller is using
    let params = new HttpParams()
    
    if (brandId) params = params.append('brandId',brandId);
    if (typeId) params= params.append('typeId', typeId)


    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', {params: params}); // This is the correct way to add query string params -> but in our case names are equal and we can just pu params
  }

  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands')
  }

  getTypes(){
    // Be carefully from where do we import the type , there are many places from where we can import it , we need exactly our interface from shared module
    return this.http.get<Type[]>(this.baseUrl + 'products/types')
  }
}
