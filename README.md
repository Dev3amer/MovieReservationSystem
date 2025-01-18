# Movie Reservation System API

Welcome to the **Movie Reservation System API**! This platform allows users to search for movies, reserve tickets, and view showtimes, all while providing seamless interactions and secure booking for moviegoers. The project is developed by **Me** with a focus on building an intuitive, secure backend using **ASP.NET Core**.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [API Documentation](#api-documentation)
- [Setup and Installation](#setup-and-installation)

## Features

The **Movie Reservation System** offers the following key features:

## Technologies Used

- **Backend Framework**: ASP.NET Core
- **RESTful API**: For front-end integration
- **Database**: SQL Server
- **Authentication**: JWT (JSON Web Tokens)
- **ORM**: Entity Framework Core
- **Payment Integration**: Stripe
- **Email Service**: Zoho Mail
- **Mapping**: Auto Mapper
- **Validation**: Fluent Validation
- **API Docs**: Swagger/OpenAPI

## Architecture

The **Movie Reservation System API** follows an **Clean Architecture** with the following layers:

1. **API Layer:** Offers API endpoints for client interaction and communication.
2. **Core Layer:** Handles CQRS and business workflows.
3. **Service Layer:** Handles application logic.
4. **Infrastructure Layer:** Manages external concerns like database access
5. **Data Layer:** Contains the core entities, value objects, and business rules.

Patterns used:

- **Repository Pattern:**
  Abstracts data access logic to promote maintainability and testability.
  Enables easy switching of data sources or database technologies without affecting business logic.

- **CQRS (Command Query Responsibility Segregation):**
  Separates read and write operations into distinct models, improving performance and scalability.
  Commands handle operations that modify state (e.g., creating a reservation), while Queries fetch data without side effects.

- **Dependency Injection:**
  Ensures loose coupling and promotes testability by injecting dependencies into classes.
  Configured via .NET Core's built-in DI container.

- **Factory Pattern:**
  Used for creating complex objects, such as initializing specific configurations for third-party services like Stripe and Zoho Mail.

- **SOLID Principles** applied:
  - **Single Responsibility Principle**
  - **Open/Closed Principle**
  - **Dependency Inversion Principle**

## API Documentation

For detailed API documentation and testing, visit our Swagger UI:

[Movie Reservation API Documentation](https://cinemaapi.runasp.net/swagger/index.html)

## Setup and Installation

To set up the project locally, follow these steps:

1. **Clone the repository**:

   ```bash
   git clone https://github.com/your-repo/MovieReservationSystem.git
   ```

2. **Navigate to the project directory and restore the dependencies**:

   ```bash
   dotnet restore
   ```

3. **Update the configuration settings in appsettings.json with your Zoho Mail & Stripe credentials, SQL Server, and other necessary credentials.**

4. **Run the database migrations:**

   ```bash
   dotnet ef database update
   ```

Start the application:
```bash
    dotnet run
```
