import { Routes, RouterModule } from '@angular/router';
import { ActiveEventListComponent } from './components/active-event-list/active-event-list.component';

const routes: Routes = [{ path: '', component: ActiveEventListComponent }];

export const UserRoutes = RouterModule.forChild(routes);
