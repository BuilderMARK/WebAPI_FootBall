using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIFootball.Model;

namespace WebAPIFootball.Data
{
    public interface ITeamRepo
    {
        Task<IList<Team>> ReadAllTeams(int Rating, string TeamName);
        
        Task<Team> AddTeam(Team addTeam);
    }
}