using Microsoft.Extensions.Options;
using Zaptibot.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureOptions<JwtTokenSettingsSetup>();

builder.Services.AddSingleton<JwtTokenGenerator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
    });
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.MapGet("options", (IOptionsSnapshot<JwtTokenSettings> optionsSnapshot) => optionsSnapshot.Value);

// app.MapPost("/login", (LoginRequest request, TokenGenerator tokenGenerator) => new
// {
//     access_token = tokenGenerator.GenerateToken(request.Email)
// });

app.Run();