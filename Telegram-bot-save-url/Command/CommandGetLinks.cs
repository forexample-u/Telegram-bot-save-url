using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository;
using App.UserData;

namespace App.Command
{
    public class CommandGetLinks : BaseCommand, ICommand
    {
        public CommandGetLinks(IUserData user, IChat chat, IRepositoryDictionary<string, string> repository) : base(user, chat, repository) { }

        private string currentCategoria;
        public async Task ExecuteAsync()
        {
            string[] buttons = repository.GetKeys().ToArray();
            if (buttons.Length != 0)
            {
                await chat.SendMenuMessageAsync(user, "Выберите вашу сохранёную категорию", buttons);
                currentCategoria = await chat.ReadUserMessageAsync(user);
                await Task.Delay(100);

                List<string> listAll = repository.GetByKey(currentCategoria).ToList();
                if (listAll.Count != 0)
                {
                    string allUrls = string.Join("\n", listAll);
                    if (allUrls.Length != 0)
                    {
                        await chat.SendMessageAsync(user, $"Категория: {currentCategoria}\nВключает:\n{allUrls}");
                    }
                    else
                    {
                        await chat.SendMessageAsync(user, $"Категория: \"{currentCategoria}\" - пустая");
                    }
                }
                else
                {
                    await chat.SendMessageAsync(user, $"Категория: \"{currentCategoria}\" - не существует");
                }
            }
            else
            {
                await chat.SendMessageAsync(user, "У вас не одной сохранённой ссылки!");
            }
            await Task.Delay(100);
            await chat.SendMessageAsync(user, "Введите /store_link или /get_links");
        }
    }
}