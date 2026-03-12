# RealTime Order Engine

A full-stack real-time restaurant order management system built with .NET 10 and Blazor WebAssembly.

Live: https://realtime-order-engine-production.up.railway.app

---

## How it works

Clicking the link assigns you to an available table and opens the customer menu. From there you can browse the menu, place orders, and track their status.

Staff and kitchen panels are accessible via buttons on the menu page. Both require PIN `1234`.

Admin panel is accessible from the staff page after login.

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
- Deployment: Railway (Docker)

---

## Architecture

Clean Architecture — Domain / Application / Infrastructure / Api / Client / Shared

- Repository pattern for data access
- DTO layer decoupling API contracts from domain entities
- Rate limiting on auth and general endpoints
- Blazor WASM served as static files from the API host

---

## Local Development

```bash
# Start database
docker-compose up -d

# Run API
cd src/RealTimeOrderEngine.Api
dotnet run

# Run client (separate terminal)
cd src/RealTimeOrderEngine.Client
dotnet run
```

> Note: DevHelper page at `/admin/devhelper` is available in development for seeding test data. Do not expose in production.
