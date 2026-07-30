using System.Collections;

namespace ReactProj.Models
{
    public class ScoreModel
    {
        public ScoreModel() { }
        public ScoreModel(IList<APAPlayer> players)
        {
            Players = players;
        }
        public IList<APAPlayer>? Players { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int? Balls { get; set; }
        public int? OppBalls { get; set; }
        public int? Innings { get; set; }
        public int? Defenses { get; set; }
        public int? Sl { get; set; }
        public int? OppSl { get; set; }
        public int PlayerId { get; set; }

    }
    public abstract class PlayerScore
    {
        protected PlayerScore()
        {
            Date = DateTime.Today;
            Innings = null;
            Defenses = null;
            Sl = null;
            OppSl = null;
            OppPlayerId = null;
        }
        public DateTime Date { get; set; } = DateTime.Now;
        public int? Innings { get; set; }
        public int? Defenses { get; set; }
        public int? Sl { get; set; }
        public int? OppSl { get; set; }
        public int? OppPlayerId { get; set; }
        public virtual bool IsValid()
        {
            if (!Innings.HasValue)
                return false;
            if (!Defenses.HasValue)
                return false;
            if (!Sl.HasValue)
                return false;
            if (!OppSl.HasValue)
                return false;
            if (!OppPlayerId.HasValue)
                return false;
            return true;
        }
        public int PlayerId { get; set; }

    }
    public class Player9BallScore : PlayerScore
    {
        public Player9BallScore() : base()
        {
            Balls = null;
            OppBalls = null;
        }
        public int? Balls { get; set; }
        public int? OppBalls { get; set; }

        public override bool IsValid()
        {
            if (!Balls.HasValue)
                return false;
            if (!OppBalls.HasValue)
                return false;
            return base.IsValid();
        }
    }
    public class Player8BallScore : PlayerScore
    {
        public Player8BallScore() : base()
        {
            Points = null;
            Games = null;
        }
        public int? Points { get; set; }
        public int? Games { get; set; }

        public override bool IsValid()
        {
            if (!Games.HasValue)
                return false;
            if (!Points.HasValue)
                return false;
            return base.IsValid();
        }
    }
}
