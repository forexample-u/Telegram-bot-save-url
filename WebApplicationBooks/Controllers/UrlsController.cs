using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Domain;
using AutoMapper;
using WebApplicationBooks.Domain.Repository.Abstract;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using WebApplicationBooks.Mapping.Entity;
using WebApplicationBooks.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplicationBooks.Controllers
{
    public class UrlsController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<AccountUser> userManager;
        private readonly IMapper mapper;

        public UrlsController(AppDbContext context, 
            UserManager<AccountUser> userManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> UrlsUsers()
        {
            var loginUser = await userManager.GetUserAsync(HttpContext.User);

            IBooksRepository booksRepository = new EFBooksRepository(context);
            IUsersRepository usersRepository = new EFUsersRepository(context);
            User user = await usersRepository.GetUserByUsernameAsync(loginUser.MessagerUsername);
            List<Book> books = await booksRepository.GetBooksByUserIdAsync(user.UserId);

            List<DtoUrls> Urls = new();
            foreach (Book book in books)
            {
                DtoUrls urlsOnly = mapper.Map<DtoUrls>(book);
                Urls.Add(urlsOnly);
            }
            return View(Urls);
        }
    }
}