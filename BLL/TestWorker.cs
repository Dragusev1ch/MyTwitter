using DAL.EF;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TestWorker
    {
        public static KeyValuePair<byte[], byte[]> GeneratePassword(string password)
        {
            byte[] passwordHash, passwordSalt;


            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }


            return new KeyValuePair<byte[], byte[]>(key: passwordHash, value: passwordSalt);
        }
        public TestWorker()
        {
            using var context = new MainContext(DBContextFactory.GenerateOptions());

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var posts = new List<Post>
            {
                new()
                {
                    Title = "Holiday",
                    Description = "Something",
                    Views = (uint) Random.Shared.Next(500),
                    Likes = (uint) Random.Shared.Next(500)
                },
                new()
                {
                    Title = "Weekend",
                    Description = "Something about weekend",
                    Views = (uint) Random.Shared.Next(500),
                    Likes = (uint) Random.Shared.Next(500)
                },
                new()
                {
                    Title = "Dinner",
                    Description = "Something about dinner",
                    Views = (uint) Random.Shared.Next(500),
                    Likes = (uint) Random.Shared.Next(500)
                },
            };

            context.Posts.AddRange(posts);
            context.SaveChanges();

            var defaultU = GeneratePassword("default");
            var adminU = GeneratePassword("admin");

            var users = new List<User>
            {
                new()
                {
                    Name = "default",
                    Email = "default@email.com",
                    PasswordHash = defaultU.Key,
                    PasswordSalt = defaultU.Value,
                    Posts = new List<Post>
                    { new() { ID = posts[0].ID }, new() { ID = posts[1].ID }, new() { ID = posts[2].ID }
                    }
                },
                new()
                {
                    Name = "admin",
                    Email = "admin@email.com",
                    PasswordHash = adminU.Key,
                    PasswordSalt = adminU.Value,
                    Posts = new List<Post>
                    { new() { ID = posts[0].ID }, new() { ID = posts[1].ID }, new() { ID = posts[2].ID }
                    },
                    IsAdmin = true
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            var coments = new List<Coment>
            {
                new()
                {
                    ID = posts[0].ID,
                    Description = "OMG",
                    Likes = (uint) Random.Shared.Next(500)
                },
                 new()
                {
                    ID = posts[1].ID,
                    Description = "OMG",
                    Likes = (uint) Random.Shared.Next(500)
                }
            };
            context.Coments.AddRange(coments);
            context.SaveChanges();
        }
       
    }
}
