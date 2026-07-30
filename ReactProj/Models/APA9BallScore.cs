namespace ReactProj.Models
{
    public class APA9BallScore
    {
        public APA9BallScore() { }
        public APA9BallScore(DateTime date, bool isWin, int innings, int defenses, int balls, int playerSL, int oppBalls, int oppPlayerSL)
        {
            Date = date;
            IsWin = isWin;
            Innings = innings;
            Defenses = defenses;
            Balls = balls;
            PlayerSL = playerSL;
            OppBalls = oppBalls;
            OppPlayerSL = oppPlayerSL;
        }
        public DateTime Date { get; set; }
        public bool IsWin { get; set; }
        public int Innings { get; set; }
        public int Defenses { get; set; }
        public int Balls { get; set; }
        public int PlayerSL { get; set; }
        internal int OppBalls { get; set; }
        public int OppPlayerSL { get; set; }
        public int Points
        {
            get
            {
                var sl = IsWin ? OppPlayerSL : PlayerSL;
                var balls = IsWin ? OppBalls : Balls;
                var loserPoints = _pointsTable.GetScore(sl, balls);
                return IsWin ? 20 - loserPoints : loserPoints;
            }
        }
        private static readonly PointTable _pointsTable = new();
        public override string ToString() => $"Balls made {Balls}, Opponent {OppBalls} Score: {Points}";

    }
}
