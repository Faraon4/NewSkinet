import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination'



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    PaginationModule.forRoot() // use for root , because we want to use it as singleton
  ],
  exports: [
    PaginationModule
  ]
})
export class SharedModule { }
