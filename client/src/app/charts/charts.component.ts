import { Component, OnInit, ViewChild } from '@angular/core';
import {
  ChartErrorEvent,
  ChartMouseLeaveEvent,
  ChartMouseOverEvent,
  ChartSelectionChangedEvent,
  ChartType,
  Column,
  GoogleChartComponent
} from 'angular-google-charts';
import { ChartsService } from './charts.service';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.scss']
})
export class ChartsComponent implements OnInit {
  errorMessage: string;

  title = 'Dummy Naruto sales in the last 6 years ';
  type = ChartType.ColumnChart;
  data = [
    ['2013', 40.0],
    ['2014', 56.8],
    ['2015', 42.8],
    ['2016', 38.5],
    ['2017', 30.2],
    ['2018', 46.7]
 ];
  columnNames: Column[] = ['Year', 'Profit'];
  options = {
   // chartArea: { backgroundColor: '#f1f7f9' },
    width: 555,
    height: 300,
    backgroundColor: '#ffff00',
    hAxis: { title: '' },
  };
  width = 777;
  height = 300;


  constructor(private chartsService: ChartsService) { }

  ngOnInit() {
    this.chartsService.getProfitOrLosses().subscribe(
      result => {
        // clear up the database so it doesn't keep pushing unwanted data into it.
        this.data = [];
        // change the title
        this.title = 'Database data of Profits or Losses';
        // change the type, ti si ostavio pie, možeš column
        this.type = ChartType.ColumnChart;
        console.log(result.list);


        // push in the data foreach result year and sales.
        // I used the .toString method because the x-axis has to be in string format.
        // sve ti je ovo vidljivo u postmanu, i year i narutostats i salesinmillion
        for (const data in result.list) {
          this.data.push([result.list[data].year.toString(), result.list[data].amount]);
        }
      },
      error => {
        console.log(error);
        // give error message a value/
        this.errorMessage = 'Sorry something went wrong somewhere';
      }
    );

  }
}
