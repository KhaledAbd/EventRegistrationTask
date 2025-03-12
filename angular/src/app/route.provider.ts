import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: 'EventRegistrationTask::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: 'admin/events',
        name: 'Events::EventTitle',
        iconClass: 'fas fa-list',
        order: 2,
        layout: eLayoutType.account,
        requiredPolicy: 'EventRegistrationTask.Event.List'
      },
    ]);
  };
}
