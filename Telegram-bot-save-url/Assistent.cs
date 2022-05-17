using App.Chat;
using App.Repository;
using App.Command;

namespace App.Assistent
{
    public class Application
    {
        public void Run()
        {
            IChat chat = new TelegramBotApiChat();
            IRepository<string, string> repository = new UrlsRepository();

            CommandHandler commandHandler = new CommandHandler(chat, repository);
        }
    }
}
