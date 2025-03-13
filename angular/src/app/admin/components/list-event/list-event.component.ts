import { EventService } from './../../../proxy/events/event.service';
import { LocalizationModule, LocalizationService, PermissionService } from '@abp/ng.core';
import { IdentityUserService } from '@abp/ng.identity/proxy';
import { ToasterService } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { EventActionDto, EventDto } from '@proxy/events';

@Component({
  selector: 'app-list-event',
  imports: [LocalizationModule, RouterModule, CommonModule, FormsModule],
  templateUrl: './list-event.component.html',
  styleUrl: './list-event.component.scss',
})
export class ListEventComponent {
  events: EventActionDto[];

  filter = {
    organizerName: '',
    retrieveOwnUser: false,
  };

  constructor(
    private eventService: EventService,
    private router: Router,
    private permissionService: PermissionService,
    private toastrService: ToasterService,
    private localization: LocalizationService
  ) {
    this.getData();
    const canDelete = this.permissionService.getGrantedPolicy('EventRegistrationTask.Event.Delete');
  }

  getData() {
    this.eventService
      .getList(this.filter.organizerName, this.filter.retrieveOwnUser)
      .subscribe(c => {
        this.events = c.items;
      });
  }

  remove(id: string) {
    this.eventService.delete(id).subscribe(() => {
      this.toastrService.warn(this.localization.instant('RemoveSuccesFul'));
      this.router.navigateByUrl('/admin/events');
    });
  }
  active(event: EventDto) {
    this.eventService.active(event.id).subscribe(() => {
      this.toastrService.warn(
        this.localization.instant(event.isActive ? 'UnActiveSuccessfully' : 'ActiveSuccessfully')
      );
      this.getData();
      this.router.navigateByUrl('/admin/events');
    });
  }
}
