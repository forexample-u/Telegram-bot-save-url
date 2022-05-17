using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository;

namespace App.Command
{
    public class CommandStoreLink : BaseCommand, ICommand
    {
        public CommandStoreLink(IChat chat, IRepository<string, string> repository) : base(chat, repository) { }

        private string currentCategoria;
        private string messageWithUrls;

        public void Execute()
        {
            SelectCategoriaByInput().Wait();
            SelectUrlsByInput().Wait();
            Task.Run(() => SaveUrlsInCategoria());
        }

        private async Task SelectCategoriaByInput()
        {
            await chat.SendMessage("Впишите категорию");
            currentCategoria = await chat.ReadMessage();
        }

        private async Task SelectUrlsByInput()
        {
            await chat.SendMessage("Впишите url, который нужно сохранить");
            messageWithUrls = await chat.ReadMessage();
        }

        private async Task SaveUrlsInCategoria()
        {
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
                if(finalMessage == string.Empty)
                {
                    finalMessage = "Успешно добавлено!";
                }

                await chat.SendMessage(finalMessage.ToString());
            }
            else
            {
                await chat.SendMessage("Категория - \"Все\", является зарезервированным, вы не можете его использовать");
            }
        }
    }
}
