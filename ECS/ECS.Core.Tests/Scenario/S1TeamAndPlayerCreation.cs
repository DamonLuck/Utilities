using DL.ECS.Core.Tests.TestComponents;
using Xunit;
using System.Linq;
using Faker;
using System.Collections.Generic;
using FluentAssertions;
using System;
using DL.ECS.Core.Entity;

namespace DL.ECS.Core.Tests.Scenario
{
    public class S1TeamAndPlayerCreation
    {
        private Context.Context _context;

        public S1TeamAndPlayerCreation()
        {
            _componentLookup.Add(typeof(PlayerComponent));
            _componentLookup.Add(typeof(TeamComponent));

            _context = new Context.Context(_componentLookup);
        }

        [Fact]
        public void GetEntitiesByComponent_Will_MatchCreatedEntites()
        {
            int teamCount = 3;
            int playerCount = 5;

            IEnumerable<IEntity> teams = AddTeams(teamCount);
            IEnumerable<IEntity> players = AddPlayers(playerCount);

            IEnumerable<IEntity> result = _context.GetEntitiesByComponent<PlayerComponent>();
            result.ToList().Should().BeEquivalentTo(players.ToList());

            result = _context.GetEntitiesByComponent<TeamComponent>();
            result.ToList().Should().BeEquivalentTo(teams.ToList());
        }

        private IEnumerable<IEntity> AddTeams(int count)
        {
            List<IEntity> result = new List<IEntity>();
            for (int i = 0; i < count; i++)
                result.Add(AddTeam(_context));

            return result;
        }

        private IEnumerable<IEntity> AddPlayers(int count)
        {
            List<IEntity> result = new List<IEntity>();
            for (int i = 0; i < count; i++)
                result .Add(AddPlayer(_context));

            return result;
        }

        private IEntity AddPlayer(Context.Context context)
        {
            PlayerComponent player = new PlayerComponent() { Name = Name.FullName() };
            return context.Create().AddComponent(player);
        }

        private IEntity AddTeam(Context.Context context)
        {
            TeamComponent team = new TeamComponent() { Name = Address.City() };
            return context.Create().AddComponent(team);
        }

        private IList<Type> _componentLookup
            = new List<Type>();
    }
}
