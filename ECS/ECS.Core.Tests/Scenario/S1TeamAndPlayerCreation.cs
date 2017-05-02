using DL.ECS.Core.Tests.TestComponents;
using Xunit;
using System.Linq;
using Faker;
using System.Collections.Generic;
using FluentAssertions;
using System;

namespace DL.ECS.Core.Tests.Scenario
{
    public class S1TeamAndPlayerCreation
    {
        private Context _context;

        public S1TeamAndPlayerCreation()
        {
            _componentLookup.Add(typeof(PlayerComponent));
            _componentLookup.Add(typeof(TeamComponent));

            _context = new Context(_componentLookup);
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

        [Fact]
        public void EntitiesWithSet_Matches_ThoseAddedToTheSet()
        {
            int teamCount = 3;
            int playerCount = 5;

            IEnumerable<IEntity> teams = AddTeams(teamCount);
            IEnumerable<IEntity> players = AddPlayers(playerCount);

            var set = _context.CreateSet()
                .AddEntities(teams)
                .AddEntities(players);

            set.GetEntities().Should().BeEquivalentTo(teams.Union(players));
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

        private IEntity AddPlayer(Context context)
        {
            PlayerComponent player = new PlayerComponent() { Name = Name.FullName() };
            return context.Create().AddComponent<PlayerComponent>(player);
        }

        private IEntity AddTeam(Context context)
        {
            TeamComponent team = new TeamComponent() { Name = Address.City() };
            return context.Create().AddComponent(team);
        }

        private IList<Type> _componentLookup
            = new List<Type>();
    }
}
