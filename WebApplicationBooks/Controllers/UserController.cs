using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Domain.Repository;
using WebApplicationBooks.Domain;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using WebApplicationBooks.Domain.Repository.Abstract;
using WebApplicationBooks.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplicationBooks.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<AccountUser> userManager;

        public UserController(AppDbContext context,
            UserManager<AccountUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> UserContent()
        {
            var loginUser = await userManager.GetUserAsync(HttpContext.User);

            IBooksRepository booksRepository = new EFBooksRepository(context);
            IUsersRepository usersRepository = new EFUsersRepository(context);
            User user = await usersRepository.GetUserByUsernameAsync(loginUser.MessagerUsername);
            List<Book> books = await booksRepository.GetBooksByUserIdAsync(user.UserId);

            return View(books);
        }
    }
}