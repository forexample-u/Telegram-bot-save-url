using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Domain.Repository;
using WebApplicationBooks.Domain;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using WebApplicationBooks.Domain.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationBooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext context;
        public AdminController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> AdminContent()
        {
            IBooksRepository booksRepository = new EFBooksRepository(context);
            IUsersRepository usersRepository = new EFUsersRepository(context);
            List<Book> books = await booksRepository.GetBooksAsync();

            Dictionary<User, Book> usersWithBooks = new();
            foreach (Book book in books)
            {
                User user = await usersRepository.GetUserByUserIdAsync(book.UserId);
                usersWithBooks.Add(user, book);
            }
            return View(usersWithBooks);
        }
    }
}