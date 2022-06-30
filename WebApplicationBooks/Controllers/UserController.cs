using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Domain.Repository;
using WebApplicationBooks.Domain;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using WebApplicationBooks.Domain.Repository.Abstract;

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
            IUsersRepository usersRepository = new EFUsersRepository(context);
            List<Book> books = await booksRepository.GetBooksByUserIdAsync(1003342); //TODO find id user

            Dictionary<User, Book> usersWithBooks = new();
            foreach (Book book in books)
            {
                User user = await usersRepository.GetUserByUserIdAsync(book.UserId);
                usersWithBooks.Add(user, book);
            }
            return View(books);
        }
    }
}