using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Chat
{
    /// <summary>
    /// Любой месседжер должен реализовать свой тип сообщения
    /// </summary>
    public interface IChat
    {
        /// <summary>
        /// Отправить одно или несколько сообщений пользователю
        /// </summary>
        /// <param name="message">Можно передать через запятую несколько сообщений</param>
        void SendMessage(params string[] messages);

        /// <summary>
        /// Прочитать сообщение пользователя
        /// </summary>
        /// <returns></returns>
        string ReadMessage();

        /// <summary>
        /// Создать меню выбора пользователю
        /// </summary>
        /// <param name="message">Сообщение для меню</param>
        /// <param name="buttons">Кнопки</param>
        void SendMenuMessage(string message, string[] buttons);

        /// <summary>
        /// Начать
        /// </summary>
        void Start();

        /// <summary>
        /// Завершить
        /// </summary>
        void Stop();
    }
}
