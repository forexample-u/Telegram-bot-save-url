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
        private List<long> clients = new List<long>();

        public CommandHandler(IChat chat, 
            IBooksRepository booksRepository,
            IUsersRepository usersRepository)
        {
            this.chat = chat;
            this.booksRepository = booksRepository;
            this.usersRepository = usersRepository;
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
            string inputCommand = await chat.ReadUserMessageAsync(userData);
            User isExistUser = await usersRepository.GetUserByUserIdAsync(userData.UserId);

            if (isExistUser == null)
            {
                var newUser = new User()
                {
                    UserId = userData.UserId,
                    FirstName = userData.FirstName,
                    SecondName = userData.SecondName,
                    Username = userData.Username,
                };
                usersRepository.AddUserAsync(newUser);
            }
            
            CommandFactory commandFactory = new CommandFactory();
            ICommand command = commandFactory.CreateCommand(userData, chat, booksRepository, inputCommand);
            await command.ExecuteAsync();
            clients.Remove(userData.UserId);
        }
    }
}