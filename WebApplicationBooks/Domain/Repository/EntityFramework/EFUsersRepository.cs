using WebApplicationBooks.Domain.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using WebApplicationBooks.Domain.Repository.Abstract;
using System.Linq;

namespace WebApplicationBooks.Domain.Repository.EntityFramework
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
            User User = context.Users.FirstOrDefault(x => x.UserId == userId);
            if (User != null)
            {
                context.Users.Remove(User);
                await context.SaveChangesAsync();
            }
        }
    }
}