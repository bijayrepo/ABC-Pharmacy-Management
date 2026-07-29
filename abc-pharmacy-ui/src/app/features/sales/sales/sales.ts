import { Component } from '@angular/core';

import { SaleService } from '../../../core/services/sale';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Medicine } from '../../../models/medicine';
import { Sale } from '../../../models/sale';
import { MedicineService } from '../../../core/services/medicine';
import { CommonModule } from '@angular/common';


@Component({

  selector: 'app-sales',

  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './sales.html'

})


export class Sales {

  sales: Sale[] = [];
  form: FormGroup;
  medicines: Medicine[] = [];


  constructor(private fb: FormBuilder,
    private service: SaleService,
    private medicineService: MedicineService
  )
  {
    this.form = this.fb.group({
      medicineId: [0],
      quantitySold: [1]
    });
  }



  ngOnInit(): void {

    this.service.getAll()
      .subscribe(x => {

        this.sales = x;
        this.loadMedicines();
      });

  }
  save() {

    this.service.createSale(this.form.value)
      .subscribe({

        next: () => {
          alert("Sale completed.");

          this.loadMedicines();

        },

        error: err => {

          alert(err.error.message);

        }

      });

  }
  loadMedicines(): void {
    this.medicineService.getAll().subscribe({
      next: (data) => {
        this.medicines = data;
        console.log(this.medicines);
      },
      error: (err) => {
        console.error('Error loading medicines', err);
      }
    });
  }

}
