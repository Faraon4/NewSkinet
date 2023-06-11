import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent {
  @Input() pageSize?: number;
  @Input() totalCount?: number;

  @Output() pageChanged = new EventEmitter<number>() // This is a child componenet , we create and OutPut to send to parent, and we made it as new EventEmitter that is emiting a number

  onPagerChanged(event: any){
    this.pageChanged.emit(event.page); // out own method, that used our Outputproperty, that use event.page , that is coming from our thi component template, and send this event to shop html component
  }

}
