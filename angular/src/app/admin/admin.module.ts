import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddEventComponent } from './components/add-update-event/add-event.component';
import { AdminRoutes } from './admin.routing';

@NgModule({
  imports: [
    CommonModule,
    AddEventComponent,
    AdminRoutes
  ],
  declarations: []
})
export class AdminModule { }
