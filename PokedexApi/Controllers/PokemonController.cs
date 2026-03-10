using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokedexApi.Data;
using PokedexApi.Models;

namespace PokedexApi.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PokemonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPokemon()
        {
            var sql = "SELECT Id, Name, Height, Weight, ImageUrl FROM Pokemon";
            
            // In a real application, we would use Dapper or EF Core projection.
            // But following the lab's spirit of "raw SQL", we use FromSqlRaw.
            // Note: FromSqlRaw usually requires a DbSet, but since we don't have one,
            // we can use the Database.SqlQuery (available in EF Core 7.0+).
            
            var result = await _context.Database
                .SqlQueryRaw<PokemonModel>(sql)
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPokemonByName(string name)
        {
            var sql = "SELECT Id, Name, Height, Weight, ImageUrl FROM Pokemon WHERE Name = {0}";
            
            var result = await _context.Database
                .SqlQueryRaw<PokemonModel>(sql, name.ToLower())
                .ToListAsync();

            var pokemon = result.FirstOrDefault();

            if (pokemon == null)
            {
                return NotFound(new { message = "Pokemon not found" });
            }

            return Ok(pokemon);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePokemon(CreatePokemonRequest request)
        {
            var sql = @"
            INSERT INTO Pokemon (Name, Height, Weight, ImageUrl)
            VALUES ({0}, {1}, {2}, {3});";

            var rowsAffected = await _context.Database
                .ExecuteSqlRawAsync(sql, request.Name, request.Height, request.Weight, request.ImageUrl);

            return Ok(new
            {
                message = "Pokemon created successfully",
                rowsAffected = rowsAffected
            });
        }
    }
}
