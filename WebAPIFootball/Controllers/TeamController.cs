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
    public class TeamController : ControllerBase
    {
        private ITeamRepo teamRepo;

        public TeamController(ITeamRepo teamRepo)
        {
            this.teamRepo = teamRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Team>>> GetTeams([FromQuery] string TeamName, [FromQuery] int Ranting)
        {
            try
            {
                IList<Team> validInput = await teamRepo.ReadAllTeams(Ranting, TeamName);
                return Ok(validInput);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }




        [HttpPost]
                public async Task<ActionResult<Team>> AddTeam([FromBody] Team addTeam)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
        
                    try
                    {
                        Team added = await teamRepo.AddTeam(addTeam);
                        return Created($"/{added.TeamName}", added);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return StatusCode(500, e.Message);
                    }
                }
            }
    }
