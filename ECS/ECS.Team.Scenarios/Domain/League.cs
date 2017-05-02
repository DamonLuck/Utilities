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
        private readonly ComponentFactory _componentFactory;

        public League(Context context, ComponentFactory componentFactory)
        {
            _context = context;
            _componentFactory = componentFactory;
        }

        public void Create(int numberOfLeagues, int numberOfTeamsPerLeague)
        {
            int skip = 0;
            IComponentBuilder<LeagueComponent> builder = 
                _componentFactory.LeagueComponentBuilder();
            IEnumerable<IEntity> teams = _context.GetEntitiesByComponent<TeamComponent>();
            for (int i = 0; i < numberOfLeagues; i++)
            {
                IEntity league = _context.Create()
                    .AddComponent<LeagueComponent>(builder.Build());
                league.AddComponent<LeagueMembershipComponent>(
                    new LeagueMembershipComponent(league.EntityId.Id, true));
                LeagueMembershipComponent leagueTeamMembership = 
                    new LeagueMembershipComponent(league.EntityId.Id, false);
                var teamEntities = teams.Skip(skip).Take(numberOfTeamsPerLeague);
                teamEntities.ToList().ForEach(
                    x => x.AddComponent<LeagueMembershipComponent>(leagueTeamMembership));
                skip += numberOfTeamsPerLeague;
            }
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
