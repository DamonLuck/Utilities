using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{

    public class League
    {
        private Context _context;

        public League(Context context)
        {
            _context = context;
        }

        public void Create(IEnumerable<IEntity> teams)
        {
            IEntity league = _context.Create()
                    .CreateLeagueComponent()
                    .CreateLeagueMembershipComponent();
            AddTeamsToLeague(league, teams);
        }

        private static void AddTeamsToLeague(IEntity league, IEnumerable<IEntity> teams)
        {
            teams.ToList().ForEach(
                    x => x.CreateLeagueTeamMembershipComponent(league));
        }

        public IEnumerable<LeagueModel> GetAll()
        {
            return _context.GetEntitiesByComponent<LeagueComponent>()
                .Select(x => new LeagueModel()
                {
                    Id = x.EntityId.Id,
                    Name = x.GetComponent<LeagueComponent>().Name
                });
        }
    }
}
