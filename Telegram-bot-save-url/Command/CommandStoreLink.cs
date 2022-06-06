using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository;
using App.UserData;

namespace App.Command
{
    public class CommandStoreLink : BaseCommand, ICommand
    {
        public CommandStoreLink(IUserData user, IChat chat, IRepositoryDictionary<string, string> repository) : base(user, chat, repository) { }

        private string currentCategoria;
        private string messageWithUrls;

        public async Task ExecuteAsync()
        {
            await chat.SendMessageAsync(user, "Впишите категорию");
            currentCategoria = await chat.ReadUserMessageAsync(user);

            await chat.SendMessageAsync(user, "Впишите url, который нужно сохранить");
            messageWithUrls = await chat.ReadUserMessageAsync(user);

            if (currentCategoria != "Все")
            {
                string[] urlInMessage = messageWithUrls.Split(" ");
                StringBuilder notUrls = new StringBuilder("");
                foreach (var url in urlInMessage)
                {
                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        repository.Add(currentCategoria, url);
                        repository.Add("Все", url);
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
            await Task.Delay(100);
            await chat.SendMessageAsync(user, "Введите /store_link или /get_links");
        }
    }
}