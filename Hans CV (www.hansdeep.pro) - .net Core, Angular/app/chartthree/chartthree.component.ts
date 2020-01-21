import { Component, OnInit } from '@angular/core';
import * as Chart from 'chart.js';

@Component({
    selector: 'app-chartthree',
    templateUrl: './chartthree.component.html',
    styleUrls: ['./chartthree.component.css']
})
export class ChartthreeComponent implements OnInit {

    constructor() { }
    mychart;
    test;
    ngOnInit() {
       
        var ctx = document.getElementById('chartThree');
        
        this.mychart = new Chart(ctx, {
            type: 'bar',
            data: {
                datasets: [
                    {
                        label: 'Bar Dataset', data: [45, 21, 11, 63, 54,23], backgroundColor: [
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)'
                        ]
                    },
                    {
                        label: 'Line Dataset', data: [11, 25, 34, 65, 35,11], type: 'line', backgroundColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                    }
                ],
                labels: ['January', 'February', 'March', 'April', 'May', 'June']
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
                        },
                        gridLines: {
                            display: false,
                            color: "#FFFFFF"
                        }
                    }]
                }
            }
        });
      
        this.test = function () {this.mychart.data.labels.push('Hans'); this.mychart.update(); }
    }

}
