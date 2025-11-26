# Inventory Ledger

A lightweight inventory ledger system for managing items, tracking stock transactions (IN/OUT), and monitoring low-stock levels.  
Built with ASP.NET Core Web API, Entity Framework Core, SQLite, and Blazor WebAssembly.

## Features
- Create, edit, and delete items
- Record stock increases and decreases
- View transaction history per item
- Calculate current on-hand quantity
- Inventory summary with low-stock alerts
- Blazor WebAssembly user interface
- REST API for integration with other tools

## Technologies Used
- ASP.NET Core Web API
- Entity Framework Core (SQLite)
- Blazor WebAssembly
- C#
- xUnit (Unit Testing)

## Project Structure
- `Inventory.Api` – Web API
- `Inventory.Client` – Blazor WebAssembly frontend
- `Inventory.Domain` – Core entities
- `Inventory.Application` – Business logic and interfaces
- `Inventory.Infrastructure` – EF Core and persistence
- `Inventory.Tests` – Automated tests

## Getting Started

### 1. Run the APP
```bash
cd Inventory.Api
dotnet run

cd Inventory.Client
dotnet watch 


