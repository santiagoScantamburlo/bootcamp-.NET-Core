using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Models
{
    public static class DbInitializer
    {
        public static void Initialize(SalvoContext context)
        {
            if (!context.Players.Any())
            {
                var players = new Player[]
                {
                    new Player
                    {
                        Name = "Jack Bauer",
                        Email = "j.bauer@ctu.gov",
                        Password = "24"
                    },
                    new Player
                    {
                        Name = "Chloe O'Brian",
                        Email = "c.obrian@ctu.gov",
                        Password = "42"
                    },
                    new Player
                    {
                        Name = "Kim Bauer ",
                        Email = "kim_bauer@gmail.com",
                        Password = "kb"
                    },
                    new Player
                    {
                        Name = "Tony Almeida",
                        Email = "t.almeida@ctu.gov",
                        Password = "mole"
                    },
                };


                foreach (Player p in players)
                {
                    context.Players.Add(p);
                }
                context.SaveChanges();
            }

            if (!context.Games.Any())
            {
                var games = new Game[]
                {
                    new Game { CreationDate = DateTime.Now },
                    new Game { CreationDate = DateTime.Now.AddHours(1) },
                    new Game { CreationDate = DateTime.Now.AddHours(2) },
                    new Game { CreationDate = DateTime.Now.AddHours(3) },
                    new Game { CreationDate = DateTime.Now.AddHours(4) },
                    new Game { CreationDate = DateTime.Now.AddHours(5) },
                    new Game { CreationDate = DateTime.Now.AddHours(6) },
                    new Game { CreationDate = DateTime.Now.AddHours(7) }
                };
                foreach (Game g in games)
                {
                    context.Games.Add(g);
                }
                context.SaveChanges();
            }
        }
    }
}
