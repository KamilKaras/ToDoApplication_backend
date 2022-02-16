using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ToDoAplication.Interfaces.Repositories;

namespace ToDoAplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
  
    public class ToDoController : ControllerBase
    {
        private readonly IToDoItemsRepository _toDoItemsRepository;
        public ToDoController(IToDoItemsRepository toDoItemsRepository)
        {
            _toDoItemsRepository = toDoItemsRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int count)
        {
            var toDoItems = await _toDoItemsRepository.GetAll(page, count);
            return Ok(toDoItems);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _toDoItemsRepository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem(ToDoItem item)
        {
            if (ModelState.IsValid)
            {
                var newItem = await _toDoItemsRepository.CreateItem(item);
                return CreatedAtAction("GetItem", new { newItem.Id}, newItem);
            }
            return new JsonResult("Something went wrong.") { StatusCode = 500 };
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id) {
            var item = await _toDoItemsRepository.DeleteItem(id);
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllItem()
        {
            var items = await _toDoItemsRepository.DeleteAllItem();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, ToDoItem toDoItem)
        {
            var itemFromDb = await _toDoItemsRepository.UpdateItemAsync(id, toDoItem);
            if(itemFromDb is null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUpdateItemAsync(int id, JsonPatchDocument<ToDoItem> patchDocument)
        {
            if (patchDocument is null || !patchDocument.Operations.Any() || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var itemFromDb = await _toDoItemsRepository.PatchUpdateItemAsync(id, patchDocument);
            if (itemFromDb is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
