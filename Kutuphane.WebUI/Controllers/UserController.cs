using AspNetCoreHero.ToastNotification.Abstractions;
using Kutuphane.Bussiness.Abstract;
using Kutuphane.Entities.Concrete;
using Kutuphane.WebUI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Controllers
{
    public class UserController : Controller
    {
        private INotyfService _notyfService;
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IAuthorService _authorService;
        private IOperationsService _operationsService;
        private IUserService _userService;
        public UserController(INotyfService notyfService, IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IOperationsService operationsService, IUserService userService)
        {
            _notyfService = notyfService;
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _operationsService = operationsService;
            _userService = userService;

        }
        [HttpGet]
        public IActionResult Index(UserListModel model)
        {

            IQueryable<User> users = _userService.GetQueryable(true);
            var booklist = users.ToList().Select(p => new UserListModel()
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                TC = p.TC,
                PhoneNumber = p.PhoneNumber,
                UserEmail = p.UserEmail,
                UserID = p.UserID,
            }).ToList();
            return View(booklist);
        }
       
        public IActionResult DeleteUser(int id)
        {
            var deleteuser = _userService.GetByID(id);
            if (deleteuser == null)
            {
                return RedirectToAction("Index", "User");
            }
            deleteuser.UserStatus = false;
            _userService.Update(deleteuser);
            _notyfService.Success("Kullanıcı Silme İşlemi Başarılı", 10);
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserAddModel model)
        {
            var intTc = Convert.ToInt64(model.TC);
            if (intTc % 2 != 0)
            {

                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var adduser = _userService.Add(new User
            {
                UserID = model.UserID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                TC = model.TC,
                UserEmail = model.UserEmail,
                UserPassword = model.UserPassword,
            });
            _notyfService.Success("Kullanıcı Ekleme İşlemi Başarılı", 10);
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userEntity = _userService.GetByID(id);
            if (userEntity == null)
            {
                _notyfService.Error("Kayıt Bulunamadı", 7);
                return RedirectToAction("Index", "User");
            }
            var editModel = new UserEditModel
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                PhoneNumber = userEntity.PhoneNumber,
                TC = userEntity.TC,
                UserEmail = userEntity.UserEmail,
                UserID = userEntity.UserID,
                UserPassword = userEntity.UserPassword,
            };
            _notyfService.Success("Kullanıcı Düzenleme İşlemi Başarılı", 10);
            return View(editModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, UserEditModel userEditModel)
        {
            var update = _userService.GetByID(id);
            if (update == null)
            {
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            update.FirstName = userEditModel.FirstName;
            update.LastName = userEditModel.LastName;
            update.PhoneNumber = userEditModel.PhoneNumber;
            update.TC = userEditModel.TC;
            update.UserEmail = userEditModel.UserEmail;
            //update.UserID = userEditModel.UserID;
            update.UserPassword = userEditModel.UserPassword;
            //update.UserStatus = true;

            _userService.Update(update);
            _notyfService.Success("Kullanıcı Düzenleme İşlemi Başarılı", 10);
            return RedirectToAction("Index", "User");
        }
        }
}
