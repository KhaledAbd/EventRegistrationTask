import type { EventRegistrationDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EventRegistrationService {
  apiName = 'Default';
  

  cancel = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: `/api/app/event-registration/${id}/cancel`,
    },
    { apiName: this.apiName,...config });
  

  getRegistrationsForEvent = (eventId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventRegistrationDto[]>({
      method: 'GET',
      url: `/api/app/event-registration/registrations-for-event/${eventId}`,
    },
    { apiName: this.apiName,...config });
  

  register = (eventId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: `/api/app/event-registration/register/${eventId}`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
