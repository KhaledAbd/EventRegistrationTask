import { Routes, RouterModule } from '@angular/router';
import { CreateUpdateEventComponent } from './components/add-update-event/create-update-event.component';
import { ListEventComponent } from './components/list-event/list-event.component';

const routes: Routes = [
  { path: 'events', component: ListEventComponent },
  { path: 'create-update-event/:id', component: CreateUpdateEventComponent },
  { path: 'create-update-event', component: CreateUpdateEventComponent },
];

export const AdminRoutes = RouterModule.forChild(routes);
