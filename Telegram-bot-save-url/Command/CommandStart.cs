using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository.Abstract;
using App.UserData;

namespace App.Command
{
    public class CommandStart : BaseCommand, ICommand
    {
        public CommandStart(IUserData user, IChat chat, IBooksRepository repository) : base(user, chat, repository) { }

        public async Task ExecuteAsync()
        {
            await chat.SendMessageAsync(user, "Введите /store_link или /get_links");
        }
    }
}
