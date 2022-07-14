using WebApplicationBooks.Domain.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using WebApplicationBooks.Domain.Repository.Abstract;
using System.Linq;

namespace WebApplicationBooks.Domain.Repository.EntityFramework
{
    public class EFBooksRepository : IBooksRepository
    {
        private readonly AppDbContext context;
        public EFBooksRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddBookWithUrlAsync(Book book)
        {
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetBooksByCategoriaIdAsync(long userId, string categoria)
        {
            List<Book> books = new List<Book>();

            books = await (from book in context.Books
                           where book.UserId == userId && book.Categoria == categoria
                           select book).ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetBooksByUserIdAsync(long userId)
        {
            List<Book> books = new List<Book>();

            books = await (from book in context.Books
                           where book.UserId == userId
                           select book).ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books = await context.Books.ToListAsync();
            return books;
        }

        public async Task UpdateBookWithUrlAsync(Book oldBook, Book newBook)
        {
            newBook.Id = oldBook.Id;
            context.Books.Remove(oldBook);
            await context.Books.AddAsync(newBook);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBookWithUrlByIdAsync(long id)
        {
            Book book = context.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
        }
    }
}