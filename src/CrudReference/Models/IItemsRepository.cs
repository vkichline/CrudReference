using System.Collections.Generic;

namespace CrudReference.Models {
    public interface IItemsRepository {
        Item Add(Item item);
        Item Get(int id);
        IEnumerable<Item> GetAll();
        bool Update(int id, Item item);
        bool Remove(int id);
        void RemoveAll();
        int Count();
    }
}
