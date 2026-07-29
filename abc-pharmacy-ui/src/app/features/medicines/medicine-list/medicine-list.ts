import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MedicineService } from '../../../core/services/medicine';
import { Medicine } from '../../../models/medicine';


@Component({
  selector: 'app-medicine-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './medicine-list.html',
  styleUrl: './medicine-list.css'
})
export class MedicineList {

  medicines: Medicine[] = [];

  searchText = '';


  constructor(
    private service: MedicineService
  ) {
  }


  ngOnInit(): void {
    this.load();
  }


  load() {

    this.service.getAll()
      .subscribe({

        next: (result) => {


          this.medicines = result;

          console.log("Medicine Array:", this.medicines);

        },

        error: (err) => {

          console.error("API Error:", err);

        }

      });

  }


  search() {

    this.service.search(this.searchText)
      .subscribe(result => {
        this.medicines = result;
      });

  }


  daysLeft(date: string): number {

    const expiry = new Date(date);
    const today = new Date();

    return Math.floor(
      (expiry.getTime() - today.getTime())
      /
      (1000 * 60 * 60 * 24)
    );

  }


  getRowClass(medicine: Medicine) {

    if (this.daysLeft(medicine.expiryDate) < 30) {
      return 'expired';
    }


    if (medicine.quantity < 10) {
      return 'low-stock';
    }


    return '';

  }

}
