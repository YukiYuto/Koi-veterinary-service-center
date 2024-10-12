# Use the official .NET 8.0 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all the project files
COPY ["KoiVeterinaryServiceCenter.API/KoiVeterinaryServiceCenter.API.csproj", "KoiVeterinaryServiceCenter.API/"]
COPY ["KoiVeterinaryServiceCenter.Services/KoiVeterinaryServiceCenter.Services.csproj", "KoiVeterinaryServiceCenter.Services/"]
COPY ["KoiVeterinaryServiceCenter.DataAccess/KoiVeterinaryServiceCenter.DataAccess.csproj", "KoiVeterinaryServiceCenter.DataAccess/"]
COPY ["KoiVeterinaryServiceCenter.Model/KoiVeterinaryServiceCenter.Model.csproj", "KoiVeterinaryServiceCenter.Model/"]
COPY ["KoiVeterinaryServiceCenter.Utility/KoiVeterinaryServiceCenter.Utility.csproj", "KoiVeterinaryServiceCenter.Utility/"]
RUN dotnet restore "./KoiVeterinaryServiceCenter.API/KoiVeterinaryServiceCenter.API.csproj"
COPY . .
WORKDIR "/src/KoiVeterinaryServiceCenter.API"
RUN dotnet build "./KoiVeterinaryServiceCenter.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KoiVeterinaryServiceCenter.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage - create the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "KoiVeterinaryServiceCenter.API.dll"]
