using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebAppLibrary.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppLibrary.Controllers.Web
{
    public class AppController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchBook()
        {
            return View();
        }

        public IActionResult BuyBook()
        {
            return View();
        }

        public IActionResult LibraryLocals()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactViewModel model)
        {
            return View(); 
        }

    }
}
