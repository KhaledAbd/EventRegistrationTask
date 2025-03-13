using FluentValidation;
using Microsoft.Extensions.Localization;
using RAG.EventRegistrationTask.Localization;
using System;

namespace RAG.EventRegistrationTask.Events
{
    public class CreateEventDtoValidator : AbstractValidator<CreateUpdateEventDto>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private IStringLocalizer _localizer => _stringLocalizerFactory.Create(typeof(EventResource));

        public CreateEventDtoValidator(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
            RuleFor(x => x.NameEn)
                .NotEmpty()
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_NAME_EN])
                .MaximumLength(100)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_NAME_EN])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_NAME_EN);

            RuleFor(x => x.NameAr)
                .NotEmpty()
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_NAME_AR])
                .MaximumLength(100)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_NAME_AR])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_NAME_AR);

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .When(x => !x.IsOnline)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_CAPACITY])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_CAPACITY);

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_START_DATE])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_START_DATE);

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_END_DATE])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_END_DATE);

            RuleFor(x => x.Link)
                .NotEmpty()
                .When(x => x.IsOnline)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_LINK])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_LINK);

            RuleFor(x => x.OrganizerId)
                .NotNull()
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_ORGANIZER])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_ORGANIZER);

            RuleFor(x => x.Street)
                .NotEmpty()
                .When(x => !x.IsOnline)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_STREET])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_STREET);

            RuleFor(x => x.Government)
                .NotEmpty()
                .When(x => !x.IsOnline)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_GOVERNMENT])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_GOVERNMENT);

            RuleFor(x => x.City)
                .NotEmpty()
                .When(x => !x.IsOnline)
                .WithMessage(_localizer[EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_CITY])
                .WithErrorCode(EventRegistrationTaskDomainErrorCodes.INVALID_EVENT_CITY);
            _stringLocalizerFactory = stringLocalizerFactory;
        }
    }
}
