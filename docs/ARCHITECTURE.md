# Architecture Overview

## Design Principles

- **Clean Architecture** - Separation of concerns with clear boundaries
- **Domain-Driven Design** - Rich domain models with business logic
- **SOLID Principles** - Maintainable and extensible code
- **Repository Pattern Ready** - Easy to add abstraction layer

## Layers

### 1. Presentation Layer
- **Controllers** - Handle HTTP requests and responses
- **Views** - Razor templates for rendering HTML
- **ViewModels** - DTOs for data transfer between layers

### 2. Domain Layer
- **Entities** - Core business objects (Project, BlogPost, ContactMessage)
- **Value Objects** - Immutable objects (Email, Url, TechnologyStack)
- **Enums** - Type-safe status values

### 3. Data Layer
- **ApplicationDbContext** - EF Core database context
- **Migrations** - Database schema versioning
- **Configurations** - Entity configurations and relationships

## Core Concepts

### Base Entity
All entities inherit from `BaseEntity` providing:
- GUID primary keys
- Audit fields (CreatedAt, UpdatedAt, DeletedAt)
- Soft delete support
- User tracking (CreatedBy, UpdatedBy, DeletedBy)

### Query Filters
- Global soft delete filter
- Automatic filtering of deleted records
- Override with `.IgnoreQueryFilters()` when needed

### Navigation Properties
- Category ↔ Projects/BlogPosts (One-to-Many)
- Tags ↔ Projects/BlogPosts (Many-to-Many via junction tables)
- Category ↔ SubCategories (Self-referencing)

## Data Flow

```
Request → Controller → DbContext → Database
                ↓
            ViewModel → View → Response
```

## Security

- **Authentication** - ASP.NET Core Identity
- **Authorization** - Role-based access control
- **Anti-forgery Tokens** - CSRF protection
- **Input Validation** - Data annotations + ModelState
- **SQL Injection Prevention** - Parameterized queries via EF Core
