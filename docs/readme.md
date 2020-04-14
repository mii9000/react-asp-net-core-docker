## Development
- Mac OS X
- Visual Studio Code
- ASP.NET Core 3.1
- React
- Semantic React UI
- Redux Toolkit
- Typescript
- JWT
- Google OAuth
- Azure Data Studio
- docker

## Debugging/Running
- Visual Studio
- Visual Studio Code (.NET Core Debugger)
- `dotnet run src/SPA.Web/SPA.Web.csproj`
- `docker-compose up --build` 

## Tests
- `dotnet test ./tests/SPA.Tests`
- Visual Studio Test Explorer
- Visual Studio Code Test Explorer

## Solution

#### Overview
Solution was approached by first drawing out ERD and application architecture on whiteboard. All initial questions and assumptions were drawn on the board right after reading the problem statement. 

#### SPA.Data
This .NET Standard library encapsulates data layer and its logic so that if its required to move from one storage to a different kind of storage then very few changes would be required.
- Repository Pattern
- Async APIs

#### SPA.Web
This is the ASP.NET Core project that is serving both the clientside and API endpoints.
- REST
- Options Pattern
- DI
- JWT
- API Versioning
- Auth

#### SPA.Web/ClientSide
This is the SPA built with React and scaffolded with `create-react-app`.
- Functional Components
- Use of Hooks
- Redux

#### Improvements
- API Validation e.g. FluentValidation
- Exception Handling Middleware
- Convention based DI Registration
- Client-side Validation 
- Frontend/Backend Unit Testing
- Inject credentials and secrets from secure storage like Consul Vault or Azure Vault

#### NOTE
- Find a video in this directory for a demo