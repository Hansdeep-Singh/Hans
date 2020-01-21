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
export class ChartfourService {
    readonly ROOT_URL;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.ROOT_URL = baseUrl;
    }
    getArg(inputa): Observable<chartput> { return this.http.get<chartput>(this.ROOT_URL + 'Api/Charts/Search/' + inputa) }  
   

}
