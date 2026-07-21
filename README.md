# 🚀 Postgram API

A RESTful social media API built with **ASP.NET Core**, **Entity Framework Core**, and **SQL Server**.

The project demonstrates modern backend development practices including layered architecture, JWT authentication, Repository Pattern, Dependency Injection, FluentValidation, and global exception handling.

---

## ✨ Features

- 👤 User management (CRUD)
- 📝 Post management (CRUD)
- 💬 Comment management (CRUD)
- ❤️ Like / Unlike posts
- 🔐 JWT Authentication
- 🔒 Password hashing using BCrypt
- ✅ Request validation with FluentValidation
- ⚠️ Global exception handling middleware
- 🗄️ SQL Server + Entity Framework Core
- 🏗️ Repository & Service architecture

---

## 🛠️ Tech Stack

- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- BCrypt.Net
- FluentValidation
- LINQ
- Dependency Injection

---

## 🏛️ Architecture

The application follows the Clean Architecture pattern, separating responsibilities into independent layers with inward-facing dependencies.

                API
              /     \
             ▼       ▼
      Application  Infrastructure
             ▼       ▼
             Domain

### Project Structure

**Postgram.API**

The entry point of the application.

Responsibilities:

- Exposes REST API endpoints.
- Handles HTTP requests and responses.
- Configures Dependency Injection.
- Registers middleware and application services.

**Postgram.Application**

Contains the application's business logic and use cases.

Responsibilities:

- Business services.
- DTOs.
- Validation using FluentValidation.
- Service interfaces.
- Application abstractions (repository and infrastructure interfaces).

**Postgram.Domain**

The core of the application.

Responsibilities:

- Domain entities.
- Domain enums and business models.
- Independent of ASP.NET Core, Entity Framework Core, and any external libraries.

**Postgram.Infrastructure**

Implements external dependencies.

Responsibilities:

- Entity Framework Core.
- DbContext.
- Repository implementations.
- JWT token generation.
- Password hashing.
- Database migrations.

The Domain layer has no dependencies on other projects. Application depends only on Domain, Infrastructure implements the abstractions defined by the inner layers, and API composes the application through Dependency Injection.

---

# 📂 Project Structure

```text
Postgram.sln

├── Postgram.API
│   ├── Controllers
│   ├── Middleware
│   ├── Program.cs
│   └── appsettings.json
│
├── Postgram.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Validators
│
├── Postgram.Domain
│   └── Models
│
└── Postgram.Infrastructure
    ├── Data
    ├── Repositories
    ├── Migrations
    └── Helpers

```

---

# 🗄️ Database Entities

## User

- UserId
- Name
- Username
- Nickname
- Email
- PasswordHash
- Age
- CreatedAt

## Post

- PostId
- Title
- Text
- UserId
- CreatedAt

## Comment

- CommentId
- Text
- UserId
- PostId
- CreatedAt

## Like

- UserId
- PostId

---

# 🌐 REST API Endpoints

## Authentication

| Method | Endpoint |
|---------|----------|
| POST | `/api/auth/register` |
| POST | `/api/auth/login` |

---

## Users

| Method | Endpoint |
|---------|----------|
| GET | `/api/users` |
| GET | `/api/users/{id}` |
| GET | `/api/users/{id}/posts` |
| PUT | `/api/users/{id}` |
| DELETE | `/api/users/{id}` |

---

## Posts

| Method | Endpoint |
|---------|----------|
| GET | `/api/posts` |
| GET | `/api/posts/{id}` |
| POST | `/api/posts` |
| PUT | `/api/posts/{id}` |
| DELETE | `/api/posts/{id}` |

---

## Comments

| Method | Endpoint |
|---------|----------|
| GET | `/api/comments` |
| GET | `/api/comments/{id}` |
| POST | `/api/comments` |
| PUT | `/api/comments/{id}` |
| DELETE | `/api/comments/{id}` |

---

## Likes

| Method | Endpoint |
|---------|----------|
| POST | `/api/likes` |
| DELETE | `/api/likes` |

---

# 🔐 Authentication

The API uses **JWT Bearer Authentication**.

After successful authentication, the server returns a JWT token.

Every protected request must include:

```http
Authorization: Bearer <your_jwt_token>
```

Passwords are securely hashed using **BCrypt** before being stored in the database.

---
