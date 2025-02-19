using FluentValidation;
using OrderProject.Application.ViewModels.Orders;
using OrderProject.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Validators.Orders
{

    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Müşteri adı boş geçilemez.");

            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage("Müşteri email adresi boş geçilemez.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.CustomerGsm)
                .NotEmpty().WithMessage("Müşteri GSM bilgisi boş geçilemez.");

            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("En az bir sipariş detayı eklenmelidir.")
                .ForEach(detail =>
                {
                    detail.SetValidator(new CreateOrderDetailValidator());
                });
        }
    }
}
