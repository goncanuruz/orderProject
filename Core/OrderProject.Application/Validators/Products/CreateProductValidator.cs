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
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Ürün adı boş geçmeyiniz")
                .Length(5, 50).WithMessage("Ürün adı 5-50 karakter arasında olmalıdır");

            RuleFor(p => p.Stock)
    .NotEmpty()
    .NotNull()
        .WithMessage("Lütfen stok bilgisini boş geçmeyiniz.")
    .Must(s => s >= 0)
        .WithMessage("Stok bilgisi negatif olamaz!");

        }
    }
}
