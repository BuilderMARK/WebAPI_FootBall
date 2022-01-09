using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIFootball.DataAcces;
using WebAPIFootball.Model;

namespace WebAPIFootball.Data
{
    public class PlayerRepo : IPlayerRepo
    {

        protected FootbalContext ctx;

        public PlayerRepo(FootbalContext ctx)
        {
            this.ctx = ctx;
        }
        
        public async Task<IList<Player>> ReadAllPlayers()
        {
            List<Player> players = ctx.Players.ToList();
            return players;
        }

        public async Task<Player> AddPlayer(Player addPlayer, string teamname)
        {
            await ctx.Players.AddAsync(addPlayer);
            await ctx.SaveChangesAsync();
            Console.WriteLine("Add player Player Repo" + addPlayer);
            Console.WriteLine("TeamName Player Repo"+ teamname);
            return addPlayer;
        }

        public async Task DeletePlayer(int deletePlayer)
        {
            Player firstAsync = await ctx.Players.FirstOrDefaultAsync(p => p.PlayerID == deletePlayer);
            ctx.Remove(firstAsync);
            await ctx.SaveChangesAsync();
        }
    }
}