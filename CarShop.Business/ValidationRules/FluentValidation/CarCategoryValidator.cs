using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using FluentValidation;

namespace CarShop.Business.ValidationRules.FluentValidation
{
    public class CarCategoryValidator : AbstractValidator<CarCategory>
    {
        public CarCategoryValidator()
        {
            RuleFor(c => c.Category)
                .NotNull().WithMessage("Kategori eksik.")
                .Must(c => !string.IsNullOrEmpty(c.Name)).WithMessage("Kategori adı boş bırakılamaz.");
        }
    }
}