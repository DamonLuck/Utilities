using DL.ECS.Core;
using DL.ECS.Team.Scenarios.Components;
using System.Collections.Generic;
using System.Linq;

namespace DL.ECS.Team.Scenarios.Domain
{
    public class PlayerModel
    {
        public long Id { get; set; }
        public long TeamId { get; set; }
        public string Name { get; set; }
        public bool IsCaptain { get; set; }
        public int Handling { get; set; }
        public int Tacking { get; set; }
        public int Passing { get; set; }
        public int Shooting { get; set; }
        public int Stamina { get; set; }
    }
}
