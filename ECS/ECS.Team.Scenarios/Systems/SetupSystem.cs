using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Domain;
using DL.Infrastructure;

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
            _context.Players.Create(NUMBER_OF_PLAYERS);
            _context.Teams.Create(NUMBER_OF_TEAMS, NUMBER_OF_PLAYERS_PER_TEAM);
            _context.League.Create(NUMBER_OF_LEAGUES, NUMBER_OF_TEAMS_PER_LEAGUE);
        }
    }
}
