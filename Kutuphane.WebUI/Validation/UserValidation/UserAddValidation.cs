using FluentValidation;
using Kutuphane.WebUI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Validation
{
    public class UserAddValidation : AbstractValidator<UserAddModel>
    {
        public UserAddValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim Boş Geçilemez").MinimumLength(3).WithMessage("3 karakterden Az Olamaz");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad Boş Geçilemez").MinimumLength(2).WithMessage("2 Karakterden Az Olamaz");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Numara Boş Geçilemez");
            RuleFor(x => x.TC).NotEmpty().WithMessage("TC kısmı boş geçilemez").MinimumLength(11).WithMessage("10 Karakterden Az Olamaz").MaximumLength(11).WithMessage("11 Karakterden Fazla Olamaz");
            RuleFor(x => x.UserEmail).NotEmpty().WithMessage("Email Kısmı Boş Geçilemez");
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("Parola Kısmı Boş Geçilemez").MinimumLength(4).WithMessage("Şifre 4 Karakterden Az Olamaz").MaximumLength(16).WithMessage("Şifre 16 Karakterden Fazla Olamaz");


        }
    }
}
