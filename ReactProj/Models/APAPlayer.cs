
namespace ReactProj.Models
{
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
        public int Sl8 { get; set; }
        public int Sl9 { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
    }
}
