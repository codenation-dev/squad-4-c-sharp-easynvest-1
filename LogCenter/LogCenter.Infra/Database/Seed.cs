using System;
using System.Linq;
using LogCenter.Domain.Entities;
using LogCenter.Domain.Enums;

namespace LogCenter.Infra.Database
{
    public static class Seed
    {
        public static void Run(DatabaseContext context)
        {
            context.Database.EnsureCreated();
            // Do not run seed, there is already data in db
            if (context.Logs.Count() > 0) {
                return;
            }
            CreateUsers(context);
            CreateLogs(context);
        }

        private static void CreateLogs(DatabaseContext database)
        {
            var users = database.Users.Select(x => x.Id).ToList();
            var levels = new [] { LevelType.Debug, LevelType.Error, LevelType.Warning };
            var envs = new [] { Domain.Entities.Environment.Dev, Domain.Entities.Environment.Homologacao, Domain.Entities.Environment.Producao };
            var random = new Random();
            var logs = Enumerable.Range(1, 1000).Select(i => new Log {
                Title = $"Log generated through seed {i}",
                Level = levels[random.Next(levels.Length)],
                Origin = "http://localhost:5001",
                Description = "Starting application on port 5001, you are ready to rock!",
                CreationDate = DateTime.Now,
                Environment = envs[random.Next(envs.Length)],
                UserId = users[random.Next(users.Count)]
            });
            database.Logs.AddRange(logs);
            database.SaveChanges();
        }

        private static void CreateUsers(DatabaseContext database)
        {
            database.Users.AddRange(new User[] {
                new User {
                    CreationDate = DateTime.Now,
                    Email = "johndoe@email.com",
                    Nome = "John Doe",
                    Password = "P@ssw0rd",
                    Token = Guid.NewGuid().ToString()
                },
                new User {
                    CreationDate = DateTime.Now,
                    Email = "foobar@email.com",
                    Nome = "Foo Bar",
                    Password = "P@ssw0rd",
                    Token = Guid.NewGuid().ToString()
                },
            });
            database.SaveChanges();
        }
    }
}
