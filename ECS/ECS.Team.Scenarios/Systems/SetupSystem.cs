﻿using DL.ECS.Core.Systems;
using DL.ECS.Team.Scenarios.Components;
using DL.ECS.Team.Scenarios.Domain;
using DL.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using DL.ECS.Core.Entity;
using DL.ECS.Team.Scenarios.Domain.Models;

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
        private const int NUMBER_OF_TURNS = 10;

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
            GenerateFixtures();
        }

        private void CreatePlayers()
        {
            for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
                _context.Players.Create();
        }

        private void CreateTeams()
        {
            int skip = 0;
            IEnumerable<IEntity> players = _context.GetEntitiesByComponent<PlayerComponent>().ToList();

            for (int i = 0; i < NUMBER_OF_TEAMS; i++)
            {
                _context.Teams.Create(players.Skip(skip).Take(NUMBER_OF_PLAYERS_PER_TEAM));
                skip += NUMBER_OF_PLAYERS_PER_TEAM;
            }
        }

        private void CreateLeagues()
        {
            int skip = 0;
            IEnumerable<IEntity> teams = _context.GetEntitiesByComponent<TeamComponent>().ToList();

            for (int i = 0; i < NUMBER_OF_LEAGUES; i++)
            {
                _context.League.Create(teams.Skip(skip).Take(NUMBER_OF_TEAMS_PER_LEAGUE));
                skip += NUMBER_OF_TEAMS_PER_LEAGUE;
            }
        }

        private void GenerateFixtures()
        {
            var leagues = _context.League.GetAll().ToList();
            foreach (var league in leagues)
            {
                var teams = _context.Teams.GetAll(league.Id).ToList();
                FixtureGenerator(league, teams);
            }
        }

        // http://bluebones.net/2005/05/generating-fixture-lists/
        private void FixtureGenerator(LeagueModel league, IEnumerable<TeamModel> teams)
        {
            TeamModel[] teamsArray = teams.ToArray();
            int numberOfTeams = teamsArray.Length;

            if (numberOfTeams % 2 == 1)
            {
                numberOfTeams++;
            }

            int totalRounds = numberOfTeams - 1;
            int matchesPerRound = numberOfTeams / 2;
            for (int round = 0; round < totalRounds; round++)
            {
                for (int match = 0; match < matchesPerRound; match++)
                {
                    int home = (round + match) % (numberOfTeams - 1);
                    int away = (numberOfTeams - 1 - match + round) % (numberOfTeams - 1);
                    // Last team stays in the same place while the others
                    // rotate around it.
                    if (match == 0)
                    {
                        away = numberOfTeams - 1;
                    }
                    // Add one so teams are number 1 to teams not 0 to teams - 1
                    // upon display.
                    _context.Create().CreateFixture(round, league.Id, 
                        teamsArray[home].Id, 
                        teamsArray[away].Id);
                }
            }
        }
    }
}
