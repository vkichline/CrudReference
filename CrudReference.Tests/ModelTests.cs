using CrudReference.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CrudReference.Tests {
    public class ModelTests {
        private IItemsRepository _repo;


        public ModelTests() {
            _repo = new ItemsRepository();
        }


        // A new repository should be empty
        [Fact]
        public void VerifyInitialRepoIsEmpty() {
            Assert.Equal(0, _repo.Count());
        }


        // Adding an item should supply an Id, which could be any Integer.
        // GetAll should return a list containing just the new Item, from which the Id can be extracted.
        // Get(id) should return the same item.
        [Fact]
        public void VerifyAddingAndRetrievingItem() {
            _repo.Add(new Item { Content = "Item 1" });

            Assert.IsType(typeof(Item), _repo.GetAll().First());

            IEnumerable<Item> items = _repo.GetAll();
            Assert.Equal(1, items.Count());
            Item item = items.First();
            Assert.NotNull(item.Id);
            Assert.Equal("Item 1", item.Content);

            int id = item.Id;
            item = _repo.Get(id);
            Assert.Equal(id, item.Id);
            Assert.Equal("Item 1", item.Content);
        }


        // Add should add a new item for each call, rewriting the Id if present.
        // Add returns the Item actually added (with modified Id.)
        // Remove should return true if an item exists and remove it, false if no item is found.
        [Fact]
        public void VerifyCountAndRemove() {
            bool result;

            Item item1 = _repo.Add(new Item { Content = "Same Each Time" });
            Assert.Equal(1, _repo.Count());
            _repo.Add(new Item { Id = 1, Content = "Same Each Time" });
            Assert.Equal(2, _repo.Count());
            Item item3 = _repo.Add(new Item { Id = 1, Content = "Same Each Time" });
            Assert.Equal(3, _repo.Count());

            IEnumerable<Item> items = _repo.GetAll();
            Assert.Equal(3, items.Count());
            int id1 = items.First().Id;
            Assert.Equal(item1.Id, id1);
            int id3 = items.Last().Id;
            Assert.Equal(item3.Id, id3);
            result = _repo.Remove(id1);
            Assert.True(result);
            result = _repo.Remove(id3);
            Assert.True(result);
            Assert.Equal(1, _repo.Count());

            result = _repo.Remove(-1234);
            Assert.False(result);
            Assert.Equal(1, _repo.Count());
        }


        // RemoveAll should empty the respository
        [Fact]
        public void VerifyRemoveAll() {
            _repo.Add(new Item { Content = "Item 1" });
            _repo.Add(new Item { Content = "Item 2" });
            _repo.Add(new Item { Content = "Item 3" });
            Assert.Equal(3, _repo.Count());

            _repo.RemoveAll();
            Assert.Equal(0, _repo.Count());
        }


        // Update should return true if an item was successfully updated, false if no such item exists.
        [Fact]
        public void VerifyUpdate() {
            bool result;

            Item item = _repo.Add(new Item { Content = "xyzzy" });
            Assert.Equal(1, _repo.Count());
            int id = item.Id;

            result = _repo.Update(id, new Item { Content = "FooBarBaz" });
            Assert.True(result);
            item = _repo.Get(id);
            Assert.NotNull(item);
            Assert.Equal("FooBarBaz", item.Content);

            result = _repo.Update(-1234, item);
            Assert.False(result);
            Assert.Equal(1, _repo.Count());
        }


        // Update should overwrite the Item's Id if it exists with the Id supplied in the parameter.
        [Fact]
        public void VerifyUpdateCorrectsId() {
            bool result;

            Item item = _repo.Add(new Item { Content = "xyzzy" });
            Assert.Equal(1, _repo.Count());

            int id = item.Id;
            item.Id = -1234;
            item.Content = "FooBarBaz";
            result = _repo.Update(id, item);
            Assert.Equal(1, _repo.Count());

            item = _repo.Get(id);
            Assert.Equal(id, item.Id);
            Assert.Equal("FooBarBaz", item.Content);

            result = _repo.Update(-1234, item);
            Assert.False(result);
            Assert.Equal(1, _repo.Count());

            item = _repo.Get(-1234);
            Assert.Null(item);
        }
    }
}
