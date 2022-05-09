using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Storage;

namespace App.Command
{
    public class CommandHandler
    {
        private IChat chat;
        private IStorage storage;
        private string currentCategoria { get; set; }
        private CommandFactory commandFactory = new CommandFactory();

        /// <summary>
        /// Выбирает команду
        /// </summary>
        /// <param name="chat">кому пишмем сообщение</param>
        /// <param name="storage">Храним данные</param>
        public CommandHandler(IChat chat, IStorage storage)
        {
            this.chat = chat;
            this.storage = storage;
            storage.AddEntity(key: "Все", value: "");

            //start bot
            this.chat.Start();
            ICommand command;
            while (true)
            {
                this.chat.SendMessage("Введите /store-link или /get-links");
                string inputeCommand = this.chat.ReadMessage();
                switch (inputeCommand)
                {
                    case "/store-link":
                        command = commandFactory.CreateCommandStoreLinks(this.chat, this.storage);
                        command.Execute();
                        break;

                    case "/get-links":
                        command = commandFactory.CreateCommandGetLinks(this.chat, this.storage);
                        command.Execute();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
