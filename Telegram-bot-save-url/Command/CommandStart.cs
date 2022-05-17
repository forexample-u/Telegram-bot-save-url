using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository;

namespace App.Command
{
    public class CommandStart : BaseCommand, ICommand
    {
        public CommandStart(IChat chat, IRepository<string, string> repository) : base(chat, repository) { }

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
