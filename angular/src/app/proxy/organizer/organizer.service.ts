import type { OrganizerDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OrganizerService {
  apiName = 'Default';
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, OrganizerDto[]>({
      method: 'GET',
      url: '/api/app/organizer',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
