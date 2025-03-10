using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using FluentValidation;

namespace CarShop.Business.ValidationRules.FluentValidation
{
    public class CarModelValidator : AbstractValidator<CarModel>
    {
        public CarModelValidator()
        {
            RuleFor(m => m.Model)
                .NotNull().WithMessage("Model bilgisi eksik.")
                .Must(m => !string.IsNullOrEmpty(m.Name)).WithMessage("Model adı boş bırakılamaz.");
        }
    }
}