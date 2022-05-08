using App.Chat;
using App.Storage;
using App.Command;

namespace App.Assistent
{
    public class Application
    {
        public void Run()
        {
            IChat chat = new TelegramBotApiChat();
            IStorage storage = new UrlStorage();

            CommandHandler commandHandler = new CommandHandler(chat, storage);
        }
    }
}
