using App.Repository;
using App.Repository.Abstract;
using App.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.EntityFramework
{
	public class EFUsersSessionsRepository : IUsersSessionsRepository
	{
		private readonly AppDbContext context;
		public EFUsersSessionsRepository(AppDbContext context)
		{
			this.context = context;
		}

		public async Task<UserSession> GetSessionByUserIdAsync(long userId)
		{
			var session = await context.UsersSessions
						.FirstOrDefaultAsync(x => x.UserId == userId);
			return session;
		}

		public async Task AddSessionByUserIdAsync(long userId)
		{
			await context.UsersSessions.AddAsync(new UserSession() { UserId = userId });
			await context.SaveChangesAsync();
		}

		public async Task UpdateSessionByUserIdAsync(long userId, UserSession newSession)
		{
			await DeleteSessionByUserIdAsync(userId);
			newSession.UserId = userId;
			await context.UsersSessions.AddAsync(newSession);
			await context.SaveChangesAsync();
		}

		public async Task DeleteSessionByUserIdAsync(long userId)
		{
			UserSession session = await GetSessionByUserIdAsync(userId);
			if (session != null)
			{
				context.UsersSessions.Remove(session);
			}
			await context.SaveChangesAsync();
		}
	}
}