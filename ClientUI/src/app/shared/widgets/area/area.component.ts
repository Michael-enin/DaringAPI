import { Component, OnInit, Input } from '@angular/core';
import HC_exporting from 'highcharts/modules/exporting';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'app-widgets-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.scss']
})
export class AreaComponent implements OnInit {
  Highcharts: typeof Highcharts = Highcharts
  chartOptions!:{};
  @Input() data: any = [];
  constructor() { }

  ngOnInit(): void {
    this.chartOptions={
      chart: {
          type: 'area'
      },
      title: {
          text: 'Dummy Data'
      },
      subtitle: {
          text: 'xxxx'
      },

      tooltip: {
          split: true,
          valueSuffix: ' millions'
      },
      credits:{
        enabled:false
      },
     exporting:{
       enabled:true
     },
      series: this.data,
  };
  HC_exporting(Highcharts);
    setTimeout(()=>{
      window.dispatchEvent(
        new Event('resize'));
    }, 300)            
  }

}
