using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Storage
{
    public interface IStorage
    {
        void AddEntity(string key, string value);
        List<string> GetEntityByKeys(string key);
        List<string> GetEntityKeys();
    }
}
