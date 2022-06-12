using System;
using App.UserData;

namespace App.Chat
{
    /// <summary>
    /// Любой месседжер должен реализовать свой тип сообщения
    /// </summary>
    public interface IChat
    {
        /// <summary>
        /// Начать
        /// </summary>
        Task StartConnectionAsync();

        /// <summary>
        /// Завершить
        /// </summary>
        Task StopConnectionAsync();

        /// <summary>
        /// Отправить одно или несколько сообщений пользователю
        /// </summary>
        /// <param name="message">Можно передать через запятую несколько сообщений</param>
        Task SendMessageAsync(IUserData user, params string[] messages);

        /// <summary>
        /// Прочитать сообщение конкретного пользователя
        /// </summary>
        /// <returns></returns>
        Task<string> ReadUserMessageAsync(IUserData user);

        /// <summary>
        /// Прочитать любые сообщения с целью получить пользователя
        /// </summary>
        /// <returns></returns>
        Task<IUserData> ReadAnyMessageAsync();

        /// <summary>
        /// Создать меню выбора пользователю
        /// </summary>
        /// <param name="message">Сообщение для меню</param>
        /// <param name="buttons">Кнопки</param>
        Task SendMenuMessageAsync(IUserData user, string message, IEnumerable<string> buttons);
    }
}