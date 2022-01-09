using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIFootball.DataAcces;
using WebAPIFootball.Model;

namespace WebAPIFootball.Data
{
    public class TeamRepo: ITeamRepo
    {
        
        protected FootbalContext ctx;

        public TeamRepo(FootbalContext ctx)
        {
            this.ctx = ctx;
        }

        
        public async Task<IList<Team>> ReadAllTeamss(int Rating, string TeamName)
        {
            List<Team> teams = ctx.teams.ToList();
            return teams;
        }

        public async Task<Team> AddTeam(Team addTeam)
        {
            await ctx.teams.AddAsync(addTeam);
            await ctx.SaveChangesAsync();
            Console.WriteLine(addTeam);
            return addTeam;
        }
        
        public async Task<IList<Team>> ReadAllTeams(int Rating, string TeamName)
        {
            if (Rating == 0 && TeamName==null)
            {
                return await ctx.teams.Include(t=> t.Players).ToListAsync();
            }

            if (Rating == 0)
            {
                return ctx.teams.Where(t=>t.TeamName.Contains(TeamName)).Include(t=> t.Players).ToList();
            }

            if (TeamName==null)
            {
                return ctx.teams.Where(t => t.Ranking >= Rating).Include(t=> t.Players).ToList();
            }
            IList<Team> returned = ctx.teams.Where(t => t.Ranking >= Rating).Where(t=>t.TeamName.Contains(TeamName)).ToList();
            Console.WriteLine("Rating"+Rating);
            Console.WriteLine("name"+TeamName);
            return returned;
        }
    }
    }