/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using Microsoft.AspNetCore.Builder;
using EsportsManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EsportsManagementAPI.Data
{
	public static class EsportsManagementInitializer
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			EsportsManagementContext context = applicationBuilder.ApplicationServices.CreateScope()
	.ServiceProvider.GetRequiredService<EsportsManagementContext>();

			try
			{
				//Delete the database 
				context.Database.EnsureDeleted();
				//Create the database if it does not exist and apply the Migration
				context.Database.Migrate();

				//Seed Games
				if (!context.Games.Any())
				{
					context.Games.AddRange(
					 new Game
					 {
						 Name = "DOTA2",
						 Developer = "Valve",
						 Publisher = "Valve",
						 Designer = "IceFrog",
						 Engine = "Source 2",
						 ReleaseDate = DateTime.Parse("2013-07-09")
					 },
					 new Game
					 {
						 Name = "League of Legends",
						 Developer = "Riot Games",
						 Publisher = "Riot Games",
						 Designer = "Jeff Jew",
						 Engine = "Unity",
						 ReleaseDate = DateTime.Parse("2009-10-27")
					 },
					 new Game
					 {
						 Name = "Counter-Strike: Global Offensive",
						 Developer = "Valve",
						 Publisher = "Valve",
						 Designer = "Mike Morasky",
						 Engine = "Source",
						 ReleaseDate = DateTime.Parse("2012-08-21")
					 },
					 new Game
					 {
						 Name = "StarCraft II: Legacy of the Void",
						 Developer = "Blizzard Entertainment",
						 Publisher = "Blizzard Entertainment",
						 Designer = "Jason Huck",
						 Engine = "Starcraft II Engine",
						 ReleaseDate = DateTime.Parse("2015-11-05")
					 });
					context.SaveChanges();
				}

				//seed Teams
				if (!context.Teams.Any())
				{
					context.Teams.AddRange(
					new Team
					{
						Name = "PSG.LGD",
						Region = "China",
						Country = "China",
						CreateDate = DateTime.Parse("2009-05-21"),
						TotalWinnings = 24682013d,
						GameID = context.Games.FirstOrDefault(g => g.Name == "DOTA2").ID
					},
					new Team
					{
						Name = "Team Liquid",
						Region = "Europe",
						Country = "Netherlands",
						CreateDate = DateTime.Parse("2012-12-06"),
						TotalWinnings = 25117194d,
						GameID = context.Games.FirstOrDefault(g => g.Name == "DOTA2").ID
					},
					new Team
					{
						Name = "G2 Esports",
						Region = "Europe",
						Country = "Germany",
						CreateDate = DateTime.Parse("2014-02-24"),
						TotalWinnings = 2997030d,
						GameID = context.Games.FirstOrDefault(g => g.Name == "League of Legends").ID
					},
					new Team
					{
						Name = "T1",
						Region = "Korea",
						Country = "South Korea",
						CreateDate = DateTime.Parse("2014-02-15"),
						TotalWinnings = 6907844d,
						GameID = context.Games.FirstOrDefault(g => g.Name == "League of Legends").ID
					},
					new Team
					{
						Name = "Natus Vincere",
						Region = "CIS",
						Country = "Ukraine",
						CreateDate = DateTime.Parse("2012-11-04"),
						TotalWinnings = 9431259d,
						GameID = context.Games.FirstOrDefault(g => g.Name == "Counter-Strike: Global Offensive").ID
					},
					new Team
					{
						Name = "Platinum Heroes",
						Region = "Europe",
						Country = "France",
						CreateDate = DateTime.Parse("2019-02-17"),
						TotalWinnings = 22664d,
						GameID = context.Games.FirstOrDefault(g => g.Name == "StarCraft II: Legacy of the Void").ID
					},
					new Team
					{
						Name = "SSLT",
						Region = "China",
						Country = "China",
						CreateDate = DateTime.Parse("2022-03-14"),
						TotalWinnings = 6327d,
						GameID = context.Games.FirstOrDefault(g => g.Name == "StarCraft II: Legacy of the Void").ID
					});
					context.SaveChanges();
				}

				//seed players
				if (!context.Players.Any())
				{
					context.Players.AddRange(
					new Player
					{
						FirstName = "Chunyu",
						LastName = "Wang",
						Nickname = "Ame",
						DOB = DateTime.Parse("1997-04-07"),
						Position = "Carry",
						JoinDate = DateTime.Parse("2020-09-16"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "PSG.LGD").ID
					},
					new Player
					{
						FirstName = "Jinxiang",
						LastName = "Cheng",
						Nickname = "NothingToSay",
						DOB = DateTime.Parse("2000-12-21"),
						Position = "Middle",
						JoinDate = DateTime.Parse("2020-09-16"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "PSG.LGD").ID
					},
					new Player
					{
						FirstName = "Ruida",
						LastName = "Zhang",
						Nickname = "Faith_bian",
						DOB = DateTime.Parse("1998-03-21"),
						Position = "Offlane",
						JoinDate = DateTime.Parse("2020-09-16"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "PSG.LGD").ID
					},
					new Player
					{
						FirstName = "Zixing",
						LastName = "Zhao",
						Nickname = "XinQ",
						DOB = DateTime.Parse("1998-07-06"),
						Position = "Support",
						JoinDate = DateTime.Parse("2020-09-16"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "PSG.LGD").ID
					},
					new Player
					{
						FirstName = "Yiping",
						LastName = "Zhang",
						Nickname = "y",
						DOB = DateTime.Parse("1998-08-08"),
						Position = "Support",
						JoinDate = DateTime.Parse("2020-09-16"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "PSG.LGD").ID
					},
					new Player
					{
						FirstName = "Michael",
						LastName = "Vu",
						Nickname = "miCKe",
						DOB = DateTime.Parse("1999-07-26"),
						Position = "Carry",
						JoinDate = DateTime.Parse("2019-10-02"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Team Liquid").ID
					},
					new Player
					{
						FirstName = "Michael",
						LastName = "Jankowski",
						Nickname = "Nisha",
						DOB = DateTime.Parse("2000-09-28"),
						Position = "Middle",
						JoinDate = DateTime.Parse("2019-10-02"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Team Liquid").ID
					},
					new Player
					{
						FirstName = "Ludwig",
						LastName = "Wahlberg",
						Nickname = "zai",
						DOB = DateTime.Parse("1997-08-05"),
						Position = "Offlane",
						JoinDate = DateTime.Parse("2019-10-02"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Team Liquid").ID
					},
					new Player
					{
						FirstName = "Samuel",
						LastName = "Svahn",
						Nickname = "Boxi",
						DOB = DateTime.Parse("1998-04-10"),
						Position = "Support",
						JoinDate = DateTime.Parse("2019-10-02"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Team Liquid").ID
					},
					new Player
					{
						FirstName = "Aydin",
						LastName = "Sarkohi",
						Nickname = "Insania",
						DOB = DateTime.Parse("1994-06-18"),
						Position = "Support",
						JoinDate = DateTime.Parse("2019-10-02"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Team Liquid").ID
					},
					new Player
					{
						FirstName = "Sergen",
						LastName = "Celik",
						Nickname = "BrokenBlade",
						DOB = DateTime.Parse("2000-01-19"),
						Position = "Top",
						JoinDate = DateTime.Parse("2021-12-01"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "G2 Esports").ID
					},
					new Player
					{
						FirstName = "Martin",
						LastName = "Sundelin",
						Nickname = "Yike",
						DOB = DateTime.Parse("2000-11-11"),
						Position = "Jungle",
						JoinDate = DateTime.Parse("2021-12-01"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "G2 Esports").ID
					},
					new Player
					{
						FirstName = "Rasmus",
						LastName = "Winther",
						Nickname = "Caps",
						DOB = DateTime.Parse("1999-11-17"),
						Position = "Middle",
						JoinDate = DateTime.Parse("2021-12-01"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "G2 Esports").ID
					},
					new Player
					{
						FirstName = "Steven",
						LastName = "Liv",
						Nickname = "Hans sama",
						DOB = DateTime.Parse("1999-09-02"),
						Position = "Bottom",
						JoinDate = DateTime.Parse("2021-12-01"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "G2 Esports").ID
					},
					new Player
					{
						FirstName = "Mihael",
						LastName = "Mehle",
						Nickname = "Mikyx",
						DOB = DateTime.Parse("1998-11-02"),
						Position = "Support",
						JoinDate = DateTime.Parse("2021-12-01"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "G2 Esports").ID
					},
					new Player
					{
						FirstName = "Choi",
						LastName = "Woo-je",
						Nickname = "Zeus",
						DOB = DateTime.Parse("1999-05-20"),
						Position = "Top",
						JoinDate = DateTime.Parse("2020-11-26"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "T1").ID
					},
					new Player
					{
						FirstName = "Moon",
						LastName = "Hyeon-joon",
						Nickname = "Oner",
						DOB = DateTime.Parse("1998-11-27"),
						Position = "Jumgle",
						JoinDate = DateTime.Parse("2020-11-26"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "T1").ID
					},
					new Player
					{
						FirstName = "Lee",
						LastName = "Sang-hyeok",
						Nickname = "Faker",
						DOB = DateTime.Parse("1997-07-25"),
						Position = "Middle",
						JoinDate = DateTime.Parse("2020-11-26"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "T1").ID
					},
					new Player
					{
						FirstName = "Lee",
						LastName = "Min-hyeong",
						Nickname = "Gumayusi",
						DOB = DateTime.Parse("1999-12-31"),
						Position = "Bottom",
						JoinDate = DateTime.Parse("2020-11-26"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "T1").ID
					},
					new Player
					{
						FirstName = "Ryu",
						LastName = "Min-seok",
						Nickname = "Keria",
						DOB = DateTime.Parse("2000-05-07"),
						Position = "Support",
						JoinDate = DateTime.Parse("2020-11-26"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "T1").ID
					},
					new Player
					{
						FirstName = "Oleksandr",
						LastName = "Kostyljev",
						Nickname = "s1mple",
						DOB = DateTime.Parse("1997-10-02"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2016-08-04"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Natus Vincere").ID
					},
					new Player
					{
						FirstName = "Denis",
						LastName = "Sharipov",
						Nickname = "electroNic",
						DOB = DateTime.Parse("1998-06-13"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2016-08-04"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Natus Vincere").ID
					},
					new Player
					{
						FirstName = "Ilya",
						LastName = "Zalutskiy",
						Nickname = "Perfecto",
						DOB = DateTime.Parse("1999-06-05"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2016-08-04"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Natus Vincere").ID
					},
					new Player
					{
						FirstName = "Valerij",
						LastName = "Vakhovsjkyj",
						Nickname = "b1t",
						DOB = DateTime.Parse("2000-03-08"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2016-08-04"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Natus Vincere").ID
					},
					new Player
					{
						FirstName = "Andrij",
						LastName = "Kukharsjkyj",
						Nickname = "npl",
						DOB = DateTime.Parse("2000-04-18"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2016-08-04"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Natus Vincere").ID
					},
					new Player
					{
						FirstName = "Leon",
						LastName = "Vrhovec",
						Nickname = "goblin",
						DOB = DateTime.Parse("1998-07-07"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2021-05-03"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Platinum Heroes").ID
					},
					new Player
					{
						FirstName = "Moritz",
						LastName = "Polzl",
						Nickname = "HateMe",
						DOB = DateTime.Parse("1998-05-19"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2021-05-03"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Platinum Heroes").ID
					},
					new Player
					{
						FirstName = "Thomas",
						LastName = "Labrousse",
						Nickname = "ShaDoWn",
						DOB = DateTime.Parse("1997-02-21"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2021-05-03"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Platinum Heroes").ID
					},
					new Player
					{
						FirstName = "Adrien",
						LastName = "Bouet",
						Nickname = "DnS",
						DOB = DateTime.Parse("1993-12-01"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2021-05-03"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "Platinum Heroes").ID
					},
					new Player
					{
						FirstName = "Min",
						LastName = "Huang",
						Nickname = "Cyan",
						DOB = DateTime.Parse("1995-05-16"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2022-03-14"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "SSLT").ID
					},
					new Player
					{
						FirstName = "Tao",
						LastName = "Xue",
						Nickname = "Firefly",
						DOB = DateTime.Parse("1996-08-01"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2022-03-14"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "SSLT").ID
					},
					new Player
					{
						FirstName = "Yongxin",
						LastName = "Yin",
						Nickname = "Silky",
						DOB = DateTime.Parse("1995-01-12"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2022-03-14"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "SSLT").ID
					},
					new Player
					{
						FirstName = "Huiming",
						LastName = "Huang",
						Nickname = "TooDming",
						DOB = DateTime.Parse("1993-06-23"),
						Position = "N/A",
						JoinDate = DateTime.Parse("2022-03-14"),
						TeamID = context.Teams.FirstOrDefault(t => t.Name == "SSLT").ID
					});
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.GetBaseException().Message);
			}

		}
	}
}
