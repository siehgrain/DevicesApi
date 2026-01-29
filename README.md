# Devices API — README

A RESTful API for managing devices, built with C# 13 and .NET 9.
This document provides a quick guide on how to run, test, and understand the API domain.

---

## Badges

(Add CI/CD, test coverage, and .NET version badges as available.)

---

## Table of Contents

* [Overview](#overview)
* [Features](#features)
* [Domain Model](#domain-model)
* [Domain Validations](#domain-validations)
* [Requirements](#requirements)
* [Quick Setup](#quick-setup)
* [Environment Variables](#environment-variables)
* [Main Endpoints](#main-endpoints)
* [Authentication (JWT)](#authentication-jwt)
* [Testing](#testing)
* [Docker / Deployment](#docker--deployment)
* [Future Improvements](#future-improvements)
* [Contribution](#contribution)
* [License](#license)

---

## Overview

A simple and robust API for CRUD operations on devices, with business rule validations and JWT security. Designed to be containerized easily and support multiple relational databases (PostgreSQL, SQL Server, MySQL, etc.).

---

## Features

* Create, update (PUT/PATCH), read (list and by id), and delete devices
* Filter by brand and state
* Domain validations prevent invalid operations (e.g., deleting an in-use device)
* JWT authentication to secure sensitive endpoints
* Database persistence (repository layer ready for multiple DBs)
* Docker Compose containerization
* Unit tests covering core logic

---

## Domain Model

**Device**

| Property     | Type       | Description                      |
| ------------ | ---------- | -------------------------------- |
| Id           | `Guid`     | Unique identifier                |
| Name         | `string`   | Device name                      |
| Brand        | `string`   | Device brand                     |
| State        | `enum`     | `Available`, `InUse`, `Inactive` |
| CreationTime | `DateTime` | Creation timestamp (read-only)   |

---

## Domain Validations

* `CreationTime` is read-only and **cannot** be changed.
* `Name` and `Brand` **cannot** be modified if the device is `InUse`.
* Devices in `InUse` state **cannot** be deleted.
* Additional validations (minimum length, required fields) are implemented in DTOs/commands.

---

## Requirements

* .NET 9 SDK
* C# 13
* Docker & Docker Compose (for container execution)
* A relational database (PostgreSQL, SQL Server, MySQL, etc.)

---

## Quick Setup

1. Clone the repository:

```bash
git clone https://github.com/siehgrain/DevicesApi.git
cd DevicesApi
```

2. Build and run with Docker Compose:

```bash
docker-compose up --build
```

The API will be available at: `http://localhost:7217`

To stop:

```bash
docker-compose down
```

---

## Environment Variables (examples)

Set in `appsettings.json` or via environment variables for production:

* `ConnectionStrings__Default` — database connection string
* `Auth__Username` — login username (development)
* `Auth__Password` — login password (development)
* `Jwt__Key` — secret key for JWT signing
* `Jwt__Issuer` — token issuer
* `Jwt__Audience` — token audience

> ⚠️ Never keep sensitive credentials in the repository — use environment variables, Azure Key Vault, AWS Secrets Manager, or another secrets manager in production.

---

## Main Endpoints

### Authentication

**POST** `/api/auth/login`

Request body (JSON):

```json
{
  "username": "admin",
  "password": "123456"
}
```

Response: `{ "token": "<jwt>" }` — use in the header `Authorization: Bearer <token>` to access protected endpoints.

### Devices (examples)

**GET** `/api/devices` — list all (supports query strings `brand` and `state` for filtering)

**GET** `/api/devices/{id}` — get by id

**POST** `/api/devices` — create a new device

Example request body:

```json
{
  "name": "Sensor X",
  "brand": "Acme",
  "state": "Available"
}
```

**PUT** `/api/devices/{id}` — full entity replacement

**PATCH** `/api/devices/{id}` — partial update (only allowed fields)

**DELETE** `/api/devices/{id}` — delete (fails if the device is `InUse`)

---

## curl Examples

Get JWT token:

```bash
curl -X POST http://localhost:7217/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"123456"}'
```

Create a device:

```bash
curl -X POST http://localhost:7217/api/devices \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"name":"Sensor X","brand":"Acme","state":"Available"}'
```

---

## Testing

* Run unit tests using the .NET test runner:

```bash
dotnet test
```

* Minimum coverage is recommended, with CI pipelines running tests on PRs.

---

## Docker / Deployment

`docker-compose.yml` is provided for local and test environments. Adjust environment variables/connection strings before pointing to production databases.


---

## Security Best Practices

* Replace development credentials with a proper identity provider in production (IdentityServer, Azure AD, Auth0, etc.)
* Use HTTPS and managed certificates in production
* Rotate JWT secret keys regularly and support revocation if needed

---

## Future Improvements

* Add a table to store logs for device actions and errors
* Move JWT authentication to use credentials stored in the database
* Pagination and sorting in device listings
* Advanced filtering (date ranges, multiple brands)
* Bulk import/export endpoints (CSV/JSON)
* Integration and end-to-end tests with real database
