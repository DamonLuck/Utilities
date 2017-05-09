using DL.ECS.Core;
using System;
using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios.Components
{
    public static class ComponentFactory
    {
        private static Random _playerStats = new Random();
        public static IList<Type> ComponentLookup = new List<Type>()
            {
                typeof(PlayerComponent),
                typeof(TeamComponent),
                typeof(LeagueComponent),
                typeof(LeagueMembershipComponent),
                typeof(TeamMembershipComponent),
                typeof(MatchMembershipComponent),
                typeof(MatchResultComponent),
                typeof(NameComponent)
            };

        public static IEntity CreateLeagueComponent(this IEntity entity)
            => entity.CreateName(Faker.Company.Name())
            .AddComponent(new LeagueComponent());

        public static IEntity CreatePlayerComponent(this IEntity entity)
            => entity.CreateName(Faker.Name.FullName())
                .AddComponent(new PlayerComponent(
                _playerStats.Next(100),
                _playerStats.Next(100),
                _playerStats.Next(100),
                _playerStats.Next(100),
                _playerStats.Next(100)));

        public static IEntity CreateMatchResultComponent(this IEntity entity,
            long leagueId, long homeTeamId, long awayTeamId, int homeGoals, int awayGoals)
            => entity.AddComponent(new MatchResultComponent(leagueId,
                homeTeamId,
                awayTeamId,
                homeGoals,
                awayGoals));

        public static IEntity CreateTeamComponent(this IEntity entity)
            => entity.CreateName(Faker.Address.City())
                .AddComponent(new TeamComponent());

        public static IEntity CreateLeagueMembershipComponent(this IEntity entity)
            => entity.AddComponent(new LeagueMembershipComponent(entity.EntityId.Id, true));
        public static Random rnd = new Random();
        public static IEntity CreateLeagueTeamMembershipComponent(this IEntity team, IEntity league)
            => team.AddComponent(
                new LeagueMembershipComponent(league.EntityId.Id, false, 0, 0, 0, 0, 0));

        public static IEntity CreateTeamMembershipComponent(this IEntity entity)
            => entity.AddComponent(new TeamMembershipComponent(entity.EntityId.Id, true));

        public static IEntity CreateTeamPlayerMembershipComponent(this IEntity player,
            IEntity team)
            => player.AddComponent(new TeamMembershipComponent(team.EntityId.Id, false));

        public static IEntity CreateFixture(this IEntity fixture,
            int gameTurn, long league, long team1, long team2)
            => fixture.AddComponent(
                new MatchMembershipComponent(
                    gameTurn, league, team1, team2, 0, 0));

        public static IEntity CreateName(this IEntity entity, string name)
           => entity.AddComponent(new NameComponent(name));
    }
}
