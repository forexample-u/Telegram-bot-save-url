using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository;

namespace App.Command
{
    public class CommandFactory
    {
        public ICommand CreateCommand(IChat chat, IRepository<string, string> repository, string input)
        {
            if (input == "/store-link")
            {
                return new CommandStoreLink(chat, repository);
            }
            else if (input == "/get-links")
            {
                return new CommandGetLinks(chat, repository);
            }
            else
            {
                return new CommandStart(chat, repository);
            }
        }
    }
}
