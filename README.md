# MyContactsApp

[![.NET Build](https://github.com/chetanb-11/MyContactsApp/actions/workflows/update-readme.yml/badge.svg)](https://github.com/chetanb-11/MyContactsApp/actions/workflows/update-readme.yml)
[![Open Issues](https://img.shields.io/github/issues/chetanb-11/MyContactsApp.svg)](https://github.com/chetanb-11/MyContactsApp/issues)

## 🌟 About the Project

`MyContactsApp` is a modern ASP.NET Core web application designed for efficient management of personal and professional contacts. It provides a clean, intuitive interface for adding, viewing, searching, and managing contacts, with features to organize them into distinct address books and count entries. A key enhancement includes robust **duplicate contact prevention**, ensuring that contacts with the same first and last name cannot be added. Built with a clear separation of concerns using a layered architecture and a dedicated service layer, this application demonstrates best practices in .NET development, including domain modeling, data persistence with Entity Framework Core, and an ASP.NET Core MVC web presentation layer.

## ✨ Features

*   **Duplicate Contact Prevention:** Prevents the creation of contacts with identical first and last names, ensuring data uniqueness and integrity.
*   **Address Book Management:** Organize contacts into custom address books for better categorization and management.
*   **Contact Counting:** Easily view the total number of contacts or specifically count contacts within any given address book.
*   **Comprehensive Contact Management:** Add, view, edit, and delete contact entries with essential details, including the ability to associate them with an address book.
*   **Robust Data Validation:** Ensures data integrity with custom validation rules for contact and address book information.
*   **Efficient Search:** Quickly find specific contacts using robust search functionalities across all stored data.

## 🚀 Tech Stack

This project is built using the following technologies:

*   **Framework:** .NET 10.0
*   **Language:** C#
*   **Web Framework:** ASP.NET Core MVC
*   **Data Access:** Entity Framework Core
*   **Database:** (Implicitly configured, often SQLite or SQL Server for development)
*   **Frontend:** HTML, CSS (Bootstrap-inspired styling)
*   **IDE:** Rider (inferred from `.idea` files)

## 🏗️ Architecture

The application follows a clean, layered architecture to ensure maintainability, scalability, and separation of concerns.

```
MyContactsApp
├── MyContactsApp.Web           (Presentation Layer - ASP.NET Core MVC)
├── MyContactsApp.Infrastructure (Persistence & Service Layer - Entity Framework Core, Data Context, Business Services)
└── MyContactsApp.Core          (Domain Layer - Models, Validations, Business Logic)
```

*   **`MyContactsApp.Web` (Presentation Layer):**
    *   This is the entry point of the application, an ASP.NET Core MVC project.
    *   Handles HTTP requests, renders views, and orchestrates user interactions.
    *   Contains Controllers, Views (`.cshtml`), and startup configuration (`Program.cs`).
*   **`MyContactsApp.Infrastructure` (Persistence & Service Layer):**
    *   Responsible for data access and encapsulating core business services.
    *   Utilizes Entity Framework Core to interact with the database (`AppDbContext.cs`), defining relationships like `AddressBook` to `Contact`.
    *   Contains dedicated services (`ContactService`, `AddressBookService`) that implement business logic and interact with the data store, abstracting details from the presentation layer.
*   **`MyContactsApp.Core` (Domain Layer):**
    *   The heart of the application, containing the core business logic and domain models.
    *   Defines entities like `Contact.cs` and `AddressBook.cs`, value objects, and custom validations (`IsValidNameAttribute.cs`).
    *   Independent of any specific infrastructure or presentation concerns.

## ⚙️ Getting Started

Follow these instructions to set up and run the project locally.

### Prerequisites

*   [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or higher installed.
*   A code editor (e.g., Visual Studio, Visual Studio Code, Rider).
*   Git for cloning the repository.

### Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/chetanb-11/MyContactsApp.git
    cd MyContactsApp
    ```

2.  **Restore dependencies:**
    Navigate to the root of the solution and run:
    ```bash
    dotnet restore
    ```

### Running the Application

1.  **Build the project:**
    ```bash
    dotnet build
    ```

2.  **Run the web application:**
    Navigate to the `MyContactsApp.Web` directory:
    ```bash
    cd MyContactsApp.Web
    dotnet run
    ```

3.  **Access the application:**
    Once the application starts, it will typically be available at `https://localhost:5001` or `http://localhost:5000` (check the console output for the exact URL). Open this URL in your web browser.
