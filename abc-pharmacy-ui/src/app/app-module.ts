import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { MedicineList } from './features/medicines/medicine-list/medicine-list';
import { AddMedicine } from './features/medicines/add-medicine/add-medicine';
import { Sales } from './features/sales/sales/sales';

@NgModule({
  declarations: [],
  imports: [BrowserModule, AppRoutingModule],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [],
})
export class AppModule {}
