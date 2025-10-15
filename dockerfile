# -------------------- Stage 1: Build Angular --------------------
FROM node:20 AS client-build
WORKDIR /src/Web/web-ui
COPY Web/web-ui/package*.json ./
RUN npm install
COPY Web/web-ui/ .
RUN npm run build

# -------------------- Stage 2: Build .NET --------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build
WORKDIR /src

COPY *.sln ./
COPY Web/*.csproj Web/
COPY Core/Domain/*.csproj Core/Domain/
COPY Core/Application/*.csproj Core/Application/
COPY Infrastructure/Infrastructure/*.csproj Infrastructure/Infrastructure/
COPY Infrastructure/Persistence/*.csproj Infrastructure/Persistence/

RUN dotnet restore
COPY . .

# ✅ build และ publish ออกไปที่ /app/publish (สำคัญมาก)
WORKDIR /src/Web
RUN dotnet publish -c Release -o /app/publish

# -------------------- Stage 3: Runtime --------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# ✅ copy ผลลัพธ์จาก publish เข้ามา
COPY --from=dotnet-build /app/publish ./
# ✅ copy Angular build ไปไว้ใน wwwroot
COPY --from=client-build /src/Web/web-ui/dist/web-ui ./wwwroot

EXPOSE 8080

ENTRYPOINT ["dotnet", "Web.dll"]