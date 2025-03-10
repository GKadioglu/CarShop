using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShop.Entity;
using FluentValidation;

namespace CarShop.Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Brand)
                .NotEmpty().WithMessage("Marka boş bırakılamaz.");

            RuleFor(c => c.Model)
                .NotEmpty().WithMessage("Model boş bırakılamaz.");

            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("Fiyat boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Fiyat sıfırdan büyük olmalıdır.");

            RuleFor(c => c.Year)
                .NotEmpty().WithMessage("Yıl boş bırakılamaz.")
                .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Geçerli bir yıl giriniz.");

            // CarModels doğrulaması
            RuleForEach(c => c.CarModels)
                .NotNull().WithMessage("Model bilgisi eksik.")
                .SetValidator(new CarModelValidator());

            // CarCategories doğrulaması
            RuleForEach(c => c.CarCategories)
                .NotNull().WithMessage("Kategori bilgisi eksik.")
                .SetValidator(new CarCategoryValidator());
        }
    }
}
