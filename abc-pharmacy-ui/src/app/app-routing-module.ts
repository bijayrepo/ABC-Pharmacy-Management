import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicineList } from './features/medicines/medicine-list/medicine-list';
import { AddMedicine } from './features/medicines/add-medicine/add-medicine';
import { Sales } from './features/sales/sales/sales';

export const routes: Routes = [
  {
    path: '',
    component: MedicineList
  },


  {
    path: 'add',
    component: AddMedicine
  },
  {
    path: 'sale',
    component: Sales
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
