using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Hirad23.Domain.Catalog;
using Hirad23.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;


namespace Hirad23.Api.Controllers
{
    [ApiController]
    [Route("/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly StoreContext _db;

        public CatalogController(StoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            // var items = new List<Item>()
            // {
            return Ok(_db.Items);
            // new Item("Shirt", "OSU Shirt", "Nike", 29.99m),
            // new Item("Shorts", "OSU shorts", "Nike", 44.99m)
        // };
        // return Ok(items);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetItem(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            // var item = new Item("Shirt", "OSU shirt.", "Nike", 29.99m);
            // item.Id = id;

            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return Created($"/catalog/{item.Id}", item);
            
            // return Created("/catalog/42", item);
        }

        [HttpPost("{id:int}/ratings")]
        public IActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            item.AddRating(rating);
            _db.SaveChanges();

            // var item = new Item("Shirt", "OSU shirt", "Nike", 29.99m);
            // item.Id = id;
            // item.AddRating(rating);

            return Ok(item);
        }

        // [HttpPut("{id:int}")]
        // public IActionResult Put(int id, Item item)
        // {
        //     return NoContent();
        // }

        [HttpPut("{id:int}")]
        public IActionResult PutItem(int id, [FromBody] Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            if (_db.Items.Find(id) == null)
            {
                return NotFound();
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize("delete:catalog")]
        public IActionResult DeleteItem(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _db.Items.Remove(item);
            _db.SaveChanges();

            return Ok();
        }
    }
}
