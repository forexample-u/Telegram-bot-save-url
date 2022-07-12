﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository.Abstract;
using App.UserData;
using App.Repository.Entities;
using App.Repository.EntityFramework;

namespace App.Command
{
    public class CommandStoreLink : BaseCommand, ICommand
    {
        public CommandStoreLink(IUserData user, IChat chat, IBooksRepository repository) : base(user, chat, repository) { }

        public async Task<string> InsertCategory()
		{
            await chat.SendMessageAsync(user, "Впишите категорию");
            string currentCategoria = await chat.ReadUserMessageAsync(user);
            return currentCategoria;
        }

        public async Task<string> InsertUrl()
		{
            await chat.SendMessageAsync(user, "Впишите url, который нужно сохранить");
            string messageWithUrls = await chat.ReadUserMessageAsync(user);
            return messageWithUrls;
        }

        public async Task SendSuccessfulAddCategoryWithUrls(string currentCategoria, string messageWithUrls)
		{
            if (currentCategoria != "Все")
            {
                string[] urlInMessage = messageWithUrls.Split(" ");
                StringBuilder notUrls = new StringBuilder("");
                foreach (var url in urlInMessage)
                {
                    if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        await repository.AddBookWithUrlAsync(new Book() { UserId = user.Id, Categoria = currentCategoria, Url = url });
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
        }

        public async Task SendStartMessage()
		{
            await chat.SendMessageAsync(user, "Введите /store_link или /get_links");
        }

        public async Task ExecuteAsync()
        {
            string currentCategoria = await InsertCategory();
            string currentUrls = await InsertUrl();
            await SendSuccessfulAddCategoryWithUrls(currentCategoria, currentUrls);
            await SendStartMessage();
        }
    }
}