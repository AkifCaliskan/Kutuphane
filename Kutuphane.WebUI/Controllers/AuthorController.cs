using AspNetCoreHero.ToastNotification.Abstractions;
using Kutuphane.Bussiness.Abstract;
using Kutuphane.Entities.Concrete;
using Kutuphane.WebUI.Models.Author;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Controllers
{
    public class AuthorController : Controller
    {
        private INotyfService _notyfService;
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IOperationsService _operationsService;
        private IUserService _userService;
        public AuthorController(INotyfService notifyService, IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IOperationsService operationsService, IUserService userService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _operationsService = operationsService;
            _userService = userService;
            _notyfService = notifyService;
        }
        [HttpGet]
        public IActionResult Index(AuthorListModel model)
        {
            IQueryable<Author> authors = _authorService.GetQueryable(true);
            var authorlist = authors.ToList().Select(model => new AuthorListModel()
            {
                AuthorName = model.AuthorName,
                AuthorID = model.AuthorID,
                status = model.status,
            }).ToList();
            return View(authorlist);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AuthorAddModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            var existAuthorName = _authorService.GetQueryable(true).Any(p => p.AuthorName.Contains(model.AuthorName));
            if (existAuthorName)
            {
                _notyfService.Error("Aynı İsimde Yazar Mevcut", 7);
                return View();
            }


            var addAuthor = _authorService.Add(new Author
            {

                AuthorName = model.AuthorName,
                status = true,
            });
            _notyfService.Success("Yazar Ekleme İşlemi Başarılı", 7);
            return RedirectToAction("Index", "Author");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var authorEntity = _authorService.GetById(id);
            if (authorEntity == null)
            {
                return RedirectToAction("Index", "Author");
            }
            var editModel = new AuthorEditModel
            {
                AuthorName = authorEntity.AuthorName,
            };

            return View(editModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, AuthorEditModel authorEditModel)
        {
            var update = _authorService.GetById(id);
            if (update == null)
            {

                return View();
            }

            if (!ModelState.IsValid)
            {

                return View();
            }

            update.AuthorName = authorEditModel.AuthorName;

            _authorService.Update(update);
            _notyfService.Success("Yazar Düzenleme İşlemi Başarılı", 7);
            return RedirectToAction("Index", "Author");
        }
        public IActionResult Delete(int id)
        {
            var exitsauthor = _authorService.GetById(id);
            if (exitsauthor == null)
            {
                return RedirectToAction("Index", "Author");
            }

            exitsauthor.status = false;
            _authorService.Update(exitsauthor);
            _notyfService.Success("Yazar Silme İşlemi Başarılı", 7);
            return RedirectToAction("Index", "Author");
        }
    }
}
