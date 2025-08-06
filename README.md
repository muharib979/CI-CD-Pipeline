# Caching in .NET Core

This project demonstrates how to implement **caching** in a .NET Core application using **in-memory cache** and **distributed cache**.

## Why Caching?
Caching improves performance by storing frequently accessed data in memory or a distributed store, reducing expensive operations like database calls.

---

## Types of Caching in .NET Core

### 1. In-Memory Caching
Stores cache data in the memory of the web server.

**Setup in `Program.cs`**
```csharp
builder.Services.AddMemoryCache();
