# 🛒 Grocery Shop Analytics

![GitHub repo size](https://img.shields.io/github/repo-size/your-username/grocery-shop-analytics?style=flat-square)
![GitHub stars](https://img.shields.io/github/stars/your-username/grocery-shop-analytics?style=flat-square)
![GitHub issues](https://img.shields.io/github/issues/your-username/grocery-shop-analytics?style=flat-square)

## 📌 Project Overview
**Grocery Shop Analytics** is a **full-stack web application** that provides **daily income, outcome, and revenue analytics** for a small grocery shop. It features an interactive chart with real-time filtering and maintains browser history for seamless navigation.

### 🏗 Tech Stack
- **Frontend:** Angular 19, Google Charts, Bootstrap & Angular Material
- **Backend:** ASP.NET Core 8 Web API
- **Database:** PostgreSQL (Neon)
- **Other:** OpenAPI, Serilog, FluentValidation, Dapper & EF Core

---

## ✅ Features
### 📊 Interactive Shop Analytics Chart
- Uses **Google Charts** to visualize:
  - **Daily Income** 📈 (Red Line)
  - **Daily Outcome** 📉 (Blue Line)
  - **Revenue (Income - Outcome)** 💰 (Green Line)
- Chart updates dynamically based on filters.

### 📅 Date Range Filtering
- **Default date range:** `June 1, 2021 - December 31, 2021`
- Users can select a different date range and click **Filter** to update the chart.
- The **current date range is displayed in the header**.

### 🖥️ Frontend (Angular 19)
- **SPA (Single Page Application)** using `angular-google-charts`
- **State Management:** Implemented via `ShopAnalyticsService`
- **Routing & History Management:**
  - Uses `Router` to maintain the date range in the URL.
  - Enables users to **navigate back/forward without losing filters**.
- **UI Styling:** Bootstrap & Angular Material.

### 🚀 Backend (ASP.NET Core 8)
- **.NET 8 Web API** serving shop analytics data.
- **CQRS Pattern:** Uses **Dapper** for queries & **EF Core** for commands.
- **OpenAPI Spec:** Auto-generated API contracts.
- **Database-First Approach:**
  - Tables created in PostgreSQL, scaffolded into **Entities & DbContext**.
- **Global Exception Handling:** Errors are routed to `ErrorController`.
- **Validation Pipeline:** Uses FluentValidation to validate client requests.

---

## 📂 Project Structure
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

## 🚀 How to Run the Project
### Prerequisites
Ensure you have:
- **Node.js & npm** (for frontend)
- **.NET 8 SDK** (for backend)
- **PostgreSQL (Neon)** (for database)

### 🏃 Run Locally (Windows)
```sh
powershell -ExecutionPolicy Bypass -File run.ps1
```

### 🏃 Run Locally (Linux/Mac)
```sh
bash run.sh
```

### ✅ This starts:
- **Frontend:** `http://localhost:4200`
- **Backend:** `http://localhost:5000`

---

## 🔗 API Endpoints
| Method | Endpoint | Description |
|--------|---------|-------------|
| **GET** | `/api/shops` | Get list of available shops |
| **GET** | `/api/analytics/{shopId}?fromDate=YYYY-MM-DD&toDate=YYYY-MM-DD` | Get analytics data for a shop |

📌 **Example Request:**
```http
GET /api/analytics/1?fromDate=2021-06-01&toDate=2021-12-31
```

📌 **Example Response:**
```json
{
  "shopId": 1,
  "data": [
    { "date": "2021-06-01", "income": 150, "outcome": 100, "revenue": 50 },
    { "date": "2021-06-02", "income": 200, "outcome": 120, "revenue": 80 }
  ]
}
```

---

## 🛠 Technologies Used
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

