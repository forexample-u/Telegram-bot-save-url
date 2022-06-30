using WebApplicationBooks.Domain.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApplicationBooks.Domain.Repository.Abstract
{
    public interface IBooksRepository
    {
        public Task AddBookWithUrlAsync(Book book);

        public Task<List<Book>> GetBooksByCategoriaIdAsync(long userId, string categoria);

        public Task<List<Book>> GetBooksByUserIdAsync(long userId);

        public Task UpdateBookWithUrlAsync(Book oldBook, Book newBook);

        public Task DeleteBookWithUrlByIdAsync(long id);
    }
}