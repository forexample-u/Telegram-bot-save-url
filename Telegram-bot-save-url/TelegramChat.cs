using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.MessagerApi;

namespace App.Chat
{
    /// <summary>
    /// Реализация отправка сообщения для телеграмма
    /// </summary>
    public class TelegramBotApiChat : IChat
    {
        public void Start()
        {
            TelegramApi.Start();
        }

        public void Stop()
        {
            TelegramApi.Stop();
        }

        public void SendMessage(params string[] messages)
        {
            foreach (string message in messages)
            {
                Task.Run(() => TelegramApi.SendMessage(message));
                Thread.Sleep(100);
            }
        }

        public string ReadMessage()
        {
            string input = TelegramApi.ReadMessage().Result;
            return input;
        }

        public void SendMenuMessage(string message, string[] buttons)
        {
            Task.Run(() => TelegramApi.SendMenuMessage(message, buttons));
        }
    }
}
