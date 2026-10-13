# Floristo

A modular **e-commerce backend** built in **.NET 9**, designed around **Domain-Driven Design**, **CQRS**, and **Clean Architecture**. Floristo is a personal project exploring how to model a non-trivial commerce domain properly — rich domain models, clear boundaries, and a strict separation between write and read paths — rather than a CRUD layer over a database.

> Status: in active development.

## Architecture

The solution follows Clean Architecture, with dependencies pointing inward toward the domain:

```
Floristo.Api              → ASP.NET Core controllers (storefront + backoffice), auth, OpenAPI
Floristo.Application      → use cases: commands, queries, handlers, DTOs (CQRS)
Floristo.Domain           → aggregates, entities, value objects, domain events, factories
Floristo.Infrastructure   → EF Core, PostgreSQL, read/write DbContexts, external services
Floristo.Shared /
Floristo.SharedAbstractions→ cross-cutting primitives (commands, queries, paging, base types)
```

### Key design decisions

- **Domain-Driven Design.** Each bounded context (Products, Categories, Customers, Cart) is an aggregate with invariants enforced inside the domain — value objects instead of primitives, `internal` constructors behind factories, and domain events raised on state changes.
- **CQRS with separate read and write models.** Writes go through aggregates and a `WriteDbContext`; reads are served by dedicated read models and a `ReadDbContext`, so query shapes never leak into the domain. For example, a cart's total price is computed on read from current product prices — it is not stored on the aggregate.
- **Secure by default.** Authentication/authorization via Firebase (JWT); secrets are pulled from Google Secret Manager, never committed to source control.

## Tech stack

- **.NET 9**, ASP.NET Core, Entity Framework Core
- **PostgreSQL**
- **Firebase** (authentication / JWT)
- **Google Cloud** — Cloud Run (API), Cloud SQL (database)
- OpenAPI with Bearer authentication

## Domain

| Context | Responsibility |
|---|---|
| **Products** | Catalog items, pricing, images, SEO, publishing lifecycle |
| **Categories** | Hierarchical category tree and storefront navigation |
| **Customers** | Customer profiles, linked to Firebase identities |
| **Cart** | Line items as references to products, quantity invariants |

## Testing

Unit tests cover the **domain** (aggregate invariants) and the **application** layer (command handlers), kept separate so failures point to a single layer.

## Running locally

Requires the **.NET 9 SDK** and a **PostgreSQL** instance.

```bash
dotnet build
dotnet run --project src/Floristo.Api
```

Configure the PostgreSQL connection and Firebase/secret settings via user secrets or environment variables (the app reads secrets from Google Secret Manager in cloud environments).
