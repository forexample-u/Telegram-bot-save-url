using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository;

namespace App.Command
{
    public class CommandHandler
    {
        private IChat _chat;
        private IRepository<string, string> urlRepository;
        private CommandFactory _commandFactory = new CommandFactory();

        /// <summary>
        /// Выбирает команду
        /// </summary>
        /// <param name="chat">кому пишем сообщение</param>
        /// <param name="repository">Храним данные</param>
        public CommandHandler(IChat chat, IRepository<string, string> repository)
        {
            _chat = chat;
            urlRepository = repository;
            urlRepository.Add("Все", string.Empty);

            _chat.Start();
            while (true)
            {
                string inputCommand = _chat.ReadMessage().Result;
                var command = _commandFactory.CreateCommand(_chat, urlRepository, inputCommand);
                command.Execute();
            }
        }
    }
}
