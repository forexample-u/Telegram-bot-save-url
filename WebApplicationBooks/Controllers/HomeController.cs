using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Domain;

namespace WebApplicationBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            var books = context.Books;
            return View(books);
        }
    }
}
