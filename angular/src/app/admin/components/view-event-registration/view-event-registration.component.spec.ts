import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEventRegistrationComponent } from './view-event-registration.component';

describe('ViewEventRegistrationComponent', () => {
  let component: ViewEventRegistrationComponent;
  let fixture: ComponentFixture<ViewEventRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewEventRegistrationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewEventRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
