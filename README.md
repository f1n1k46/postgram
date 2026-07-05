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

The application follows a layered architecture.

```text
Client
   │
   ▼
Controllers
   │
   ▼
Services
   │
   ▼
Repositories
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server
```

### Layers

#### Controllers

Handle HTTP requests and return HTTP responses.

#### Services

Contain business logic and application rules.

#### Repositories

Handle communication with the database using Entity Framework Core.

#### DTOs

Transfer data between the client and the server.

#### Middleware

Provides centralized exception handling.

#### Validators

Validate incoming requests using FluentValidation.

---

# 📂 Project Structure

```text
Postgram
│
├── Controllers
├── Services
├── Repositories
├── Data
├── Models
├── DTOs
├── Validators
├── Middleware
├── Helpers
└── Program.cs
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
