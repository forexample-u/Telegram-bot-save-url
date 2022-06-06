using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    /// <summary>
    /// Хранилище для пользователя
    /// </summary>
    public class UrlsRepository : IRepositoryDictionary<string, string>
    {
        private Dictionary<string, List<string>> categoriaWithUrls = new();

        public IEnumerable<string> GetByKey(string categoria)
        {
            if (categoriaWithUrls.TryGetValue(categoria, out List<string> urls))
            {
                return urls;
            }

            return Enumerable.Empty<string>();
        }

        public IEnumerable<string> GetKeys()
        {
            List<string> categorias = categoriaWithUrls.Keys.ToList();
            return categorias;
        }

        public void Add(string categoria, string url)
        {
            if (!categoriaWithUrls.ContainsKey(categoria)) //emtpy
            {
                categoriaWithUrls[categoria] = new List<string>();
            }
            categoriaWithUrls[categoria].Add(url);
        }

        public void Remove(string categoria, string url)
        {
            if (categoriaWithUrls.ContainsKey(categoria))
            {
                categoriaWithUrls[categoria].Remove(url);
            }
        }
    }
}