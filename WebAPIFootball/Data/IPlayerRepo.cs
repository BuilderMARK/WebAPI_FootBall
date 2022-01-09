using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIFootball.Model;

namespace WebAPIFootball.Data
{
    public interface IPlayerRepo
    {
        Task<IList<Player>> ReadAllPlayers();
        Task<Player> AddPlayer(Player addPlayer, string teamname);
        Task DeletePlayer(int deletePlayer);
    }
}