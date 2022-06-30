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

        public async Task<List<User>> GetUserByUserIdAsync(long userId)
        {
            List<User> users = new List<User>();

            users = await (from user in context.Users
                           where user.UserId == userId
                           select user).ToListAsync();
            return users;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            List<User> Users = new List<User>();
            Users = await context.Users.ToListAsync();
            return Users;
        }

        public async Task UpdateUserWithUrlAsync(User oldUser, User newUser)
        {
            User User = newUser;
            User.Id = oldUser.Id;
            context.Users.Update(User);
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