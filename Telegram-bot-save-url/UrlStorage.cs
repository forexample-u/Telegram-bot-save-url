using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Storage
{
    /// <summary>
    /// Хранилище для пользователя
    /// </summary>
    public class UrlStorage : IStorage
    {
        private Dictionary<string, List<string>> categoriaWithUrl = new();

        public List<string> GetEntityKeys()
        {
            List<string> categorias = categoriaWithUrl.Keys.ToList();
            return categorias;
        }

        public List<string> GetEntityByKeys(string categoria)
        {
            List<string> links = new();
            if (categoriaWithUrl.TryGetValue(categoria, out links))
            {
                return links;
            }
            return new List<string>();
        }

        public void AddEntity(string categoria, string url)
        {
            if (!categoriaWithUrl.ContainsKey(categoria)) //emtpy
            {
                categoriaWithUrl[categoria] = new List<string>();
            }

            var urls = categoriaWithUrl[categoria];
            urls.Add(url);
            categoriaWithUrl[categoria] = urls;
        }
    }
}
