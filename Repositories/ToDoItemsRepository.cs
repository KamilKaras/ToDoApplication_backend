using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoAplication.Interfaces.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;

namespace ToDoAplication.Repositories
{
    public class ToDoItemsRepository : IToDoItemsRepository
    {
        private readonly DataContext _dataContext;

        public ToDoItemsRepository(DataContext context)
        {
            _dataContext = context;
        }
        public async Task <List <ToDoItem>> GetAll(int page, int count)
        {
            var returnedList = await _dataContext.ToDoItems.Skip((page - 1) * count).Take(count).ToListAsync();
            return returnedList;
        }

        public async Task<ToDoItem> GetItem(int id)
        {
            var item = await _dataContext.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
            return item;
        }

        public async Task<ToDoItem> CreateItem(ToDoItem item)
        {
            await _dataContext.ToDoItems.AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<ToDoItem> DeleteItem(int id)
        {
            var item = await _dataContext.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
            if(item == null)
            {
                return null;
            }
            _dataContext.Remove(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<List<ToDoItem>> DeleteAllItem()
        {
            var items = await _dataContext.ToDoItems.ToListAsync();
            if (items == null)
            {
                return null;
            }
            items.ForEach(item => _dataContext.ToDoItems.Remove(item));
            await _dataContext.SaveChangesAsync();
            return items;
        }
        public async Task<ToDoItem> UpdateItemAsync(int id, ToDoItem toDoItem)
        {
            if (toDoItem.Id != id)
            {
                return null;
            }
            var itemFromDb = await _dataContext.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
            itemFromDb.Title = toDoItem.Title;
            itemFromDb.Description = toDoItem.Description;
            await _dataContext.SaveChangesAsync();
            return itemFromDb;
        }

        public async Task<ToDoItem> PatchUpdateItemAsync(int id, JsonPatchDocument<ToDoItem> patchDocument)
        {
            var itemFromDb = await _dataContext.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
            if(itemFromDb is null)
            {
                return null;
            }
            patchDocument.ApplyTo(itemFromDb);
            await _dataContext.SaveChangesAsync();
            return itemFromDb;
        }
    }
}
