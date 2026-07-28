
namespace ReactProj
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
    public class APA8BallScore
    {
        public APA8BallScore() { }
        public APA8BallScore(DateTime date, int result, int innings, int defenses, int playerSL, int oppPlayerSL, int games)
        {
            Date = date;
            Points = result;
            Innings = innings;
            Defenses = defenses;
            PlayerSL = playerSL;
            OppPlayerSL = oppPlayerSL;
            Games = games;
        }
        public DateTime Date { get; set; }
        public bool IsWin => Points >= 2;
        public int Innings { get; set; }
        public int Defenses { get; set; }
        public int PlayerSL { get; set; }
        public int OppPlayerSL { get; set; }
        public int Points { get; set; }
        public int Games { get; set; }
        public override string ToString() => $"Opponent SL {OppPlayerSL} Score: {Points}";
    }
    public class PointTable
    {
        public PointTable()
        {
            for (int sl = 1; sl <= PointsPerLevel.Count; sl++)
            {
                var pointRange = PointsPerLevel[sl - 1];                
                var low = 0;
                for (int points = 0; points < pointRange.Length; points++)
                {
                    int high = pointRange[points] - 1;
                    _ranges.Add(new ApaPointRange(sl, low, high, points));
                    low = pointRange[points];
                };
            }
        }
        public int GetScore(int sl, int balls)
        {
            var range = _ranges.FirstOrDefault(r => r.SL == sl && balls >= r.Low && balls <= r.High);
            if (range != null)
            {
                return range.Points;
            }
            return 0;
        }
        private static List<int[]> PointsPerLevel
        {
            get
            {
                var pts = new List<int[]>
                {
                    new int[] { 3, 4, 5, 7, 8, 9, 11, 12, 14 },
                    new int[] { 4, 6, 8, 9, 11, 13, 15, 17, 19 },
                    new int[] { 5, 7, 10, 12, 15, 17, 20, 22, 25 },
                    new int[] { 6, 9, 12, 15, 19, 22, 25, 28, 31 },
                    new int[] { 7, 11, 15, 19, 23, 27, 30, 34, 38 },
                    new int[] { 9, 13, 18, 23, 28, 32, 37, 41, 46 },
                    new int[] { 11, 16, 22, 27, 33, 38, 44, 50, 55 },
                    new int[] { 14, 20, 27, 33, 40, 46, 53, 59, 65 },
                    new int[] { 18, 25, 32, 39, 47, 54, 61, 68, 75 }
                };
                return pts;
            }
        }
        internal static int? GetBallsToWin(int sl)
        {
            if (sl < 1 || sl > 9)
                throw new ArgumentOutOfRangeException(nameof(sl), "9 ball skill levels are from 1 to 9");
            return _ballsToWin[sl - 1];
        }
        internal static int[] _ballsToWin = new int[] { 14, 19, 25, 31, 38, 46, 55, 65, 75 };
        private readonly List<ApaPointRange> _ranges = new();
    }
    public class ApaPointRange
    {
        public ApaPointRange(int sl, int low, int high, int points)
        {
            SL = sl;
            Low = low;
            High = high;
            Points = points;
        }
        public int SL { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public int Points { get; set; }
    }
    public class APAPlayer
    {
        public APAPlayer() { }
        public APAPlayer(int number, string lname, string fname, bool scorable = false)
        {
            PlayerNumber = number;
            LastName = lname;
            FirstName = fname;
            CanScoreFor = scorable;
        }
        public int? PlayerNumber { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public bool CanScoreFor { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
    }
}

