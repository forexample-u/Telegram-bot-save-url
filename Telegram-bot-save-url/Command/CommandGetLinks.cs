using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository.Entities;
using App.Repository.Abstract;
using App.UserData;

namespace App.Command
{
    public class CommandGetLinks : BaseCommand, ICommand
    {
        public CommandGetLinks(IUserData user, IChat chat, IBooksRepository repository) : base(user, chat, repository) { }

        public async Task<string> SelectCategory(List<Book> buttons)
		{
            await chat.SendMenuMessageAsync(user, "Выберите вашу сохранёную категорию", buttons.Select(x => x.Categoria));
            string currentCategoria = await chat.ReadUserMessageAsync(user);
            return currentCategoria;
        }

        public async Task SendStartMessage()
        {
            await chat.SendMessageAsync(user, "Введите /store_link или /get_links");
        }

        public async Task SendCurrentBooksInfo(string currentCategoria)
		{
            List<Book> listAll = await repository.GetBooksByCategoriaUserIdAsync(user.Id, currentCategoria);
            if (currentCategoria == "Все")
            {
                listAll = await repository.GetBooksByUserIdAsync(user.Id);
            }

            if (listAll.Count != 0)
            {
                string allUrls = string.Join("\n", listAll.Select(x => x.Url));
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

        public async Task ExecuteAsync()
        {
            List<Book> buttons = await repository.GetBooksByUserIdAsync(user.Id);
            if (buttons.Count != 0)
            {
                buttons.Add(new Book { Categoria = "Все" });

                string currentCategoria = await SelectCategory(buttons);
                await SendCurrentBooksInfo(currentCategoria);
            }
            else
            {
                await chat.SendMessageAsync(user, "У вас не одной сохранённой ссылки!");
            }
            await SendStartMessage();
        }
    }
}