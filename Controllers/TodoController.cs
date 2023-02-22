using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext context;
        

        public TodoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<TodoController>
        [HttpGet]
        public ActionResult<IEnumerable<TodoModel>> Get()
        {
            return Ok(context.Todos.ToList());
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoModel>? Get(int id)
        {
            TodoModel? todo = context.Todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return NotFound("The todo you searched for does not exist.");
            }
            return Ok(todo);
        }

        // POST api/<TodoController>
        [HttpPost]
        public IActionResult Post([FromBody] TodoModel todo)
        {
                context.Todos.Add(todo);
                context.SaveChanges();
                return Ok();
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public ActionResult<TodoModel>? Put(int id, [FromBody] TodoModel todo)
        {
            var todoToModify = context.Todos.FirstOrDefault(t => t.Id == id);

            if (todoToModify == null)
            {
                return NotFound("Requested Todo item was not found");
            }

            todoToModify.Description = todo.Description;
            todoToModify.Completed = todo.Completed;

            context.Todos.Update(todoToModify);
            context.SaveChanges();

            return Ok("The changes have been saved.");
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var todo = context.Todos.FirstOrDefault(t => t.Id == id);
            context.Todos.Remove(todo);
            context.SaveChangesAsync();
        }
    }
}
