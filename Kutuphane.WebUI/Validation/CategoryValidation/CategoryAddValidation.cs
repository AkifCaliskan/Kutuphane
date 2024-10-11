using FluentValidation;
using Kutuphane.WebUI.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Validation
{
    public class CategoryAddValidation : AbstractValidator<CategoryAddModel>
    {
        public CategoryAddValidation()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori Adı Boş Geçilemez").MinimumLength(3).WithMessage("Kategori Adı 3 Karakterden Az Olamaz").MaximumLength(10).WithMessage("Kategori Adı 10 Karakterden Fazla Olamaz");

        }
    }
}
