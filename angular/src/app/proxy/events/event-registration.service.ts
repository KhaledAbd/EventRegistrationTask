import type { ActionEventRegistration } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
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
  

  getRegistrationsEvent = (eventId: string, skipCount?: number, maxResultCount: number = 10, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ActionEventRegistration>>({
      method: 'GET',
      url: `/api/app/event-registration/registrations-event/${eventId}`,
      params: { skipCount, maxResultCount },
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
