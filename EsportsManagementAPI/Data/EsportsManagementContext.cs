/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using EsportsManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace EsportsManagementAPI.Data
{
	public class EsportsManagementContext : DbContext
	{
		//To give access to IHttpContextAccessor for Audit Data with IAuditable
		private readonly IHttpContextAccessor _httpContextAccessor;

		//Property to hold the UserName value
		public string UserName
		{
			get; private set;
		}

		public EsportsManagementContext(DbContextOptions<EsportsManagementContext> options,
			IHttpContextAccessor httpContextAccessor)
			: base(options)
		{
			_httpContextAccessor = httpContextAccessor;
			if (_httpContextAccessor.HttpContext != null)
			{
				UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
				UserName ??= "Unknown";
			}
			else
			{
				//No HttpContext so seeding data
				UserName = "Seed Data";
			}
		}

		public DbSet<Game> Games { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Player> Players { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Add a unique index to the Player's Nickname
			modelBuilder.Entity<Player>()
			.HasIndex(p => p.Nickname)
			.IsUnique();

			//Add a unique index to the Team Name
			modelBuilder.Entity<Team>()
			.HasIndex(p => p.Name)
			.IsUnique();

			//Add a unique index to the Game Name
			modelBuilder.Entity<Game>()
			.HasIndex(p => p.Name)
			.IsUnique();

			//cannot delete a Game if there are Teams assigned
			modelBuilder.Entity<Game>()
				.HasMany(g => g.Teams)
				.WithOne(t => t.Game)
				.OnDelete(DeleteBehavior.Restrict);

			//cannot delete a Team if there are Players assigned
			modelBuilder.Entity<Team>()
				.HasMany(t => t.Players)
				.WithOne(p => p.Team)
				.OnDelete(DeleteBehavior.Restrict);
		}
		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			OnBeforeSaving();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			OnBeforeSaving();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void OnBeforeSaving()
		{
			var entries = ChangeTracker.Entries();
			foreach (var entry in entries)
			{
				if (entry.Entity is IAuditable trackable)
				{
					var now = DateTime.UtcNow;
					switch (entry.State)
					{
						case EntityState.Modified:
							trackable.UpdatedOn = now;
							trackable.UpdatedBy = UserName;
							break;

						case EntityState.Added:
							trackable.CreatedOn = now;
							trackable.CreatedBy = UserName;
							trackable.UpdatedOn = now;
							trackable.UpdatedBy = UserName;
							break;
					}
				}
			}
		}
	}
}
