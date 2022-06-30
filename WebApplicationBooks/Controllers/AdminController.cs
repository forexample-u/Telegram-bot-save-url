using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Domain.Repository;
using WebApplicationBooks.Domain;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using WebApplicationBooks.Domain.Repository.Abstract;

namespace WebApplicationBooks.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext context;
        public AdminController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult AdminContent()
        {
            IBooksRepository booksRepository = new EFBooksRepository(context);
            IEnumerable<Book> users = context.Books;
            return View(users);
        }
    }
}