import { Routes, RouterModule } from '@angular/router';
import { AddEventComponent } from './components/add-update-event/add-event.component';
import { ListEventComponent } from './components/list-event/list-event.component';

const routes: Routes = [
  { path:'events', component: ListEventComponent },
  { path:'add-event', component: AddEventComponent },
];

export const AdminRoutes = RouterModule.forChild(routes);
