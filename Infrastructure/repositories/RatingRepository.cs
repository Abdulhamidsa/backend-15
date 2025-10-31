using System.Data;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(string Tconst, decimal Avg)> RateAsync(long userId, string tconst, int rating)
        {
            await using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM rate(@p_user_id, @p_tconst, @p_rating)";

            command.CommandType = CommandType.Text;

command.Parameters.Add(new NpgsqlParameter("@p_user_id", (int)userId));
            command.Parameters.Add(new NpgsqlParameter("@p_tconst", tconst));
            command.Parameters.Add(new NpgsqlParameter("@p_rating", rating));

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var titleId = reader.GetString(reader.GetOrdinal("title_tconst"));
                var avg = reader.GetDecimal(reader.GetOrdinal("avg_rating"));
                return (titleId, avg);
            }

            throw new Exception("No result returned from rate function.");
        }
    }
}
