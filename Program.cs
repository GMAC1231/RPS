var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Register CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// 2️⃣ Enable CORS
app.UseCors("AllowAll");

string[] choices = { "rock", "paper", "scissors" };
Random r = new Random();

app.MapGet("/play/{player}", (string player) =>
{
    player = player.ToLower();
    string bot = choices[r.Next(3)];

    string winner =
        player == bot ? "draw" :
        (player == "rock" && bot == "scissors") ||
        (player == "paper" && bot == "rock") ||
        (player == "scissors" && bot == "paper")
        ? "player"
        : "bot";

    return Results.Json(new { player, bot, winner });
});

app.Run();
