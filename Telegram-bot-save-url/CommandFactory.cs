using System;
using App.Chat;
using App.Repository.Abstract;
using App.UserData;

namespace App.Command
{
    public class CommandFactory
    {
        public ICommand CreateCommand(IUserData user, IChat chat, IBooksRepository booksRepository, IUsersSessionsRepository usersSessionRepository, string input)
        {
            if (input == "/store_link")
            {
                return new CommandStoreLink(user, chat, booksRepository, usersSessionRepository);
            }
            else if (input == "/get_links")
            {
                return new CommandGetLinks(user, chat, booksRepository, usersSessionRepository);
            }
            else if (input == "/start")
            {
                return new CommandStart(user, chat, booksRepository, usersSessionRepository);
            }
            else
            {
                return new CommandStart(user, chat, booksRepository, usersSessionRepository);
            }
        }
    }
}
