using System;
using App.Chat;
using App.Repository;
using App.UserData;

namespace App.Command
{
    public class CommandHandler
    {
        private IChat userChat;
        private Dictionary<long, UrlsRepository> urlsRepository = new Dictionary<long, UrlsRepository>();
        private List<long> clients = new List<long>();

        public CommandHandler(IChat chat)
        {
            userChat = chat;
            userChat.StartConnection();
            while (true)
            {
                IUserData userData = userChat.ReadAnyMessageAsync().Result;
                if (!clients.Contains(userData.Id))
                {
                    clients.Add(userData.Id);
                    Task.Run(() => Start(userData));
                    Thread.Sleep(100);
                }
            }
        }

        private async Task Start(IUserData userData)
        {
            string inputCommand = await userChat.ReadUserMessageAsync(userData);

            if (!urlsRepository.ContainsKey(userData.Id))
            {
                urlsRepository.Add(userData.Id, new UrlsRepository());
            }

            CommandFactory commandFactory = new CommandFactory();
            ICommand command = commandFactory.CreateCommand(userData, userChat, urlsRepository[userData.Id], inputCommand);
            await command.ExecuteAsync();
            clients.Remove(userData.Id);
        }
    }
}