#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MES.Service/MES.Service.csproj", "MES.Service/"]
RUN dotnet restore "MES.Service/MES.Service.csproj"
COPY . .
WORKDIR "/src/MES.Service"
RUN dotnet build "MES.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MES.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MES.Service.dll"]