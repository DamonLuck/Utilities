using DL.ECS.Core;
using System;

namespace DL.ECS.Team.Scenarios.Components
{
    public class PersonComponentBuilder : IComponentBuilder<PersonComponent>
    {
        public Random _random = new Random();
        public PersonComponent Build()
        {
            return new PersonComponent()
            {
                Name = Faker.Name.FullName(),
                Age = _random.Next(5, 70)
            };
        }
    }
}
