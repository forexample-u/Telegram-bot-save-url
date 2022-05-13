using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Storage;

namespace App.Command
{
    public class CommandFactory
    {
        public ICommand CreateCommand(IChat chat, IStorage storage, string input)
        {
            if (input == "/store-link")
            {
                return new CommandStoreLink(chat, storage);
            }
            else if (input == "/get-links")
            {
                return new CommandGetLinks(chat, storage);
            }
            else
            {
                return new CommandStart(chat, storage);
            }
        }
    }
}
