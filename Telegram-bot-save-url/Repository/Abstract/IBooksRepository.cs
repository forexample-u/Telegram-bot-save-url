using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using App.Repository.Entities;

namespace App.Repository.Abstract
{
    public interface IBooksRepository
    {
        public Task<List<Book>> GetBooksByCategoriaIdAsync(long id, string categoria);
        public Task<List<Book>> GetBooksByIdAsync(long id);
        public Task UpdateBookUrlAsync(long id, string categoria, string url, string newCategoria, string newUrl);
        public Task AddBookUrlAsync(long id, string categoria, string url);
        public Task DeleteBookUrlAsync(long id, string categoria, string url);
    }
}
