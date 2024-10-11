using AspNetCoreHero.ToastNotification.Abstractions;
using Kutuphane.WebUI.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private INotyfService _notifyService;
        public AdminController(INotyfService notifyService)
        {
            _notifyService = notifyService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }
        // POST: When submitting the login credentials
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (viewModel.Login == "Admin" && viewModel.Password == "1234")
            {
                _notifyService.Success("Giriş İşlemi Başarılı AnaSayfaya Yönlendiriliyorsunuz", 10);
                return RedirectToAction("AnaSayfa", "Home");
            }
            _notifyService.Error("Bilgileri Kontrol Ediniz", 10);
            return View();
        }
    }

}
