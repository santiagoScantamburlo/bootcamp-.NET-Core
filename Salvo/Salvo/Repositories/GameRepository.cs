﻿using Microsoft.EntityFrameworkCore;
using Salvo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(SalvoContext repositoryContext) : base(repositoryContext) { }
        public IEnumerable<Game> GetAllGames()
        {
            return FindAll()
                .OrderBy(game => game.CreationDate)
                .ToList();
        }

        public IEnumerable<Game> GetAllGamesWithPlayers()
        {
            return FindAll(source => source.Include(game => game.GamePlayers)
                .ThenInclude(gamePlayer => gamePlayer.Player))
                .OrderBy(game => game.CreationDate)
                .ToList();

        }
    }
}