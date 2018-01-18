using FluentValidation;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Validators
{
    public class ImageViewModelValidator : AbstractValidator<ImageViewModel>
    {
        public ImageViewModelValidator()
        {
            RuleFor(image => image.FileName).NotEmpty()
                .WithMessage("Select a FileName");
        }
    }
}