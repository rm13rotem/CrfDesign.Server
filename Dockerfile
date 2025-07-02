# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copy the actual solution folder into the container
COPY CrfDesign.Server ./CrfDesign.Server

# Set working directory to WebAPI project folder
WORKDIR /src/CrfDesign.Server/CrfDesign.Server.WebAPI

# Restore and publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Environment setup
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80
ENTRYPOINT ["dotnet", "CrfDesign.Server.WebAPI.dll"]
