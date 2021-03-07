using KittyCatApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KittyCatApp.Services
{
    public class SQLiteDataStore : IDataStore<Translation>
    {
        readonly SQLiteAsyncConnection database;

        public SQLiteDataStore()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Storage.db3");
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<Translation>().Wait();
        }

        public async Task<bool> AddItemAsync(Translation translatedText)
        {
            try
            {
                await database.InsertAsync(translatedText);
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


        public Task<Translation> GetItemAsync(int id)
        {
            return database.Table<Translation>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Translation>> GetItemsAsync(bool forceRefresh = false)
        {
            return database.Table<Translation>().ToListAsync();
        }

        public async Task<bool> UpdateItemAsync(Translation translatedText)
        {
            try
            {
                int updatedItem = await database.UpdateAsync(translatedText);
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
