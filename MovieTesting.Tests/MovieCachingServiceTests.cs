using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieTesting.DataModel;
using MovieTesting.Interfaces;
using MovieTesting.Services;
using MovieTesting.Tests.Common;

namespace MovieTesting.Tests
{
    [TestClass]
    public class MovieCachingServiceTests
    {
        private Mock<IStorage> storageServiceMock;

        private MovieCachingService GetService()
        {
            this.storageServiceMock = new Mock<IStorage>();
            return new MovieCachingService(this.storageServiceMock.Object);
        }

        // ...

        [TestMethod]
        public async Task GetCachedObjectsAsync_AnyCase_ShouldInvokeStorageService()
        {
            var service = this.GetService();
            await service.GetCachedObjectsAsync();

            this.storageServiceMock.Verify(x => x.GetTextFileAsync(It.IsAny<string>()));
        }

        [TestMethod]
        public async Task GetCachedObjectsAsync_MoviesCached_ShouldReturnMovies()
        {
            var service = this.GetService();
            var movies = new List<Movie>
            {
                new Movie { Title = "Test" }
            };

            this.storageServiceMock.Setup(x => x.GetTextFileAsync(It.IsAny<string>())).ReturnsAsync("[{\"Title\":\"Test\"}]");

            var verifyMovies = await service.GetCachedObjectsAsync();

            Assert.IsTrue(movies.SequenceEqual(verifyMovies, new MovieComparer()));
        }

        [TestMethod]
        public async Task GetCachedObjectsAsync_NoMoviesCached_ShouldReturnEmptyList()
        {
            var service = this.GetService();
            this.storageServiceMock.Setup(x => x.GetTextFileAsync(It.IsAny<string>())).ReturnsAsync(string.Empty);

            var movies = await service.GetCachedObjectsAsync();

            Assert.IsNotNull(movies);
            Assert.IsTrue(movies.Count == 0);
        }

        // ...
    }
}
