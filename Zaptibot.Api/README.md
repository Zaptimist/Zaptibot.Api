# Zaptibot.Api

This project is a demonstration of my coding skills and knowledge, created to showcase my abilities and allow others to review my work. After the review, I will maintain this project to develop a real API for my old Discord bot (personal project).

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Getting Started

1. **Configure the connection string:**

    Update the connection string in `appsettings.json` to point to your SQLite database file:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=path_to_your_database/zaptibot.sqlite"
      }
    }
    ```

2. **Run the application:**

    ```bash
    dotnet run --project Zaptibot.Api
    ```

## Documentation

This project uses Swagger for API documentation. Once the application is running, you can access the Swagger UI at `/swagger` to explore and test the API endpoints.