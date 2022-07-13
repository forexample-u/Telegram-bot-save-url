using System;
using System.Text;
using App.Chat;
using App.Repository.Abstract;
using App.UserData;

namespace App.Command
{
    public class CommandStart : BaseCommand, ICommand
    {
        public CommandStart(IUserData user, IChat chat, IBooksRepository repository, IUsersSessionsRepository usersSessionsRepository) : base(user, chat, repository, usersSessionsRepository) { }

        public async Task ExecuteAsync()
        {
            await chat.SendMessageAsync(user, "Введите /store_link или /get_links");
        }
    }
}