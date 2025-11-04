# ClinicManagementSystem

ClinicManagementSystem is a web-based application for managing clinic operations, including appointments, visits, diagnoses, prescriptions, and doctor notes. Built with ASP.NET Core Razor Pages and targeting .NET 9, it provides a modern, responsive interface for clinic staff.

## Features
- Appointment scheduling and management
- Patient visit tracking
- Diagnosis and prescription recording
- Doctor notes and visit history
- User-friendly forms with validation
- Responsive design using Bootstrap

## Technologies Used
- ASP.NET Core Razor Pages (.NET 9)
- Entity Framework Core (for data access)
- Bootstrap 5 & Bootstrap Icons
- jQuery
- Notiflix for notifications

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or later (recommended)
- SQL Server (or update connection string for your DB)

### Setup
1. Clone the repository:
   ```sh
   git clone https://github.com/3mosakr/ClinicManagementSystem.git
   ```
2. Open the solution in Visual Studio.
3. Update the database connection string in `appsettings.json` if needed.
4. Run database migrations (if using EF Core):
   ```sh
   dotnet ef database update
   ```
5. Build and run the project:
   ```sh
   dotnet run --project ClinicManagementSystem/ClinicManagementSystem.csproj
   ```
6. Open your browser and navigate to `https://localhost:5001` (or the port shown in the console).

## Project Structure
- `ClinicManagementSystem/` - Main project directory
  - `Pages/` - Razor Pages for UI
  - `ViewModel/` - View models for data transfer
  - `wwwroot/` - Static files (CSS, JS, images)
  - `appsettings.json` - Configuration

## Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

