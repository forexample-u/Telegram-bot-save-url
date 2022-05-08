using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Storage;

namespace App.Command
{
    //TODO: нарушаем DRY, нужно сделать абстрактный класс для StoreLink и GetLinks
    public class CommandStoreLink : ICommand
    {
        private IChat chat;
        private IStorage storage;
        private string currentCategoria { get; set; }

        public CommandStoreLink(IChat chat, IStorage storage)
        {
            this.chat = chat;
            this.storage = storage;
        }

        public void Execute()
        {
            SelectCategoriaByInput();
            SaveUrlsInCategoria();
        }

        private void SelectCategoriaByInput()
        {
            chat.SendMessage("Впишите категорию");
            currentCategoria = chat.ReadMessage();
        }

        private void SaveUrlsInCategoria()
        {
            if (currentCategoria != "Все")
            {
                chat.SendMessage("Впишите url, который нужно сохранить");
                string messageWithUrls = chat.ReadMessage();

                string[] urlInMessage = messageWithUrls.Split(" ");
                StringBuilder notUrls = new StringBuilder("");
                foreach (var url in urlInMessage)
                {
                    if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                    {
                        storage.AddEntity(key: currentCategoria, value: url);
                        storage.AddEntity(key: "Все", value: url);
                    }
                    else
                    {
                        notUrls.Append($"{url} - не является url\n");
                    }
                }
                chat.SendMessage(notUrls.ToString());
            }
            else
            {
                chat.SendMessage("Категория - \"Все\", является зарезервированным, вы не можете его использовать");
            }
        }
    }
}
