﻿using Microsoft.EntityFrameworkCore;
using Salvo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Repositories
{
    public class GamePlayerRepository : RepositoryBase<GamePlayer>, IGamePlayerRepository
    {
        public GamePlayerRepository(SalvoContext repositoryContext) : base(repositoryContext) { }

        public GamePlayer FindById(long idGamePlayer)
        {
            return FindByCondition(gamePlayer => gamePlayer.Id == idGamePlayer)
                .Include(gamePlayer => gamePlayer.Game)
                    .ThenInclude(game => game.GamePlayers)
                        .ThenInclude(gamePlayer => gamePlayer.Salvos)
                            .ThenInclude(salvo => salvo.Locations)
                .Include(gamePlayer => gamePlayer.Game)
                    .ThenInclude(game => game.GamePlayers)
                        .ThenInclude(gamePlayer => gamePlayer.Ships)
                            .ThenInclude(ship => ship.Locations)
                .Include(gamePlayer => gamePlayer.Player)
                .Include(gamePlayer => gamePlayer.Ships)
                .Include(gamePlayer => gamePlayer.Salvos)
                .FirstOrDefault();
        }

        public GamePlayer GetGamePlayerView(long idGamePlayer)
        {
            return FindAll(
                source => source.
                Include(gamePlayer => gamePlayer.Ships)
                    .ThenInclude(ship => ship.Locations)
                .Include(gamePlayer => gamePlayer.Salvos)
                    .ThenInclude(salvo => salvo.Locations)
                .Include(gamePlayer => gamePlayer.Game)
                    .ThenInclude(game => game.GamePlayers)
                        .ThenInclude(gp => gp.Player)
                .Include(gamePlayer => gamePlayer.Game)
                    .ThenInclude(game => game.GamePlayers)
                        .ThenInclude(gp => gp.Salvos)
                            .ThenInclude(salvo => salvo.Locations)
                .Include(gamePlayer => gamePlayer.Game)
                    .ThenInclude(game => game.GamePlayers)
                        .ThenInclude(gp => gp.Ships)
                            .ThenInclude(ship => ship.Locations))
                .Where(gamePlayer => gamePlayer.Id == idGamePlayer)
                .OrderBy(game => game.JoinDate)
                .FirstOrDefault();
        }

        public void Save(GamePlayer gamePlayer)
        {
            if (gamePlayer.Id == 0)
            {
                Create(gamePlayer);
            }
            else {
                Update(gamePlayer);
            }
            SaveChanges();
        }
    }
}
