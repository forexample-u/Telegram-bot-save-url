using App.Chat;
using App.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using App.Repository;
using App.Repository.Abstract;
using App.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace App.Assistent
{
    public class Application
    {
        private DbContextOptions<AppDbContext> PostgreSqlConnectionStart(string connectionString)
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
            optionsBuilder.UseNpgsql(connectionString);
            return optionsBuilder.Options;
        }

        public void Run()
        {
            //database settings
            string databaseTextJson = File.ReadAllText("appsettings.json");
            JObject databaseSettings = JObject.Parse(databaseTextJson);
            string connectionString = databaseSettings["ConnectionStrings"]["DefaultConnection"].ToString();

            var database = PostgreSqlConnectionStart(connectionString);
            var context = new AppDbContext(database);
            IBooksRepository booksRepository = new EFBooksRepository(context);
            IUsersRepository usersRepository = new EFUsersRepository(context);

            //telegram settings
            string telegramTextJson = File.ReadAllText("telegram-settings.json");
            JObject telegramSettings = JObject.Parse(telegramTextJson);
            string telegramToken = telegramSettings["bots"]["book_url"]["TOKEN"].ToString();

            IChat chat = new TelegramBotApiChat(telegramToken);
            CommandHandler commandHandler = new CommandHandler(chat, booksRepository, usersRepository);
            commandHandler.Start();
        }
    }
}