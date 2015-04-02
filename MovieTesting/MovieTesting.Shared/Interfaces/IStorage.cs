using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieTesting.Interfaces
{
    /// <summary>
    /// Provides methods to store and load files.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Gets the content of a text file.
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns>The file content</returns>
        Task<string> GetTextFileAsync(string fileName);

        /// <summary>
        /// Saves a text file.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="content">Content of the file</param>
        Task SaveTextFileAsync(string fileName, string content);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        Task DeleteFileAsync(string fileName);
    }
}
