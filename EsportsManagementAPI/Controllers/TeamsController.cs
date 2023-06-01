/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EsportsManagementAPI.Data;
using EsportsManagementAPI.Models;
using System.Numerics;

namespace EsportsManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeamsController : ControllerBase
	{
		private readonly EsportsManagementContext _context;

		public TeamsController(EsportsManagementContext context)
		{
			_context = context;
		}

		// GET: api/Teams
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
		{
			//get all teams including game
			return await _context.Teams
				.Include(t => t.Game)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					}
				})
				.ToListAsync();
		}

		// GET: api/Teams/count
		[HttpGet("count")]
		public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeamsCount()
		{
			//get all teams including the game, also incluing a count of players in each team
			return await _context.Teams
				.Include(t => t.Game)
				.Include(t => t.Players)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					NumberOfPlayers = t.Players.Count,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					}
				})
				.ToListAsync();
		}

		// GET: api/Teams/inc - Include the Players Collection
		[HttpGet("inc")]
		public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeamsInc()
		{
			//get all teams including game and players collection
			return await _context.Teams
				.Include(t => t.Game)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					},
					Players = t.Players.Select(tPlayer => new PlayerDTO
					{
						ID = tPlayer.ID,
						FirstName = tPlayer.FirstName,
						LastName = tPlayer.LastName,
						Nickname = tPlayer.Nickname,
						DOB = tPlayer.DOB,
						Position = tPlayer.Position,
						JoinDate = tPlayer.JoinDate,
						TeamID = tPlayer.TeamID
					}).ToList()
				})
				.ToListAsync();
		}

		// GET: api/Teams/5
		[HttpGet("{id}")]
		public async Task<ActionResult<TeamDTO>> GetTeam(int id)
		{
			//get team by id, including game
			var team = await _context.Teams
				.Include(t => t.Game)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					}
				})
				.FirstOrDefaultAsync(t => t.ID == id);

			if (team == null)
			{
				return NotFound(new { message = "Error: Team not found." });
			}

			return team;
		}

		// GET: api/Teams/count/5
		[HttpGet("count/{id}")]
		public async Task<ActionResult<TeamDTO>> GetTeamCount(int id)
		{
			//get a team base on team id, including the game and a count of the players on the team
			var teamDTO = await _context.Teams
				.Include(t => t.Game)
				.Include(t => t.Players)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					NumberOfPlayers = t.Players.Count,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					}
				})
				.FirstOrDefaultAsync(t => t.ID == id);

			if (teamDTO == null)
			{
				return NotFound(new { message = "Error: Team record not found." });
			}

			return teamDTO;
		}

		// GET: api/Teams/inc/5
		[HttpGet("inc/{id}")]
		public async Task<ActionResult<TeamDTO>> GetTeamInc(int id)
		{
			//get team by id, including game and players collection
			var team = await _context.Teams
				.Include(t => t.Game)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					},
					Players = t.Players.Select(tPlayer => new PlayerDTO
					{
						ID = tPlayer.ID,
						FirstName = tPlayer.FirstName,
						LastName = tPlayer.LastName,
						Nickname = tPlayer.Nickname,
						DOB = tPlayer.DOB,
						Position = tPlayer.Position,
						JoinDate = tPlayer.JoinDate,
						TeamID = tPlayer.TeamID
					}).ToList()
				})
				.FirstOrDefaultAsync(t => t.ID == id);

			if (team == null)
			{
				return NotFound(new { message = "Error: Team not found." });
			}

			return team;
		}

		// GET: api/Teams/ByGame/5
		[HttpGet("ByGame/{id}")]
		public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeamsByGame(int id)
		{
			//get all teams of a game, including game
			var team = await _context.Teams
				.Include(t => t.Game)
				.Where(t => t.GameID == id)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					}
				})
			.ToListAsync();

			if (team.Count() > 0)
			{
				return team;
			}
			else
			{
				return NotFound(new { message = "Error: No Team records for the given Game." });
			}
		}

		// GET: api/Teams/ByGameCount/5
		[HttpGet("ByGameCount/{id}")]
		public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeamsByGameCount(int id)
		{
			//get all team by game, including game and a count of players in the team
			var teamDTOs = await _context.Teams
				.Include(t => t.Game)
				.Include(t => t.Players)
				.Where(t => t.GameID == id)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					NumberOfPlayers = t.Players.Count,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					}
				})
				.ToListAsync();


			if (teamDTOs.Count() > 0)
			{
				return teamDTOs;
			}
			else
			{
				return NotFound(new { message = "Error: No Team records for that League." });
			}
		}

		// GET: api/Teams/ByGameInc/5
		[HttpGet("ByGameInc/{id}")]
		public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeamsByGameInc(int id)
		{
			//get all teams of a game, including game and players collection
			var team = await _context.Teams
				.Include(t => t.Game)
				.Where(t => t.GameID == id)
				.Select(t => new TeamDTO
				{
					ID = t.ID,
					Name = t.Name,
					Region = t.Region,
					Country = t.Country,
					CreateDate = t.CreateDate,
					TotalWinnings = t.TotalWinnings,
					GameID = t.GameID,
					Game = new GameDTO
					{
						ID = t.Game.ID,
						Name = t.Game.Name,
						Developer = t.Game.Developer,
						Publisher = t.Game.Publisher,
						Designer = t.Game.Designer,
						Engine = t.Game.Engine,
						ReleaseDate = t.Game.ReleaseDate
					},
					Players = t.Players.Select(tPlayer => new PlayerDTO
					{
						ID = tPlayer.ID,
						FirstName = tPlayer.FirstName,
						LastName = tPlayer.LastName,
						Nickname = tPlayer.Nickname,
						DOB = tPlayer.DOB,
						Position = tPlayer.Position,
						JoinDate = tPlayer.JoinDate,
						TeamID = tPlayer.TeamID
					}).ToList()
				})
			.ToListAsync();

			if (team.Count() > 0)
			{
				return team;
			}
			else
			{
				return NotFound(new { message = "Error: No Team records for the given Game." });
			}
		}

		// PUT: api/Teams/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTeam(int id, TeamDTO teamDTO)
		{
			if (id != teamDTO.ID)
			{
				return BadRequest(new { message = "Error: The given ID does not match Team" });
			}

			//check for validation
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//Get the team record to update
			var teamToUpdate = await _context.Teams.FindAsync(id);

			//Check if the record exist
			if (teamToUpdate == null)
			{
				return NotFound(new { message = "Error: Team record not found." });
			}

			teamToUpdate.ID = teamDTO.ID;
			teamToUpdate.Name = teamDTO.Name;
			teamToUpdate.Region = teamDTO.Region;
			teamToUpdate.Country = teamDTO.Country;
			teamToUpdate.CreateDate = teamDTO.CreateDate;
			teamToUpdate.TotalWinnings = teamDTO.TotalWinnings;
			teamToUpdate.GameID = teamDTO.GameID;

			try
			{
				await _context.SaveChangesAsync();
				return NoContent();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TeamExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			catch (DbUpdateException dex)
			{
				if (dex.GetBaseException().Message.Contains("UNIQUE"))
				{
					return BadRequest(new { message = "Unable to save: Duplicate Team Name." });
				}
				else
				{
					return BadRequest(new { message = "Unable to save changes to the database. Please try again." });
				}
			}
		}

		// POST: api/Teams
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Team>> PostTeam(TeamDTO teamDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Team team = new Team
			{
				ID = teamDTO.ID,
				Name = teamDTO.Name,
				Region = teamDTO.Region,
				Country = teamDTO.Country,
				CreateDate = teamDTO.CreateDate,
				TotalWinnings = teamDTO.TotalWinnings,
				GameID = teamDTO.GameID,
			};

			try
			{
				_context.Teams.Add(team);
				await _context.SaveChangesAsync();

				//Assign Database Generated pk back into the DTO
				teamDTO.ID = team.ID;

				return CreatedAtAction(nameof(GetTeam), new { id = team.ID }, teamDTO);
			}
			catch (DbUpdateException dex)
			{
				if (dex.GetBaseException().Message.Contains("UNIQUE"))
				{
					return BadRequest(new { message = "Unable to save: Duplicate Team Name." });
				}
				else
				{
					return BadRequest(new { message = "Unable to save changes to the database. Please try again." });
				}
			}
		}

		// DELETE: api/Teams/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTeam(int id)
		{
			var team = await _context.Teams.FindAsync(id);

			if (team == null)
			{
				return NotFound(new { message = "Error: Team has already been removed." });
			}

			try
			{
				_context.Teams.Remove(team);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch (DbUpdateException dex)
			{
				if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
				{
					return BadRequest(new { message = "Error: You cannot delete a Team that has Players assigned." });
				}
				else
				{
					return BadRequest(new { message = "Error: Unable to delete Team. Please try again." });
				}
			}
		}

		private bool TeamExists(int id)
		{
			return _context.Teams.Any(e => e.ID == id);
		}
	}
}
