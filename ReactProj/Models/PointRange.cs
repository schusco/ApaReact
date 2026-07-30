namespace ReactProj.Models
{
    public class PointRange(int sl, int low, int high, int points)
    {
        public int SL { get; set; } = sl;
        public int Low { get; set; } = low;
        public int High { get; set; } = high;
        public int Points { get; set; } = points;
    }
}
