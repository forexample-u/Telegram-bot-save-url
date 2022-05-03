using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Message;

namespace App.Assistent
{
    /// <summary>
    /// Ассистент
    /// </summary>
    public class Assistent
    {
        public IMessage message;
        public string currentCategoria { get; private set; }
        public string currentLinkUrl { get; private set; }
        Dictionary<string, HashSet<string>> CategoriaAndLinksUrl = new Dictionary<string, HashSet<string>>();

        /// <summary>
        /// В Конструктор передать интерфейсный месседже
        /// </summary>
        /// <param name="newMessage">Можно передать любой реализованый месседже</param>
        public Assistent(IMessage newMessage)
        {
            message = newMessage;
        }

        /// <summary>
        /// Вывод по категориям
        /// </summary>
        /// <param name="selectCategoria">указанная категория</param>
        private void PrintByCategoria(string selectCategoria)
        {
            if (CategoriaAndLinksUrl.TryGetValue(selectCategoria, out var currectInput))
            {
                StringBuilder allLinks = new StringBuilder();
                foreach (var link in CategoriaAndLinksUrl[selectCategoria])
                {
                    allLinks.Append($"\n{link}");
                }
                message.Send($"Категория: {selectCategoria}\nВключает:" + allLinks.ToString());
            }
            else
            {
                message.Send($"Категория: {selectCategoria} - не существует");
            }
        }

        /// <summary>
        /// Ввод пользователя
        /// </summary>
        private void Input()
        {
            message.Send("Впишите категорию");
            currentCategoria = message.Read();
            message.Send("Впишите url, который нужно сохранить");
            currentLinkUrl = message.Read();

            //validator
            bool currectUrl = true;
            var allUrl = currentLinkUrl.Split(" ");
            foreach (var link in allUrl)
            {
                if (Uri.IsWellFormedUriString(link, UriKind.RelativeOrAbsolute))
                {
                    message.Send("Одна или несколько записей не являются url");
                    currectUrl = false;
                }
            }

            if (currectUrl)
            {
                if (!(CategoriaAndLinksUrl.ContainsKey(currentCategoria))) //emtpy
                {
                    CategoriaAndLinksUrl.Add(currentCategoria, new HashSet<string>());
                }
                CategoriaAndLinksUrl[currentCategoria].Add(currentLinkUrl);
            }
        }


        /// <summary>
        /// Запустить ассистента
        /// </summary>
        public void Start()
        {
            message.Read(); //start
            while (true)
            {
                message.Send("Введите /store-link или /get-links");
                string inputeCommand = message.Read();
                switch (inputeCommand)
                {
                    case "/store-link":
                        Input();
                        break;

                    case "/get-links":
                        message.Menu("Выберите вашу сохранёную категорию", CategoriaAndLinksUrl.Keys.ToArray());
                        currentCategoria = message.Read();
                        PrintByCategoria(currentCategoria);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
