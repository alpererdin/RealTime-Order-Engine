# RealTime Order Engine

A full-stack real-time restaurant order and session management system built with `.NET 10`, `Blazor WebAssembly`, `SignalR`, `EF Core`, and `PostgreSQL`.

This project was designed as an interview-ready sample to demonstrate:

- Layered backend architecture with clear separation of concerns
- Real-time communication using SignalR
- Authentication and role-based authorization with JWT
- Inventory-aware order processing
- QA mindset through automated unit tests around critical business rules

---

## How It Works

Clicking the customer link assigns the user to an available table and opens the menu. From there, customers can browse products, place orders, and track order status in real time.

Staff and kitchen panels are accessible from the menu page. The admin panel is accessible from the staff page after login.

> Note: Demo credentials should be configured through environment variables or user secrets. Do not hardcode them for production use.

---

## Screenshots

| Admin — Products | Kitchen Dashboard | Staff — Tables |
|---|---|---|
| <img width="500" src="https://github.com/user-attachments/assets/38d0564a-3ead-4154-a9b2-0a51d607886b" /> | <img width="500" src="https://github.com/user-attachments/assets/b0c8fec6-72ab-4828-86f7-7fd35633bfbe" /> | <img width="500" src="https://github.com/user-attachments/assets/572fc4e1-f602-45be-9a91-0a8e8b1aa36a" /> |

| Staff — Table Detail | Customer Menu | Order History |
|---|---|---|
| <img width="500" src="https://github.com/user-attachments/assets/509a40a2-d020-429f-b4d1-07faed3af22c" /> | <img width="200" src="https://github.com/user-attachments/assets/fe602f98-bfcf-450c-87a2-3df9c45b7fc4" /> | <img width="200" src="https://github.com/user-attachments/assets/cbed6a91-661b-4a0e-897e-9751430db30c" /> |

---

## Tech Stack

- Backend: ASP.NET Core Web API, .NET 10
- Frontend: Blazor WebAssembly
- Real-time: SignalR
- Database: PostgreSQL
- Auth: JWT with PIN-based access
- Deployment: Docker / Railway-ready

---

## Architecture

The solution follows a layered structure:

- `src/RealTimeOrderEngine.Api`
  ASP.NET Core API, authentication, SignalR hub, middleware, startup configuration
- `src/RealTimeOrderEngine.Application`
  Business services, repository contracts, service contracts, application exceptions
- `src/RealTimeOrderEngine.Domain`
  Core entities and enums
- `src/RealTimeOrderEngine.Infrastructure`
  EF Core `DbContext`, migrations, repository implementations
- `src/RealTimeOrderEngine.Shared`
  DTOs and SignalR/shared message contracts
- `src/RealTimeOrderEngine.Client`
  Blazor WebAssembly frontend for menu, kitchen, staff, and admin screens
- `tests/RealTimeOrderEngine.Application.Tests`
  Unit tests for critical auth and order workflows

Key implementation details:

- Repository pattern for data access
- DTO layer decoupling API contracts from domain entities
- Rate limiting on auth and general endpoints
- Blazor WASM served as static files from the API host
- Soft delete support with EF Core query filters
- Centralized API exception handling with `ProblemDetails`
- Model validation using `DataAnnotations`

## Interview Highlights

This repository is strongest when presented as:

- A practical full-stack `.NET` sample with real-time behavior
- A project that shows awareness of production concerns, not only happy-path coding
- A QA-minded implementation that validates inputs, handles failures centrally, and protects key business flows with tests

Recent hardening improvements added for interview quality:

- Removed hardcoded JWT secret and database connection details from source-controlled settings
- Replaced permissive CORS policy with explicit allowed origins
- Removed hardcoded default admin credentials from startup
- Added centralized exception handling and standardized API error responses
- Added validation attributes to key request DTOs
- Moved product update/delete behavior back behind the service layer
- Added unit tests for authentication and order processing rules

## Prerequisites

- `.NET SDK 10`
- Docker
- PostgreSQL, or Docker Compose for local database

## Local Setup

### 1. Start PostgreSQL

```bash
docker compose up -d
```

### 2. Configure Secrets

Set these values with environment variables or user secrets before starting the API:

```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=OrderEngineDb;Username=postgres;Password=postgres"
export Jwt__Secret="replace_this_with_a_long_random_secret_key_32_chars_min"
export Jwt__Issuer="RealTimeOrderEngine"
export Jwt__Audience="RealTimeOrderEngineUsers"
export AllowedOrigins__0="http://localhost:5104"
```

Optional development-only seed user:

```bash
export SeedAdmin__Name="Demo Admin"
export SeedAdmin__PinCode="2468"
export SeedAdmin__Role="Admin"
```

### 3. Run the API

```bash
dotnet run --project src/RealTimeOrderEngine.Api
```

### 4. Run the Client

```bash
dotnet run --project src/RealTimeOrderEngine.Client
```

> Note: DevHelper page at `/admin/devhelper` is available in development for seeding test data. Do not expose it in production.

## Run Tests

```bash
dotnet test src/RealTimeOrderEngine.slnx
```

## Suggested Demo Flow

1. Show the layered solution structure and explain why `Domain`, `Application`, and `Infrastructure` are separated.
2. Demonstrate the order flow from frontend to API to SignalR notification.
3. Explain stock validation and order status updates as business rules.
4. Show the centralized exception handler and DTO validation as production-readiness improvements.
5. Run the unit tests and explain what risks they cover.

## Testing Scope

Current automated coverage focuses on the most important business paths:

- login fails for missing/inactive staff
- login returns token for valid staff
- order creation decreases tracked stock
- order creation prevents insufficient stock scenarios
- order status updates trigger notifications

## Next Improvements

If the project is extended further, the most valuable additions would be:

- integration tests with `WebApplicationFactory`
- transactional order creation to make stock updates and order persistence atomic
- hashed PIN storage instead of plain-text PIN values
- structured logging and audit trails
- CI pipeline with build, test, and lint steps
- containerized API startup in `docker-compose`
