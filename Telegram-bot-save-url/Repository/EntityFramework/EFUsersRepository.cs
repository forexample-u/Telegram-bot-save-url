using App.Repository;
using App.Repository.Abstract;
using App.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly AppDbContext context;
        public EFUsersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUserIdAsync(long userId)
        {
            List<User> users = await context.Users.ToListAsync();
            User user = users.FirstOrDefault(x => x.UserId == userId);
            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            List<User> Users = await context.Users.ToListAsync();
            return Users;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            List<User> users = await context.Users.ToListAsync();
            User user = users.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return new User();
            }
            return user;
        }

        public async Task UpdateUserWithUrlAsync(User oldUser, User newUser)
        {
            newUser.Id = oldUser.Id;
            context.Users.Remove(oldUser);
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserByUserIdAsync(long userId)
        {
            User user = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
    }
}