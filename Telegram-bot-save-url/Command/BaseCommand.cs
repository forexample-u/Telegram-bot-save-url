using System;
using App.Chat;
using App.Repository.Abstract;
using App.UserData;

namespace App.Command
{
    public abstract class BaseCommand
    {
        protected readonly IChat chat;
        protected readonly IBooksRepository repository;
        protected readonly IUserData user;
        protected readonly IUsersSessionsRepository usersSessionsRepository;

        public BaseCommand(IUserData user, IChat chat, IBooksRepository repository, IUsersSessionsRepository usersSessionsRepository)
        {
            this.chat = chat;
            this.repository = repository;
            this.user = user;
            this.usersSessionsRepository = usersSessionsRepository;
        }
    }
}