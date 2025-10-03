# ------------------------ Stage 1: Build Angular ------------------------
FROM node:16.20.2-alpine AS client-build

# เลือกว่าจะ build dev / ss / prod (default=dev)
ARG ANGULAR_ENV=dev
ENV ANGULAR_ENV=${ANGULAR_ENV}

WORKDIR /src/Web/web-ui

COPY Web/web-ui/package*.json ./
RUN npm install

COPY Web/web-ui/ ./
RUN if [ "$ANGULAR_ENV" = "dev" ]; then npm run build:dev; \
    elif [ "$ANGULAR_ENV" = "ss" ]; then npm run build:ss; \
    elif [ "$ANGULAR_ENV" = "prod" ]; then npm run build:prod; \
    else npm run build; fi

# -------------------- Stage 2: Restore & Publish .NET ------------------
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

WORKDIR /src/Web
RUN dotnet publish -c Release -o /app/publish

# ----------- Stage 3: Combine & Runtime ---------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=dotnet-build /app/publish ./
COPY --from=client-build /src/Web/web-ui/dist/web-ui ./wwwroot

EXPOSE 80

# ใช้ Angular ARG เพื่อตั้งค่า ASP.NET Core environment
ARG ANGULAR_ENV=dev
ENV ASPNETCORE_ENVIRONMENT=${ANGULAR_ENV}
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "Web.dll"]
