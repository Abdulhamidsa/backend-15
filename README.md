# IMDb-Style API

A RESTful API backend service for managing movie and TV show information, user ratings, bookmarks, and celebrity profiles. Built with Clean Architecture using .NET 8 and PostgreSQL.

## Tech Stack

- .NET 8.0 / Entity Framework Core 9.0
- PostgreSQL / Npgsql
- JWT Authentication (cookie-based)
- Swagger/OpenAPI

## Prerequisites

- .NET 8.0 SDK or higher
- PostgreSQL 12+ database server
- IDE (Visual Studio 2022, VS Code, or Rider)

## Configuration

Update connection string and JWT settings in `Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DB_CONNECTION": "Host=localhost;Database=imdb;Username=your_user;Password=your_password;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-min-32-characters",
    "Issuer": "Backend15Api",
    "Audience": "Backend15Client",
    "ExpiryMinutes": 60
  }
}
```

## Database Setup

1. Create a PostgreSQL database named `imdb`
2. Run migrations to initialize schema:
   ```bash
   dotnet ef database update --project Infrastructure --startup-project Api
   ```

## Running the Application

### Using .NET CLI

```bash
dotnet restore
dotnet build
dotnet run --project Api
```

### Using Visual Studio

1. Open `backend-15.sln`
2. Set `Api` as the startup project
3. Press F5 to run

The API will be available at `https://localhost:5001` (or ports specified in launchSettings.json).

Swagger documentation is accessible at the root URL when running in Development mode.

## Authentication

The API uses JWT tokens stored in HTTP-only cookies. After successful login/registration, the token is automatically included in subsequent requests.

CORS is configured for `http://localhost:5174` and `https://localhost:5174`.

## Testing

Run all tests:

```bash
dotnet test
```

Run specific test project:

```bash
dotnet test Tests/Tests.csproj
```

## License

This project is for educational purposes.
