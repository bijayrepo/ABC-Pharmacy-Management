import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';

import { Medicine } from '../../models/medicine';


@Injectable({
  providedIn: 'root'
})
export class MedicineService {


  private api =
    environment.apiUrl + "/medicines";


  constructor(
    private http: HttpClient
  ) { }



  getAll() {

    return this.http.get<Medicine[]>(
      this.api
    );

  }



  search(name: string) {

    return this.http.get<Medicine[]>(
      `${this.api}/search?name=${name}`
    );

  }



  addMedicine(data: Medicine) {

    return this.http.post(
      this.api,
      data
    );

  }

}
