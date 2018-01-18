using FluentValidation;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Validators
{
    public class GroupViewModelValidator : AbstractValidator<GroupViewModel>
    {
        public GroupViewModelValidator()
        {
            RuleFor(group => group.Name).NotEmpty()
                .WithMessage("Select a description");
        }
    }
}