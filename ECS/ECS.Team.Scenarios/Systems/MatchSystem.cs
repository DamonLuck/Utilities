using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Domain;
using System;
using System.Collections.Generic;
using DL.ECS.Core.Entity;

namespace DL.ECS.Team.Scenarios.Systems
{
    public class MatchSystem : ISystem
    {
        private readonly DomainContext _context;
        private Random _random = new Random();
        public MatchSystem(DomainContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            IEnumerable<IEntity> gameEntities = _context
                .GetEntitiesByComponent<MatchMembershipComponent>
                (x => x.GameTurn == GameTurnSystem.CurrentTurn);

            foreach(var gameEntity in gameEntities)
            {
                MatchMembershipComponent match = gameEntity.GetComponent<MatchMembershipComponent>();
                if (match.IsGamePlayed)
                    throw new InvalidOperationException("Cannot play game that has already been played");

                int homeGoals = _random.Next(4);
                int awayGoals = _random.Next(4);

                // Play match
                gameEntity.ReplaceComponent(new MatchMembershipComponent
                    (match.GameTurn, match.LeagueId, match.HomeTeamId, match.AwayTeamId,
                        homeGoals, awayGoals, true));

                _context.Create()
                    .CreateMatchResultComponent(match.LeagueId, match.HomeTeamId, 
                        match.AwayTeamId, homeGoals, awayGoals);
            }
        }

    }
}
