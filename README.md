# 📱 Device Management API

REST API for managing device resources.

## 🧰 Tech Stack
- .NET 9 / C# 13
- ASP.NET Core Web API
- PostgreSQL
- Entity Framework Core
- Docker & Docker Compose
- xUnit

## 🌐 API Overview

The API exposes a RESTful interface to manage devices:

- ➕ Create, update (PUT/PATCH), retrieve and delete devices
- 🔍 Filter by brand and state

All endpoints are documented via Swagger.

#### ❗ Error Handling

The API uses `ProblemDetails` class to provide consistent error responses.<br>
Domain exceptions are mapped to appropriate HTTP status codes.

#### ✅ Validation

 Input validation is performed using Data Annotations.<br>
> **Note:** This was chosen for simplicity, as current validations are structural and straightforward.

#### 📑 Pagination

Offset-based pagination (`page` and `pageSize`) is used for simplicity and clarity.

#### 🧾 JSON Conventions

- camelCase property names<br>
- Enums serialized as strings (camelCase)<br>
- Dates in UTC

#### 🪵 Logging

Basic console logging is enabled and can be extended if needed.

#### ❤️ Health Check

A `/health` endpoint is exposed to allow basic health monitoring and database connectivity checks.

#### 🔐 Security

Authentication and authorization _were **not implemented**._<br>
If needed, security could be added at the API layer using ASP.NET Core authentication middleware (e.g., JWT with `[Authorize]` attributes).

### 🧪 Tests

The solution includes unit tests for the domain layer (business rules) and application layer (use case orchestration).
> **Note:** HTTP-level integration tests were intentionally omitted, as the API layer is thin and declarative, and its behavior is already validated through contracts and Swagger documentation.

## 🚀 How to Run

### 🐳 Using Docker

```
docker-compose up --build
```
API will be available at:

- http://localhost:8080
- Swagger: http://localhost:8080/swagger

### 💻 Running Locally
```
# Apply migrations
dotnet ef database update -p src/Device.Infrastructure -s src/Device.Api

dotnet build
dotnet run --project src/Device.Api
```

⚠️ **A local PostgreSQL instance is required.**