import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { chartput } from '../models/chartput';
import { catchError, retry } from 'rxjs/operators';
import { Observable } from 'rxjs';


const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};


@Injectable({
  providedIn: 'root'
})
export class UpdatechartService {
    readonly ROOT_URL;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.ROOT_URL = baseUrl; }
    chartdata: chartput[];
    
    singlechart: chartput;
    test: number;
    handleError;
    

    updateDb(chartPost: chartput): Observable<chartput> {
      
        return this.http.put<chartput>(this.ROOT_URL + "Api/Charts/PutChart/", chartPost, httpOptions)
            .pipe(catchError(this.handleError));
    }

   addChart(chartPost: chartput): Observable<chartput> {

        return this.http.post<chartput>(this.ROOT_URL + "Api/Charts/AddChart/", chartPost, httpOptions)
            .pipe(catchError(this.handleError));
    }


}
