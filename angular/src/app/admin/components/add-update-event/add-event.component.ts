import { EventService } from '../../../proxy/events/event.service';
import { LocalizationModule } from '@abp/ng.core';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.scss',
  imports:[LocalizationModule, ReactiveFormsModule, CommonModule]
})

export class AddEventComponent implements OnInit {
  eventForm!: FormGroup;
  isOnline = false;
  governments = ['Cairo', 'Giza', 'Alexandria']; // Example list for government
  cities = ['City A', 'City B', 'City C']; // Example list for cities

  constructor(private fb: FormBuilder, private eventService: EventService) {}

  ngOnInit(): void {
    this.eventForm = this.fb.group({
      nameEn: ['', [Validators.required, Validators.maxLength(100)]],
      nameAr: ['', [Validators.required, Validators.maxLength(100)]],
      capacity: [null, [Validators.min(1)]],
      isOnline: [false],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      link: [''],
      government: [''],
      city: [''],
      street: ['']
    });

    // Update validation based on isOnline value
    this.eventForm.get('isOnline')?.valueChanges.subscribe((isOnline) => {
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
  }

  onSubmit() {
    if (this.eventForm.valid) {
      const data = this.eventForm.value;
      this.eventService.create(data).subscribe(c => {
        console.log(c);
      });
    } else {
      this.eventForm.markAllAsTouched();
    }
  }
}
