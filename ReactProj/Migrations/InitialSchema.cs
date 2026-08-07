using FluentMigrator;

namespace ReactProj.Migrations
{
    [Migration(2026080602)]
    public class InitialSchema : Migration
    {
        public override void Up()
        {
            Create.Table("apaplayers")
                .WithColumn("playerId").AsInt32().PrimaryKey()
                .WithColumn("lname").AsString(45).Nullable()
                .WithColumn("fname").AsString(45).Nullable()
                .WithColumn("scoreable").AsBoolean().NotNullable()
                .WithColumn("curSl8").AsInt32().Nullable()
                .WithColumn("curSl9").AsInt32().Nullable()
                .WithColumn("password").AsString(255).Nullable();

            Create.Table("scores8")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("result").AsInt32().NotNullable()
                .WithColumn("scoreDate").AsDate().NotNullable()
                .WithColumn("innings").AsInt32().NotNullable()
                .WithColumn("defenses").AsInt32().NotNullable()
                .WithColumn("sl").AsInt32().NotNullable()
                .WithColumn("oppsl").AsInt32().NotNullable()
                .WithColumn("playerid").AsInt32().NotNullable()
                .WithColumn("games").AsInt32().NotNullable()
                .WithColumn("oppPlayerId").AsInt32().Nullable();

            Create.Table("scores9")
                .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                .WithColumn("scoreDate").AsDate().NotNullable()
                .WithColumn("result").AsInt16().NotNullable()
                .WithColumn("innings").AsInt32().NotNullable()
                .WithColumn("defenses").AsInt32().NotNullable()
                .WithColumn("balls").AsInt32().NotNullable()
                .WithColumn("sl").AsInt32().NotNullable()
                .WithColumn("oppsl").AsInt32().NotNullable()
                .WithColumn("oppballs").AsInt32().NotNullable()
                .WithColumn("playerid").AsInt32().NotNullable()
                .WithColumn("oppPlayerId").AsInt32().Nullable();

        }
        public override void Down()
        {
            Delete.Table("apaplayers");
            Delete.Table("scores8");
            Delete.Table("scores9");
        }
    }
}
