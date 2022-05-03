using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Message;
using App.TelegramMessage;

namespace App.Assistent
{
    /// <summary>
    /// Ассистент
    /// </summary>
    public class Assistent
    {
        public IMessage message;

        /// <summary>
        /// В Конструктор передать интерфейсный месседже
        /// </summary>
        /// <param name="newMessage">Можно передать любой реализованый месседже</param>
        public Assistent(IMessage newMessage)
        {
            message = newMessage;
        }

        /// <summary>
        /// Запустить ассистента
        /// </summary>
        public void Start()
        {
            message.Read(); //start
            while (true)
            {
                message.Send("Введите /store-link или /get-links");
                string inputeCommand = message.Read();
                switch (inputeCommand)
                {
                    case "/store-link":
                        Console.WriteLine($"Пользователь ввёл - {inputeCommand}");
                        break;

                    case "/get-links":

                        break;

                    default:
                        Console.WriteLine($"Пользователь ввёл - {inputeCommand}");
                        break;
                }
            }
        }
    }
}
