using System.Collections.Generic;
using System.Linq;

namespace CrudReference.Models
{
    // Simplistic in-memory repositry.  The focus is in the WebAPI controller.
    public class ItemsRepository : IItemsRepository
    {
        private Dictionary<int, Item> _items;   // Assume no knowledge about contents of Item, other than Id is int.
        private int _nextId;                    // Matches the type of Id

        public ItemsRepository()
        {
            _items = new Dictionary<int, Item>();
            _nextId = 100;
        }

        // Add a unique Id and insert into the Dictionary, then return the item.
        // Return null if the item cannot be added
        public Item Add(Item item)
        {
            // Note: if we knew more about Items, we could check to see if there already was a matching item in the Dictionary.
            // However, in this example we don't want to assume anything except the int Id.
            if(null == item)
            {
                return null;
            }
            item.Id = NextId(item);
            _items.Add(item.Id, item);
            return item;
        }

        public Item Get(int id)
        {
            Item item = null;
            if (_items.ContainsKey(id))
            {
                item = _items[id];
            }
            return item;
        }

        public IEnumerable<Item> GetAll()
        {
            return _items.Values;
        }

        public bool Remove(int id)
        {
            bool have = _items.ContainsKey(id);
            if (have)
            {
                _items.Remove(id);
            }
            return have;
        }

        public void RemoveAll()
        {
            _items.Clear();
        }

        public bool Update(int id, Item item)
        {
            if (item == null) return false;
            // Don't allow the id to be changed.
            item.Id = id;
            bool have = _items.ContainsKey(id);
            if (have)
            {
                _items[id] = item;
            }
            return have;
        }

        public void Save()
        {
            // TBD.  Implementation specific.
        }

        public int Count()
        {
            return _items.Count();
        }

        // Generating the next index is pretty simple with an int, but this makes it easier to
        // generate the next unique Id for any index type.
        private int NextId(Item item)
        {
            return _nextId++;
        }
    }
}
