# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7194

# Base image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["StockPredictor.csproj", "./"]
RUN dotnet restore "StockPredictor.csproj"

# Copy the entire project
COPY . .

# Publish the app
RUN dotnet publish "StockPredictor.csproj" -c Release -o /app/publish

# Final image for running the app
FROM base AS final
WORKDIR /app

# Copy published files from the build stage
COPY --from=build /app/publish .

# Copy sp500.json file into the app's App_Data folder
COPY App_Data/sp500.json ./App_Data/sp500.json

# Set the entry point
ENTRYPOINT ["dotnet", "StockPredictor.dll"]
