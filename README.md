# Gym Tracker API

A REST API for workout tracking, built with ASP.NET Core. Handles user authentication, workout template management, and session logging.

**Live API (Swagger):** https://gym-tracker-api-production.up.railway.app/swagger

---

## Tech stack

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- JWT Authentication

## Architecture

The API follows a standard layered structure — controllers handle HTTP routing, with EF Core managing database access via a code-first schema. Authentication uses JWT bearer tokens, with protected endpoints requiring a valid token in the `Authorization` header.

PostgreSQL was chosen over a lightweight alternative like SQLite to match a realistic production environment and to gain experience with a relational database that scales beyond local development.

## Endpoints

Full interactive documentation is available via Swagger at the live API link above. Core resource groups:

- `/api/auth` — registration and login, returns JWT
- `/api/templates` — CRUD for workout templates and exercises
- `/api/sessions` — start, log, and retrieve workout sessions

## Running locally

Clone the repository, then set the following environment variables (via `appsettings.json` or environment):

| Variable | Description |
|---|---|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string |
| `Jwt__Key` | Secret key for signing JWT tokens |
| `Jwt__Issuer` | JWT issuer |
| `Jwt__Audience` | JWT audience |
| `AllowedOrigin` | Frontend URL for CORS policy |

Then run:

```bash
dotnet run
```

The API will be available at `https://localhost:7149` by default.

## What I'd add next

- More detailed session analytics (total volume, per-exercise progression)
- Personal records endpoint (track lifetime bests per exercise per user)
- Refresh token support alongside the current JWT implementation
- Expanded test coverage across controllers and service logic
