using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTesting.DataModel;
using MovieTesting.Interfaces;
using Newtonsoft.Json;

namespace MovieTesting.Services
{
    /// <summary>
    /// Provides a caching mechanism for movies.
    /// </summary>
    public class MovieCachingService : ICache<Movie>
    {
        /// <summary>
        /// The caching file name.
        /// </summary>
        private readonly string fileName;

        /// <summary>
        /// The storage service to save and load the cached movies.
        /// </summary>
        private IStorage storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicObjectCachingService"/> class.
        /// </summary>
        /// <param name="localStorage">Storage service to save and load the cached movies</param>
        public MovieCachingService(IStorage storage)
        {
            this.fileName = "movies.json";
            this.storage = storage;
        }

        /// <summary>
        /// Gets the cached movies from the storage.
        /// </summary>
        /// <returns>List of objects</returns>
        public async Task<ICollection<Movie>> GetCachedObjectsAsync()
        {
            var data = await this.storage.GetTextFileAsync(this.fileName);
            if (!string.IsNullOrEmpty(data))
            {
                return JsonConvert.DeserializeObject<ICollection<Movie>>(data);
            }

            return new List<Movie>();
        }

        /// <summary>
        /// Caches the movies to the storage.
        /// </summary>
        /// <param name="items">List of objects</param>
        public async Task CacheObjectsAsync(ICollection<Movie> items)
        {
            var serializedItems = JsonConvert.SerializeObject(items);
            await this.storage.SaveTextFileAsync(this.fileName, serializedItems);
        }

        /// <summary>
        /// Clears the object cache.
        /// </summary>
        public async Task ClearCachedObjects()
        {
            await this.storage.DeleteFileAsync(this.fileName);
        }
    }
}
