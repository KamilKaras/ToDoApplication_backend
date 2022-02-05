using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoAplication
{
    public interface IToDoItemsService
    {
        Task<List<ToDoItem>> GetAll(int page, int count);
    }
}
