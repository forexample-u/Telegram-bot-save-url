﻿using WebApplicationBooks.Domain.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApplicationBooks.Domain.Repository.Abstract
{
    public interface IUsersRepository
    {
        public Task AddUserAsync(User user);

        public Task<List<User>> GetUserByUserIdAsync(long userId);

        public Task<List<User>> GetUsersAsync();

        public Task UpdateUserWithUrlAsync(User oldUser, User newUser);

        public Task DeleteUserByUserIdAsync(long id);
    }
}