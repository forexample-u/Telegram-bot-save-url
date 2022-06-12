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
        private DbContextOptions<AppDbContext> MySqlConnectionStart(string connectionString)
        {
            DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 11))
            );
            Console.WriteLine($"ConnectionString: {connectionString}");
            return optionsBuilder.Options;
        }

        public void Run()
        {
            //database settings
            string databaseTextJson = File.ReadAllText("appsettings.json");
            JObject databaseSettings = JObject.Parse(databaseTextJson);
            string connectionString = databaseSettings["connectionString"].ToString();

            var database = MySqlConnectionStart(connectionString);
            IBooksRepository booksRepository = new EFBooksRepository(new AppDbContext(database));

            booksRepository.AddBookUrlAsync(467, "DASDS", "asd");

            //telegram settings
            string telegramTextJson = File.ReadAllText("telegram-settings.json");
            JObject telegramSettings = JObject.Parse(telegramTextJson);
            string telegramToken = telegramSettings["bots"]["book_url"]["TOKEN"].ToString();
            
            IChat chat = new TelegramBotApiChat(telegramToken);
            CommandHandler commandHandler = new CommandHandler(chat, booksRepository);
        }
    }
}
