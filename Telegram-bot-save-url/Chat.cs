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
        Task SendMessage(params string[] messages);

        /// <summary>
        /// Прочитать сообщение пользователя
        /// </summary>
        /// <returns></returns>
        Task<string> ReadMessage();

        /// <summary>
        /// Создать меню выбора пользователю
        /// </summary>
        /// <param name="message">Сообщение для меню</param>
        /// <param name="buttons">Кнопки</param>
        Task SendMenuMessage(string message, string[] buttons);

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
