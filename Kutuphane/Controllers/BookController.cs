﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUi.Controllers
{
    public class BookController : Controller
    {
        public IActionResult BookList()
        {
            return View();
        }
    }
}
