using System;

namespace App.Repository
{
    public interface IRepository <Entity, Item>
    {
        IEnumerable<Entity> GetKeys();
        IEnumerable<Item> GetByKey(Entity key);
        void Add(Entity key, Item value);
        void Remove(Entity key, Item value);
    }
}
