<div align="center">
<img src="https://marcroussy.com/wp-content/uploads/2018/03/onion-architecture.png" width="192" height="192" />

# Onion Architecture Base Project

![React.js](https://img.shields.io/badge/C%23-.NET-61DAFB?logo=dotnet)
![GitHub Repo stars](https://img.shields.io/github/stars/yusufkalyoncu/OnionArchitecture?style=flat)
![GitHub Issues](https://img.shields.io/github/issues/yusufkalyoncu/OnionArchitecture)
![GitHub Pull Requests](https://img.shields.io/github/issues-pr/yusufkalyoncu/OnionArchitecture)
![GitHub License](https://img.shields.io/github/license/yusufkalyoncu/OnionArchitecture)
</div>

This project is a foundational implementation of the Onion Architecture pattern, designed to serve as a base for building scalable, maintainable, and testable .NET applications. The architecture ensures a clear separation of concerns and adheres to SOLID principles, making it a robust starting point for any enterprise-level application.

## Project Structure

The project is divided into three main layers: Core, Infrastructure, and Presentation. Each layer contains specific projects that encapsulate distinct responsibilities.

### 1. Core

The Core layer encapsulates the business logic and domain entities, ensuring the application’s core remains independent of external concerns.

#### OnionArchitecture.Application
- **Abstractions:** Interfaces for services, repositories, and other components.
- **Behaviors:** Implements cross-cutting concerns such as logging and validation as pipeline behaviors.
- **Repositories:** Interfaces for Read and Write repositories, as well as the UnitOfWork pattern.
- **Services:** Interfaces for core services such as password hashing, token management, and user services.
- **DTOs:** Data Transfer Objects for passing data between layers.
- **Features:** Application features organized into commands and queries.
- **Options:** Configuration options mapped from `appsettings.json`.

#### OnionArchitecture.Domain
- **Entities:** Represents the core business entities.
- **Errors & Exceptions:** Manages domain-specific errors and exceptions.
- **Shared:** Contains shared logic and utilities across the domain.
- **ValueObjects:** Immutable objects defined by their value rather than identity.

### 2. Infrastructure

The Infrastructure layer provides implementations for interacting with external systems and includes low-level services necessary for the application’s operations.

#### OnionArchitecture.Infrastructure
- **Services:** Implements core infrastructure services such as password hashing and JWT token management.

#### OnionArchitecture.Persistence
- **Configurations:** Entity Framework Core configurations for database entities.
- **Contexts:** Database context classes for managing Entity Framework Core's interaction with the database.
- **Repositories:** Implements the repository pattern, separated into Read and Write operations to adhere to the CQRS principle.
- **Seeds:** Responsible for seeding initial data into the database.
- **Services:** Additional services related to database operations.

### 3. Presentation

The Presentation layer contains the API project, which serves as the entry point for client applications.

#### OnionArchitecture.API
- **Controllers:** Handles HTTP requests and maps them to application features.
- **Extensions:** Contains extension methods specific to the API layer.
- **Middlewares:** Implements custom middlewares such as global exception handling and request logging.

## Key Design Patterns & Principles

- **Onion Architecture:** Promotes a clear separation of concerns, making the application more maintainable and testable.
- **Generic Repository Pattern:** Abstracts data access logic into reusable components, adhering to SOLID principles.
- **Options Pattern:** Uses the Options Pattern to bind configuration settings from `appsettings.json` to strongly-typed objects.
- **Decorator Pattern:** Implements repository caching using the Decorator Pattern to enhance performance.
- **Result Pattern:** The application is built around a Result pattern for consistent handling of success and failure states.
- **Global Exception Handling:** Centralized error handling through a global exception handler middleware ensures consistent exception management.
- **Pipeline Behaviors:** Implements cross-cutting concerns like logging and validation as pipeline behaviors, maintaining a clean separation of concerns.

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- PostgreSQL Server (for database operations)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yusufkalyoncu/OnionArchitecture.git
   cd OnionArchitecture

2. Restore NuGet packages:
   ```bash
   dotnet restore

3. Configure your database connection string (PostgreSQL)

4. Add migration:
   ```bash
   Add-Migration Initial
   dotnet ef migrations add

Add-Migration: When using the Package Manager Console (PMC)

dotnet ef migrations add: When using the .NET Command Line Interface (.NET CLI)

5. Update database:
    ```bash
    Update-Database
    dotnet ef database update

Update-Database: When using the Package Manager Console (PMC)

dotnet ef database update: When using the .NET Command Line Interface (.NET CLI)

6. Run the project:
    ```bash
    dotnet run --project src/Presentation/OnionArchitecture.API

7. Run the unit tests to ensure everything is working correctly:
    ```bash
    dotnet test

## Contributing
Contributions are welcome! Feel free to fork this repository, make your changes, and submit a pull request. Please ensure that your contributions are well-documented and include tests where applicable.

## License

This project is licensed under the MIT License. See the [MIT](https://choosealicense.com/licenses/mit/) file for more details.

  
