using System;
using App.UserData;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;

namespace App.Chat
{
    public class TelegramBotApiChat : IChat
    {
        private ITelegramBotClient bot;
        private Update updateUser = new Update();
        private UserTelegramData newUserData;
        private bool isUserInput = false;
        private bool isMenuInput = false;
        private bool isAnyInput = false;

        public TelegramBotApiChat(string TOKEN)
        {
            bot = new TelegramBotClient(TOKEN);
        }

        public async Task StartConnectionAsync()
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

        public async Task StopConnectionAsync()
        {
            await bot.CloseAsync();
        }

        //Handle
        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await Task.Run(() => Console.WriteLine(JsonConvert.SerializeObject(exception)));
        }

        private async Task HandleInputUserAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                updateUser = update;
                newUserData = new UserTelegramData(
                    id: updateUser.Message.Chat.Id,
                    firstName: updateUser.Message.Chat.FirstName,
                    secondName: updateUser.Message.Chat.LastName,
                    username: updateUser.Message.Chat.Username
                );
                newUserData.LastMessage = updateUser.Message.Text;

                isUserInput = true; //Пользователь ввёл что-то
                isAnyInput = true;
                if (isMenuInput)
                {
                    isMenuInput = false;
                    var removeKeyboard = new ReplyKeyboardRemove();
                    await bot.SendTextMessageAsync(newUserData.Id, "Принят ответ", replyMarkup: removeKeyboard);
                }
            }
        }

        public async Task SendMenuMessageAsync(IUserData user, string message, IEnumerable<string> buttons)
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
            await bot.SendTextMessageAsync(user.Id, message, replyMarkup: keyboard);
        }

        public async Task SendMessageAsync(IUserData user, params string[] messages)
        {
            foreach (string message in messages)
            {
                await bot.SendTextMessageAsync(user.Id, message);
                await Task.Delay(100);
            }
        }

        public async Task<string> ReadUserMessageAsync(IUserData user)
        {
            await Task.Run(() =>
            {
                do
                {
                    Thread.Sleep(100);
                }
                while (!(isUserInput && (newUserData.Id == user.Id)));
                isUserInput = false;
            });
            return updateUser.Message.Text;
        }

        public async Task<IUserData> ReadAnyMessageAsync()
        {
            await Task.Run(() =>
            {
                do
                {
                    Thread.Sleep(100);
                }
                while (!isAnyInput);
                isAnyInput = false;
            });

            IUserData user = new UserTelegramData(
                id: updateUser.Message.Chat.Id,
                firstName: updateUser.Message.Chat.FirstName,
                secondName: updateUser.Message.Chat.LastName,
                username: updateUser.Message.Chat.Username
            );
            user.LastMessage = updateUser.Message.Text;
            return user;
        }
    }
}