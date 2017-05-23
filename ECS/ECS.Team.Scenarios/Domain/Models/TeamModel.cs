namespace DL.ECS.Team.Scenarios.Domain.Models
{
    public class TeamModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int P
        {
            get
            {
                return W + D + L;
            }
        }
        public int Pts
        {
            get
            {
                return W * 3 + D * 1;
            }
        }

        public int W { get; set; }
        public int D { get; set; }
        public int L { get; set; }
        public int Gf { get; set; }
        public int Ga { get; set; }

    }
}
