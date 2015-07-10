using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
