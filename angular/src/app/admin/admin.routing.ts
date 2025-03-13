import { Routes, RouterModule } from '@angular/router';
import { CreateUpdateEventComponent } from './components/add-update-event/create-update-event.component';
import { ListEventComponent } from './components/list-event/list-event.component';
import { permissionGuard } from '@abp/ng.core';
import { ViewEventRegistrationComponent } from './components/view-event-registration/view-event-registration.component';

const routes: Routes = [
  { path: 'events', component: ListEventComponent },
  {
    path: 'create-update-event/:id',
    component: CreateUpdateEventComponent,
    canActivate: [permissionGuard],
    data: {
      requiredPolicy: 'EventRegistrationTask.Event.CreateEdit',
    },
  },
  {
    path: 'create-update-event',
    component: CreateUpdateEventComponent,
    canActivate: [permissionGuard],
    data: {
      requiredPolicy: 'EventRegistrationTask.Event.CreateEdit',
    },
  },
  {
    path: 'view-event-registration/:id',
    component: ViewEventRegistrationComponent,
    canActivate: [permissionGuard],
    data: {
      requiredPolicy: 'EventRegistrationTask.EventRegistration.View',
    },
  },
];

export const AdminRoutes = RouterModule.forChild(routes);
