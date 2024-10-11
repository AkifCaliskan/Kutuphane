using FluentValidation;
using Kutuphane.WebUI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Validation
{
    public class UserEditValidation : AbstractValidator<UserEditModel>
    {
        public UserEditValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Kullanıcı İsmi Boş Geçilemez").MinimumLength(3).WithMessage("Kullanıcı İsmi 3 Karakterden Boş Geçilemez");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Kullanıcı Soyadı Boş Geçilemez");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Kullanıcı Numarası Boş Geçilemez").MinimumLength(11).WithMessage("Kullanıcı İsmi 3 Karakterden Boş Geçilemez").MaximumLength(12).WithMessage("Telefon Numarası 12 Karakterden Az Olamaz");
            RuleFor(x => x.TC).NotEmpty().WithMessage("Kullanıcı TC Boş Geçilemez").MinimumLength(11).WithMessage("TC 11 Karakterden Az Olamaz");
            RuleFor(x => x.UserEmail).NotEmpty().WithMessage("Kullanıcı E-Mail Boş Geçilemez");
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("Kullanıcı Şifresi Boş Geçilemez");
        }
    }
}
