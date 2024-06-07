# Handlebars.Net.Helpers DynamicLinq Issue

## Description

This repository contains a console application and a WebAPI project to demonstrate an issue with Handlebars.Net.Helpers where the DynamicLinq helpers are not registered correctly in a .NET 8.0 WebAPI project.

## Projects

- `ConsoleApp`: Demonstrates correct registration of helpers.
- `WebAPI`: Demonstrates the issue with helper registration.

## Steps to Reproduce

1. Clone the repository.
2. Open the solution in Visual Studio or your preferred IDE.
3. Build and run the console application project.
4. Observe the console output showing the number of registered helpers.
5. Build and run the WebAPI project.
6. Observe the API response showing a different number of registered helpers.

### Expected Behavior
Both projects should register the same number of helpers:
- 134 Block Helpers and 265 Helpers

### Actual Behavior
- Console Application: 134 Block Helpers and 265 Helpers
- WebAPI Project: 97 Block Helpers and 193 Helpers

## Environment

- .NET Version: 8.0
- Handlebars.Net Version: 2.1.6
- Handlebars.Net.Helpers Version: 2.4.3
- Handlebars.Net.Helpers.Core Version: 2.4.3
- Handlebars.Net.Helpers.DynamicLinq Version: 2.4.3
- Handlebars.Net.Helpers.Json Version: 2.4.3
- Operating System: Windows 11
