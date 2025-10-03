# dotnet_sample_web_app

A small sample .NET web application that demonstrates how to implement a widget using C#.

## Prerequisites

- .NET SDK (recommended: .NET Core 3.1 / .NET 5+ — use the version targeted by the project)  
- Visual Studio 2019 (or later) **with** the ASP.NET and web development workload (optional if you prefer the CLI)  
- NuGet package restore enabled (Visual Studio handles this automatically)

## Building

### Using Visual Studio
1. Open the solution (`*.sln`) in Visual Studio.  
2. Ensure NuGet package restore is enabled (`Tools → Options → NuGet Package Manager → General → Automatically check for missing packages during build`).  
3. Build the solution (`Build → Build Solution`).  

### Using the command line (dotnet CLI)
From the repository root:  
```bash
# Restore dependencies
dotnet restore

# Build the solution or a specific project
dotnet build
