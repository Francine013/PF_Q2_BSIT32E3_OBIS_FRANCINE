using Microsoft.EntityFrameworkCore;
using PokedexApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext registration (Minimalist, no DbSet in AppDbContext)
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=pokemon.db"));

// 2. CORS configuration for frontend access
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
app.MapControllers();

// 3. Database Initialization & Seeding (Strictly via Raw SQL)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    
    // Create Table if missing
    db.Database.ExecuteSqlRaw(@"
        CREATE TABLE IF NOT EXISTS Pokemon (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL UNIQUE,
            Height INTEGER,
            Weight INTEGER,
            ImageUrl TEXT
        );");

    // Seed Data (from instructions)
    var seedingSql = new[]
    {
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (1, 'pikachu', 1, 2, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/25.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (2, 'bulbasaur', 7, 69, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/1.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (3, 'charmander', 6, 85, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/4.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (4, 'squirtle', 5, 90, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/7.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (5, 'butterfree', 11, 320, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/12.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (6, 'pidgeot', 15, 395, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/18.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (7, 'arcanine', 19, 1550, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/59.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (8, 'gengar', 15, 405, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/94.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (9, 'gyarados', 65, 2350, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/130.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (10, 'snorlax', 21, 4600, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/143.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (11, 'dragonite', 22, 2100, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/149.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (12, 'mewtwo', 20, 1220, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/150.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (13, 'lucario', 12, 540, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/448.png')",
        "INSERT OR IGNORE INTO Pokemon (Id, Name, Height, Weight, ImageUrl) VALUES (14, 'greninja', 15, 400, 'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/658.png')"
    };

    foreach (var sql in seedingSql)
    {
        db.Database.ExecuteSqlRaw(sql);
    }
}

app.Run();
