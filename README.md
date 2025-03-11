﻿#  Grocery Shop Analytics

## **📌 Project Overview**
This project is a **full-stack web application**  It provides **daily income, outcome, and revenue analytics** for a small grocery shop, displayed in an interactive chart. Users can filter data by date range, and the application allows for **real-time filtering** while maintaining browser history.

The solution consists of:
- **Frontend:** Angular SPA (Single Page Application) using Google Charts.
- **Backend:** .NET 8 Web API for fetching shop analytics data.
- **Database:** PostgreSQL (Neon) for storing shop data.

---

## **✅ Features Implemented**
### **1. Display Shop Analytics on a Chart**
- Uses **Google Charts** to visualize:
    - **Daily Income** (Red line)
    - **Daily Outcome** (Blue line)
    - **Revenue (Income - Outcome)** (Green line)
- Chart updates dynamically based on selected filters.

### **2. Date Range Filtering**
- **Default date range:** `June 1, 2021 - December 31, 2021`
- Users can select a different date range and click **Filter** to update the chart.
- The **current date range is displayed in the header**.

### **3. Frontend (Angular 19) Implementation**
- **Angular SPA (Single Page Application)** built using **Angular 19**.
- Uses `angular-google-charts` for data visualization.
- **State management:** Implemented via an Angular service (`ShopAnalyticsService`) to store the selected date range and shop details.
- **Routing & History Management:**
    - Uses `Router` to keep the date range in the URL.
    - Users can **navigate back/forward without losing the selected filter**.
- **Bootstrap & Material UI** for styling.

### **4. Backend (ASP.NET 8) Implementation**
- **.NET 8 Web API** serves shop analytics data.
- Implements a **`Result Pattern`** for consistent API responses.
- Supports **database PostgreSQL (Neon)** for storing shop data.
- **CQRS Pattern:**
    - Uses **Dapper** for querying and **Entity Framework** for commands.
- **OpenAPI Spec for Frontend & Backend Integration:**
    - The frontend and backend were generated using **OpenAPI CLI**, ensuring both are in sync.
- **Database-First Approach:**
    - Tables were created directly in the database, and `scaffold` command was used to generate **entities and DbContext**.
- **Global Exception Handling:**
    - Unhandled exceptions are routed to an `ErrorController` for consistent error handling.
- **Validation Pipeline:**
    - Client requests are validated based on given parameters and logic.


---

## **📂 Project Structure**
```
📦 ShopAnalytics
 ┣ 📂 ShopAnalyticsClient  # Angular Frontend
 ┃ ┣ 📂 src
 ┃ ┣ 📄 package.json        # Frontend dependencies & scripts
 ┃ ┣ 📄 angular.json        # Angular project config
 ┃ ┗ ...
 ┣ 📂 ShopAnalytics        # .NET Backend
 ┃ ┣ 📂 Controllers        # API controllers
 ┃ ┣ 📂 Services           # Business logic
 ┃ ┣ 📂 Models             # Data models
 ┃ ┣ 📄 appsettings.json    # Configurations
 ┃ ┣ 📄 Program.cs         # API startup configurations
 ┃ ┗ ...
 ┣ 📄 README.md             # Project documentation
 ┗ ...
```

---

## **🚀 How to Run the Project**

### Prerequisites
- **Make sure you run in the root directory of the project.**
### Run Locally (Windows) 
Run the following commands to install all dependencies:
```sh
    powershell -ExecutionPolicy Bypass -File run.ps1
```

### Run Locally (Linux/Mac)
Run the following commands to install all dependencies:
```sh
    	bash run.sh
```




✅ This starts:
- **Angular frontend:** `http://localhost:4200`
- **.NET backend:** `http://localhost:5000`

---


## **🛠 Technologies Used**
| Technology                       | Description |
|----------------------------------|-------------|
| **Angular 19**                   | Frontend framework |
| **ASP.NET Core 9**               | Backend framework |
| **PostgreSQL (Neon)**            | Database |
| **Google Charts**                | Chart visualization |
| **Serilog**                      | Structured logging |
| **FluentValidation**             | Request validation |
| **Dapper & EF Core**             | Querying and ORM |
| **OpenAPI Codegen**              | Auto-generates frontend & backend integration |
| **RxJS**                         | Reactive programming in Angular |
| **Bootstrap & Angular Material** | UI components |

---

## **🔗 API Endpoints**
| Method | Endpoint | Description |
|--------|---------|-------------|
| **GET** | `/api/shops` | Get list of available shops |
| **GET** | `/api/analytics/{shopId}?fromDate=YYYY-MM-DD&toDate=YYYY-MM-DD` | Get analytics data for a shop |

Example request:
```
GET /api/analytics/1?fromDate=2021-06-01&toDate=2021-12-31
```

Example response:
```json
{
  "shopId": 1,
  "data": [
    { "date": "2021-06-01", "income": 150, "outcome": 100, "revenue": 50 },
    { "date": "2021-06-02", "income": 200, "outcome": 120, "revenue": 80 }
  ]
}
```




#   G r o c e r y - S h o p - A n a l y t i c s  
 