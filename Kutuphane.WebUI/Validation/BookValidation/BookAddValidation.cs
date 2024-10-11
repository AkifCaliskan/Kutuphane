using FluentValidation;
using Kutuphane.WebUI.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Validation
{
    public class BookAddValidation : AbstractValidator<BookAddModel>
    {
        public BookAddValidation()
        {
            RuleFor(x => x.BookName).NotEmpty().WithMessage("Kitap Adı Boş Geçilemez").MinimumLength(3).WithMessage("En az 3 karakter olmalıdır").MaximumLength(100).WithMessage("En fazla 100 karakter olmalıdır");
            RuleFor(x => x.AuthorId).GreaterThan(0).WithMessage("Yazar Seçilmelidir");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Kategori Seçilmelidir");
            RuleFor(x => x.Description).NotNull().WithMessage("Açıklama Boş Geçilemez").MinimumLength(20).WithMessage("Açıklama 20 Karakterden az Olamaz");
            RuleFor(x => x.SerialNumber).NotEmpty().WithMessage("Seri Numarası Boş Bırakılamaz").MaximumLength(8).WithMessage("Seri Numarası 8 Karakterden Fazla Olamaz");
            RuleFor(x => x.PageCount).NotEmpty().WithMessage("Sayfa Sayısı Boş Geçilemez").MaximumLength(7).WithMessage("Sayfa sayısı 7 karakterden büyük olamaz");
            RuleFor(x => x.PublicationYear).NotEmpty().WithMessage("Basım Yılı Boş Geçilemez").MinimumLength(3).WithMessage("4 Karakterden az olamaz.").MaximumLength(4).WithMessage("4 Karakterden fazla olamaz");
            RuleFor(x => x.YayinYili).NotEmpty().WithMessage("Yayın Yılı Boş Geçilemez");

        }
    }
}
