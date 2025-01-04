using Zaptibot.Identity;
using Zaptibot.Identity.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureOptions<JwtTokenSettingsConfiguration>();

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

app.Run();