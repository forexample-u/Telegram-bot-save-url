using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Storage;

namespace App.Command
{
    public class CommandStart : ICommand
    {
        private IChat chat;
        private IStorage storage;
        private string currentCategoria { get; set; }

        public CommandStart(IChat chat, IStorage storage)
        {
            this.chat = chat;
            this.storage = storage;
        }

        public void Execute()
        {
            Task.Run(() => SendStartMessage());
        }

        public async Task SendStartMessage()
        {
            await chat.SendMessage("Введите /store-link или /get-links");
        }
    }
}
