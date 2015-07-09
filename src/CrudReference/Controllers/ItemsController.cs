﻿using CrudReference.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

// Reference implementaion of RESTful CRUD controller for MVC6 WebAPI.
namespace CrudReference.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private IItemsRepository _items;

        // Repository is injectd.  Created in Startup.cs.
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

        // GET api/items/100
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
            if (null != item)
            {
                Context.Response.Headers["Location"] = LocationUrl(Request, value.Id); ;
                return new HttpStatusCodeResult(201);    // Created
            }
            else
            {
                return HttpBadRequest();
            }
        }

        // PUT api/items/100
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Item value)
        {
            bool ok = _items.Update(id, value);
            if (ok)
            {
                Context.Response.Headers["Location"] = LocationUrl(Request, value.Id);
                return new HttpStatusCodeResult(200);    // OK
            }
            else
            {
                return HttpBadRequest();
            }
        }

        // DELETE api/items/100
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

        private string LocationUrl(HttpRequest request, int id)
        {
            return request.Scheme + "://" + request.Host + "/api/items/" + id.ToString();
        }
    }
}
