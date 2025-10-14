# ------------------------ Stage 1: Build Angular ------------------------
FROM node:16-alpine AS client-build

# เลือกว่าจะ build dev หรือ prod (default=production)
ARG ANGULAR_ENV=dev
ENV ANGULAR_ENV=${ANGULAR_ENV}

WORKDIR /src/Web/web-ui

# ตัดเฉพาะ package*.json เพื่อ cache npm install
COPY Web/web-ui/package*.json ./
RUN npm install

# คัดลอกโค้ดทั้งหมด แล้ว build ตาม env ที่พี่ส่งเข้าไป
COPY Web/web-ui/ ./
RUN npm run build 

# -------------------- Stage 2: Restore & Publish .NET ------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build

# WORKDIR /
# 1) copy .sln + csproj เพื่อ restore dotnet เด้ออ
COPY *.sln ./
# COPY Web.API/*.csproj Web.API/
COPY Web/*.csproj Web/
COPY Core/Domain/*.csproj Core/Domain/
COPY Core/Application/*.csproj Core/Application/
COPY Infrastructure/Infrastructure/*.csproj Infrastructure/Infrastructure/
COPY Infrastructure/Persistence/*.csproj Infrastructure/Persistence/

RUN dotnet restore

# 2) copy โค้ด .NET ทั้งหมด แล้ว publish
COPY . .
WORKDIR /Web
RUN dotnet publish -c Release -o /app/publish

# ----------- Stage 3: Combine & Runtime ---------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# นำไฟล์ publish จาก Stage 2 มา
COPY --from=dotnet-build /app/publish .

# นำ static Angular จาก Stage 1 มาไว้ใน wwwroot
# (โดย Angular CLI จะ output ไปที่ dist/<projectName>)
COPY --from=client-build \
    /src/Web/web-ui/dist/web-ui \
    ./wwwroot

ADD https://api.nuget.org/v3/index.json addtest/
# COPY dev5.tar copytest/     

# expose port และเซ็ต environment ของ ASP.NET
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV HTTP_PORTS=
# ถ้าต้องการให้รันใน dev mode ก็เปลี่ยนค่าตรงนี้
ENV ASPNETCORE_ENVIRONMENT=dev

ENTRYPOINT ["dotnet", "Web.dll"]
