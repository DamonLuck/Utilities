using DL.ECS.Core.Components;
using DL.ECS.Core.Tests.TestComponents;
using Xunit;
using System.Linq;
using Faker;
using System.Collections.Generic;
using FluentAssertions;
using DL.Infrastructure;

namespace DL.ECS.Core.Tests.Scenario
{
    public class S1TeamAndPlayerCreation
    {
        private ComponentId _playerComponentIndex;
        private ComponentId _teamComponentIndex;
        private Context _context;
        private int _componentCount = 2;

        public S1TeamAndPlayerCreation()
        {
            _playerComponentIndex = new ComponentId(0);
            _teamComponentIndex = new ComponentId(1);
            _context = new Context(_componentCount);
        }

        [Fact]
        public void GetEntitiesByComponent_Will_MatchCreatedEntites()
        {
            int teamCount = 3;
            int playerCount = 5;

            IEnumerable<IEntity> teams = AddTeams(teamCount);
            IEnumerable<IEntity> players = AddPlayers(playerCount);

            IEnumerable<IEntity> result = _context.GetEntitiesByComponent(_playerComponentIndex);
            result.ToList().Should().BeEquivalentTo(players.ToList());

            result = _context.GetEntitiesByComponent(_teamComponentIndex);
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
                result.Add(AddTeam(_context, _teamComponentIndex));

            return result;
        }

        private IEnumerable<IEntity> AddPlayers(int count)
        {
            List<IEntity> result = new List<IEntity>();
            for (int i = 0; i < count; i++)
                result .Add(AddPlayer(_context, _playerComponentIndex));

            return result;
        }

        private IEntity AddPlayer(Context context, ComponentId index)
        {
            PlayerComponent player = new PlayerComponent() { Name = Name.FullName() };
            return context.Create().AddComponent(player, index);
        }

        private IEntity AddTeam(Context context, ComponentId index)
        {
            TeamComponent team = new TeamComponent() { Name = Address.City() };
            return context.Create().AddComponent(team, index);
        }
    }
}
