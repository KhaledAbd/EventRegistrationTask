import { EventService } from './../../../proxy/events/event.service';
import { LocalizationModule } from '@abp/ng.core';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EventDto } from '@proxy/events';

@Component({
  selector: 'app-list-event',
  imports:[LocalizationModule, RouterModule, CommonModule],
  templateUrl: './list-event.component.html',
  styleUrl: './list-event.component.scss'
})
export class ListEventComponent {
  events: EventDto[];

  constructor(private eventService:EventService){
    this.getData();
  }
  getData(){
    this.eventService.getList().subscribe(c => {
      this.events = c;
    })
  }
}
