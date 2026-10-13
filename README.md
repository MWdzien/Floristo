# Floristo

A full-stack **e-commerce application** for a florist shop — a **.NET 9** backend and an **Angular** storefront, kept in a single Nx-managed monorepo. The project is a deliberate exploration of building a non-trivial commerce domain the right way: a rich, well-bounded backend domain rather than a CRUD layer over a database.

> Status: in active development. The backend is the primary focus of the project.

## Repository layout

```
floristo-backend/   .NET 9 backend — DDD, CQRS, Clean Architecture (see its own README)
floristo-web/       Angular storefront
```

## Backend — the core of the project

The backend is where the interesting design lives — **Domain-Driven Design, CQRS with separate read/write models, and Clean Architecture** over PostgreSQL, with Firebase auth and secrets managed through Google Secret Manager, deployed on Google Cloud (Cloud Run + Cloud SQL).

📖 **[Full backend documentation →](./floristo-backend/README.md)**

## Frontend

An **Angular** storefront (`floristo-web/`) consuming the backend's storefront REST API, deployed to Firebase Hosting.

## Tech stack

| Layer | Stack |
|---|---|
| Backend | .NET 9, ASP.NET Core, EF Core, PostgreSQL |
| Frontend | Angular, TypeScript |
| Auth | Firebase (JWT) |
| Cloud | Google Cloud — Cloud Run, Cloud SQL, Firebase Hosting |
| Tooling | Nx monorepo |

## Running locally

Each part runs independently — see **[floristo-backend/README.md](./floristo-backend/README.md)** for the backend (.NET 9 SDK + PostgreSQL) and `floristo-web/` for the Angular app.
