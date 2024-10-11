using AspNetCoreHero.ToastNotification.Abstractions;
using Kutuphane.Bussiness.Abstract;
using Kutuphane.Entities.Concrete;
using Kutuphane.WebUI.Models.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private INotyfService _notifyService;
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IOperationsService _operationsService;
        private IUserService _userService;
        public CategoryController(INotyfService notifyService, IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IOperationsService operationsService, IUserService userService)
        {
            _notifyService = notifyService;
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _operationsService = operationsService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index(CategoryListModel model)
        {
            IQueryable<Category> categories = _categoryService.GetQueryable(true);
            var categorylist = categories.ToList().Select(p => new CategoryListModel()
            {
                CategoryID = p.CategoryID,
                CategoryName = p.CategoryName,
                CategoryStatus = p.CategoryStatus,
            }).ToList();
            return View(categorylist);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryAddModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            //Aynı isimde kategori adı var mı ?  Contains: içeriyor mu Any: Hiç,,,, Var mı Anlamına Geliyor.
            var existCategoryName = _categoryService.GetQueryable(true).Any(p => p.CategoryName.Contains(model.CategoryName));
            if (existCategoryName)
            {
                
                return View();
            }
            var addCategory = _categoryService.Add(new Category
            {
                
                CategoryName = model.CategoryName,
                CategoryStatus= true,
            });
            _notifyService.Success("Kategori Ekleme İşlemi Başarılı", 7);
            return RedirectToAction("Index", "Category");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var categoryEntity = _categoryService.GetById(id);
            if (categoryEntity == null)
            {
                return RedirectToAction("Index", "Category");
            }
            var editModel = new CategoryEditModel
            {
                CategoryName = categoryEntity.CategoryName,
            };

            return View(editModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryEditModel categoryEditModel)
        {
            var update = _categoryService.GetById(id);
            if (update == null)
            {

                return View();
            }

            if (!ModelState.IsValid)
            {

                return View();
            }

            update.CategoryName = categoryEditModel.CategoryName;

            _categoryService.Update(update);
            _notifyService.Success("Kategori Düzenleme İşlemi Başarılı", 7);

            return RedirectToAction("Index", "Category");
        }
        public IActionResult Delete(int id)
        {
            var exitscategory = _categoryService.GetById(id);
            if (exitscategory == null)
            {
                return RedirectToAction("Index", "Category");
            }

            var hasCategoryRelatedBook = _bookService.GetQueryable(true).Any(p => p.CategoryId == id);

            if (hasCategoryRelatedBook)
            {
                //Bu kategoriye ait kitap kaydı bulunmaktadır. Silinemez
                return RedirectToAction("Index", "Category");
            }
            exitscategory.CategoryStatus = false;
            _categoryService.Update(exitscategory);
            _notifyService.Success("Kategori Silme İşlemi Başarılı", 7);
            return RedirectToAction("Index", "Category");
        }



    }
}