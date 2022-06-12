using System;
using App.Chat;
using App.Repository.Abstract;
using App.Repository.EntityFramework;
using App.UserData;

namespace App.Command
{
    public class CommandHandler
    {
        private IChat userChat;
        private List<long> clients = new List<long>();

        public CommandHandler(IChat chat, IBooksRepository booksRepository)
        {
            userChat = chat;
            userChat.StartConnectionAsync();
            while (true)
            {
                IUserData userData = userChat.ReadAnyMessageAsync().Result;
                if (!clients.Contains(userData.Id))
                {
                    clients.Add(userData.Id);
                    Task.Run(() => Start(userData, booksRepository));
                    Thread.Sleep(100);
                }
            }
        }

        private async Task Start(IUserData userData, IBooksRepository booksRepository)
        {
            string inputCommand = await userChat.ReadUserMessageAsync(userData);

            //TODO: ссылка на чат не нужна! В фабрики
            CommandFactory commandFactory = new CommandFactory();
            ICommand command = commandFactory.CreateCommand(userData, userChat, booksRepository, inputCommand);
            await command.ExecuteAsync();
            clients.Remove(userData.Id);
        }
    }
}