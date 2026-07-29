import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Sale } from '../../models/sale';


@Injectable({
  providedIn: 'root'
})
export class SaleService {


  private apiUrl =
    environment.apiUrl + '/sales';



  constructor(
    private http: HttpClient
  ) { }



  // Get all sale records
  getAll(): Observable<Sale[]> {

    return this.http.get<Sale[]>(
      this.apiUrl
    );

  }



  // Create new sale
  createSale(data: any): Observable<any> {

    return this.http.post(
      this.apiUrl,
      data
    );

  }


}
