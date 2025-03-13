import { LocalizationModule, LocalizationService } from '@abp/ng.core';
import { ToasterService } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EventActiveDto, EventRegistrationService, EventService } from '@proxy/events';

@Component({
  selector: 'app-active-event-list',
  imports: [CommonModule, LocalizationModule, FormsModule],
  templateUrl: './active-event-list.component.html',
  styleUrl: './active-event-list.component.scss',
})
export class ActiveEventListComponent {
  events: EventActiveDto[] = [];
  filter: {
    organizerName: string;
    eventName: string;
  } = {
    organizerName: '',
    eventName: '',
  };
  constructor(
    private eventRegistrationService: EventRegistrationService,
    private toaster: ToasterService,
    private localization: LocalizationService,
    private eventService: EventService
  ) {
    this.getListActive();
  }

  getListActive() {
    this.eventService
      .getActiveEvent(this.filter.organizerName, this.filter.eventName)
      .subscribe(c => {
        this.events = c.items;
      });
  }

  registerForEvent(event: EventActiveDto) {
    if (!event.isRegistered) {
      event.isRegistered = true;
      this.eventRegistrationService.register(event.event.id).subscribe(c => {
        this.toaster.success(this.localization.instant('Events::AddRegisterDone'));
        this.getListActive();
      });
    }
  }

  cancelRegistration(event: EventActiveDto) {
    if (event.isRegistered) {
      event.isRegistered = false;
      this.eventRegistrationService.cancel(event.eventRegistrationId).subscribe(
        c => {
          this.toaster.success(this.localization.instant('Events::CancelRegisterDone'));
        },
        e => console.log(e),
        () => this.getListActive()
      );
    }
  }
}
