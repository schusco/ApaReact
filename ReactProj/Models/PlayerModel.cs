
namespace ReactProj.Models
{
    public class PlayerModel
    {
        public PlayerModel(APAPlayer player)
        {
            Player = player;
        }

        public APAPlayer Player { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }

        internal PlayerModel SetError(string error)
        {
            Success = false;
            Error = error;
            return this;
        }
    }
}
