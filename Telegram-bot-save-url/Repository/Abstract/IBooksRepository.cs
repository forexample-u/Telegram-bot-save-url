using System;
using App.Repository.Entities;

namespace App.Repository.Abstract
{
    public interface IBooksRepository
    {
        public Task<List<Book>> GetBooksByCategoriaIdAsync(long id, string categoria);
        public Task<List<Book>> GetBooksByUserIdAsync(long id);
        public Task UpdateBookWithUrlAsync(Book oldBook, Book newBook);
        public Task AddBookWithUrlAsync(Book book);
        public Task DeleteBookWithUrlByIdAsync(long id);
    }
}
