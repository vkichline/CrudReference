﻿using CrudReference.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

// Reference implementaion of RESTful CRUD controller for MVC6 WebAPI.
namespace CrudReference.Controllers {
    [Route("api/[controller]")]
    public class ItemController : Controller {
        // Repository is injected.  Created in Startup.cs.
        [FromServices]
        public IItemsRepository _items { get; set; }

        // GET: api/items
        [HttpGet]
        public IEnumerable<Item> Get() {
            return _items.GetAll();
        }

        // GET api/items/100
        // This route must be named for CreatedAtRoute call in Post below:
        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult Get(int id) {
            Item item = _items.Get(id);
            if (null != item) {
                return new ObjectResult(item);
            }
            else {
                return HttpNotFound();
            }
        }

        // POST api/items
        [HttpPost]
        public IActionResult Post([FromBody]Item value) {
            Item item = _items.Add(value);
            if (null != item) {
                return CreatedAtRoute("GetItem", new { controller = "Item", id = item.Id }, item);
            }
            else {
                return HttpBadRequest();
            }
        }

        // PUT api/items/100
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Item value) {
            bool ok = _items.Update(id, value);
            if (ok) {
                return new NoContentResult();
            }
            else {
                return HttpNotFound();
            }
        }

        // DELETE api/items/100
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (_items.Remove(id)) {
                return new NoContentResult();
            }
            else {
                return HttpNotFound();
            }
        }
    }
}
