using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoAplication.Interfaces.Repositories
{
    public interface IToDoItemsRepository
    {
        Task<List<ToDoItem>> GetAll(int page, int count);
        Task<ToDoItem> GetItem(int id);
        Task<ToDoItem> CreateItem(ToDoItem item);
        Task<ToDoItem> DeleteItem(int id);
        Task<List<ToDoItem>> DeleteAllItem();
        Task<ToDoItem> UpdateItemAsync(int id, ToDoItem toDoItem);
        Task<ToDoItem> PatchUpdateItemAsync(int id, JsonPatchDocument<ToDoItem> patchDocument);
    }
}
