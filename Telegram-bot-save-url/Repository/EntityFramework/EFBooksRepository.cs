﻿using System;
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

        public async Task<List<Book>> GetBooksByCategoriaUserIdAsync(long userId, string categoria)
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

        public async Task UpdateBookWithUrlAsync(Book oldBook, Book newBook)
        {
            Book book = newBook;
            book.Id = oldBook.Id;
            context.Books.Update(book);
            await context.SaveChangesAsync();
        }

        public async Task AddBookWithUrlAsync(Book book)
        {
            await context.Books.AddAsync(book);
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