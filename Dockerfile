FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster as build

#install node
RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

WORKDIR /app

#copy source code
COPY src .

#inside executable directory
WORKDIR /app/SPA.Web

#restore will default to the only .csproj file
RUN dotnet restore

#publish into a folder 
RUN dotnet publish -c Release -o /app/publish

#only runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime

WORKDIR /app

#copy from previous output to root of /app folder
COPY --from=build /app/publish .

WORKDIR /app

# expose http and https
EXPOSE 80
EXPOSE 443

ENTRYPOINT [ "dotnet", "SPA.Web.dll" ]
