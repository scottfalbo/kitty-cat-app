using KittyCatApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KittyCatApp.Services
{
    public class SQLiteDataStore : IDataStore<Item>
    {
        readonly SQLiteAsyncConnection database;

        public SQLiteDataStore()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Storage.db3");
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<Item>().Wait();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                await database.InsertAsync(item);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                await database.DeleteAsync(id);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Task<Item> GetItemAsync(int id)
        {
            return database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return database.Table<Item>().ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                int updatedItem = await database.UpdateAsync(item);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
