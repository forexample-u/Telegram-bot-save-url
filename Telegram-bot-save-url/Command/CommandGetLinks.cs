using System;
using System.Text;
using App.Chat;
using App.Repository.Entities;
using App.Repository.Abstract;
using App.UserData;

namespace App.Command
{
    public class CommandGetLinks : BaseCommand, ICommand
    {
        public CommandGetLinks(IUserData user, IChat chat, IBooksRepository repository, IUsersSessionsRepository usersSessionsRepository) : base(user, chat, repository, usersSessionsRepository) { }

        public async Task SendMenuCategoryAsync(List<Book> buttons)
		{
            await chat.SendMenuMessageAsync(user, "Выберите вашу сохранёную категорию", buttons.Select(x => x.Categoria).Distinct());
        }

        public async Task<string> SelectMenuCategory()
		{
            string currentCategoria = await chat.ReadUserMessageAsync(user);
            return currentCategoria;
        }

        public async Task SendStartMessage()
        {
            await chat.SendMessageAsync(user, "Введите /store_link или /get_links");
        }

        public async Task SendCurrentBooksInfo(string currentCategoria)
		{
            List<Book> listAll = await repository.GetBooksByCategoriaUserIdAsync(user.UserId, currentCategoria);
            if (currentCategoria == "Все")
            {
                listAll = await repository.GetBooksByUserIdAsync(user.UserId);
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
            UserSession userSession = await usersSessionsRepository.GetSessionByUserIdAsync(user.UserId);

            List<Book> buttons = await repository.GetBooksByUserIdAsync(user.UserId);
            if (buttons.Count != 0)
            {
                buttons.Add(new Book { Categoria = "Все" });
                
                if (userSession.PrintFirstComplete == null)
                {
                    await SendMenuCategoryAsync(buttons);
                    userSession.PrintFirstComplete = true;
                    usersSessionsRepository.UpdateSessionByUserIdAsync(userSession.UserId, userSession);
                }
                
                string currentCategoria = await SelectMenuCategory();

                if (userSession.PrintSecondComplete == null)
                {
                    await SendCurrentBooksInfo(currentCategoria);
                    userSession.PrintSecondComplete = true;
                    usersSessionsRepository.UpdateSessionByUserIdAsync(userSession.UserId, userSession);
                }
            }
            else
            {
                if (userSession.PrintFirstComplete == null)
                {
                    await chat.SendMessageAsync(user, "У вас не одной сохранённой ссылки!");
                    userSession.PrintFirstComplete = true;
                    usersSessionsRepository.UpdateSessionByUserIdAsync(userSession.UserId, userSession);
                }
            }
            await SendStartMessage();
        }
    }
}