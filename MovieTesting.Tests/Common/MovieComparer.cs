using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTesting.DataModel;

namespace MovieTesting.Tests.Common
{
    /// <summary>
    /// Comparer for <see cref="Movie"/> objects.
    /// </summary>
    public class MovieComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie x, Movie y)
        {
            return x.Title == y.Title;
        }

        public int GetHashCode(Movie obj)
        {
            return obj.Title.GetHashCode();
        }
    }
}
