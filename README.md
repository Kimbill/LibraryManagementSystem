# Library Management System – ASP.NET Core Web API

This project is a Library Management System built with ASP.NET Core (.NET 8) as part of a backend engineering assessment.

The API allows authenticated users to:
- Register and log in
- Add, view, update, and delete books
- Search and paginate book records

The solution follows clean architecture principles, proper layering, and secure JWT-based authentication.

## Architecture Overview

The solution is organized into the following layers:

## LibraryManagementSystem
│
├── LawPavillion.LibraryManagement.Api
│ └── Controllers, Middleware, Configuration
│
├── LawPavillion.LibraryManagement.Application
│ └── DTOs, Interfaces, Services, Helpers
│
├── LawPavillion.LibraryManagement.Domain
│ └── Entities
│
└── LawPavillion.LibraryManagement.Infrastructure
└── DbContext, EF Core Configuration
