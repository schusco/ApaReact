namespace ReactProj.Models
{
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
                    _ranges.Add(new PointRange(sl, low, high, points));
                    low = pointRange[points];
                }
            }
        }
        public int GetScore(int sl, int balls)
        {
            var range = _ranges.FirstOrDefault(r => r.SL == sl && balls >= r.Low && balls <= r.High);
            return range != null ? range.Points : 0;
        }
        private static List<int[]> PointsPerLevel => [pointsSl1, pointsSl2, pointsSl3, pointsSl4, pointsSl5, pointsSl6, pointsSl7, pointsSl8, pointsSl9];
        internal static int? GetBallsToWin(int sl)
        {
            if (sl < 1 || sl > 9)
                throw new ArgumentOutOfRangeException(nameof(sl), "9 ball skill levels are from 1 to 9");
            return _ballsToWin[sl - 1];
        }
        internal static int[] _ballsToWin = [14, 19, 25, 31, 38, 46, 55, 65, 75];
        private readonly List<PointRange> _ranges = [];
        private static readonly int[] pointsSl9 = [18, 25, 32, 39, 47, 54, 61, 68, 75];
        private static readonly int[] pointsSl8 = [14, 20, 27, 33, 40, 46, 53, 59, 65];
        private static readonly int[] pointsSl7 = [11, 16, 22, 27, 33, 38, 44, 50, 55];
        private static readonly int[] pointsSl6 = [9, 13, 18, 23, 28, 32, 37, 41, 46];
        private static readonly int[] pointsSl5 = [7, 11, 15, 19, 23, 27, 30, 34, 38];
        private static readonly int[] pointsSl4 = [6, 9, 12, 15, 19, 22, 25, 28, 31];
        private static readonly int[] pointsSl3 = [5, 7, 10, 12, 15, 17, 20, 22, 25];
        private static readonly int[] pointsSl2 = [4, 6, 8, 9, 11, 13, 15, 17, 19];
        private static readonly int[] pointsSl1 = [3, 4, 5, 7, 8, 9, 11, 12, 14];
    }
}
