# Product Order API — Clean Architecture (ASP.NET Core 8.0)

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/) 
[![C#](https://img.shields.io/badge/C%23-8.0-green)](https://learn.microsoft.com/en-us/dotnet/csharp/) 
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-blue)](https://www.postgresql.org/)
[![License](https://img.shields.io/badge/License-MIT-yellow)](LICENSE)



---

## Table of Contents

- [Overview](#overview)  
- [Features Implemented](#features-implemented)  
- [Architecture](#architecture)  
- [Folder Structure](#folder-structure)  
- [Tech Stack](#tech-stack)
- [Live Link](#live-link) 
- [Database Schema](#database-schema) 
- [API Endpoints](#api-endpoints)
- [Setup Instructions](#setup-instructions)  
- [Author](#author)

---

## Overview

A production-grade C# Web API built with Clean Architecture, Entity Framework Core, and PostgreSQL, supporting.

```
• Product Catalog (CRUD)
• Order Placement with multiple products
• Stock validation (prevents overselling)
• Transaction-safe order processing
• Repository pattern + Service layer
• Clean Architecture

```

---

## Features Implemented

###  Product Management (CRUD)
- Create product  
- Get product  
- Get all products  
- Update product  
- Delete product  

###  Place Order
- Users can order multiple products at once
- Validates stock before processing
- Uses **database transactions** to avoid overselling
- Deducts stock only after order success

###  Concurrency Handling
- Transaction-safe operations
- Row-level locks with EF Core
- Prevents race conditions in high concurrency scenarios

---

## Folder Structure
```


src/
 ├── Domain
 │   ├── Entities
 │   ├── Interfaces
 │   └── Shared
 ├── Application
 │   ├── DTOs
 │   ├── Services
 │   └── Exceptions
 ├── Infrastructure
 │   ├── Persistence
 │   ├── Repositories
 │   └── Configurations
 └── API
     ├── Controllers
     └── Program.cs

```
---

## Tech Stack
```
• .NET Core (C#)
• Entity Framework Core
• PostgreSQL
• Clean Architecture
• Repository Pattern & Dependency Injection
• Unit of Work & Database Transactions
• Docker
• Swagger UI
```
---
## Live Link
• Live Link (Swagger Docs): https://core-banking-solution.onrender.com/swagger/index.html <br>

##  Database Schema

### Products Table
| Column          | Type        | Description |
|----------------|-------------|-------------|
| Id             | int         | Primary Key |
| Name           | string      | Product title |
| Description    | string      | Product description |
| Price          | decimal     | Cost of one unit |
| StockQuantity  | int         | Current stock |

---

###  Orders Table
| Column          | Type        | Description |
|----------------|-------------|-------------|
| Id             | int         | Primary Key |
| Reference      | string      | Unique order reference (e.g. ORD-20251211-XYZ) |
| OrderDate      | DateTime    | Timestamp when order was created |

---

### OrderItems Table
| Column          | Type        | Description |
|----------------|-------------|-------------|
| OrderId        | int         | FK → Orders.Id |
| ProductId      | int         | FK → Products.Id |
| Quantity       | int         | Quantity ordered |
| UnitPrice      | decimal     | Price at time of order |

**Composite Key:**  
`OrderId + ProductId` is the primary key.  

---

##  API Endpoints

### **Products**
- **POST** `/api/Products/add-product` → Create a product   
- **GET** `/api/Products/get-all-products` → Get all products  
- **GET** `/api/Products/get-a-product-by-id` → Get product by ID  
- **PUT** `/api/Products/update-a-product-by-id` → Update product  
- **DELETE** `/api/Products/delete-a-product-by-id` → Delete product  

### **Orders**
- **POST** `/api/Order/place-order` → Place an order with multiple items  

#### Request Example:
```json
{
  "items": [
    { "productId": 1, "quantity": 2 },
    { "productId": 3, "quantity": 1 }
  ]
}
```

## Setup Instructions

### Prerequisites
Before you begin, make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [PostgreSQL](https://www.postgresql.org/download/)  
- [Git](https://git-scm.com/downloads)  

---

### Steps

1. **Clone the repository**

```bash
git clone https://github.com/Yamuhammad01/Product-Order-Solution.git
```
2. **Restore dependencies**
```bash
dotnet restore

```
3. **Update appsettings.json**
```bash
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=ProductOrder_Db;Username=postgres;Password=yourpassword"
}

```
4. **Create database migration**
```bash
Add-Migration "InitialCreate"
```
5. **Update Database**
```bash
Update-Database
```
6. **Run the API**
```bash
dotnet run
```
7. **Access API at https://localhost:yourport**

---
## Author
Muhammad Idris

• GitHub: https://github.com/Yamuhammad01 <br>
• LinkedIn: https://www.linkedin.com/in/muhammad-idrisb2/ <br>
• Email: idrismuhd814@gmail.com <br>

---
