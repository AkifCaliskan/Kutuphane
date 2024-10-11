using AspNetCoreHero.ToastNotification.Abstractions;
using Kutuphane.Bussiness.Abstract;
using Kutuphane.Entities.Concrete;
using Kutuphane.WebUI.Models.Book;
using Kutuphane.WebUI.Models.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Controllers
{
    public class BookController : Controller
    {
        //private IToastifyService _notifyService;
        private INotyfService _notifyService;
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IOperationsService _operationsService;
        private IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BookController(INotyfService notifyService, IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IOperationsService operationsService, IUserService userService, IHostingEnvironment hostingEnvironment)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _operationsService = operationsService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
            _notifyService = notifyService;
        }
        public IActionResult Index()
        {
            IQueryable<Book> bookList = _bookService.GetQueryable(true).Include(p => p.Category).Include(p => p.Author).Include(p => p.Operationss);
            var books = bookList.ToList().Select(p => new BookListModel()
            {
                AuthorName = _authorService.GetById(p.AuthorId).AuthorName,
                BasimYili = p.PublicationYear,
                CategoryName = _categoryService.GetById(p.CategoryId).CategoryName,
                BookId = p.BookID,
                BookName = p.BookName,
                UserId = _operationsService.GetQueryable(false).Include(k => k.User).Include(k => k.Book).FirstOrDefault(k => k.BookId == p.BookID)?.UserId ?? 0,
                //UserName = _operationsService.GetQueryable(false).Include(k => k.User).Include(k => k.Book).FirstOrDefault(k => k.BookId == p.BookID)?.User.LastName?? "" +   _operationsService.GetQueryable().Include(k => k.User).FirstOrDefault(k => k.BookId == p.BookID)?.User.LastName ?? "",
                SayfaSayisi = p.PageCount,
                UserName = string.Join(" ", _operationsService.GetQueryable(false).Include(k => k.User).Include(k => k.Book).FirstOrDefault(k => k.BookId == p.BookID)?.User.FirstName ?? "", _operationsService.GetQueryable(false).Include(k => k.User).Include(k => k.Book).FirstOrDefault(k => k.BookId == p.BookID)?.User.LastName ?? ""),
                SeriNo = p.SerialNumber,
            }).ToList();

            return View(books);
        }
        public IActionResult Add()
        {
            SetAddViewData();
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookAddModel model)
        {
            if (!ModelState.IsValid)
            {
                SetAddViewData();
                _notifyService.Error("Bilgileri Kontrol Ediniz", 7);
                return View();
            }

            var addBook = _bookService.Add(new Book
            {
                AuthorId = model.AuthorId,
                YayinYili = model.YayinYili,
                BookName = model.BookName,
                CategoryId = model.CategoryId,
                PageCount = model.PageCount,
                PublicationYear = model.PublicationYear,
                Status = true,
                SerialNumber = model.SerialNumber,
                BookDescription = model.Description

            }); ;

            //_notifyService.Custom("Custom Notification - closes in 5 seconds.", 5, "whitesmoke", "fa fa-gear");
            _notifyService.Success("Kitap Ekleme İşlemi Başarılı", 7);
            return RedirectToAction("Index", "Book");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var bookEntity = _bookService.GetById(id);
            if (bookEntity == null)
            {
                _notifyService.Error("Kayıt Bulunamadı", 7);
                return RedirectToAction("Index", "Book");
            }
            var editModel = new BookEditModel
            {
                AuthorId = bookEntity.AuthorId,
                BookName = bookEntity.BookName,
                BookImage = "",
                CategoryId = bookEntity.CategoryId,
                Description = bookEntity.BookDescription,
                PageCount = bookEntity.PageCount,
                PublicationYear = bookEntity.PublicationYear,
                SerialNumber = bookEntity.SerialNumber,
                YayinYili = bookEntity.YayinYili,
            };
            SetEditViewData(editModel, bookEntity);
            _notifyService.Success("Kitap Düzenleme İşlemi Başarılı", 10);
            return View(editModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, BookEditModel bookEditModel)
        {
            var update = _bookService.GetById(id);
            if (update == null)
            {
                SetEditViewData(bookEditModel, update);
                return View();
            }

            if (!ModelState.IsValid)
            {
                SetEditViewData(bookEditModel, update);
                return View();
            }

            update.AuthorId = bookEditModel.AuthorId;
            update.BookDescription = bookEditModel.Description;
            update.BookName = bookEditModel.BookName;
            update.CategoryId = bookEditModel.CategoryId;
            update.PageCount = bookEditModel.PageCount;
            update.PublicationYear = bookEditModel.PublicationYear;
            update.SerialNumber = bookEditModel.SerialNumber;
            update.YayinYili = bookEditModel.YayinYili;

            _bookService.Update(update);
            _notifyService.Success("Kitap Güncelleme İşlemi Başarılı", 10);
            return RedirectToAction("Index", "Book");
        }

        public IActionResult Delete(int id)
        {
            var exitsBook = _bookService.GetById(id);
            if (exitsBook == null)
            {
                _notifyService.Error("Kitap Silme İşlemi Başarısız", 10);
                return RedirectToAction("Index", "Book");
            }

            exitsBook.Status = false;
            _bookService.Update(exitsBook);
            _notifyService.Success("Kitap Silme İşlemi Gerçekleşmiştir.", 10);
            return RedirectToAction("Index", "Book");
        }

        //public IActionResult Give()
        //{
        //    SetGiveViewData();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Give(BookGiveModel model)
        //{
        //    var givebook = _operationsService.Add(new Operations
        //    {
        //        BookId = model.BookId,
        //        İssueDate = model.İssueDate,
        //        ReturnTime = model.ReturnTime,
        //        OperationsID = model.OperationsID,
        //        OperationStatus = model.OperationStatus,
        //        UserId = model.UserId
        //    });

        //    return RedirectToAction("Index", "Book");
        //}

        [HttpPost]
        public IActionResult Upload(string fullname, IFormFile pic)
        {
            ViewData["fname"] = fullname;
            string fileName = Guid.NewGuid().ToString();
            if (pic != null)
            {
                var Upload = Path.Combine(_hostingEnvironment.WebRootPath, Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(fileName, FileMode.Create));
                ViewData["fileLocation"] = "/" + Path.GetFileName(pic.FileName);
            }
            return View();
        }

        private void SetAddViewData()
        {

            var authors = _authorService.GetQueryable().Where(x => x.status == true)
                .ToList()
                .Select(x => new SelectListItem(x.AuthorName, x.AuthorID.ToString())).ToList();
            authors.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.AuthorId = authors;

            //var books = _bookService.GetQueryable().Where(x => x.Status == false)
            //   .ToList()
            //   .Select(x => new SelectListItem(x.BookName, x.BookID.ToString())).ToList();
            //books.Insert(0, new SelectListItem("Seçiniz", "0"));
            //ViewBag.BookID = books;

            var category = _categoryService.GetQueryable().Where(x => x.CategoryStatus == true)
               .ToList()
               .Select(x => new SelectListItem(x.CategoryName, x.CategoryID.ToString())).ToList();
            category.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.CategoryId = category;
        }

        private void SetEditViewData(BookEditModel editModel, Book bookEntity)
        {

            var authors = _authorService.GetQueryable(true)
                .ToList()
                .Select(x => new SelectListItem(x.AuthorName, x.AuthorID.ToString())).ToList();
            authors.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.AuthorId = authors;

            //var books = _bookService.GetQueryable().Where(x => x.Status == false)
            //   .ToList()
            //   .Select(x => new SelectListItem(x.BookName, x.BookID.ToString())).ToList();
            //books.Insert(0, new SelectListItem("Seçiniz", "0"));
            //ViewBag.BookID = books;

            var category = _categoryService.GetQueryable(true)
               .ToList()
               .Select(x => new SelectListItem(x.CategoryName, x.CategoryID.ToString())).ToList();
            category.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.CategoryId = category;
        }

        private void SetGiveViewData()
        {
            var books = _bookService.GetQueryable().Where(x => x.Status == false)
               .ToList()
               .Select(x => new SelectListItem(x.BookName, x.BookID.ToString())).ToList();
            books.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.BookID = books;

            //user
            var users = _userService.GetQueryable().Where(x => x.UserStatus == false)
                .ToList()
                .Select(x => new SelectListItem(x.FirstName, x.UserID.ToString())).ToList();
            users.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.UserID = users;

            //using AspNetCoreHero.ToastNotification.Abstractions
            //app.UseNotyf();


        }

    }
}
