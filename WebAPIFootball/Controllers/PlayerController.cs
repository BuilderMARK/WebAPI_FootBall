using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPIFootball.Data;
using WebAPIFootball.Model;

namespace WebAPIFootball.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private IPlayerRepo playerRepo;

        public PlayerController(IPlayerRepo playerRepo)
        {
            this.playerRepo = playerRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Player>>> GetPlayver()
        {
            try
            {
                IList<Player> players = await playerRepo.ReadAllPlayers();
                return Ok(players);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Player>> AddPlayver([FromBody] Player addPlayver, string teamname)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Player added = await playerRepo.AddPlayer(addPlayver, teamname);
                Console.WriteLine("Addplayer PlayerController " + addPlayver +" TeamName PlayerController"+ teamname);
                return Created($"/{added.PlayerID}", added);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult> deletePlayer([FromBody] int playerId)
        {
            try
            {
                await playerRepo.DeletePlayer(playerId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

    }
}