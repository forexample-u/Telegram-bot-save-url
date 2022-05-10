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
        /// <param name="chat">кому пишем сообщение</param>
        /// <param name="storage">Храним данные</param>
        public CommandHandler(IChat chat, IStorage storage)
        {
            this.chat = chat;
            this.storage = storage;
            storage.AddEntity(key: "Все", value: "");

            //start bot
            this.chat.Start();

            while (true)
            {
                this.chat.SendMessage("Введите /store-link или /get-links");
                string inputCommand = this.chat.ReadMessage();

                commandFactory.CreateCommand(input: inputCommand, name: "/store-link", run: new CommandStoreLink(chat, storage));
                commandFactory.CreateCommand(input: inputCommand, name: "/get-links",  run: new CommandGetLinks(chat, storage));
            }
        }
    }
}
