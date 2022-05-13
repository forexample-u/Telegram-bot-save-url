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
        private IChat _chat;
        private IStorage _storage;
        private CommandFactory _commandFactory = new CommandFactory();

        /// <summary>
        /// Выбирает команду
        /// </summary>
        /// <param name="chat">кому пишем сообщение</param>
        /// <param name="storage">Храним данные</param>
        public CommandHandler(IChat chat, IStorage storage)
        {
            _chat = chat;
            _storage = storage;
            _storage.AddEntity(key: "Все", value: string.Empty);

            _chat.Start();
            while (true)
            {
                string inputCommand = _chat.ReadMessage().Result;
                var command = _commandFactory.CreateCommand(_chat, _storage, inputCommand);
                command.Execute();
               
            }
        }
    }
}
