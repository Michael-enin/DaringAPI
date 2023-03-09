import { Component, Input, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';
import HC_exporting from 'highcharts/modules/exporting';
HC_exporting(Highcharts);
@Component({
  selector: 'app-widgets-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  Highcharts  = Highcharts;
  chartOptions={}
  @Input() label!:string;
  @Input() total!:string;
  @Input() percentage!:string;
  @Input() mdata:any=[]
  constructor() { }

  ngOnInit(): void {
    this.chartOptions={
      chart: {
          type: 'area',
          backgroundColor:null, 
          borderWidth:0,
          margin:[3, 3, 3, 3],
          height:60
      },
      title: {
          text: null
      },
      subtitle: {
          text: null
      },

      tooltip: {
          split: true,
          outside:true
      },
      legend:{
        enabled:false
      },
      xAxis:{
        labels:{
          enabled:false
        }, 
        title:{
          Text:null
        }, 
        startOnTick:false,
        endOnTick:false,
        tickOptions:[]
      },
      yAxis:{
        labels:{
          enabled:false
        }, 
        title:{
          Text:null
        }, 
        startOnTick:false,
        endOnTick:false,
        tickOptions:[]
      },
      credits:{
        enabled:false
      },
     exporting:{
       enabled:false
     },
      series: [{
        data:this.mdata
      }]
  };
  HC_exporting(Highcharts);
    setTimeout(()=>{
      window.dispatchEvent(
        new Event('resize'));
    }, 300)  
  }

}
