using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.ApiMessager;
using App.Message;

namespace App.TelegramMessage
{
    /// <summary>
    /// Реализация отправка сообщения для телеграмма
    /// </summary>
    public class TelegramMessage : IMessage
    {
        public void Send(params string[] messages)
        {
            foreach (string message in messages)
            {
                TelegramApi.SendMessage(message);
            }
        }

        public string Read()
        {
            string input = TelegramApi.ReadMessage();
            return input;
        }

        public void Menu(string message, string[] buttons)
        {
            TelegramApi.SendMenu(message, buttons);
        }
    }
}
