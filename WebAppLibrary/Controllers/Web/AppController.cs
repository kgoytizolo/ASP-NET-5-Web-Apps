using Microsoft.AspNet.Mvc;
using WebAppLibrary.ViewModels;
using WebAppLibrary.Services;
using WebAppLibrary.Repositories.Interface;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppLibrary.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private ILibraryRepository _libraryRepository;

        //Generating constructor uses dependency injection (controller injection from constructor - constructor injection)
        //Uses the implementation of the emailService, search the list of services added in Startup.cs for the first time, to be reused anytime without being instanced
        //ASPNET 5 has a constructor which searchs in Startup class all services to be used for dependency injection
        public AppController(IMailService service, ILibraryRepository libraryRepository) {
            _mailService = service;
            _libraryRepository = libraryRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchBook()
        {
            var books = _libraryRepository.GetAllBooks();
            return View(books);
        }

        public IActionResult BuyBook()
        {
            return View();
        }

        public IActionResult LibraryLocals()
        {
            return View();
        }

        public IActionResult ContactUs() {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactViewModel model)
        {
            //Validates if values returned where validated correctly and valid
            if (ModelState.IsValid) {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];       //Showing dictionary of names and values. Hierarchy is separated by ":"
                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("","Could not send email, wrong email!");
                }
                if (_mailService.SendMail(email, model.Email, $"Contact requirement from ({model.Name})", model.Message)) {
                    ModelState.Clear();                                                 //Clear the form, not just the state of the form. 
                    ViewBag.message = "Thank you! Mail message has been sent.";
                }
            }
            return View();
        }

    }
}
