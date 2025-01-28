using WebApplicationWithDapper.Interfaces;
using WebApplicationWithDapper.DataFactory;
using WebApplicationWithDapper.Models;
using Dapper;
namespace WebApplicationWithDapper.Repositories
{
    public class MovieRepository: IMovie
    {
        private readonly DapperDbContext _context;
        public MovieRepository(DapperDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> Get()
        {
            var sql = $@"SELECT [Id],
                               [Title],
                               [Director],
                               [Year]
                            FROM
                               [Movie]";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Movie>(sql);
        }
        public async Task<Movie> Find(int? uid)
        {
            var sql = $@"SELECT [Id],
                               [Title],
                               [Director],
                               [Year]
                            FROM
                               [Movie]
                            WHERE
                              [Id]=@uid";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Movie>(sql,new { uid });
        }
        public async Task<Movie> Add(Movie movie)
        {
            var sql = $@"INSERT INTO [dbo].[Movie]
                                ([Title],
                                 [Director],
                                 [Year])
                                VALUES
                                (
                                 @Title,
                                 @Director,
                                 @Year)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, movie);
            return movie;
        }
        public async Task<Movie> Update(Movie movie)
        {
            var sql = $@"UPDATE[dbo].[Movie]
                           SET [Title] = @Title,
                               [Director] = @Director,
                               [Year] = @Year
                          WHERE
                              Id=@Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, movie);
            return movie;
        }
        public async Task<Movie> Remove(Movie movie)
        {
            var sql = $@"
                        DELETE FROM
                            [dbo].[Movie]
                        WHERE
                            [Id]=@Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, movie);
            return movie;
        }
    }
}
