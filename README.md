# .NET 7 Custom CQRS and Mediator Web API

## Overview

This repository hosts a .NET 7 Web API project that demonstrates an advanced implementation of custom Command Query Responsibility Segregation (CQRS) and Mediator patterns. Tailored for minimal APIs in .NET 7, this project showcases a clean and efficient way to building Web APIs using modern approach.

By leveraging the CQRS and Mediator patterns, the project provides a clear separation of concerns, enhancing maintainability and allowing for more flexible and optimized interactions.

## Features

- **Custom CQRS Implementation**: Incorporates a custom CQRS setup, enabling distinct handling of command and query responsibilities.
- **Mediator Pattern Integration**: Utilizes the Mediator pattern for decoupling the in-process sending of messages, improving code modularity and testability.
- **Minimal API Design**: Leverages .NET 7's minimal API capabilities for a more streamlined and concise API development experience.

## Command Query Responsibility Segregation (CQRS)

**CQRS** stands for **Command Query Responsibility Segregation**, a design pattern in software architecture. This pattern advocates for the separation of read operations from write or update operations on data.

<img src="https://github.com/ivanvyd/cqrs-mediator/assets/87537042/6ea82670-9be3-43e6-8cb7-dea7629ffab4" width=35% height=35%>

### Pros

- It enables handling write requests (commands) and read requests (queries) separately.
- By improving read data sources with caching, it can reduce response times and enhance performance as well as save storage costs.
- It allows for more tailored scaling of storage resources based on actual usage patterns.

### Cons

- The architecture introduces more potential failure points, necessitating robust monitoring and fail-safe procedures.
- Suitable for experienced developers who want more flexibility than with built-in middlewares and understand the benefits of the pattern (including storage partitioning at the endpoint), while CQRS might be not so straightforward for beginners.

## Mediator

**Mediator** is a behavioral design pattern to reduce chaotic dependencies between objects. This pattern limits direct connections between objects and forces them to interact only through an intermediary object.

**Mediator** allows all calls to be managed centrally and eliminates direct dependencies between components.

### Pros
- The <ins>Single Responsibility Principle</ins> is upheld as it centralizes interactions among different components into one location, simplifying understanding and upkeep.
- Adhering to the <ins>Open/Closed Principle</ins>, the architecture allows for the addition of new mediators without necessitating modifications to existing components.
- This approach diminishes <ins>the interdependence</ins> between different parts of the software, reducing coupling.
- It facilitates <ins>the reusability</ins> of components within the system.
  
### Cons
- Over time, there is a risk that the mediator might develop into a "<ins>God Object</ins>", becoming overly complex and handling too many responsibilities.
- Adds more <ins>complexity</ins> during debugging and can be difficult for beginners.

## Getting started

To get started with this project, clone the repository to your local machine. Ensure you have .NET 7 SDK installed.

```console
git clone https://github.com/ivanvyd/cqrs-mediator.git
```

Restore and run the application using the .NET CLI:
```console
dotnet restore
```
```console
dotnet run
```

The API will be available at `https://localhost:7018` or `http://localhost:5264` with open api support by `/swagger` endpoint.
