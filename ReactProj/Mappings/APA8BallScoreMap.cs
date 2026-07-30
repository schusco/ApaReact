using Dapper.FluentMap.Mapping;
using ReactProj.Models;

namespace ReactProj.Mappings
{
    public class APA8BallScoreMap : EntityMap<APA8BallScore>
    {
        public APA8BallScoreMap()
        {
            Map(m => m.Date).ToColumn("scoreDate");
            Map(m => m.PlayerSL).ToColumn("sl");
            Map(m => m.OppPlayerSL).ToColumn("oppsl");
            Map(m => m.Points).ToColumn("result");
        }
    }
    
}
