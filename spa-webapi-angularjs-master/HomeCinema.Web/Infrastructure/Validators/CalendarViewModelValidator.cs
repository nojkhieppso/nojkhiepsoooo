using FluentValidation;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Validators
{
    public class CalendarViewModelValidator : AbstractValidator<CalendarViewModel>
    {
        public CalendarViewModelValidator()
        {
            RuleFor(group => group.title).NotEmpty()
                .WithMessage("Select a description");
        }
    }
}