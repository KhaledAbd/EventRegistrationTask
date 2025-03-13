import { OrganizerService } from '../../../proxy/organizer/organizer.service';
import { EventService } from '../../../proxy/events/event.service';
import { LocalizationModule, LocalizationService } from '@abp/ng.core';
import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { OrganizerDto } from '@proxy/organizer'; // Assuming EventDto is defined
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { EventDto } from '@proxy/events';
import { ToasterService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-create-update-event',
  templateUrl: './create-update-event.component.html',
  styleUrl: './create-update-event.component.scss',
  imports: [LocalizationModule, ReactiveFormsModule, CommonModule, RouterModule],
})
export class CreateUpdateEventComponent implements OnInit {
  eventForm!: FormGroup;
  isOnline = false;
  governments = ['Cairo', 'Giza', 'Alexandria']; // Example list for government
  cities = ['City A', 'City B', 'City C']; // Example list for cities
  currentDate: string;
  organizers: OrganizerDto[];
  eventId: string | null = null; // To store the event ID from the route
  isEditMode = false; // To determine if the form is in edit mode

  constructor(
    private fb: FormBuilder,
    private eventService: EventService,
    private datePipe: DatePipe,
    private organizerService: OrganizerService,
    private router: Router,
    private route: ActivatedRoute, // Inject ActivatedRoute to read route parameters
    private toaster: ToasterService,
    private localization: LocalizationService
  ) {}

  ngOnInit(): void {
    this.eventForm = this.fb.group({
      id: [null],
      nameEn: ['', [Validators.required, Validators.maxLength(100)]],
      nameAr: ['', [Validators.required, Validators.maxLength(100)]],
      capacity: [null, [Validators.min(1)]],
      isOnline: [false],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      link: [''],
      government: [''],
      city: [''],
      street: [''],
      organizerId: [''],
    });

    this.currentDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm:ss');

    // Read the event ID from the route
    this.eventId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.eventId; // Set edit mode if ID exists

    // If in edit mode, fetch the event details
    if (this.isEditMode) {
      this.fetchEventDetails(this.eventId);
    }

    // Update validation based on isOnline value
    this.eventForm.get('isOnline')?.valueChanges.subscribe(isOnline => {
      this.isOnline = isOnline;

      if (isOnline) {
        this.eventForm.get('link')?.setValidators([Validators.required]);
        this.eventForm.get('capacity')?.clearValidators();
        this.eventForm.get('government')?.clearValidators();
        this.eventForm.get('city')?.clearValidators();
        this.eventForm.get('street')?.clearValidators();
      } else {
        this.eventForm.get('link')?.clearValidators();
        this.eventForm.get('capacity')?.setValidators([Validators.min(1)]);
        this.eventForm.get('government')?.setValidators([Validators.required]);
        this.eventForm.get('city')?.setValidators([Validators.required]);
        this.eventForm.get('street')?.setValidators([Validators.required]);
      }

      this.eventForm.get('link')?.updateValueAndValidity();
      this.eventForm.get('capacity')?.updateValueAndValidity();
      this.eventForm.get('government')?.updateValueAndValidity();
      this.eventForm.get('city')?.updateValueAndValidity();
      this.eventForm.get('street')?.updateValueAndValidity();
    });

    // Fetch organizers
    this.getOrganizers();
  }

  // Fetch event details for edit mode
  fetchEventDetails(eventId: string) {
    this.eventService.get(eventId).subscribe((event: EventDto) => {
      if (event) {
        event.startDate = this.datePipe.transform(event.startDate, 'yyyy-MM-dd HH:mm:ss');
        event.endDate = this.datePipe.transform(event.endDate, 'yyyy-MM-dd HH:mm:ss');
        this.eventForm.patchValue(event); // Populate the form with event details
      }
    });
  }

  // Fetch organizers
  getOrganizers() {
    this.organizerService.getList().subscribe((organizers: OrganizerDto[]) => {
      this.organizers = organizers;
    });
  }

  // Handle form submission
  onSubmit() {
    if (this.eventForm.valid) {
      const data = this.eventForm.value;

      // Convert to UTC with timezone
      data.startDate = new Date(data.startDate).toISOString();
      data.endDate = new Date(data.endDate).toISOString();

      if (this.isEditMode) {
        // Update existing event
        this.eventService.update(this.eventId, data).subscribe(() => {
          this.router.navigateByUrl('/admin/events'); // Redirect to events list
          this.toaster.success(this.localization.instant('Events::Success'));
        });
      } else {
        // Create new event
        this.eventService.create(data).subscribe(() => {
          this.router.navigateByUrl('/admin/events'); // Redirect to events list
          this.toaster.success(this.localization.instant('Events::AddSuccess'));
        });
      }
    } else {
      this.eventForm.markAllAsTouched();
    }
  }
}
