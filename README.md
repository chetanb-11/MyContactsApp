# MyContactsApp

[![.NET Build](https://github.com/your-username/MyContactsApp/actions/workflows/update-readme.yml/badge.svg)](https://github.com/your-username/MyContactsApp/actions/workflows/update-readme.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Open Issues](https://img.shields.io/github/issues/your-username/MyContactsApp.svg)](https://github.com/your-username/MyContactsApp/issues)

## 🌟 About the Project

`MyContactsApp` is a modern ASP.NET Core web application designed to help users efficiently manage their personal and professional contacts. It provides a clean, intuitive interface for adding new contacts, viewing existing ones, and searching through them. Built with a clear separation of concerns using a layered architecture, this application demonstrates best practices in .NET development, including domain modeling, data persistence with Entity Framework Core, and an ASP.NET Core MVC web presentation layer.

## ✨ Features

*   **Add New Contacts:** Easily create and save new contact entries with essential details.
*   **View All Contacts:** Browse through a comprehensive list of all stored contacts.
*   **Search Contacts:** Quickly find specific contacts using robust search functionalities.
*   **Robust Validation:** Ensures data integrity with custom validation rules for contact information.

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
├── MyContactsApp.Infrastructure (Persistence Layer - Entity Framework Core, Data Context)
└── MyContactsApp.Core          (Domain Layer - Models, Validations, Business Logic)
```

*   **`MyContactsApp.Web` (Presentation Layer):**
    *   This is the entry point of the application, an ASP.NET Core MVC project.
    *   Handles HTTP requests, renders views, and orchestrates user interactions.
    *   Contains Controllers, Views (`.cshtml`), and startup configuration (`Program.cs`).
*   **`MyContactsApp.Infrastructure` (Persistence Layer):**
    *   Responsible for data access and external integrations.
    *   Utilizes Entity Framework Core to interact with the database (`AppDbContext.cs`).
    *   Manages data storage and retrieval, abstracting the details from the business logic.
*   **`MyContactsApp.Core` (Domain Layer):**
    *   The heart of the application, containing the core business logic and domain models.
    *   Defines entities (`Contact.cs`), value objects, and custom validations (`IsValidNameAttribute.cs`).
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
    git clone https://github.com/your-username/MyContactsApp.git
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

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.

## 📧 Contact

Your Name/Organization - [your.email@example.com](mailto:your.email@example.com)
Project Link: [https://github.com/your-username/MyContactsApp](https://github.com/your-username/MyContactsApp)
