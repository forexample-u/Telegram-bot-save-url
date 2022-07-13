using System;
using App.Chat;
using App.Repository.Abstract;
using App.Repository.Entities;
using App.Repository.EntityFramework;
using App.UserData;

namespace App.Command
{
    public class CommandHandler
    {
        private readonly IChat chat;
        private readonly IBooksRepository booksRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IUsersSessionsRepository usersSessionRepository;
        private List<long> clients = new List<long>();

        public CommandHandler(IChat chat,
			IBooksRepository booksRepository,
			IUsersRepository usersRepository,
			IUsersSessionsRepository usersSessionRepository)
		{
			this.chat = chat;
			this.booksRepository = booksRepository;
			this.usersRepository = usersRepository;
			this.usersSessionRepository = usersSessionRepository;
            chat.StartConnectionAsync();
        }

		public void Start()
		{
            while (true)
            {
                IUserData userData = chat.ReadAnyMessageAsync().Result;
                if (!clients.Contains(userData.UserId))
                {
                    clients.Add(userData.UserId);
                    Task.Run(() => EventUserMessage(userData));
                    Thread.Sleep(100);
                }
            }
        }

        private async Task EventUserMessage(IUserData userData)
        {
            UserSession session = await usersSessionRepository.GetSessionByUserIdAsync(userData.UserId);
            if (session == null)
            {
                await usersSessionRepository.AddSessionByUserIdAsync(userData.UserId);
                session = await usersSessionRepository.GetSessionByUserIdAsync(userData.UserId);
            }

            string inputCommand = string.Empty;
            if (session.InputComplete == null)
			{
                inputCommand = await chat.ReadUserMessageAsync(userData);
                await usersSessionRepository.UpdateSessionByUserIdAsync(userData.UserId, new UserSession() { UserId = userData.UserId, InputComplete = inputCommand });
            }
			else
			{
                inputCommand = session.InputComplete;
			}

            User user = await usersRepository.GetUserByUserIdAsync(userData.UserId);
            if (user == null)
            {
                var newUser = new User()
                {
                    UserId = userData.UserId,
                    FirstName = userData.FirstName,
                    SecondName = userData.SecondName,
                    Username = userData.Username,
                };
                await usersRepository.AddUserAsync(newUser);
            }

            CommandFactory commandFactory = new CommandFactory();
            ICommand command = commandFactory.CreateCommand(userData, chat, booksRepository, usersSessionRepository, inputCommand);
            await command.ExecuteAsync();
            await usersSessionRepository.DeleteSessionByUserIdAsync(userData.UserId);
            clients.Remove(userData.UserId);
        }
    }
}