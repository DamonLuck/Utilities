using DL.ECS.Core;
using System;
using System.Collections.Generic;

namespace DL.ECS.Team.Scenarios.Components
{
    public static class ComponentFactory
    {
        public static IList<Type> ComponentLookup = new List<Type>()
            {
                typeof(PlayerComponent),
                typeof(TeamComponent),
                typeof(LeagueComponent),
                typeof(LeagueMembershipComponent),
                typeof(TeamMembershipComponent)
            };

        public static LeagueComponent CreateLeagueComponent()
            => new LeagueComponent() { Name = Faker.Company.Name() };

        public static PlayerComponent CreatePlayerComponent()
            => new PlayerComponent() { Name = Faker.Name.FullName() };

        public static TeamComponent CreateTeamComponent()
            => new TeamComponent() { Name = Faker.Address.City()};

        public static IEntity CreateLeagueMembershipComponent(this IEntity entity)
            => entity.AddComponent(new LeagueMembershipComponent(entity.EntityId.Id, true));

        public static LeagueMembershipComponent CreateLeagueTeamMembershipComponent(IEntity league)
            => new LeagueMembershipComponent(league.EntityId.Id, false,0,0,0,0,0);

        public static IEntity CreateTeamMembershipComponent(this IEntity entity)
            => entity.AddComponent(new TeamMembershipComponent(entity.EntityId.Id, true));

        public static TeamMembershipComponent CreateTeamPlayerMembershipComponent(IEntity team)
            => new TeamMembershipComponent(team.EntityId.Id, false);
    }
}
