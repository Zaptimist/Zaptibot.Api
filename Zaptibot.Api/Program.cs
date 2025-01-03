WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

WebApplication app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // Improves developments speed. You don't have to press the "Try it out" button
            options.EnableTryItOutByDefault();
        });
    }

    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }
}

app.Run();