# -------------------- Stage 1: Restore & Publish .NET --------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build

WORKDIR /src

COPY *.sln ./
COPY Web/*.csproj Web/
COPY Core/Domian/*.csproj Core/Domian/
COPY Core/Application/*.csproj Core/Application/
COPY Infrastructure/Infrastructure/*.csproj Infrastructure/Infrastructure/
COPY Infrastructure/Persistence/*.csproj Infrastructure/Persistence/

RUN dotnet restore
COPY . .

# Build & Publish .NET app
WORKDIR /src/Web
RUN dotnet publish -c Release -o /app/publish

# -------------------- Stage 2: Build Angular --------------------
FROM node:20 AS client-build

WORKDIR /src/Web/web-ui
COPY Web/web-ui/package*.json ./
RUN npm install
COPY Web/web-ui/ .
ARG ANGULAR_ENV=dev
RUN npm run build -- --configuration=$ANGULAR_ENV

# -------------------- Stage 3: Runtime --------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copy published .NET app
COPY --from=dotnet-build /app/publish ./

# Copy Angular build output to wwwroot
COPY --from=client-build /src/Web/web-ui/dist/web-ui ./wwwroot

EXPOSE 80

# Set environment
ARG ANGULAR_ENV=dev
ENV ASPNETCORE_ENVIRONMENT=${ANGULAR_ENV}
ENV ASPNETCORE_URLS=http://+:80

# Run .NET app in foreground
ENTRYPOINT ["dotnet", "Web.dll"]
