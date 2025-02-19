using OrderProject.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Unit)
                .NotEmpty().WithMessage("Zorunludur")
                .Length(5, 50).WithMessage("5-50 karakter arasında olmalıdır");

            RuleFor(p => p.UnitPrice)
    .NotEmpty()
    .NotNull()
        .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz.")
    .Must(s => s >= 0)
        .WithMessage("Fiyat bilgisi negatif olamaz!");

        }
    }
}
