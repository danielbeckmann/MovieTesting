using System;
using System.Collections.Generic;
using MovieTesting.DataModel;
using MovieTesting.Interfaces;
using MovieTesting.Services;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MovieTesting
{
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Movie storage object.
        /// </summary>
        private ICache<Movie> movieStorage;

        public MainPage()
        {
            this.InitializeComponent();
            this.movieStorage = new MovieCachingService(new LocalStorageService());
        }

        private async void SaveMovies_Clicked(object sender, RoutedEventArgs e)
        {
            var movies = new List<Movie>
            {
                new Movie { Title = "Matrix", Subtitle = "Lorem ipsum...", ImagePath = new Uri("http://lorempixel.com/250/250/nightlife/1") },
                new Movie { Title = "Der Pate", Subtitle = "Lorem ipsum...", ImagePath = new Uri("http://lorempixel.com/250/250/nightlife/2") },
            };

            await this.movieStorage.CacheObjectsAsync(movies);
        }

        private async void LoadMovies_Clicked(object sender, RoutedEventArgs e)
        {
            var movies = await this.movieStorage.GetCachedObjectsAsync();
            if (movies != null && movies.Count > 0)
            {
                foreach (var movie in movies)
                {
                    var dialog = new MessageDialog(string.Format("{0}\r\n{1}", movie.Title, movie.ImagePath), movie.Title);
                    await dialog.ShowAsync();
                }
            }
            else
            {
                var dialog = new MessageDialog("Keine Filme im Cache", "Info");
                await dialog.ShowAsync();
            }
        }

        private async void ClearMovies_Clicked(object sender, RoutedEventArgs e)
        {
            await this.movieStorage.ClearCachedObjects();
        }
    }
}
