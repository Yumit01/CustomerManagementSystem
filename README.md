# Customer Management System

## Overview
A .NET 6.0 application for managing customer data with a multi-layered architecture.

## Project Structure
- **CustomerManagement.Entities**: Data models
- **CustomerManagement.DataAccess**: Database interactions
- **CustomerManagement.Business**: Business logic
- **CustomerManagement.UI**: User interface

## Prerequisites
- .NET 6.0 SDK
- SQL Server
- Visual Studio Code

## Database Setup
```sql
CREATE DATABASE CustomerManagement;
USE CustomerManagement;

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerCode VARCHAR(50) UNIQUE,
    CustomerName NVARCHAR(50),
    CustomerCategory INT,
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdateDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(50),
    CategoryDescription NVARCHAR(255)
);

INSERT INTO Categories(CategoryName, CategoryDescription) VALUES 
    ('Customer', 'Standard Customers'),
    ('Supplier', 'Product Suppliers'),
    ('Distributor', 'Distribution Partners'),
    ('VIP', 'High-Value Customers');
```

## Installation
1. Clone the repository
2. Open solution in Visual Studio Code
3. Restore NuGet packages
4. Update connection string
5. Run database migration

## Running the Application
```bash
# Navigate to UI project
dotnet run
```