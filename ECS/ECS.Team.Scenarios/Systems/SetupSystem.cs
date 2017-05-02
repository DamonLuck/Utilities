using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Domain;
using DL.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Systems
{
    public class SetupSystem : ISystem
    {
        private readonly DomainContext _context;
        private const int NUMBER_OF_PLAYERS_PER_TEAM = 15;
        private const int NUMBER_OF_TEAMS_PER_LEAGUE = 20;
        private const int NUMBER_OF_LEAGUES = 5;
        private const int NUMBER_OF_TEAMS = NUMBER_OF_TEAMS_PER_LEAGUE * NUMBER_OF_LEAGUES;
        private const int NUMBER_OF_PLAYERS = NUMBER_OF_TEAMS * NUMBER_OF_PLAYERS_PER_TEAM;

        public SetupSystem(DomainContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            AmbientLogger.SystemNotification.SystemSetup();
            CreatePlayers();
            CreateTeams();
            CreateLeagues();
        }

        private void CreatePlayers()
        {
            for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
                _context.Players.Create();
        }

        private void CreateTeams()
        {
            int skip = 0;
            IEnumerable<IEntity> players = _context.GetEntitiesByComponent<PlayerComponent>();

            for (int i = 0; i < NUMBER_OF_TEAMS; i++)   
            {
                _context.Teams.Create(players.Skip(skip).Take(NUMBER_OF_PLAYERS_PER_TEAM));
                skip += NUMBER_OF_PLAYERS_PER_TEAM;
            }
        }

        private void CreateLeagues()
        {
            int skip = 0;
            IEnumerable<IEntity> teams = _context.GetEntitiesByComponent<TeamComponent>();

            for (int i = 0; i < NUMBER_OF_LEAGUES; i++)
            {
                _context.League.Create(teams.Skip(skip).Take(NUMBER_OF_TEAMS_PER_LEAGUE));
                skip += NUMBER_OF_TEAMS_PER_LEAGUE;
            }
        }
    }
}
