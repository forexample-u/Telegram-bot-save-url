using System;
using App.Chat;
using App.Repository;
using App.UserData;

namespace App.Command
{
    public abstract class BaseCommand
    {
        protected IChat chat;
        protected IRepositoryDictionary<string, string> repository;
        protected IUserData user;

        public BaseCommand(IUserData user, IChat chat, IRepositoryDictionary<string, string> repository)
        {
            this.chat = chat;
            this.repository = repository;
            this.user = user;
        }
    }
}
