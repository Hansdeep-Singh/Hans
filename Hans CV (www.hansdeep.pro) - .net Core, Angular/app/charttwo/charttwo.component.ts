import { Component, OnInit } from '@angular/core';
import * as Chart from 'chart.js';
import { CharttwoService } from './charttwo.service';
import { chartput } from '../models/chartput';

@Component({
  selector: 'app-charttwo',
  templateUrl: './charttwo.component.html',
  styleUrls: ['./charttwo.component.css']
})
export class CharttwoComponent implements OnInit {

    constructor(private service: CharttwoService) { }
    singlechart: chartput;
    myChart;
    updatechart = () => {

        this.service.getArg('Pie');
        this.singlechart = this.service.singlechart;
        this.myChart.data.datasets.forEach((dataset) => {
            dataset.data[0] = this.service.singlechart.valone;
            dataset.data[1] = this.service.singlechart.valtwo;
            dataset.data[2] = this.service.singlechart.valthree;
            dataset.data[3] = this.service.singlechart.valfour;
            dataset.data[4] = this.service.singlechart.valfive;
            dataset.data[5] = this.service.singlechart.valsix;
        });
        this.myChart.update();
    }

    ngOnInit() {
        setInterval(this.updatechart, 1000);
        Chart.defaults.global.defaultFontColor = 'grey';
   
        var ctx = document.getElementById('chartTwo');
        this.myChart = new Chart(ctx, {
            type: 'pie',
            data: {
               labels: ['Jan', 'Feb', 'Mar', 'April', 'May', 'Jun'],
                datasets: [{
                    label: '# of Votes',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {

            
                 legend: {
                    labels: {
                        // This more specific font property overrides the global property
                         fontColor: '#3CAEA3'
                    }
                 }
                ,

               
            }
        });

  }

}
