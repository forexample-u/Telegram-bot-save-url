using Microsoft.AspNetCore.Mvc;
using WebApplicationBooks.Domain;
using AutoMapper;
using WebApplicationBooks.Domain.Repository.Abstract;
using WebApplicationBooks.Domain.Repository.Entity;
using WebApplicationBooks.Domain.Repository.EntityFramework;
using WebApplicationBooks.Mapping.Entity;

namespace WebApplicationBooks.Controllers
{
    public class UrlsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public UrlsController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IActionResult> UrlsUsers()
        {
            IBooksRepository booksRepository = new EFBooksRepository(context);
            List<Book> books = await booksRepository.GetBooksAsync();

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