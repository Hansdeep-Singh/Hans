import { Component,Inject, OnInit } from '@angular/core';
import { UpdatechartService } from './updatechart.service';
import { chartput } from '../models/chartput';
import { HttpClient } from '@angular/common/http';
import { Directive, ElementRef, HostListener } from "@angular/core";


@Component({
  selector: 'app-updatechart',
  templateUrl: './updatechart.component.html',
  styleUrls: ['./updatechart.component.css']
})
export class UpdatechartComponent implements OnInit {
    public model = new chartput(1,'Pie',2,4,5,6,7,8,9);
    
    constructor(private service: UpdatechartService){}

    ngOnInit() { }

    PutData() {

        this.service.updateDb(this.model).subscribe(
            (data: chartput) => {
                console.log(data);
            }
        )
    }

    AddChart() {
        this.service.addChart(this.model).subscribe(
            (data: chartput) => {
                console.log(data);
            }
        )
    }
}
