using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieTesting.Interfaces
{
    /// <summary>
    /// Provides methods to cache objects.
    /// </summary>
    public interface ICache<T>
    {
        /// <summary>
        /// Gets the cached objects from the storage.
        /// </summary>
        /// <returns>List of objects</returns>
        Task<ICollection<T>> GetCachedObjectsAsync();

        /// <summary>
        /// Caches the objects to the storage.
        /// </summary>
        /// <param name="items">List of objects</param>
        Task CacheObjectsAsync(ICollection<T> items);

        /// <summary>
        /// Clears the object cache.
        /// </summary>
        Task ClearCachedObjects();
    }
}
