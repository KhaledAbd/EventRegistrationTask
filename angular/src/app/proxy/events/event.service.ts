import type { CreateUpdateEventDto, EventActiveDto, EventDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  apiName = 'Default';

  create = (input: CreateUpdateEventDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventDto>(
      {
        method: 'POST',
        url: '/api/app/event',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/event/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventDto>(
      {
        method: 'GET',
        url: `/api/app/event/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getActiveEvent = (
    name?: string,
    organizerName?: string,
    skipCount?: number,
    maxResultCount: number = 10,
    config?: Partial<Rest.Config>
  ) =>
    this.restService.request<any, PagedResultDto<EventActiveDto>>(
      {
        method: 'GET',
        url: '/api/app/event/active-event',
        params: { name, organizerName, skipCount, maxResultCount },
      },
      { apiName: this.apiName, ...config }
    );

  getList = (
    organizerName?: string,
    skipCount?: number,
    maxResultCount: number = 10,
    config?: Partial<Rest.Config>
  ) =>
    this.restService.request<any, PagedResultDto<EventDto>>(
      {
        method: 'GET',
        url: '/api/app/event',
        params: { organizerName, skipCount, maxResultCount },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: CreateUpdateEventDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EventDto>(
      {
        method: 'PUT',
        url: `/api/app/event/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
