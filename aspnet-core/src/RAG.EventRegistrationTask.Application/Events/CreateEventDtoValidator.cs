using FluentValidation;
using System;

namespace RAG.EventRegistrationTask.Events
{

    public class CreateEventDtoValidator : AbstractValidator<CreateUpdateEventDto>
    {
        public CreateEventDtoValidator()
        {
            RuleFor(x => x.NameEn)
                .NotEmpty()
                .WithMessage("Event name (English) is required")
                .MaximumLength(100)
                .WithMessage("Event name (English) cannot exceed 100 characters");

            RuleFor(x => x.NameAr)
                .NotEmpty()
                .WithMessage("Event name (Arabic) is required")
                .MaximumLength(100)
                .WithMessage("Event name (Arabic) cannot exceed 100 characters");

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .When(x => !x.IsOnline)
                .WithMessage("Capacity must be greater than zero for offline events");

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Start date must be in the future");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be after the start date");

            RuleFor(x => x.Link)
                .NotEmpty()
                .When(x => x.IsOnline)
                .WithMessage("Link is required for online events");

            RuleFor(x => x.Street)
                .NotEmpty()
                .When(x => !x.IsOnline)
                .WithMessage("Street is required for offline events");

            RuleFor(x => x.Government)
                .NotEmpty()
                .When(x => !x.IsOnline)
                .WithMessage("Street is required for offline events");


            RuleFor(x => x.City)
                .NotEmpty()
                .When(x => !x.IsOnline)
                .WithMessage("Street is required for offline events");
        }
    }

}
