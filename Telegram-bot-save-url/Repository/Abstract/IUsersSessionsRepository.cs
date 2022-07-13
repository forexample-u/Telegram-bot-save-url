using App.Repository.Entities;

namespace App.Repository.Abstract
{
	public interface IUsersSessionsRepository
	{
		public Task<UserSession?> GetSessionByUserIdAsync(long userId);

		public Task AddSessionByUserIdAsync(long userId);

		public Task UpdateSessionByUserIdAsync(long userId, UserSession newSession);

		public Task DeleteSessionByUserIdAsync(long userId);
	}
}