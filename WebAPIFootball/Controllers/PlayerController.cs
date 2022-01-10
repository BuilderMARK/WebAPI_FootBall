using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIFootball.DataAcces;
using WebAPIFootball.Model;

namespace WebAPIFootball.Controllers
{
     [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private FootbalContext _context;

        public PlayerController()
        {
            _context = new FootbalContext();
        }
        
        [HttpPost]
        [Route("{teamName}")]
        public async Task<ActionResult<Player>> PostAddAsync([FromBody] Player player, [FromRoute] string teamName)
        {
            Console.WriteLine($"Attempting to put {player} in Database with teamName {teamName}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Team teamReturn = await _context.teams.Include(t => t.Players).FirstAsync(t => t.TeamName.Equals(teamName));
                teamReturn.Players.Add(player);
                Console.WriteLine($"Before Update: {player}");
                _context.Update(teamReturn);
                Console.WriteLine($"After Update: {player}");
                await _context.SaveChangesAsync();
                Console.WriteLine($"After SaveChanges: {player}");
                return Ok(player);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{playerId:int}")]
        public async Task<ActionResult> RemovePlayer([FromRoute] int playerId)
        {
            try
            {
                Player playerToDelete = _context.Players.FirstOrDefault(player => player.PlayerID == playerId);

                if (playerToDelete != null)
                {
                    _context.Players.Remove(playerToDelete);
                    await _context.SaveChangesAsync();
                    return Ok(playerToDelete);
                }
                return StatusCode(500, NotFound());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}