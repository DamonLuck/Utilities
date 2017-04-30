using DL.ECS.Core;
using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Domain;

namespace DL.ECS.Team.Scenarios.Systems
{
    public enum GameState
    {
        Setup,
        Fixture,
        Match,
        Result
    }

    public class GameStateSystem : ISystem
    {
        private FixtureSystem _fixtureSystem;
        private MatchSystem _matchSystem;
        private ResultSystem _resultSystem;
        private GameState _gameState;
        private SetupSystem _setupSystem;

        public GameStateSystem(DomainContext context)
        {
            _setupSystem = new SetupSystem(context);
            _fixtureSystem = new FixtureSystem(context);
            _matchSystem = new MatchSystem(context);
            _resultSystem = new ResultSystem(context);
            _gameState = GameState.Setup;
        }

        public void Execute()
        {
            switch (_gameState)
            {
                case GameState.Setup:
                    {
                        _setupSystem.Execute();
                        _gameState = GameState.Fixture;
                        break;
                    }

                case GameState.Fixture:
                    {
                        _fixtureSystem.Execute();
                        _gameState = GameState.Match;
                        return;
                    }

                case GameState.Match:
                    {
                        _matchSystem.Execute();
                        _gameState = GameState.Result;
                        return;
                    }

                default:
                    {
                        _resultSystem.Execute();
                        _gameState = GameState.Fixture;
                        break;
                    }
            }
        }
    }
}
