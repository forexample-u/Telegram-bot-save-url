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

        public async Task SendMessage(params string[] messages)
        {
            foreach (string message in messages)
            {
                await TelegramApi.SendMessage(message);
                Thread.Sleep(100);
            }
        }

        public async Task<string> ReadMessage()
        {
            string input = await TelegramApi.ReadMessage();
            return input;
        }

        public async Task SendMenuMessage(string message, string[] buttons)
        {
            await TelegramApi.SendMenuMessage(message, buttons);
        }
    }
}
