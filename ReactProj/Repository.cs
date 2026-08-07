using Dapper;
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using ReactProj.Models;

namespace ReactProj
{
    public interface IRepository
    {
        Task<IList<APA9BallScore>> Get9BallScores(int playerId);
        Task<IList<APA8BallScore>> Get8BallScores(int playerId);
        Task<IList<APAPlayer>> GetPlayers();
        Task<bool> Add9BallScore(Player9BallScore model);
        Task<bool> AddPlayer(APAPlayer newPlayer);
        Task<bool> IsDuplicatePlayer(int number);
        Task<bool> Add8BallScore(Player8BallScore model);
        Task<APAPlayer> ValidateLogin(int playerNumber, string password);
        Task<bool> UpdatePlayer(APAPlayer player);
        Task<bool> CheckUser(int playerNumber);
        Task<APAPlayer> SetPassword(int playerNumber, string password);
        Task<APAPlayer> GetPlayer(int playerNumber);
    }
    public class Repository(string defaultConnection) : IRepository
    {
        private readonly string _connectionString = defaultConnection;
        public async Task<IList<APA9BallScore>> Get9BallScores(int playerId)
        {
            await using var dbCon = new MySqlConnection(_connectionString);
            await dbCon.OpenAsync();
            var scores = await dbCon.QueryAsync<APA9BallScore>(@"SELECT scoredate as Date,result as isWin,innings,defenses,balls,oppBalls,
                sl as PlayerSl,oppsl as OppPlayerSl FROM scores9 where playerId=@no order by scoredate desc limit 20", new { no = playerId });
            return [.. scores];
        }
        public async Task<IList<APA8BallScore>> Get8BallScores(int playerId)
        {
            await using var dbCon = new MySqlConnection(_connectionString);
            await dbCon.OpenAsync();
            var scores = await dbCon.QueryAsync<APA8BallScore>("SELECT * FROM scores8 where playerId=@no order by scoredate desc limit 20", new { no = playerId });
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
            var player = await GetPlayer(playerNumber);
            if (!player.CanScoreFor)
                throw new InvalidOperationException($"Player number {0} is not a valid user");
            var hasher = new PasswordHasher<APAPlayer>();
            if (hasher.VerifyHashedPassword(player, player.Password, password) != PasswordVerificationResult.Success)
                throw new InvalidOperationException($"Invalid password for user: {playerNumber}");
            return player;
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
        public async Task<bool> CheckUser(int playerNumber)
        {
            var player = await GetPlayer(playerNumber);
            if (player.CanScoreFor)
                return string.IsNullOrEmpty(player.Password);
            throw new InvalidOperationException($"Player number {playerNumber} is not a valid user");
        }
        public async Task<APAPlayer> SetPassword(int playerNumber, string password)
        {
            var hasher = new PasswordHasher<APAPlayer>();
            var player = await GetPlayer(playerNumber);
            if (!player.CanScoreFor)
                throw new InvalidOperationException($"Player number {playerNumber} is not a valid user");
            var hashedPwd = hasher.HashPassword(player, password);
            try
            {
                await using var dbCon = new MySqlConnection(_connectionString);
                await dbCon.OpenAsync();
                await dbCon.QueryAsync("update apaplayers set password=@pwd where playerId=@no", new { no = playerNumber, pwd = hashedPwd });
                return player;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<APAPlayer> GetPlayer(int playerNumber)
        {
            try
            {
                await using var dbCon = new MySqlConnection(_connectionString);
                await dbCon.OpenAsync();
                var player = await dbCon.QueryFirstAsync<APAPlayer>("select * from apaplayers where playerId=@no", new { no = playerNumber });                
                return player;
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"No player found with player number {playerNumber}");
            }
        }
    }
}