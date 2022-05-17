using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository;

namespace App.Command
{
    public class CommandGetLinks : BaseCommand, ICommand
    {
        public CommandGetLinks(IChat chat, IRepository<string, string> repository) : base(chat, repository) { }

        private string currentCategoria;
        public void Execute()
        {
            SelectCategoriaByMenu().Wait();
            Task.Run(() => PrintByCurrentCategoria());
        }

        private async Task SelectCategoriaByMenu()
        {
            string[] buttons = repository.GetKeys().ToArray();
            await chat.SendMenuMessage("Выберите вашу сохранёную категорию", buttons);
            currentCategoria = await chat.ReadMessage();
        }

        private async Task PrintByCurrentCategoria()
        {
            List<string> listAll = repository.GetByKey(currentCategoria).ToList();
            if (listAll.Count != 0)
            {
                string allUrls = string.Join("\n", listAll);
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
