using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MovieTesting.Interfaces;
using Windows.Storage;

namespace MovieTesting.Services
{
    /// <summary>
    /// Local storage service for reading and storing files.
    /// </summary>
    public class LocalStorageService : IStorage
    {
        /// <summary>
        /// Gets the content of a text file in local storage.
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <param name="userIdentifier">The user id</param>
        /// <returns>The file content</returns>
        public async Task<string> GetTextFileAsync(string fileName)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(file);
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetTextFile Error {0}", e.Message);
            }

            return string.Empty;
        }

        /// <summary>
        /// Saves a text file to local storage.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="userIdentifier">The user id</param>
        /// <param name="content">Content of the file</param>
        public async Task SaveTextFileAsync(string fileName, string content)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine("SaveTextFile Error {0}", e.Message);
            }
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        public async Task DeleteFileAsync(string fileName)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                await file.DeleteAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine("DeleteFile Error {0}", e.Message);
            }
        }
    }
}
