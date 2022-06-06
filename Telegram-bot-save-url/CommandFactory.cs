using System;
using App.Chat;
using App.Repository;
using App.UserData;

namespace App.Command
{
    public class CommandFactory
    {
        public ICommand CreateCommand(IUserData user, IChat chat, IRepositoryDictionary<string, string> repository, string input)
        {
            if (input == "/store_link")
            {
                return new CommandStoreLink(user, chat, repository);
            }
            else if (input == "/get_links")
            {
                return new CommandGetLinks(user, chat, repository);
            }
            else if (input == "/start")
            {
                return new CommandStart(user, chat, repository);
            }
            else
            {
                return new CommandStart(user, chat, repository);
            }
        }
    }
}
