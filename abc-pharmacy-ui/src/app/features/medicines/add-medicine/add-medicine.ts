import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

import { MedicineService } from '../../../core/services/medicine';


@Component({
  selector: 'app-add-medicine',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './add-medicine.html',
  styleUrl: './add-medicine.css'
})
export class AddMedicine {


  form: FormGroup;


  constructor(
    private fb: FormBuilder,
    private service: MedicineService
  ) {


    this.form = this.fb.group({

      fullName: [''],

      notes: [''],

      expiryDate: [''],

      quantity: [0],

      price: [0],

      brand: ['']

    });


  }



  save() {

    this.service.addMedicine(
      this.form.value
    )
      .subscribe({

        next: () => {

          alert("Medicine added successfully");

          this.form.reset();

        },

        error: (err) => {

          console.error(err);

        }

      });

  }

}
