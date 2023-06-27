import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component'
import { CarouselModule } from 'ngx-bootstrap/carousel';



@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(), // use for root , because we want to use it as singleton,
    CarouselModule.forRoot()
  ],
  exports: [
   
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule
  ]
})
export class SharedModule { }
