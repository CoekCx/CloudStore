# CloudStore - Cloud Storage Solution

CloudStore is a Google Drive-like cloud storage solution built with .NET 8, implementing Clean Architecture principles. The application allows users to store, organize, and manage their files in the cloud with real-time notifications and email updates.

## Architecture Overview

### Clean Architecture Implementation
CloudStore follows Clean Architecture principles, ensuring:
- Independence of frameworks
- Testability
- Independence of UI
- Independence of Database
- Independence of external services

### Project Structure
```
src/
├── CloudStore.Domain/           # Enterprise Business Rules
├── CloudStore.Application/      # Application Business Rules
├── CloudStore.Infrastructure/   # External Concerns
├── CloudStore.Persistence/      # Data Access
├── CloudStore.Presentation/     # API Controllers & DTOs
└── CloudStore.Api/             # Application Entry Point
```

### Layer Dependencies
- Domain: No dependencies
- Application: Depends on Domain
- Infrastructure: Depends on Application
- Persistence: Depends on Application
- Presentation: Depends on Application
- API: Depends on all layers

### Layer Responsibilities

#### Domain Layer
- Core business entities
- Value objects
- Domain events
- Domain exceptions
- Core interfaces

#### Application Layer
- Command/Query handlers (CQRS with MediatR)
- Domain event handlers
- Application DTOs
- Interface definitions for infrastructure
- Business logic validators
- Application services interfaces

#### Infrastructure Layer
- AWS S3 implementation
- SendGrid email service
- JWT authentication
- SignalR notifications
- External service implementations

#### Persistence Layer
- Database context
- Entity configurations
- Migrations
- Read/Write repositories
- Query specifications

#### Presentation Layer
- API controllers
- Request/Response DTOs
- API-specific mapping profiles
- Route configurations
- API documentation
- Request validators

#### API Layer
- Application bootstrapping
- Dependency injection
- Middleware configuration
- Application configuration

## Technical Stack & Patterns

### Core Technologies
- .NET 8
- PostgreSQL
- AWS S3
- SendGrid
- SignalR

### Packages & Patterns
- MediatR (CQRS & Event Handling)
- Entity Framework Core (Data Access)
- FluentValidation (Validation)
- Mapster (Object Mapping)
- JWT Authentication
- Swagger/OpenAPI (API Documentation)

## V1 Features

### User Management
- User registration
- JWT-based authentication
- Profile management (update details)
- Password reset functionality

### File Management
- File upload (with progress tracking)
- File download
- File deletion
- Basic file metadata
- Folder creation and management
- Basic file hierarchy

### Storage
- AWS S3 integration
- Large file support
- Secure file access via presigned URLs

### Notifications
- Real-time upload/download status (SignalR)
- Email notifications for:
  - Account verification
  - Password reset
  - Storage warnings
- Real-time file operation notifications

## Getting Started

### Prerequisites
- .NET 8 SDK
- PostgreSQL
- AWS Account with S3 access
- SendGrid Account
- Docker (optional)