# # ProjectEvidence — Student Enrollment Management System

A full-featured **ASP.NET Core MVC** web application for managing students, course enrollments, and user roles, built with Entity Framework Core and ASP.NET Core Identity.

---

## Features

- **Student Management** — Create, edit, delete, and list students with profile pictures
- **Course Enrollment** — Enroll students in multiple courses via a many-to-many relationship
- **Course Management** — Full CRUD for courses with title and credit tracking
- **Role-Based Authorization** — Admin and Manager roles with restricted access to sensitive actions
- **User & Role Management** — Admins can create roles and assign them to registered users
- **Partial Views** — Uses partial views (`_addNewCourse`, `_success`, `_error`) for a smoother UX

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 6/7/8) |
| ORM | Entity Framework Core |
| Auth | ASP.NET Core Identity |
| Database | SQL Server |
| Frontend | Razor Views, Bootstrap |
| Image Storage | Server file system (`wwwroot/Images`) |

---

## Getting Started

### Prerequisites

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Setup

1. **Clone the repository**
```bash
   git clone https://github.com/Tajmin228/Student-Enrollment-Management-System
   cd Student-Enrollment-Management-System
```

2. **Configure the database connection** in `appsettings.json`:
```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProjectEvidenceDb;Trusted_Connection=True;"
   }
```

3. **Apply migrations**
```bash
   dotnet ef database update
```

4. **Run the application**
```bash
   dotnet run
```
   Then open `https://localhost:5001` in your browser.

---

## Authorization

| Role | Permissions |
|---|---|
| **Admin** | Full access — create students, manage and assign roles |
| **Manager** | Can edit and delete students |
| **Authenticated User** | Can view students and courses |

To set up the first Admin: register a user, then use the **Role** section to create the `Admin` role and assign it to that user.

---

## Data Model

`Enrollment` is the join table enabling a many-to-many relationship between `Student` and `Cours`.

---

## Image Upload

Student profile pictures are uploaded via `IFormFile`, saved to `wwwroot/Images/` with a random filename, and the relative path is stored in the database. Ensure the `wwwroot/Images` folder exists and has write permissions.

---

## License

This project is for educational purposes. Feel free to use and modify it.

---

## Project Structure
