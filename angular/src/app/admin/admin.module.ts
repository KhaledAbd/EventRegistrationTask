import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateUpdateEventComponent } from './components/add-update-event/create-update-event.component';
import { AdminRoutes } from './admin.routing';

@NgModule({
  imports: [CommonModule, AdminRoutes],
  declarations: [],
})
export class AdminModule {}
