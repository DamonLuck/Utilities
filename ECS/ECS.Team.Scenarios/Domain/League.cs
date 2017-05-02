using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class LeagueModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class League
    {
        private Context _context;

        public League(Context context)
        {
            _context = context;
        }

        public void Create(int numberOfLeagues, int numberOfTeamsPerLeague)
        {
            int skip = 0;
            IEnumerable<IEntity> teams = _context.GetEntitiesByComponent<TeamComponent>();
            for (int i = 0; i < numberOfLeagues; i++)
            {
                IEntity league = _context.Create()
                    .AddComponent(ComponentFactory.CreateLeagueComponent())
                    .CreateLeagueMembershipComponent();
                LeagueMembershipComponent leagueTeamMembership =
                    ComponentFactory.CreateLeagueTeamMembershipComponent(league);
                AddTeamsToLeague(numberOfTeamsPerLeague, skip, teams, leagueTeamMembership);
                skip += numberOfTeamsPerLeague;
            }
        }

        private static void AddTeamsToLeague(int numberOfTeamsPerLeague, int skip, IEnumerable<IEntity> teams, LeagueMembershipComponent leagueTeamMembership)
        {
            teams.Skip(skip).Take(numberOfTeamsPerLeague).ToList().ForEach(
                x => x.AddComponent(leagueTeamMembership));
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
