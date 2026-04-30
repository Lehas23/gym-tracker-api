\# Gym tracker API

A REST API for tracking gym workouts built with ASP.NET Core. Live API: https://gym-tracker-api-production.up.railway.app/swagger

## Tech stack

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- JWT Authentication

## Features

- User authentication with JWT
- Create and manage workout templates
- Log workout sessions
- Track sets, reps and weight per exercise

## Running the project

- Clone the repo
- Set the following environment variables:
  - `ConnectionStrings__DefaultConnection` — PostgreSQL connection string
  - `Jwt__Key` — secret key for JWT
  - `Jwt__Issuer` — JWT issuer
  - `Jwt__Audience` — JWT audience
  - `AllowedOrigin` — frontend URL for CORS
