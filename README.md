# DirectID .NET Sample Web App

This repository contains a sample .NET web application that demonstrates how to integrate and use the **DirectID widget** in C#.

## Prerequisites

- .NET SDK (recommended: .NET Core 3.1 / .NET 5+ — use the version targeted by the project)  
- Visual Studio 2019 (or later) **with** the ASP.NET and web development workload (optional if you prefer the CLI)  
- NuGet package restore enabled (Visual Studio handles this automatically)  
- A **DirectID account** and access credentials (API keys / secrets)  

## Building

### Using Visual Studio
1. Clone this repository.  
2. Open the solution (`*.sln`) in Visual Studio.  
3. Ensure NuGet package restore is enabled (`Tools → Options → NuGet Package Manager → General → Automatically check for missing packages during build`).  
4. Build the solution (`Build → Build Solution`).  

### Using the command line (dotnet CLI)
From the repository root:  
```bash
# Restore dependencies
dotnet restore

# Build the solution or a specific project
dotnet build
