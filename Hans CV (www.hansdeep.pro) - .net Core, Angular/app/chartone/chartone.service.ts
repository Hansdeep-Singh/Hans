import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { chartput } from '../models/chartput';
import { Observable } from 'rxjs';




const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};

@Injectable({
    providedIn: 'root'
})
export class ChartoneService {


    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.ROOT_URL = baseUrl;
    }
    readonly ROOT_URL;
    chartdata: chartput[];
    singlechart: chartput;
    handleError;
    getArg(inputa): Observable<chartput> {return this.http.get<chartput>(this.ROOT_URL + 'Api/Charts/Search/' + inputa)}  

}
