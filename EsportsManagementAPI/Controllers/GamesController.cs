
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
    public class GamesController : ControllerBase
    {
        private readonly EsportsManagementContext _context;

        public GamesController(EsportsManagementContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            //get all games
            return await _context.Games
                .Include(g => g.Teams)
                .Select(g => new GameDTO
				{
					ID = g.ID,
					Name = g.Name,
					Developer = g.Developer,
					Publisher = g.Publisher,
					Designer = g.Designer,
					Engine = g.Engine,
					ReleaseDate = g.ReleaseDate
				})
                .ToListAsync();
        }

		// GET: api/Games/inc
		[HttpGet("inc")]
		public async Task<ActionResult<IEnumerable<GameDTO>>> GetGamesInc()
		{
			//get all games including teams collection
			return await _context.Games
				.Include(g => g.Teams)
				.Select(g => new GameDTO
				{
					ID = g.ID,
					Name = g.Name,
					Developer = g.Developer,
					Publisher = g.Publisher,
					Designer = g.Designer,
					Engine = g.Engine,
					ReleaseDate = g.ReleaseDate,
					Teams = g.Teams.Select(gTeam => new TeamDTO
					{
						ID = gTeam.ID,
						Name = gTeam.Name,
						Region = gTeam.Region,
						Country = gTeam.Country,
						CreateDate = gTeam.CreateDate,
						TotalWinnings = gTeam.TotalWinnings,
						GameID = gTeam.GameID
					}).ToList()
				})
				.ToListAsync();
		}

		// GET: api/Games/5
		[HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            //get a game by id
            var game = await _context.Games
                .Include(g => g.Teams)
				.Select(g => new GameDTO
				{
					ID = g.ID,
					Name = g.Name,
					Developer = g.Developer,
					Publisher = g.Publisher,
					Designer = g.Designer,
					Engine = g.Engine,
					ReleaseDate = g.ReleaseDate
				})
				.FirstOrDefaultAsync(g => g.ID == id);

            if (game == null)
            {
				return NotFound(new { message = "Error: Game not found." });
			}

            return game;
        }

		// GET: api/Games/inc/5
		[HttpGet("inc/{id}")]
		public async Task<ActionResult<GameDTO>> GetGameInc(int id)
		{
			//get a game by id, including teams collection
			var game = await _context.Games
				.Include(g => g.Teams)
				.Select(g => new GameDTO
				{
					ID = g.ID,
					Name = g.Name,
					Developer = g.Developer,
					Publisher = g.Publisher,
					Designer = g.Designer,
					Engine = g.Engine,
					ReleaseDate = g.ReleaseDate,
					Teams = g.Teams.Select(gTeam => new TeamDTO
					{
						ID = gTeam.ID,
						Name = gTeam.Name,
						Region = gTeam.Region,
						Country = gTeam.Country,
						CreateDate = gTeam.CreateDate,
						TotalWinnings = gTeam.TotalWinnings,
						GameID = gTeam.GameID
					}).ToList()
				})
				.FirstOrDefaultAsync(g => g.ID == id);

			if (game == null)
			{
				return NotFound(new { message = "Error: Game not found." });
			}

			return game;
		}

		// PUT: api/Games/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDTO gameDTO)
        {
            if (id != gameDTO.ID)
            {
				return BadRequest(new { message = "Error: The given ID does not match Game." });
			}

			//check for validation
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//Get the game record to update
			var gameToUpdate = await _context.Games.FindAsync(id);

			//Check to make sure record exists
			if (gameToUpdate == null)
			{
				return NotFound(new { message = "Error: Game record not found." });
			}

			gameToUpdate.ID = gameDTO.ID;
			gameToUpdate.Name = gameDTO.Name;
			gameToUpdate.Developer = gameDTO.Developer;
			gameToUpdate.Publisher = gameDTO.Publisher;
			gameToUpdate.Designer = gameDTO.Designer;
			gameToUpdate.Engine = gameDTO.Engine;
			gameToUpdate.ReleaseDate = gameDTO.ReleaseDate;

			try
			{
				await _context.SaveChangesAsync();
				return NoContent();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GameExists(id))
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
					return BadRequest(new { message = "Unable to save: Duplicate Game Name." });
				}
				else
				{
					return BadRequest(new { message = "Unable to save changes to the database. Please try again." });
				}
			}
		}

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameDTO gameDTO)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Game game = new Game
			{
				ID = gameDTO.ID,
				Name = gameDTO.Name,
				Developer = gameDTO.Developer,
				Publisher = gameDTO.Publisher,
				Designer = gameDTO.Designer,
				Engine = gameDTO.Engine,
				ReleaseDate = gameDTO.ReleaseDate
			};

			try
			{
				_context.Games.Add(game);
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetGame), new { id = game.ID }, gameDTO);
			}
			catch (DbUpdateException dex)
			{
				if (dex.GetBaseException().Message.Contains("UNIQUE"))
				{
					return BadRequest(new { message = "Unable to save: Duplicate Game Name." });
				}
				else
				{
					return BadRequest(new { message = "Unable to save changes to the database. Please try again." });
				}
			}
		}

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
				return NotFound(new { message = "Error: Game has already been removed." });
			}

			try
			{
				_context.Games.Remove(game);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch (DbUpdateException dex)
			{
				if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
				{
					return BadRequest(new { message = "Error: You cannot delete a Game that has Teams assigned." });
				}
				else
				{
					return BadRequest(new { message = "Error: Unable to delete Game. Please try again." });
				}
			}
		}

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }
    }
}
