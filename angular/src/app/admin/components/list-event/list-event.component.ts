import { EventService } from './../../../proxy/events/event.service';
import { LocalizationModule } from '@abp/ng.core';
import { IdentityUserService } from '@abp/ng.identity/proxy';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { EventDto } from '@proxy/events';

@Component({
  selector: 'app-list-event',
  imports: [LocalizationModule, RouterModule, CommonModule],
  templateUrl: './list-event.component.html',
  styleUrl: './list-event.component.scss',
})
export class ListEventComponent {
  events: EventDto[];

  constructor(private eventService: EventService, private router: Router) {
    this.getData();
  }
  getData() {
    this.eventService.getList().subscribe(c => {
      this.events = c.items;
    });
  }

  remove(id: string) {
    this.eventService.delete(id).subscribe(() => {
      this.router.navigateByUrl('/admin/events');
    });
  }
}
