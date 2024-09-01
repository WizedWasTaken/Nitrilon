# Use the official .NET SDK 8.0 as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /Nitrilon

# Copy the .csproj files and restore dependencies
COPY ["Nitrilon.Api/Nitrilon.Api.csproj", "Nitrilon.Api/"]
COPY ["Nitrilon.DataAccess/Nitrilon.DataAccess.csproj", "Nitrilon.DataAccess/"]
COPY ["Nitrilon.Entities/Nitrilon.Entities.csproj", "Nitrilon.Entities/"]
RUN dotnet restore "Nitrilon.Api/Nitrilon.Api.csproj"

# Copy the rest of the application code and build the application
COPY . .
WORKDIR "/Nitrilon/Nitrilon.Api"
RUN dotnet build "Nitrilon.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Nitrilon.Api.csproj" -c Release -o /app/publish

# Use the official ASP.NET Core runtime 8.0 as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nitrilon.Api.dll"]
