using FluentValidation;
using Kutuphane.WebUI.Models.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Validation
{
    public class AuthorAddValidation : AbstractValidator<AuthorAddModel>
    {
        public AuthorAddValidation()
        {
            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("Yazar Adı Boş Geçilemez").MaximumLength(50).WithMessage("Yazar Adı 50 Karakterden Fazla Olamaz").MinimumLength(9).WithMessage("Yazar Adı 9 Karakterden Az Olamaz");
        }
    }
}
