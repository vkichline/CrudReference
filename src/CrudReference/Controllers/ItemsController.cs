using CrudReference.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

namespace CrudReference.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private IItemsRepository _items;

        public ItemsController(IItemsRepository items)
        {
            _items = items;
        }

        // GET: api/items
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _items.GetAll();
        }

        // GET api/items/5
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            Item item = _items.Get(id);
            return item;
        }

        // POST api/items
        [HttpPost]
        public IActionResult Post([FromBody]Item value)
        {
            Item item = _items.Add(value);
            if (null == item)
            {
                return HttpBadRequest();
            }
            else
            {
                string url = Request.Scheme + "://" + Request.Host + "/api/items/" + item.Id.ToString();
                Context.Response.Headers["Location"] = url;
                return new HttpStatusCodeResult(201);    // Created
            }
        }

        // PUT api/items/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Item value)
        {
            bool ok = _items.Update(id, value);
            if (!ok)
            {
                return HttpBadRequest();
            }
            else
            {
                string url = Request.Scheme + "://" + Request.Host + "/api/items/" + value.Id.ToString();
                Context.Response.Headers["Location"] = url;
                return new HttpStatusCodeResult(200);    // OK
            }
        }

        // DELETE api/items/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_items.Remove(id))
            {
                return new HttpStatusCodeResult(200);    // OK
            }
            else
            {
                return HttpBadRequest();
            }
        }
    }
}
