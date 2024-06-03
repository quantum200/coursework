using API.Models;
using Npgsql;

namespace FootballWebAPI
{
    public class DataBase
    {
        private readonly string _connectionString;
        public DataBase()
        {
            _connectionString = "Host=localhost; Username=postgres; Password=nikolass2006Z; Database=postgres";
        }

        public async Task InsertFootballPlayer(FootballPlayer footballPlayer)
        {
            var sql = "INSERT INTO public.\"NewPlayer\" (\"Firstname\", \"Lastname\", \"Age\", \"Height\", \"Weight\", \"Photo\", \"Club\", \"Country\", \"League\", \"LeagueCountry\", \"GoalsTotal\", \"GoalsAssists\", \"LeagueSeason\", \"TeamName\", \"TeamLogo\", \"LeagueLogo\", \"LeagueFlag\") " +
                      "VALUES (@Firstname, @Lastname, @Age, @Height, @Weight, @Photo, @Club, @Country, @League, @LeagueCountry, @GoalsTotal, @GoalsAssists, @LeagueSeason, @TeamName, @TeamLogo, @LeagueLogo, @LeagueFlag)";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var comm = new NpgsqlCommand(sql, conn);

            comm.Parameters.AddWithValue("Firstname", footballPlayer.Firstname);
            comm.Parameters.AddWithValue("Lastname", footballPlayer.Lastname);
            comm.Parameters.AddWithValue("Age", footballPlayer.Age);
            comm.Parameters.AddWithValue("Height", footballPlayer.Height);
            comm.Parameters.AddWithValue("Weight", footballPlayer.Weight);
            comm.Parameters.AddWithValue("Photo", footballPlayer.Photo);
            comm.Parameters.AddWithValue("Club", footballPlayer.Club);
            comm.Parameters.AddWithValue("Country", footballPlayer.Country);
            comm.Parameters.AddWithValue("League", footballPlayer.LeagueName);
            comm.Parameters.AddWithValue("LeagueCountry", footballPlayer.LeagueCountry);
            comm.Parameters.AddWithValue("GoalsTotal", footballPlayer.GoalsTotal ?? (object)DBNull.Value);
            comm.Parameters.AddWithValue("GoalsAssists", footballPlayer.GoalsAssists ?? (object)DBNull.Value);
            comm.Parameters.AddWithValue("LeagueSeason", footballPlayer.LeagueSeason ?? (object)DBNull.Value);
            comm.Parameters.AddWithValue("TeamName", footballPlayer.TeamName);
            comm.Parameters.AddWithValue("TeamLogo", footballPlayer.TeamLogo);
            comm.Parameters.AddWithValue("LeagueLogo", footballPlayer.LeagueLogo);
            comm.Parameters.AddWithValue("LeagueFlag", footballPlayer.LeagueFlag);

            await comm.ExecuteNonQueryAsync();
        }

        public async Task<List<FootballPlayer>> GetAllPlayers()
        {
            var players = new List<FootballPlayer>();
            var sql = "SELECT \"Firstname\", \"Lastname\", \"Age\", \"Height\", \"Weight\", \"Photo\", \"Club\", \"Country\", \"League\", \"LeagueCountry\", \"GoalsTotal\", \"GoalsAssists\", \"LeagueSeason\", \"TeamName\", \"TeamLogo\", \"LeagueLogo\", \"LeagueFlag\" FROM public.\"NewPlayer\"";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var comm = new NpgsqlCommand(sql, conn);
            await using var reader = await comm.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var player = new FootballPlayer
                {
                    Firstname = reader.IsDBNull(0) ? null : reader.GetString(0),
                    Lastname = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Age = reader.GetInt32(2),
                    Height = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Weight = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Photo = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Club = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Country = reader.IsDBNull(7) ? null : reader.GetString(7),
                    LeagueName = reader.IsDBNull(8) ? null : reader.GetString(8),
                    LeagueCountry = reader.IsDBNull(9) ? null : reader.GetString(9),
                    GoalsTotal = reader.IsDBNull(10) ? null : reader.GetInt32(10),
                    GoalsAssists = reader.IsDBNull(11) ? null : reader.GetInt32(11),
                    LeagueSeason = reader.IsDBNull(12) ? null : reader.GetInt32(12),
                    TeamName = reader.IsDBNull(13) ? null : reader.GetString(13),
                    TeamLogo = reader.IsDBNull(14) ? null : reader.GetString(14),
                    LeagueLogo = reader.IsDBNull(15) ? null : reader.GetString(15),
                    LeagueFlag = reader.IsDBNull(16) ? null : reader.GetString(16)
                };
                players.Add(player);
            }

            return players;
        }
        public async Task DeleteAllPlayers()
        {
            var sql = "DELETE FROM public.\"NewPlayer\"";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var comm = new NpgsqlCommand(sql, conn);
            await comm.ExecuteNonQueryAsync();
        }
    }
}