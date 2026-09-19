# MatricAdvisor

> A South African tertiary-study discovery project focused on helping matric learners understand APS scores, university programmes, and programme requirements.

## Project Status

**In active development.**

This repository contains two stages of the project:

- `MatricConnect/` — the earlier C# console prototype used to validate the core university-search and eligibility idea.
- `MatricAdvisor.API/` — the current ASP.NET Core Web API implementation.

The API is being developed incrementally. The sections below deliberately separate what is implemented today from what is still planned.

## Problem

Information about university programmes, APS requirements, required subjects, and application criteria is spread across many institution websites. MatricAdvisor explores how a single system could make that information easier for South African learners to navigate.

## Current API Implementation

The `MatricAdvisor.API` project currently includes:

- ASP.NET Core Web API on **.NET 10**
- `StudentsController` with APS calculation:
  - `POST /api/students/calculate-aps`
- `UniversitiesController` with early university endpoints:
  - `GET /api/universities`
  - `GET /api/universities/{id}`
- Entity Framework Core
- PostgreSQL through `Npgsql.EntityFrameworkCore.PostgreSQL`
- Initial EF Core migration
- Domain models for:
  - University
  - Programme
  - SubjectRequirement
  - SubjectMark
- University → Programme → SubjectRequirement relationships
- Swagger / OpenAPI for API exploration
- CORS configuration for a future frontend client

> **Note:** The university controller currently uses placeholder data while database-backed endpoints are being developed.

## Current Architecture

```text
Client / API consumer
        |
        v
ASP.NET Core Controllers
        |
        v
Domain Models + Business Rules
        |
        v
Entity Framework Core
        |
        v
PostgreSQL
```

## Tech Stack

### Implemented

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Npgsql
- Swagger / OpenAPI
- Git / GitHub

### Development Tools

- Visual Studio
- Postman
- DBeaver

## In Progress

The next API milestones are:

- Replace placeholder university responses with PostgreSQL-backed queries
- Complete programme endpoints
- Implement programme-requirement queries
- Move eligibility rules into dedicated business/service logic
- Add request/response DTOs and validation
- Improve error handling and API response consistency

## Planned

These are roadmap items and should not be read as currently implemented:

- Persistent student profiles
- Full programme eligibility recommendations
- Saved/favourite programmes
- React frontend
- Authentication
- Bursary and scholarship discovery
- Application tracking
- Notifications and reminders
- Automated tests and CI/CD

## Core Data Model

```text
University
    |
    | 1..*
    v
Programme
    |
    | 1..*
    v
SubjectRequirement
```

A programme stores a minimum APS and can have multiple subject requirements. This structure is intended to support programme-specific eligibility rules as the API evolves.

## Running the API

### Prerequisites

- .NET 10 SDK
- PostgreSQL
- A PostgreSQL connection string configured as `DefaultConnection`

From the repository root:

```bash
cd MatricAdvisor.API
dotnet restore
dotnet ef database update
dotnet run
```

When running in the Development environment, Swagger UI is enabled for exploring available endpoints.

## Design Principles

- Keep the MVP focused
- Prefer clear, maintainable backend structure over feature overload
- Separate implemented functionality from roadmap ideas
- Model real university requirements accurately
- Evolve from a validated console prototype into an API-driven application

## Author

**Lerato Molefe**  
Junior backend software developer in training, focused on C# and .NET.

- Portfolio: https://leratogladys.github.io/Portfolio
- GitHub: https://github.com/Leratogladys
- LinkedIn: https://www.linkedin.com/in/lerato-molefe-7403891b7
