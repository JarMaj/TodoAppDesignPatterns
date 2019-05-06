using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("todoapi")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoService;
        public TodoController(ITodoRepository todoRepository)
        {
            _todoService = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TodoViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var allTodo = await _todoService.GetAllAsync();
            return Ok(allTodo);
        }

        [HttpGet("{todo}/userid")]
        [ProducesResponseType(typeof(List<TodoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserIdAsync(Guid id)
        {
            var todobyuser = await _todoService.GetByUserIdAsync(id);
            return Ok(todobyuser);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<TodoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>  GetByIdAsync(Guid id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            return Ok(todo);
        }



        [HttpPost]
        [ProducesResponseType(typeof(TodoViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody]TodoViewModel todo)
        {
            var createdTodo = await _todoService.AddAsync(todo);
            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { user = createdTodo.User.UserName, id = createdTodo.TodoID },
                createdTodo
                );
        }

        [HttpPut]
        [ProducesResponseType(typeof(TodoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody]TodoViewModel value)
        {
            var updatedTodo = await _todoService.UpdateAsync(value);
            return Ok(updatedTodo);
        }
        
        [HttpDelete("{todo}/{key}")]
        [ProducesResponseType(typeof(TodoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deletedTodo = await _todoService.DeleteAsync(id);
            return Ok(deletedTodo);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}