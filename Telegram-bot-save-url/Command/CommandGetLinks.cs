using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Storage;

namespace App.Command
{
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
            SelectCategoriaByMenu().Wait();
            Task.Run(() => PrintByCurrentCategoria());
        }

        private async Task SelectCategoriaByMenu()
        {
            await chat.SendMenuMessage("Выберите вашу сохранёную категорию", storage.GetEntityKeys().ToArray());
            currentCategoria = await chat.ReadMessage();
        }

        private async Task PrintByCurrentCategoria()
        {
            List<string> listAllUrls = storage.GetEntityByKeys(currentCategoria);
            if (listAllUrls.Count != 0)
            {
                string allUrls = string.Join("\n", listAllUrls.ToArray());
                if (allUrls.Length != 0)
                {
                    await chat.SendMessage($"Категория: {currentCategoria}\nВключает:\n{allUrls}");
                }
                else
                {
                    await chat.SendMessage($"Категория: \"{currentCategoria}\" - пустая");
                }
            }
            else
            {
                await chat.SendMessage($"Категория: \"{currentCategoria}\" - не существует");
            }
        }
    }
}
