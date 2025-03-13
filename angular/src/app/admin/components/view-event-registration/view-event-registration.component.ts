import {
  ActionEventRegistration,
  EventRegistrationDto,
  EventRegistrationService,
} from '@proxy/events';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalizationModule } from '@abp/ng.core';

@Component({
  selector: 'app-view-event-registration',
  imports: [LocalizationModule, CommonModule],
  templateUrl: './view-event-registration.component.html',
  styleUrl: './view-event-registration.component.scss',
})
export class ViewEventRegistrationComponent {
  eventId: any;
  registrations: ActionEventRegistration[];
  constructor(
    private eventRegistrationService: EventRegistrationService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.eventId = this.route.snapshot.paramMap.get('id');

    this.getdata(this.eventId);
  }

  getdata(eventId: string) {
    this.eventRegistrationService.getRegistrationsEvent(eventId).subscribe(c => {
      this.registrations = c.items;
    });
  }
}
