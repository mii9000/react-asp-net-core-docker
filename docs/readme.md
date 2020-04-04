## Development
- Mac OSX
- Visual Studio Code
- ASP.NET Core 3.1
- NUnit
- React
- Reactstrap
- Redux
- Typescript
- JWT

## Debugging/Running
- Visual Studio
- Visual Studio Code (.NET Core Debugger)
- Command line `dotnet run` 

## Tests
- `dotnet test ./tests/SPA.Tests`
- Visual Studio Test Explorer
- Visual Studio Code Test Explorer

## Solution

#### Overview
Solution was approached by first drawing out ERD and application architecture on whiteboard. All initial questions and assumptions were drawn on the board right after reading the problem statement. 

![erd](fig1.jpg)
The above diagram is a representation of the data and their relationships.

![architecture](fig2.jpg)
The above diagram is to shows user interation and wireframe of the required features with optional features as mentioned in the problem statement. Some assumption about what technology to be used is also mentioned along with routes and possible optimizations.


#### SPA.Data
This .NET Standard library encapsulates data layer and its logic so that if its required to move from file based storage to a different kind of storage then very few changes would be required.
- Repository Pattern
- Async APIs
- Unit Tests

#### SPA.Web
This is the ASP.NET Core project that is serving both the clientside and API Endpoints.
- REST Principles
- Options Pattern
- DI
- JWT
- API Versioning

#### SPA.Web/ClientSide
This is the SPA built with React and scaffolded with `create-react-app`.
- Functional Components
- Use of Hooks
- Redux

#### Improvements
- Exception Handling Middleware
- Logging Middleware
- Convention based DI
- UI Form Validation 
- Containerized
- Save state and token in localStorage
- Frontend Unit Testing
- Extending Authorization Middleware
