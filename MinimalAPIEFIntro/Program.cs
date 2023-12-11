using Microsoft.EntityFrameworkCore;
using MinimalAPIEFIntro.Data;
using MinimalAPIEFIntro.Models;
using System.Net;

namespace MinimalAPIEFIntro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // GET /jokes fetches all setups and ids 
            app.MapGet("/jokes", (ApplicationContext context) =>
            {
                return Results.Json(context.Jokes.Select(j => new { j.Setup, j.Id }).ToArray());
            });

            // GET /jokes/1 fetches all data for a single joke
            app.MapGet("/jokes/{id}", (ApplicationContext context, int id) =>
            {
                Joke? joke = context.Jokes.Where(j => j.Id == id).SingleOrDefault();
                if(joke == null)
                {
                    return Results.NotFound();
                }
                return Results.Json(joke);
            });

            // POST /jokes creates a new joke
            app.MapPost("/jokes", (ApplicationContext context, Joke joke) =>
            {
                context.Jokes.Add(joke);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            });

            app.Run();
        }
    }
}