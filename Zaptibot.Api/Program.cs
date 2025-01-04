using Microsoft.OpenApi.Models;
using Zaptibot.Api.Exceptions;
using Zaptibot.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insert your JWT bearer token."
        });
        
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                ArraySegment<string>.Empty
            }
        });
    });
    
    // Custom extension method
    builder.Services.AddServices(builder.Configuration);
    
    builder.Services.AddControllers();
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
        
        // NOTE: Create Users table when the application starts
        await app.CreateUsersTableAsync();
        await app.CreateSoundsTableAsync();
    }

    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }

    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllers();
    
    app.UseExceptionHandler();
}

app.Run();