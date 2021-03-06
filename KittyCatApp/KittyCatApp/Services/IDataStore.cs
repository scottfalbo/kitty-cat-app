using KittyCatApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KittyCatApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T translatedText);
        Task<bool> UpdateItemAsync(T translatedText);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<List<Translation>> GetItemsAsync(bool forceRefresh = false);
    }
}
