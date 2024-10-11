using FluentValidation;
using Kutuphane.WebUI.Models.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Validation
{
    public class BookGiveValidation : AbstractValidator<BookGiveModel>
    {
        public BookGiveValidation()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("Kitap boş geçilemez, lütfen kitap seçin");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı boş geçilemez, lütfen kullanıcı seçin");
            RuleFor(x => x.İssueDate).NotEmpty().WithMessage("Kitabın verildiği tarih boş geçilemez, lütfen gün/ay/yıl şeklinde seçim yapın");
            //RuleFor(x => x.ReturnTime)..WithMessage("asd");
        }
    }
}
