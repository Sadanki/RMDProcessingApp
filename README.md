# 💼 RMD Processing Application

> A comprehensive web application for managing Required Minimum Distribution (RMD) processing for retirement plan participants with time-based workflows and role-based access control.

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![Status](https://img.shields.io/badge/status-in%20development-yellow.svg)]()

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Key Features](#-key-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Data Model](#-data-model)
- [User Roles & Permissions](#-user-roles--permissions)
- [RMD Lifecycle](#-rmd-lifecycle)
- [Time-Based Processing Rules](#-time-based-processing-rules)
- [Getting Started](#-getting-started)
- [Project Structure](#-project-structure)
- [MVP Scope](#-mvp-scope)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)
- [License](#-license)

---

## 🎯 Overview

The **RMD Processing Application** is designed to streamline the calculation, approval, and processing of Required Minimum Distributions for retirement plan participants. Built with ASP.NET Core MVC, it provides a robust, time-sensitive workflow system with role-based access control and comprehensive audit capabilities.

### Why This Application?

- **Automated Processing**: Time-based workflow automation with EST timezone compliance
- **Compliance Ready**: Built-in audit trails and approval workflows
- **Role-Based Security**: Granular permission system for Admin, Processor, and Viewer roles
- **Cut-Off Management**: Automatic locking mechanism to prevent after-hours modifications

---

## ✨ Key Features

### Core Capabilities

🔐 **Authentication & Authorization**
- Role-based access control (Admin, Processor, Viewer)
- Secure login with password reset functionality
- Session management and user activity tracking

👥 **Participant Management**
- Complete participant lifecycle management
- Account tracking and valuation history
- Support for multiple account types per participant

💰 **RMD Processing Workflow**
- Automated eligibility checking
- Multi-stage approval process
- Dual-turn processing system (Turn 1 & Turn 2)
- Payment tracking and reconciliation

⏰ **Time-Based Controls**
- EST timezone-based processing windows
- Automatic cut-off enforcement at 5:00 PM EST
- Pre-processing window (before 2:00 PM EST)
- Processing window (2:00 PM - 5:00 PM EST)

📊 **Reporting & Audit**
- Comprehensive audit logs for all actions
- RMD status reports and history
- Participant activity reports
- System configuration tracking

---

## 🛠 Tech Stack

### Backend
- **Framework**: ASP.NET Core 8.0 MVC
- **Language**: C# 12
- **ORM**: Entity Framework Core
- **Database**: SQL Server / PostgreSQL

### Frontend
- **Framework**: Razor Views
- **CSS**: Bootstrap 5
- **JavaScript**: jQuery / Vanilla JS
- **Icons**: Font Awesome / Bootstrap Icons

### Security & Authentication
- ASP.NET Core Identity
- JWT Authentication (API endpoints)
- Role-based Authorization

### Additional Tools
- **Logging**: Serilog
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **Testing**: xUnit, Moq

---

## 🏗 Architecture

This application follows the **MVC (Model-View-Controller)** architectural pattern with clear separation of concerns.

```
┌─────────────────────────────────────────────────────┐
│                   PRESENTATION LAYER                 │
│  (Razor Views, ViewModels, Client-side Validation)  │
└───────────────────┬─────────────────────────────────┘
                    │
┌───────────────────▼─────────────────────────────────┐
│                  CONTROLLER LAYER                    │
│   (Request Routing, Authorization, Flow Control)    │
└───────────────────┬─────────────────────────────────┘
                    │
┌───────────────────▼─────────────────────────────────┐
│                   SERVICE LAYER                      │
│     (Business Logic, RMD Calculation, Rules)        │
└───────────────────┬─────────────────────────────────┘
                    │
┌───────────────────▼─────────────────────────────────┐
│                   DATA ACCESS LAYER                  │
│        (Entity Framework, Repositories)              │
└───────────────────┬─────────────────────────────────┘
                    │
┌───────────────────▼─────────────────────────────────┐
│                      DATABASE                        │
│              (SQL Server / PostgreSQL)               │
└─────────────────────────────────────────────────────┘
```

### MVC Components

#### **Models** (Data Layer)
- `User`, `Participant`, `Account`, `RMD`
- `RMDProcessing`, `Payment`, `AuditLog`
- `SystemConfiguration`

#### **Controllers** (Flow Control)
- `AuthController` - Authentication & authorization
- `AdminController` - System administration
- `ParticipantController` - Participant management
- `AccountController` - Account operations
- `RMDController` - RMD lifecycle management
- `ReportController` - Reporting & analytics

#### **Views** (UI Layer)
- Login & authentication pages
- Dashboard views
- Participant & account views
- RMD processing views
- Reports & audit log views

---

## 📊 Data Model

### Core Entities

#### **USER**
```
├── UserId (PK)
├── Name
├── Email
├── Role (Admin / Processor / Viewer)
├── Status (Active / Locked)
├── CreatedDate
└── LastLoginDate
```

#### **PARTICIPANT**
```
├── ParticipantId (PK)
├── FullName
├── DateOfBirth
├── NationalId (PAN / SSN)
├── Email
├── Phone
├── Address
├── PlanType
├── EmploymentStatus
└── ParticipantStatus (Active / Retired / Deceased)
```

#### **ACCOUNT**
```
├── AccountId (PK)
├── ParticipantId (FK)
├── AccountNumber
├── AccountType
├── OpeningBalance
├── CurrentBalance
└── LastValuationDate
```

#### **RMD**
```
├── RMDId (PK)
├── ParticipantId (FK)
├── FinancialYear
├── OpeningBalance
├── CalculatedAmount
├── Status
├── CreatedDate
├── ApprovedDate
└── LockedDate
```

#### **RMDPROCESSING**
```
├── ProcessingId (PK)
├── RMDId (FK)
├── TurnNumber (1 or 2)
├── ProcessingStartTime
├── ProcessingEndTime
└── ProcessingStatus
```

#### **PAYMENT**
```
├── PaymentId (PK)
├── RMDId (FK)
├── PaymentDate
├── PaymentAmount
├── PaymentMethod
├── PaymentStatus
└── ReferenceNumber
```

#### **AUDITLOG**
```
├── AuditId (PK)
├── EntityName
├── EntityId
├── ActionPerformed
├── OldValue
├── NewValue
├── PerformedBy
└── PerformedAt
```

### Entity Relationships

```
USER ──────────────┐
                   │ performs
                   ▼
PARTICIPANT ────> ACCOUNT
    │                │
    │ has            │ tracks
    ▼                ▼
   RMD ──────────> PAYMENT
    │
    │ processes
    ▼
RMDPROCESSING

All actions ────> AUDITLOG
```

---

## 👥 User Roles & Permissions

### Role Hierarchy

```
┌─────────────────────────────────────────────────┐
│                     ADMIN                        │
│  Full system access + User management          │
└────────────────────┬────────────────────────────┘
                     │
        ┌────────────┴────────────┐
        ▼                         ▼
┌──────────────┐          ┌──────────────┐
│  PROCESSOR   │          │    VIEWER    │
│ Create/Edit  │          │  Read-only   │
└──────────────┘          └──────────────┘
```

### Detailed Permissions

| Feature | Admin | Processor | Viewer |
|---------|-------|-----------|--------|
| **User Management** |
| Create/Edit Users | ✅ | ❌ | ❌ |
| View Users | ✅ | ❌ | ❌ |
| **Participant Management** |
| Add Participant | ✅ | ✅ | ❌ |
| Edit Participant | ✅ | ✅ | ❌ |
| View Participant | ✅ | ✅ | ✅ |
| **RMD Operations** |
| Create RMD | ✅ | ✅ | ❌ |
| Approve RMD | ✅ | ❌ | ❌ |
| Stop RMD (before 2 PM) | ✅ | ✅ | ❌ |
| Stop RMD (2-5 PM) | ✅ | ❌ | ❌ |
| Cancel RMD | ✅ | ❌ | ❌ |
| View RMD Status | ✅ | ✅ | ✅ |
| **Reports & Audit** |
| View Reports | ✅ | ❌ | ✅ |
| View Audit Logs | ✅ | ❌ | ❌ |
| **System Configuration** |
| Modify Settings | ✅ | ❌ | ❌ |

---

## 🔄 RMD Lifecycle

### Status Flow Diagram

```
┌─────────┐
│  DRAFT  │ ◄── RMD Created
└────┬────┘
     │ Admin Approves
     ▼
┌─────────┐
│APPROVED │
└────┬────┘
     │ Processing Starts
     ▼
┌─────────────────┐
│ PROCESSING      │
│    TURN 1       │ ◄── Before 2:00 PM EST
│ (Validation)    │
└────┬───────┬────┘
     │       │ Can Stop (Admin/Processor)
     │       ▼
     │   ┌─────────┐
     │   │ STOPPED │
     │   └─────────┘
     │
     │ After 2:00 PM EST
     ▼
┌─────────────────┐
│ PROCESSING      │
│    TURN 2       │ ◄── 2:00 PM - 5:00 PM EST
│ (Payment Prep)  │
└────┬───────┬────┘
     │       │ Can Stop (Admin Only)
     │       ▼
     │   ┌─────────┐
     │   │ STOPPED │
     │   └─────────┘
     │
     ▼
┌───────────┐
│ COMPLETED │
└─────┬─────┘
      │ After 5:00 PM EST
      ▼
┌──────────┐
│  LOCKED  │ ◄── Final State (Read-Only)
└──────────┘

     ┌──────────┐
     │CANCELLED │ ◄── Admin Only (Before 2 PM)
     └──────────┘
```

### Status Definitions

| Status | Description | Actions Allowed | Who Can Access |
|--------|-------------|-----------------|----------------|
| **Draft** | Initial creation | Edit, Delete, Submit | Admin, Processor |
| **Approved** | Ready for processing | Start Processing | Admin |
| **Processing - Turn 1** | Pre-processing validation | Stop, View | Admin, Processor (stop) |
| **Processing - Turn 2** | Payment preparation | Stop (Admin only), View | Admin, Processor |
| **Completed** | Successfully processed | View | All |
| **Stopped** | Processing halted | View, Restart (Admin) | Admin, Processor |
| **Cancelled** | Logically cancelled | View (Audit) | Admin |
| **Locked** | System-locked after cut-off | View only | All |

### State Transition Rules

```
Draft ──────────────────> Approved
                            │
Approved ────────────────> Processing - Turn 1
                            │
Processing - Turn 1 ────> Processing - Turn 2
         │                  │
         ├────────> Stopped │
         │                  │
Processing - Turn 2 ────────┴───────> Completed
         │                              │
         └────────> Stopped            │
                                       │
Any State ──────────> Cancelled       │
                                       │
Completed ─────────────────────────> Locked
```

---

## ⏰ Time-Based Processing Rules

All processing operates on **Eastern Standard Time (EST)**.

### Daily Processing Windows

#### 🟢 **Pre-Processing Window** (Before 2:00 PM EST)

**Allowed Actions:**
- ✅ Create new RMD
- ✅ Edit draft RMD
- ✅ Approve RMD
- ✅ Cancel RMD (Admin only)
- ✅ Stop RMD (Admin & Processor)
- ✅ Full edit access

**Status:** `Draft`, `Approved`

---

#### 🟡 **Processing Window** (2:00 PM - 5:00 PM EST)

**Allowed Actions:**
- ✅ View RMD status
- ✅ Stop RMD (Admin only)
- ❌ Edit RMD
- ❌ Cancel RMD
- ⚠️ Limited modifications

**Status:** `Processing - Turn 1`, `Processing - Turn 2`

**Processing Turns:**
- **Turn 1** (2:00 PM - 3:30 PM): Validation and verification
- **Turn 2** (3:30 PM - 5:00 PM): Payment preparation

---

#### 🔴 **Cut-Off / Lock Window** (After 5:00 PM EST)

**Allowed Actions:**
- ✅ View only (Read-only mode)
- ❌ No edits
- ❌ No stops
- ❌ No cancellations
- 🔒 Automatic system lock

**Status:** `Locked`, `Completed`

---

### Role-Based Time Rules

| Time Window | Admin | Processor | Viewer |
|-------------|-------|-----------|--------|
| Before 2:00 PM | Create, Edit, Approve, Stop, Cancel | Create, Edit, Stop | View |
| 2:00 PM - 5:00 PM | View, Stop | View | View |
| After 5:00 PM | View Only | View Only | View Only |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server 2019+](https://www.microsoft.com/sql-server) or [PostgreSQL 14+](https://www.postgresql.org/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Sadanki/RMDProcessingApp.git
   cd RMDProcessingApp
   ```

2. **Configure database connection**
   
   Update `appsettings.json` with your database connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=RMDProcessing;Trusted_Connection=True;"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Seed initial data** (Optional)
   ```bash
   dotnet run --seed-data
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   
   Open your browser and navigate to: `https://localhost:5001`

### Default Credentials

```
Admin User:
  Email: admin@rmdapp.com
  Password: Admin@123

Processor User:
  Email: processor@rmdapp.com
  Password: Processor@123

Viewer User:
  Email: viewer@rmdapp.com
  Password: Viewer@123
```

⚠️ **Important:** Change these credentials after first login!

---

## 📁 Project Structure

```
RMDProcessingApp/
│
├── Controllers/              # MVC Controllers
│   ├── AuthController.cs
│   ├── AdminController.cs
│   ├── ParticipantController.cs
│   ├── AccountController.cs
│   ├── RMDController.cs
│   └── ReportController.cs
│
├── Models/                   # Data Models
│   ├── Domain/              # Entity models
│   │   ├── User.cs
│   │   ├── Participant.cs
│   │   ├── Account.cs
│   │   ├── RMD.cs
│   │   ├── RMDProcessing.cs
│   │   ├── Payment.cs
│   │   └── AuditLog.cs
│   └── ViewModels/          # View models for UI
│
├── Views/                    # Razor Views
│   ├── Auth/
│   ├── Admin/
│   ├── Participant/
│   ├── Account/
│   ├── RMD/
│   ├── Report/
│   └── Shared/
│
├── Services/                 # Business Logic
│   ├── Interfaces/
│   ├── RMDCalculationService.cs
│   ├── EligibilityService.cs
│   ├── ProcessingService.cs
│   └── AuditService.cs
│
├── Data/                     # Database Context
│   ├── ApplicationDbContext.cs
│   ├── Configurations/      # Entity configurations
│   └── Migrations/          # EF Core migrations
│
├── Repositories/             # Data Access Layer
│   ├── Interfaces/
│   └── Implementations/
│
├── Middleware/               # Custom middleware
│   ├── TimezoneMiddleware.cs
│   └── AuditMiddleware.cs
│
├── Utilities/                # Helper classes
│   ├── TimeZoneHelper.cs
│   ├── RMDCalculator.cs
│   └── Constants.cs
│
├── wwwroot/                  # Static files
│   ├── css/
│   ├── js/
│   └── images/
│
├── appsettings.json          # Configuration
├── Program.cs                # Application entry point
└── Startup.cs                # Service configuration
```

---

## 🎯 MVP Scope

### Phase 1: Core Features (Current MVP)

**✅ Included in MVP:**

- ✅ User authentication (Login/Logout)
- ✅ Role-based access control
- ✅ Participant management
  - List participants
  - Add new participant
  - View participant details
- ✅ RMD management
  - Create RMD (Draft status)
  - Approve RMD (Admin only)
  - View RMD status
- ✅ Basic dashboard

**❌ Excluded from MVP:**

- ❌ Payment processing
- ❌ Automated cut-off time enforcement
- ❌ Multi-turn processing automation
- ❌ Advanced reporting
- ❌ Comprehensive audit logs
- ❌ Email notifications

---

## 🗺 Roadmap

### Phase 2: Enhanced Processing (Q2 2025)
- [ ] Implement Turn 1 & Turn 2 processing
- [ ] Add time-based workflow automation
- [ ] Automatic cut-off time enforcement (5:00 PM EST)
- [ ] Stop/Cancel functionality with time rules

### Phase 3: Payment Integration (Q3 2025)
- [ ] Payment processing module
- [ ] Payment method configuration
- [ ] Payment status tracking
- [ ] Reconciliation reports

### Phase 4: Reporting & Analytics (Q4 2025)
- [ ] Comprehensive RMD reports
- [ ] Participant analytics dashboard
- [ ] Complete audit log viewer
- [ ] Export functionality (PDF, Excel, CSV)

### Phase 5: Advanced Features (2026)
- [ ] Email notifications and alerts
- [ ] Batch processing capabilities
- [ ] Document management system
- [ ] API for external integrations
- [ ] Mobile-responsive enhancements

---

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/AmazingFeature
   ```
3. **Commit your changes**
   ```bash
   git commit -m 'Add some AmazingFeature'
   ```
4. **Push to the branch**
   ```bash
   git push origin feature/AmazingFeature
   ```
5. **Open a Pull Request**

### Coding Standards

- Follow [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Write unit tests for new features
- Update documentation for API changes
- Ensure all tests pass before submitting PR

---

## 📞 Support

If you encounter any issues or have questions:

- **Create an Issue**: [GitHub Issues](https://github.com/Sadanki/RMDProcessingApp/issues)
- **Email**: support@rmdapp.com
- **Documentation**: [Wiki](https://github.com/Sadanki/RMDProcessingApp/wiki)

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- Built with [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- UI powered by [Bootstrap](https://getbootstrap.com/)
- Icons from [Font Awesome](https://fontawesome.com/)

---

## 📊 Project Status

![GitHub last commit](https://img.shields.io/github/last-commit/Sadanki/RMDProcessingApp)
![GitHub issues](https://img.shields.io/github/issues/Sadanki/RMDProcessingApp)
![GitHub pull requests](https://img.shields.io/github/issues-pr/Sadanki/RMDProcessingApp)

**Current Version:** MVP v1.0.0 (In Development)

**Last Updated:** February 2026

---

<div align="center">

**Made with ❤️ for Retirement Plan Management**

[⬆ back to top](#-rmd-processing-application)

</div>