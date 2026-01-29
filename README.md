# Devices Management API

A RESTful API built with **.NET 9** and **C# 13** for managing device resources, following Clean Architecture principles and Domain-Driven Design (DDD).

## 🚀 How to Run (Docker)

This project is fully containerized. To run the application, ensure you have **Docker Desktop** installed.

1. **Clone the repository**:
   ```bash
   git clone <your-repo-url>
   cd <project-folder>
Run using Docker Compose:

Bash

docker-compose up --build
Access the API:

Swagger UI: http://localhost:7217/swagger

The database (SQLite) will be automatically created and migrated on startup.

🛠 Tech Stack
Framework: .NET 9

Language: C# 13

Database: SQLite (Persisted via Docker Volumes)

ORM: Entity Framework Core

Documentation: Swagger/OpenAPI

Containerization: Docker & Docker Compose

📌 Domain Rules & Validations
The API implements the following business rules as required:

Creation Time: Set automatically and cannot be updated.

Device Protection: Name and Brand cannot be updated if the device state is in-use.

Safe Deletion: Devices in the in-use state cannot be deleted.

🏗 Architecture
The project is organized into four layers:

Domain: Entities, Enums, and Core Exceptions.

Application: DTOs, Interfaces, and Services (Business Logic).

Infrastructure: Data persistence (EF Core), Repositories, and Migrations.

API: Controllers, Middlewares (Global Exception Handling), and Configurations.

🧪 Testing
To run the unit tests:

Bash

dotnet test
The solution includes coverage for domain validations and service logic to ensure reliability.