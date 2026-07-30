using Dapper.FluentMap.Mapping;
using ReactProj.Models;

namespace ReactProj.Mappings
{
    public class APAPlayerMap:EntityMap<APAPlayer>
    {
        public APAPlayerMap()
        {
            Map(m => m.PlayerNumber).ToColumn("playerId");
            Map(m => m.CanScoreFor).ToColumn("scoreable");
            Map(m => m.Sl8).ToColumn("curSl8");
            Map(m => m.Sl9).ToColumn("curSl9");
        }
    }
}
