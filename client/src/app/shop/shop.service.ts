import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';

@Injectable({
  providedIn: 'root' //  we can change here the module where we want to put
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

  getPRoducts(){
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products?pageSize=50');
  }
}
