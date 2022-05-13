using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Storage;

namespace App.Command
{
    public class CommandStoreLink : ICommand
    {
        private IChat chat;
        private IStorage storage;
        private string currentCategoria;
        private string messageWithUrls;

        public CommandStoreLink(IChat chat, IStorage storage)
        {
            this.chat = chat;
            this.storage = storage;
        }

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
                        storage.AddEntity(key: currentCategoria, value: url);
                        storage.AddEntity(key: "Все", value: url);
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
