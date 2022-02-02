using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoAplication.Interfaces.Repositories;

namespace ToDoAplication.Services
{
    public class ToDoItemService : IToDoItemsService
    {
        private IToDoItemsRepository _toDoItemsRepository;
        public ToDoItemService(IToDoItemsRepository toDoItemsRepository)
        {
            _toDoItemsRepository = toDoItemsRepository;
        }

        public async Task<List<ToDoItem>> GetAll(int count)
        {
            return await _toDoItemsRepository.GetAll(count);
        }
    }
}
