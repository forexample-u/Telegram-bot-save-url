using System;
using System.Text;
using App.Chat;
using App.Repository.Abstract;
using App.UserData;
using App.Repository.Entities;
using App.Repository.EntityFramework;

namespace App.Command
{
    public class CommandStoreLink : BaseCommand, ICommand
    {
        public CommandStoreLink(IUserData user, IChat chat, IBooksRepository repository, IUsersSessionsRepository usersSessionsRepository) : base(user, chat, repository, usersSessionsRepository) { }


        public async Task SendMessageCategory()
		{
            await chat.SendMessageAsync(user, "Впишите категорию");
        }

        public async Task<string?> InsertCategory()
		{
            string? currentCategoria = await chat.ReadUserMessageAsync(user);
            return currentCategoria;
        }

        public async Task SendMessageUrl()
		{
            await chat.SendMessageAsync(user, "Впишите url, который нужно сохранить");
        }

        public async Task<string> InsertUrl()
		{
            string messageWithUrls = await chat.ReadUserMessageAsync(user);
            return messageWithUrls;
        }

        public async Task SendSuccessfulAddCategoryWithUrls(string currentCategoria, string messageWithUrls)
		{
            if (currentCategoria != "Все")
            {
                string[] urlInMessage = messageWithUrls.Split(" ");
                StringBuilder notUrls = new StringBuilder("");
                foreach (var url in urlInMessage)
                {
                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        await repository.AddBookWithUrlAsync(new Book() { UserId = user.UserId, Categoria = currentCategoria, Url = url });
                    }
                    else
                    {
                        notUrls.Append($"{url} - не является url\n");
                    }
                }

                string finalMessage = notUrls.ToString();
                if (finalMessage == string.Empty)
                {
                    finalMessage = "Успешно добавлено!";
                }

                await chat.SendMessageAsync(user, finalMessage.ToString());
            }
            else
            {
                await chat.SendMessageAsync(user, "Категория - \"Все\", является зарезервированным, вы не можете его использовать");
            }
        }

        public async Task SendStartMessage()
		{
            await chat.SendMessageAsync(user, "Введите /store_link или /get_links");
        }

        public async Task ExecuteAsync()
        {
            UserSession userSession = await usersSessionsRepository.GetSessionByUserIdAsync(user.UserId);
            if (userSession?.PrintFirstComplete == null)
            {
                await SendMessageCategory();
                userSession.PrintFirstComplete = true;
                usersSessionsRepository.UpdateSessionByUserIdAsync(userSession.UserId, userSession);
            }

            string? currentCategoria = string.Empty;
            if (userSession.CategoryComplete == null)
            {
                currentCategoria = await InsertCategory();
                userSession.CategoryComplete = currentCategoria;
                usersSessionsRepository.UpdateSessionByUserIdAsync(userSession.UserId, userSession);
            }
			else
			{
                currentCategoria = userSession.CategoryComplete;
            }


            if (userSession?.PrintSecondComplete == null)
            {
                await SendMessageUrl();
                userSession.PrintSecondComplete = true;
                usersSessionsRepository.UpdateSessionByUserIdAsync(userSession.UserId, userSession);
            }

            string currentUrls = await InsertUrl();
            await SendSuccessfulAddCategoryWithUrls(currentCategoria, currentUrls);
            await SendStartMessage();
        }
    }
}