# Solution Overview

## Architecture
I built this project using a Clean Architecture approach because it keeps everything organised and easy to maintain.  
The Domain layer contains my core entities (Item and StockTransaction) with no dependencies.  
My Application layer holds the business rules, especially the logic for calculating on-hand stock and the interfaces for repositories.  
Infrastructure uses Entity Framework Core with SQLite to persist data, giving me a lightweight and cross-platform database.  
The API layer exposes endpoints for items, transactions, and inventory summaries.  
The Client layer is a Blazor WebAssembly frontend that calls the API and provides a smooth user experience.

## Key Behaviours
On-hand quantity is calculated through the InventoryService by summing all IN and OUT transactions for each item.  
I use DTOs to keep the API responses clean and separate from domain models.  
Repositories help me abstract EF Core so the Application layer remains testable and not tied to the database.

## Technology Choices
I chose SQLite for simplicity, zero-configuration, and good performance for a small inventory system.  
Blazor WebAssembly lets me build a modern UI entirely in C#, which fits the full-stack .NET approach I wanted.  
The trade-offs include a slightly slower initial load for Blazor WASM and SQLite’s limited concurrency, but both are acceptable for the scope of this project.

## Summary
Overall, this solution is lightweight, cleanly structured, and practical.  
It demonstrates full-stack .NET development using good architectural principles, making it a strong fit for both real use and portfolio presentation.
