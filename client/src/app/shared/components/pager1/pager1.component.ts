import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager1',
  templateUrl: './pager1.component.html',
  styleUrls: ['./pager1.component.scss']
})
export class Pager1Component implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number;
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  onPagerChanged1(event: any) {
    this.pageChanged.emit(event.page);
  }
}
