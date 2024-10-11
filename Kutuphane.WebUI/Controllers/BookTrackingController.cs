using AspNetCoreHero.ToastNotification.Abstractions;
using Kutuphane.Bussiness.Abstract;
using Kutuphane.Entities.Concrete;
using Kutuphane.WebUI.Models.Book;
using Kutuphane.WebUI.Models.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Controllers
{
    public class BookTrackingController : Controller
    {
        private INotyfService _notyfService;
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IOperationsService _operationsService;
        private IUserService _userService;
        public BookTrackingController(INotyfService notyfService, IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IOperationsService operationsService, IUserService userService)
        {
            _notyfService = notyfService;
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _operationsService = operationsService;
            _userService = userService;

        }

        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<Operations> operationsList = _operationsService.GetQueryable(true).Include(p => p.User);
            var operations = operationsList.OrderBy(p => p.OperationsID).ToList().Select(p => new BookTrackingModel()
            {
                BookId = p.BookId,
                OperationsID = p.OperationsID,
                OperationStatus = p.OperationStatus,
                ReturnTime = p.ReturnTime,
                IsReturnTime = p.ReturnTime.ToString(),
                UserId = p.UserId,
                BookName = _bookService.GetById(p.BookId)?.BookName ?? "",
                UserName = string.Join(" ", _userService.GetQueryable(true).FirstOrDefault(k => k.UserID == p.UserId)?.FirstName ?? "", _userService.GetQueryable(true).FirstOrDefault(k => k.UserID == p.UserId)?.LastName ?? ""),
                İssueDate = p.İssueDate,
            }).ToList();

            return View(operations);
        }

        public IActionResult Give()
        {
            SetGiveViewData();
            return View();
        }

        [HttpPost]
        public IActionResult Give(BookGiveModel model)
        {
            if (!ModelState.IsValid)
            {
                //notfy("Yanlış veri kontrol edin" ,10)
                SetGiveViewData();
                return View();
            }
            var bookGiveCount = _operationsService.GetQueryable(true).Include(p => p.Book).Count(p => p.BookId == model.BookId);
            if (bookGiveCount > 6)

            {
                //notfy olunca 
                return RedirectToAction("Index", "BookTracking");
            }

            var givebook = _operationsService.Add(new Operations
            {

                //01.01.1900
                BookId = model.BookId,
                İssueDate = model.İssueDate,
                ReturnTime = Convert.ToDateTime(model.ReturnTime),
                OperationsID = model.OperationsID,
                OperationStatus = true,
                UserId = model.UserId,
            });

            _notyfService.Success("Kitap Verme İşlemi Başarılı", 10);
            return RedirectToAction("Index", "BookTracking");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var booktrackingEntity = _operationsService.GetById(id);
            if (booktrackingEntity == null)
            {
                return RedirectToAction("Index", "BookTracking");
            }
            var editModel = new BookTrackingEditModel
            {
                UserId = booktrackingEntity.UserId,
                BookId = booktrackingEntity.BookId,
                ReturnTime = booktrackingEntity.ReturnTime,
                İssueDate = booktrackingEntity.İssueDate,
                OperationsID = booktrackingEntity.OperationsID,
                OperationStatus = booktrackingEntity.OperationStatus
            };
            SetEditGiveViewData(editModel, booktrackingEntity);
            return View(editModel);
        }
        [HttpPost]
        public IActionResult Edit(int id, BookTrackingEditModel bookTrackingEditModel)
        {
            var update = _operationsService.GetById(id);
            if (update == null)
            {
                SetEditGiveViewData(bookTrackingEditModel, update);
                return View();
            }
            if (!ModelState.IsValid)
            {
                SetEditGiveViewData(bookTrackingEditModel, update);
                return View();
            }
            update.UserId = bookTrackingEditModel.UserId;
            update.BookId = bookTrackingEditModel.BookId;
            update.ReturnTime = (DateTime)(bookTrackingEditModel.ReturnTime == null ? bookTrackingEditModel.ReturnTime.GetValueOrDefault()    /*DateTime.Parse("")*/ /*default(DateTime)*/ : bookTrackingEditModel.ReturnTime);
            update.İssueDate = bookTrackingEditModel.İssueDate;

            _operationsService.Update(update);
            _notyfService.Success("Düzenleme İşlemi Başarılı", 10);
            return RedirectToAction("Index", "BookTracking");
        }

        public IActionResult Delete(int id)
        {
            var exitsbooktracking = _operationsService.GetById(id);
            if (exitsbooktracking == null)
            {
                return RedirectToAction("Index", "BookTracking");
            }



            exitsbooktracking.OperationStatus = false;
            _operationsService.Update(exitsbooktracking);
            _notyfService.Success("Silme İşlemi Başarılı", 10);
            return RedirectToAction("Index", "BookTracking");
        }

        private void SetGiveViewData()
        {
            var books = _bookService.GetQueryable(true)
               .ToList()
               .Select(x => new SelectListItem(x.BookName, x.BookID.ToString())).ToList();
            books.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.BookID = books;

            //user
            var users = _userService.GetQueryable(true)
                .ToList()
                .Select(x => new SelectListItem(string.Join(" ", x.FirstName, x.LastName), x.UserID.ToString())).ToList();
            users.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.UserID = users;
        }

        private void SetEditGiveViewData(BookTrackingEditModel editModel, Operations bookTrackingEntity)
        {
            var books = _bookService.GetQueryable()
               .ToList()
               .Select(x => new SelectListItem(x.BookName, x.BookID.ToString())).ToList();
            books.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.BookID = books;

            //user
            var users = _userService.GetQueryable()
                .ToList()
                .Select(x => new SelectListItem(string.Join(" ", x.FirstName, x.LastName), x.UserID.ToString())).ToList();
            users.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.UserID = users;
            var returns = _operationsService.GetQueryable()
                .ToList()
                .Select(p => new SelectListItem(p.ReturnTime.ToString("dd/MM/yyyy HH:mm"), p.OperationsID.ToString())).ToList();
            returns.Insert(0, new SelectListItem("Seçiniz", "0"));
            ViewBag.OperationsID = returns;
        }
    }
}
