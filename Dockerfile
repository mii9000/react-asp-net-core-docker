FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster as build

RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

WORKDIR /app

COPY src .

WORKDIR /app/SPA.Web

RUN dotnet restore

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime

WORKDIR /app

COPY --from=build /app/publish .

WORKDIR /app

RUN ls

EXPOSE 80

EXPOSE 443

ENTRYPOINT [ "dotnet", "SPA.Web.dll" ]
