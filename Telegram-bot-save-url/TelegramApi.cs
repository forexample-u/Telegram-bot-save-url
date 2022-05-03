using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;

namespace App.ApiMessager
{
    /// <summary>
    /// Подключение к API Telegram
    /// </summary>
    public static class TelegramApi
    {
        //TODO: DELETE TOKEN
        private static readonly string TOKEN = "5317155947:AAEQfFDilAgIOB6Z6j8Ki1MBsnmEcP4yQnY";
        private static ITelegramBotClient bot = new TelegramBotClient(TOKEN);
        private static Update updateMessage = new Update();
        private static bool isUserInput = false;
        private static bool isMenuInput = false;

        static TelegramApi()
        {
            Task.Run(() => AsyncExecute());
        }

        private static async Task AsyncExecute()
        {
            await Task.Run(() =>
            {
                var cts = new CancellationTokenSource();
                var cancellationToken = cts.Token;
                var receiverOptions = new ReceiverOptions()
                {
                    AllowedUpdates = { }, // receive all update types
                };
                bot.StartReceiving(
                    HandleInputUserAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken
                );
            });
        }

        //Handle
        private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await Task.Run(() => Console.WriteLine(JsonConvert.SerializeObject(exception)));
        }

        private static async Task HandleInputUserAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                updateMessage = update;
                isUserInput = true; //Пользователь ввёл что-то
                if (isMenuInput)
                {
                    isMenuInput = false;
                    var removeKeyboard = new ReplyKeyboardRemove();
                    await bot.SendTextMessageAsync(updateMessage.Message.Chat, "Принят ответ", replyMarkup: removeKeyboard);
                }
            }
        }

        public static async void SendMenu(string message, string[] buttons)
        {
            var buttonsList = new List<KeyboardButton>();
            foreach (var button in buttons)
            {
                var newbutton = new KeyboardButton(button);
                buttonsList.Add(newbutton);
            }

            var buttonsArray = buttonsList.ToArray();
            IReplyMarkup keyboard = new ReplyKeyboardMarkup(buttonsArray);

            isMenuInput = true;
            await bot.SendTextMessageAsync(updateMessage.Message.Chat, message, replyMarkup: keyboard);
        }

        public static void SendMessage(string message)
        {
            Task.Run(() => bot.SendTextMessageAsync(updateMessage.Message.Chat, message));
            Thread.Sleep(100);
        }

        public static string ReadMessage()
        {
            //TODO: убрать do while:
            //Это решение выполняет свою задачу, оно явлется безопасным (между ответами будет 1 секунда)
            //Но оно не является самым оптимальным, выходит, что у нас две ветки ждут, одна input (Read) и второй input (HandleInputUserAsync), только один input возврощает свой результат (string), а другая просто крутится всё время (он всех обслуживает её нельзя завершать), вопрос как дать отвественность за сон только одной ветки и выдать ответ другой, то-есть разбудить поток и выдать ему ответ...
            //Если узнаю тут подпишу, как правильней...
            do
            {
                Thread.Sleep(1000);
            }
            while (!isUserInput);

            isUserInput = false;

            return updateMessage.Message.Text;
        }
    }
}
