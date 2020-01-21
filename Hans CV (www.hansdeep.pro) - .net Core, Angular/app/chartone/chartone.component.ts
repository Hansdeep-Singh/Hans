import * as Chart from 'chart.js';
import { ChartoneService } from './chartone.service';
import { Component, Inject, OnInit } from '@angular/core';
import { chartput } from '../models/chartput';



@Component({
    selector: 'app-chartone',
    templateUrl: './chartone.component.html',
    styleUrls: ['./chartone.component.css']
})
export class ChartoneComponent implements OnInit {
    singlechart: chartput;
    constructor(private service: ChartoneService) {

    }

    myChart;
    updatechart = () => {

        this.service.getArg('Pie').subscribe(singlechart => (this.singlechart = singlechart));
       
        this.myChart.data.datasets.forEach((dataset) => {
            //if (typeof (this.singlechart.valone) == 'undefined') { dataset.data[0] = this.singlechart.valone; }
            dataset.data[0] = this.singlechart.valone;
            dataset.data[1] = this.singlechart.valtwo;
            dataset.data[2] = this.singlechart.valthree;
            dataset.data[3] = this.singlechart.valfour;
            dataset.data[4] = this.singlechart.valfive;
            dataset.data[5] = this.singlechart.valsix;
        });
        this.myChart.update();
    }
  

    ngOnInit() {
      
        setInterval(this.updatechart, 1000);
       
        var ctx = document.getElementById('chartOne');
        this.myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'April', 'May', 'Jun'],

                datasets: [{
                    label: 'Usage first 6 months',
                  
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.6)',
                        'rgba(54, 162, 235, 0.6)',
                        'rgba(255, 206, 86, 0.6)',
                        'rgba(75, 192, 192, 0.6)',
                        'rgba(153, 102, 255, 0.6)',
                        'rgba(255, 159, 64, 0.6)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1,
                }]
            },

            options: {
                maintainAspectRatio: false,
                styles: {
                    Color: ['rgba(75, 192, 192, 1)']
                },

                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            display: false
                        }
                    }]

                }
            }
        });

    }

}
