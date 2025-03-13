import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateEventDto extends EntityDto<string> {
  nameEn?: string;
  nameAr?: string;
  capacity?: number;
  isOnline: boolean;
  startDate?: string;
  endDate?: string;
  organizerId?: string;
  link?: string;
  government?: string;
  city?: string;
  street?: string;
}

export interface EventActionDto {
  event: EventDto;
  canEdit: boolean;
  hasActiveAction: boolean;
}

export interface EventActiveDto {
  event: EventDto;
  isRegistered: boolean;
  eventRegistrationId?: string;
}

export interface EventDto extends FullAuditedEntityDto<string> {
  nameEn?: string;
  nameAr?: string;
  capacity?: number;
  isOnline: boolean;
  startDate?: string;
  endDate?: string;
  organizerId?: string;
  link?: string;
  location?: string;
  isActive: boolean;
  registrationCount: number;
  organizerName?: string;
  government?: string;
  city?: string;
  street?: string;
}

export interface EventRegistrationDto extends EntityDto<number> {
  eventId?: string;
  userId?: string;
  registeredAt?: string;
  isCanceled?: boolean;
  eventNameEn?: string;
  eventNameAr?: string;
  eventLink?: string;
  eventLocation?: string;
}
