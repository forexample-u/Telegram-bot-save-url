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
        public ICommand CreateCommandGetLinks(IChat chat, IStorage storage)
        {
            return new CommandGetLinks(chat, storage);
        }

        public ICommand CreateCommandStoreLinks(IChat chat, IStorage storage)
        {
            return new CommandStoreLink(chat, storage);
        }
    }
}
