using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult AnaSayfa()
        {
            return View();
        }
    }
}
