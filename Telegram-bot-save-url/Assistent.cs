using App.Chat;
using App.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Assistent
{
    public class Application
    {
        public void Run()
        {
            string telegramPathJson = @"telegram-settings.json";
            string telegramTextJson = File.ReadAllText(telegramPathJson);
            JObject telegramSettings = JObject.Parse(telegramTextJson);
            string telegramToken = telegramSettings["bots"]["book_url"]["TOKEN"].ToString();

            IChat chat = new TelegramBotApiChat(telegramToken);
            CommandHandler commandHandler = new CommandHandler(chat);
        }
    }
}
