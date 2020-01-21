import { Component, OnInit } from '@angular/core';
import * as Chart from 'chart.js';
import { chartput } from '../models/chartput';
import { ChartfourService } from './chartfour.service';

@Component({
  selector: 'app-chartfour',
  templateUrl: './chartfour.component.html',
  styleUrls: ['./chartfour.component.css']
})
export class ChartfourComponent implements OnInit {
    singlechart: chartput;
    myChart;
    constructor(private service: ChartfourService) { }

    pushChart = () => {
        this.service.getArg('Pie').subscribe(singlechart => (this.singlechart = singlechart));
        this.myChart.data.datasets.forEach((dataset) => {
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
        setInterval(this.pushChart, 1000);
        var ctx = document.getElementById('chartFour');
        this.myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'April', 'May', 'Jun'],
                datasets: [{
                    label: '# of Votes',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
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
                        fontColor: '#ffffff'
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });

  }



}
