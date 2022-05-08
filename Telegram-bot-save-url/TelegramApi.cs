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

namespace App.MessagerApi
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
        private static bool isStart = false;

        public static void Start()
        {
            if (!isStart) //Защита на случай попытки запустить более одного раза
            {
                isStart = true;
                Task.Run(() => AsyncExecute());
            }
        }

        public static void Stop()
        {
            Task.Run(() => AsyncStop());
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

        private static async Task AsyncStop()
        {
            await bot.CloseAsync();
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


        public static async void SendMenuMessage(string message, string[] buttons)
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

        public static async Task SendMessage(string message)
        {
            await Task.Run(() => bot.SendTextMessageAsync(updateMessage.Message.Chat, message));
        }

        public static async Task<string> ReadMessage()
        {
            //Всё таки я узнал, чтобы убрать do while, нужно использовать web hock! Если будет вопрос оптимизации (эффективности), то можно избавится от двух циклов
            string userMessage = "";

            await Task.Run(() =>
            {
                do
                {
                    Thread.Sleep(1000);
                }
                while (!isUserInput);

                isUserInput = false;

                userMessage = updateMessage.Message.Text;
            });

            return userMessage;
        }
    }
}
