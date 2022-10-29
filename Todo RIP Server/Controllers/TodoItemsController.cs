using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Todo_RIP_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        Models.TodoItemsContext db;
        public TodoItemsController(Models.TodoItemsContext context)
        {
            db = context;
            if (!db.TodoItems.Any())
            {
                db.TodoItems.Add(new Models.TodoItem { Name = "Сходить в магазин", Description = "Нужно сходить в магазин и купить: хлеб, молоко, сыр", Status = false });
                db.TodoItems.Add(new Models.TodoItem { Name = "Сделать лабораторную 1 по РИП", Description = "Написать Web-API на PHP, или не на PHP", Status = false });
                db.SaveChanges();
            }
        }

        //GET api/todoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.TodoItem>>> Get()
        {
            return await db.TodoItems.ToListAsync();
        }

        //GET api/todoitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.TodoItem>> Get(int id)
        {
            Models.TodoItem task = await db.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
                return null;
            return new ObjectResult(task);
        }

        //POST api/todoitems
        [HttpPost]
        public async Task<ActionResult<Models.TodoItem>> Post(Models.TodoItem task)
        {
            if (task == null)
                return BadRequest();

            db.TodoItems.Add(task);
            await db.SaveChangesAsync();
            return Ok(task);
        }


        //PUT api/todoitems/
        [HttpPut]
        public async Task<ActionResult<Models.TodoItem>> Put(Models.TodoItem task)
        {
            if (task == null)
                return BadRequest();

            if (!db.TodoItems.Any(x => x.Id == task.Id))
            {
                return NotFound();
            }

            db.Update(task);
            await db.SaveChangesAsync();
            return Ok(task);
        }

        //DELETE api/todoitems/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.TodoItem>> Delete(int id)
        {
            Models.TodoItem task = db.TodoItems.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return NotFound();
            db.TodoItems.Remove(task);
            await db.SaveChangesAsync();
            return Ok(task);
        }

        //PUT api/todoitems/change/{id}
        [HttpPut("change/{id}")]
        public async Task<ActionResult<Models.TodoItem>> Put(int id)
        {
            Models.TodoItem task = db.TodoItems.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return NotFound();

            if (task.Status == false)
                task.Status = true;
            else task.Status = false;

            db.Update(task);
            await db.SaveChangesAsync();
            return Ok(task);
        }
    }
}
