using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Message
{
    /// <summary>
    /// Любой месседжер должен реализовать свой тип сообщения
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Отправить одно или несколько сообщений пользователю
        /// </summary>
        /// <param name="message">Можно передать через запятую несколько сообщений</param>
        void Send(params string[] messages);

        /// <summary>
        /// Прочитать сообщение пользователя
        /// </summary>
        /// <returns></returns>
        string Read();

        /// <summary>
        /// Создать меню выбора пользователю
        /// </summary>
        /// <param name="message">Сообщение для меню</param>
        /// <param name="buttons">Кнопки</param>
        void Menu(string message, string[] buttons);
    }
}
