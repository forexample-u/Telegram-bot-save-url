using System;

namespace App.Repository
{
    public interface IRepositoryDictionary <Entity, Item>
    {
        IEnumerable<Entity> GetKeys();
        IEnumerable<Item> GetByKey(Entity key);
        void Add(Entity key, Item value);
        void Remove(Entity key, Item value);
    }
}
