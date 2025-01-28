using WebApplicationWithDapper.Models;

namespace WebApplicationWithDapper.Interfaces
{
    public interface IMovie
    {
        Task<IEnumerable<Movie>> Get();
        Task<Movie> Find(int? uid);
        Task<Movie> Add(Movie movie);
        Task<Movie> Update(Movie movie);
        Task<Movie> Remove(Movie movie);
    }
}
