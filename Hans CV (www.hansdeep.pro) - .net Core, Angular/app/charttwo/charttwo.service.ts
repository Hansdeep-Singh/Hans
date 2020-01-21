import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { chartput } from '../models/chartput';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};

@Injectable({
  providedIn: 'root'
})
export class CharttwoService {

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.ROOT_URL = baseUrl;
    }
    readonly ROOT_URL;
    chartdata: chartput[];
    singlechart: chartput;
    handleError;



    getArg(inputa) {
        this.http.get<chartput>(this.ROOT_URL + "Api/Charts/Search/" + inputa).subscribe(result => {
            this.singlechart = result;
        }, error => console.error(error));
    }

}
