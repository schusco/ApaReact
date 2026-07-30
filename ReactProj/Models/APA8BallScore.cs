namespace ReactProj.Models
{
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
}
