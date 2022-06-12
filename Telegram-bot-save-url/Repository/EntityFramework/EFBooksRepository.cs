using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using App.Repository.Abstract;
using App.Repository.Entities;

namespace App.Repository.EntityFramework
{
    public class EFBooksRepository : IBooksRepository
    {
        private readonly AppDbContext context;
        public EFBooksRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Book>> GetBooksByCategoriaIdAsync(long id, string categoria)
        {
            List<Book> books = new List<Book>();

            books = await (from book in context.Books
                           where book.UserId == id && book.Categoria == categoria
                           select book).ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetBooksByIdAsync(long id)
        {
            List<Book> books = new List<Book>();

            books = await (from book in context.Books
                           where book.UserId == id
                           select book).ToListAsync();
            return books;
        }

        public async Task UpdateBookUrlAsync(long id, string categoria, string url, string newCategoria, string newUrl)
        {
            Book book = new Book() { UserId = id, Categoria = newCategoria, Url = newUrl };
            context.Books.Update(book);
            await context.SaveChangesAsync();
        }

        public async Task AddBookUrlAsync(long id, string categoria, string url)
        {
            Book book = new Book() { UserId = id, Categoria = categoria, Url = url };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBookUrlAsync(long id, string categoria, string url)
        {
            Book book = new Book() { UserId = id, Categoria = categoria, Url = url };
            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }
    }
}
