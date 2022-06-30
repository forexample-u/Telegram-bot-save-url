using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Domain.Repository;
using WebApplicationBooks.Domain;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using WebApplicationBooks.Domain.Repository.Abstract;
using System.Collections.Generic;

namespace WebApplicationBooks.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext context;
        public UserController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> UserContent()
        {
            IBooksRepository booksRepository = new EFBooksRepository(context);
            List<Book> users = await booksRepository.GetBooksByUserIdAsync(1000); //TODO find id user
            return View(users);
        }
    }
}