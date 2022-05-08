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
    public class CommandGetLinks : ICommand
    {
        private IChat chat;
        private IStorage storage;
        private string currentCategoria { get; set; }

        public CommandGetLinks(IChat chat, IStorage storage)
        {
            this.chat = chat;
            this.storage = storage;
        }

        public void Execute()
        {
            SelectCategoriaByMenu();
            PrintByCurrentCategoria();
        }

        private void SelectCategoriaByMenu()
        {
            chat.SendMenuMessage("Выберите вашу сохранёную категорию", storage.GetEntityKeys().ToArray());
            currentCategoria = chat.ReadMessage();
        }

        private void PrintByCurrentCategoria()
        {
            List<string> listAllUrls = storage.GetEntityByKeys(currentCategoria);
            if (listAllUrls.Count != 0)
            {
                string allUrls = string.Join("\n", listAllUrls.ToArray());
                if (allUrls.Length != 0)
                {
                    chat.SendMessage($"Категория: {currentCategoria}\nВключает:\n{allUrls}");
                }
                else
                {
                    chat.SendMessage($"Категория: \"{currentCategoria}\" - пустая");
                }
            }
            else
            {
                chat.SendMessage($"Категория: \"{currentCategoria}\" - не существует");
            }
        }
    }
}
