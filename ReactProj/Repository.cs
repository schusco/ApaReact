using MySql.Data.MySqlClient;
using System.Resources;
using ReactProj.Models;

namespace ReactProj
{
    public class Repository
    {
        public static ResourceManager Rm { get; } = new ResourceManager("ReactProj.Properties.Resources", typeof(Repository).Assembly);

        internal static IList<APA9BallScore> Get9BallScores()
        {
            using var dbCon = new MySqlConnection(Rm.GetString("MySqlConString"));
            dbCon.Open();
            using var cmd = new MySqlCommand("SELECT * FROM scores9 order by scoredate desc limit 20", dbCon);
            var reader = cmd.ExecuteReader();
            var scores = new List<APA9BallScore>();
            while (reader.Read())
            {
                var date = reader.GetDateTime("scoredate");
                var isWin = reader.GetBoolean("result");
                var innings = reader.GetInt32("innings");
                var defenses = reader.GetInt32("defenses");
                var balls = reader.GetInt32("balls");
                var playerSL = reader.GetInt32("sl");
                var oppBalls = reader.GetInt32("oppBalls");
                var oppPlayerSL = reader.GetInt32("oppSL");
                scores.Add(new APA9BallScore(date, isWin, innings, defenses, balls, playerSL, oppBalls, oppPlayerSL));
            }
            return scores;
        }
        internal static IList<APA8BallScore> Get8BallScores()
        {
            using var dbCon = new MySqlConnection(Rm.GetString("MySqlConString"));
            dbCon.Open();
            using var cmd = new MySqlCommand("SELECT * FROM scores8 order by scoredate desc limit 20", dbCon);
            var reader = cmd.ExecuteReader();
            var scores = new List<APA8BallScore>();
            while (reader.Read())
            {
                var date = reader.GetDateTime("scoredate");
                var result = reader.GetInt32("result");
                var innings = reader.GetInt32("innings");
                var defenses = reader.GetInt32("defenses");
                var playerSL = reader.GetInt32("sl");
                var oppPlayerSL = reader.GetInt32("oppSL");
                var games = reader.GetInt32("games");
                scores.Add(new APA8BallScore(date, result, innings, defenses, playerSL, oppPlayerSL, games));
            }
            return scores;
        }
        internal static IList<APAPlayer> GetPlayers()
        {
            using var dbCon = new MySqlConnection(Rm.GetString("MySqlConString"));
            dbCon.Open();
            using var cmd = new MySqlCommand("SELECT * FROM apaplayers order by lastName asc", dbCon);
            var reader = cmd.ExecuteReader();
            var scores = new List<APAPlayer>();
            while (reader.Read())
            {
                var number = reader.GetInt32("playerId");
                var lname = reader.GetString("lastName");
                var fname = reader.GetString("firstName");
                scores.Add(new APAPlayer(number, lname, fname));
            }
            return scores;
        }
        internal static bool Add9BallScore(Player9BallScore model)
        {
            try
            {
                using var dbCon = new MySqlConnection(Rm.GetString("MySqlConString"));
                dbCon.Open();
                using var cmd = new MySqlCommand("insert into scores9 values (default,@date,@result,@innings,@defenses,@balls,@sl,@oppsl,@oppballs,@playerid,@oppid)", dbCon);
                cmd.Parameters.AddWithValue("@date", model.Date);
                var result = PointTable.GetBallsToWin(model.Sl.GetValueOrDefault()) == model.Balls ? 1 : 0;
                cmd.Parameters.AddWithValue("@result", result);
                cmd.Parameters.AddWithValue("@innings", model.Innings);
                cmd.Parameters.AddWithValue("@defenses", model.Defenses);
                cmd.Parameters.AddWithValue("@balls", model.Balls);
                cmd.Parameters.AddWithValue("@sl", model.Sl);
                cmd.Parameters.AddWithValue("@oppsl", model.OppSl);
                cmd.Parameters.AddWithValue("@oppballs", model.OppBalls);
                cmd.Parameters.AddWithValue("@playerid", model.PlayerId);
                cmd.Parameters.AddWithValue("@oppid", model.OppPlayerId);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool AddPlayer(APAPlayer newPlayer)
        {
            try
            {
                using var dbCon = new MySqlConnection(Rm.GetString("MySqlConString"));
                dbCon.Open();
                using var cmd = new MySqlCommand("insert into apaplayers values (@id,@last,@first,@score)", dbCon);
                cmd.Parameters.AddWithValue("@id", newPlayer.PlayerNumber);
                cmd.Parameters.AddWithValue("@last", newPlayer.LastName);
                cmd.Parameters.AddWithValue("@first", newPlayer.FirstName);
                cmd.Parameters.AddWithValue("@score", newPlayer.CanScoreFor);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static bool IsDuplicatePlayer(int number)
        {
            using var dbCon = new MySqlConnection(Rm.GetString("MySqlConString"));
            dbCon.Open();
            using var cmd = new MySqlCommand("SELECT count(*) FROM apaplayers where playerId=@number", dbCon);
            cmd.Parameters.AddWithValue("@number", number);
            var result = Convert.ToInt32(cmd.ExecuteScalar());
            return result == 1;
        }

        internal static bool Add8BallScore(Player8BallScore model)
        {
            try
            {
                using var dbCon = new MySqlConnection(Rm.GetString("MySqlConString"));
                dbCon.Open();
                using var cmd = new MySqlCommand("insert into scores8 values (default,@result,@date,@innings,@defenses,@sl,@oppsl,@playerid,@games,@oppid)", dbCon);
                cmd.Parameters.AddWithValue("@result", model.Points);
                cmd.Parameters.AddWithValue("@date", model.Date);
                cmd.Parameters.AddWithValue("@innings", model.Innings);
                cmd.Parameters.AddWithValue("@defenses", model.Defenses);
                cmd.Parameters.AddWithValue("@sl", model.Sl);
                cmd.Parameters.AddWithValue("@oppsl", model.OppSl);
                cmd.Parameters.AddWithValue("@playerid", model.PlayerId);
                cmd.Parameters.AddWithValue("@games", model.Games);
                cmd.Parameters.AddWithValue("@oppid", model.OppPlayerId);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}