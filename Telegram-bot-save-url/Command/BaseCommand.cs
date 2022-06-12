using System;
using App.Chat;
using App.Repository.Abstract;
using App.UserData;

namespace App.Command
{
    public abstract class BaseCommand
    {
        protected IChat chat;
        protected IBooksRepository repository;
        protected IUserData user;

        public BaseCommand(IUserData user, IChat chat, IBooksRepository repository)
        {
            this.chat = chat;
            this.repository = repository;
            this.user = user;
        }
    }
}
