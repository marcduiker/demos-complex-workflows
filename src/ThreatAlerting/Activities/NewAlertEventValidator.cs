using System;
using FluentValidation;
using ThreatAlerting.Models;

namespace ThreatAlertProcessing.Activities
{
    public class NewAlertEventValidator : AbstractValidator<NewAlertEvent>
    {
        public NewAlertEventValidator()
        {
            RuleFor(t => t.Id).NotEqual(Guid.Empty);
            RuleFor(t => t.Attributes).NotEmpty();
        }
    }
}