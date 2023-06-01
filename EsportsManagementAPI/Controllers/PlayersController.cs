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

namespace EsportsManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlayersController : ControllerBase
	{
		private readonly EsportsManagementContext _context;

		public PlayersController(EsportsManagementContext context)
		{
			_context = context;
		}

		// GET: api/Players
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers()
		{
			//get all players including team and game
			return await _context.Players
				.Include(p => p.Team)
				.Select(p => new PlayerDTO
				{
					ID = p.ID,
					FirstName = p.FirstName,
					LastName = p.LastName,
					Nickname = p.Nickname,
					DOB = p.DOB,
					Position = p.Position,
					JoinDate = p.JoinDate,
					//RowVersion = p.RowVersion,	//diabled currently due to no identity in maui webapi client
					TeamID = p.TeamID,
					Team = new TeamDTO
					{
						ID = p.Team.ID,
						Name = p.Team.Name,
						Region = p.Team.Region,
						Country = p.Team.Country,
						CreateDate = p.Team.CreateDate,
						TotalWinnings = p.Team.TotalWinnings,
						GameID = p.Team.GameID,
						Game = new GameDTO
						{
							ID = p.Team.Game.ID,
							Name = p.Team.Game.Name,
							Developer = p.Team.Game.Developer,
							Publisher = p.Team.Game.Publisher,
							Designer = p.Team.Game.Designer,
							Engine = p.Team.Game.Engine,
							ReleaseDate = p.Team.Game.ReleaseDate
						}
					}
				})
				.ToListAsync();
		}

		// GET: api/Players/5
		[HttpGet("{id}")]
		public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
		{
			//get player by player id, including team and game
			var player = await _context.Players
				.Include(p => p.Team)
				.Select(p => new PlayerDTO
				{
					ID = p.ID,
					FirstName = p.FirstName,
					LastName = p.LastName,
					Nickname = p.Nickname,
					DOB = p.DOB,
					Position = p.Position,
					JoinDate = p.JoinDate,
					//RowVersion = p.RowVersion,  //diabled currently due to no identity in maui webapi client
					TeamID = p.TeamID,
					Team = new TeamDTO
					{
						ID = p.Team.ID,
						Name = p.Team.Name,
						Region = p.Team.Region,
						Country = p.Team.Country,
						CreateDate = p.Team.CreateDate,
						TotalWinnings = p.Team.TotalWinnings,
						GameID = p.Team.GameID,
						Game = new GameDTO
						{
							ID = p.Team.Game.ID,
							Name = p.Team.Game.Name,
							Developer = p.Team.Game.Developer,
							Publisher = p.Team.Game.Publisher,
							Designer = p.Team.Game.Designer,
							Engine = p.Team.Game.Engine,
							ReleaseDate = p.Team.Game.ReleaseDate
						}
					}
				})
				.FirstOrDefaultAsync(p => p.ID == id);

			if (player == null)
			{
				return NotFound(new { message = "Error: Player record not found." });
			}

			return player;
		}

		// GET: api/Players/ByTeam/5
		[HttpGet("ByTeam/{id}")]
		public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayersByTeam(int id)
		{
			//get all players of a team, including team and game
			var player = await _context.Players
				.Include(p => p.Team)
				.Where(p => p.TeamID == id)
				.Select(p => new PlayerDTO
				{
					ID = p.ID,
					FirstName = p.FirstName,
					LastName = p.LastName,
					Nickname = p.Nickname,
					DOB = p.DOB,
					Position = p.Position,
					JoinDate = p.JoinDate,
					//RowVersion = p.RowVersion,	//diabled currently due to no identity in maui webapi client
					TeamID = p.TeamID,
					Team = new TeamDTO
					{
						ID = p.Team.ID,
						Name = p.Team.Name,
						Region = p.Team.Region,
						Country = p.Team.Country,
						CreateDate = p.Team.CreateDate,
						TotalWinnings = p.Team.TotalWinnings,
						GameID = p.Team.GameID,
						Game = new GameDTO
						{
							ID = p.Team.Game.ID,
							Name = p.Team.Game.Name,
							Developer = p.Team.Game.Developer,
							Publisher = p.Team.Game.Publisher,
							Designer = p.Team.Game.Designer,
							Engine = p.Team.Game.Engine,
							ReleaseDate = p.Team.Game.ReleaseDate
						}
					}
				})
				.ToListAsync();

			if (player.Count() > 0)
			{
				return player;
			}
			else
			{
				return NotFound(new { message = "Error: No Player records for the given Team." });
			}
		}


		// PUT: api/Players/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPlayer(int id, PlayerDTO playerDTO)
		{
			if (id != playerDTO.ID)
			{
				return BadRequest(new { message = "Error: ID does not match Player" });
			}

			//check for validation
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//Get the player record to update
			var playerToUpdate = await _context.Players.FindAsync(id);

			//Check if the record exist
			if (playerToUpdate == null)
			{
				return NotFound(new { message = "Error: Player record not found." });
			}

			////check for concurrency		//diabled currently due to no identity in maui webapi client
			//if (playerToUpdate.RowVersion != null)
			//{
			//	if (!playerToUpdate.RowVersion.SequenceEqual(playerDTO.RowVersion))
			//	{
			//		return Conflict(new { message = "Concurrency Error: Player has been changed by another user. Please try again." });
			//	}
			//}

			//copy over the properties from DTO
			playerToUpdate.ID = playerDTO.ID;
			playerToUpdate.FirstName = playerDTO.FirstName;
			playerToUpdate.LastName = playerDTO.LastName;
			playerToUpdate.Nickname = playerDTO.Nickname;
			playerToUpdate.DOB = playerDTO.DOB;
			playerToUpdate.Position = playerDTO.Position;
			playerToUpdate.JoinDate = playerDTO.JoinDate;
			//playerToUpdate.RowVersion = playerDTO.RowVersion;
			playerToUpdate.TeamID = playerDTO.TeamID;

			////Put the original RowVersion value in the OriginalValues collection for the entity
			//_context.Entry(playerToUpdate).Property("RowVersion").OriginalValue = playerDTO.RowVersion;

			try
			{
				await _context.SaveChangesAsync();
				return NoContent();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PlayerExists(id))
				{
					return Conflict(new { message = "Concurrency Error: Player has been Removed." });
				}
				else
				{
					return Conflict(new { message = "Concurrency Error: Player has been updated by another user.  Please try again." });
				}
			}
			catch (DbUpdateException dex)
			{
				if (dex.GetBaseException().Message.Contains("UNIQUE"))
				{
					return BadRequest(new { message = "Unable to save: Duplicate Player Nickname." });
				}
				else
				{
					return BadRequest(new { message = "Unable to save changes to the database. Please try again." });
				}
			}
		}

		// POST: api/Players
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Player>> PostPlayer(PlayerDTO playerDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Player player = new Player
			{
				ID = playerDTO.ID,
				FirstName = playerDTO.FirstName,
				LastName = playerDTO.LastName,
				Nickname = playerDTO.Nickname,
				DOB = playerDTO.DOB,
				Position = playerDTO.Position,
				JoinDate = playerDTO.JoinDate,
				//RowVersion = playerDTO.RowVersion,
				TeamID = playerDTO.TeamID
			};

			try
			{
				_context.Players.Add(player);
				await _context.SaveChangesAsync();

				//Assign Database Generated values back into the DTO
				playerDTO.ID = player.ID;
				//playerDTO.RowVersion = player.RowVersion;

				return CreatedAtAction(nameof(GetPlayer), new { id = player.ID }, playerDTO);
			}
			catch (DbUpdateException dex)
			{
				if (dex.GetBaseException().Message.Contains("UNIQUE"))
				{
					return BadRequest(new { message = "Unable to save: Duplicate Player Nickname." });
				}
				else
				{
					return BadRequest(new { message = "Unable to save changes to the database. Please try again." });
				}
			}
		}

		// DELETE: api/Players/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePlayer(int id)
		{
			var player = await _context.Players.FindAsync(id);

			if (player == null)
			{
				return NotFound(new { message = "Error: Player has already been removed." });
			}

			try
			{
				_context.Players.Remove(player);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch (DbUpdateException)
			{
				return BadRequest(new { message = "Delete Error: Unable to delete Player." });
			}
		}

		private bool PlayerExists(int id)
		{
			return _context.Players.Any(e => e.ID == id);
		}
	}
}
