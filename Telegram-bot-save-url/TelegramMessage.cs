using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Message;

namespace App.TelegramMessage
{
    /// <summary>
    /// Реализация отправка сообщения для телеграмма
    /// </summary>
    public class TelegramMessage : IMessage
    {
        public void Send(params string[] message);

        public string Read();

        public void Menu(string message, string[] buttons);
    }
}
