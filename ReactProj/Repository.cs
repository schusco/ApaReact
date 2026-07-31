using Dapper;
using MySql.Data.MySqlClient;
using ReactProj.Models;

namespace ReactProj
{
    public interface IRepository
    {
        Task<IList<APA9BallScore>> Get9BallScores();
        Task<IList<APA8BallScore>> Get8BallScores();
        Task<IList<APAPlayer>> GetPlayers();
        Task<bool> Add9BallScore(Player9BallScore model);
        Task<bool> AddPlayer(APAPlayer newPlayer);
        Task<bool> IsDuplicatePlayer(int number);
        Task<bool> Add8BallScore(Player8BallScore model);
        Task<APAPlayer> ValidateLogin(int playerNumber, string password);
        Task<bool> UpdatePlayer(APAPlayer player);
    }
    public class Repository(string defaultConnection) : IRepository
    {
        private readonly string _connectionString = defaultConnection;
        public async Task<IList<APA9BallScore>> Get9BallScores()
        {
            await using var dbCon = new MySqlConnection(_connectionString);
            await dbCon.OpenAsync();
            var scores = await dbCon.QueryAsync<APA9BallScore>("SELECT scoredate as Date,result as isWin,innings,defenses,balls,oppBalls,sl as PlayerSl,oppsl as OppPlayerSl FROM scores9 order by scoredate desc limit 20");
            return [.. scores];
        }
        public async Task<IList<APA8BallScore>> Get8BallScores()
        {
            await using var dbCon = new MySqlConnection(_connectionString);
            await dbCon.OpenAsync();
            var scores = await dbCon.QueryAsync<APA8BallScore>("SELECT * FROM scores8 order by scoredate desc limit 20");
            return [.. scores];
        }
        public async Task<IList<APAPlayer>> GetPlayers()
        {
            await using var dbCon = new MySqlConnection(_connectionString);
            await dbCon.OpenAsync();
            var scores = await dbCon.QueryAsync<APAPlayer>("SELECT * FROM apaplayers order by lastName asc");
            return [.. scores];
        }
        public async Task<bool> Add9BallScore(Player9BallScore model)
        {
            try
            {
                var result = PointTable.GetBallsToWin(model.Sl.GetValueOrDefault()) == model.Balls ? 1 : 0;
                await using var dbCon = new MySqlConnection(_connectionString);
                await dbCon.OpenAsync();
                dbCon.Query("insert into scores9 values (default,@date,@result,@innings,@defenses,@balls,@sl,@oppsl,@oppballs,@playerid,@oppid)",
                    new
                    {
                        date = model.Date,
                        result,
                        innings = model.Innings,
                        defenses = model.Defenses,
                        balls = model.Balls,
                        sl = model.Sl,
                        oppsl = model.OppSl,
                        oppballs = model.OppBalls,
                        playerid = model.PlayerId,
                        oppid = model.OppPlayerId
                    });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> AddPlayer(APAPlayer newPlayer)
        {
            try
            {
                await using var dbCon = new MySqlConnection(_connectionString);
                await dbCon.OpenAsync();
                await dbCon.QueryAsync("insert into apaplayers (playerId,lastName,firstName,scoreable) values (@id,@last,@first,@score)",
                    new { id = newPlayer.PlayerNumber, last = newPlayer.LastName, first = newPlayer.FirstName, score = newPlayer.CanScoreFor });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> IsDuplicatePlayer(int number)
        {
            await using var dbCon = new MySqlConnection(_connectionString);
            await dbCon.OpenAsync();
            var result = await dbCon.QueryFirstAsync<int>("SELECT count(*) FROM apaplayers where playerId=@number", new { number });
            return result == 1;
        }
        public async Task<bool> Add8BallScore(Player8BallScore model)
        {
            try
            {
                await using var dbCon = new MySqlConnection(_connectionString);
                await dbCon.OpenAsync();
                await dbCon.QueryAsync("insert into scores8 values (default,@result,@date,@innings,@defenses,@sl,@oppsl,@playerid,@games,@oppid)",
                       new
                       {
                           result = model.Points,
                           date = model.Date,
                           innings = model.Innings,
                           defenses = model.Defenses,
                           sl = model.Sl,
                           oppsl = model.OppSl,
                           playerid = model.PlayerId,
                           games = model.Games,
                           oppid = model.OppPlayerId
                       });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<APAPlayer> ValidateLogin(int playerNumber, string password)
        {
            await using var dbCon = new MySqlConnection(_connectionString);
            await dbCon.OpenAsync();
            var result = await dbCon.QuerySingleAsync<APAPlayer>("SELECT * from apa.apaplayers where playerId=@no and scoreable=1", new { no = playerNumber });
            return result;
        }

        public async Task<bool> UpdatePlayer(APAPlayer player)
        {
            try
            {
                await using var dbCon = new MySqlConnection(_connectionString);
                await dbCon.OpenAsync();
                await dbCon.QueryAsync("update apaplayers set curSl8=@sl8, curSl9=@sl9, lastName=@ln, firstName=@fn where playerId=@no",
                    new { no = player.PlayerNumber, ln = player.LastName, fn = player.FirstName, sl8 = player.Sl8, sl9 = player.Sl9 });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}