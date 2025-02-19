using FluentValidation;
using OrderProject.Application.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Validators.Orders
{
    public class CreateOrderDetailValidator : AbstractValidator<CreateOrderDetail>
    {
        public CreateOrderDetailValidator()
        {
            RuleFor(x => x.ProductId)
              .NotEmpty().WithMessage("Ürün Id bilgisi boş geçilemez.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Birim fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Adet bilgisi 0'dan büyük olmalıdır.");
        }
    }
}
