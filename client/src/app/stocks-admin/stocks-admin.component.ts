import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { IStock } from '../shared/models/stock';
import { StocksService } from '../stocks/stocks.service';
import { StocksAdminService } from './stocks-admin.service';

@Component({
  selector: 'app-stocks-admin',
  templateUrl: './stocks-admin.component.html',
  styleUrls: ['./stocks-admin.component.scss']
})
export class StocksAdminComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  stocks: IStock[];
  myParams = new MycategoryParams();
  totalCount: number;

  constructor(private stockAdminService: StocksAdminService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
    this.getStocks();
  }

  getStocks() {
    this.stockAdminService.getStocks(this.myParams)
    .subscribe(response => {
      this.stocks = response.data;
      this.myParams.pageNumber = response.pageIndex;
      this.myParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getStocks();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MycategoryParams();
    this.getStocks();
  }

  onPageChanged(event: any) {
    if (this.myParams.pageNumber !== event) {
      this.myParams.pageNumber = event;
      this.getStocks();
    }
}

onDelete(id: number) {
  if (confirm('Are you sure you want to delete this record?')) {
    this.stockAdminService.deletingStock(id)
      .subscribe(
        res => {
          this.getStocks();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
}
}

