FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY [".", "."]
RUN dotnet restore "PhysicsWars.WebApi/PhysicsWars.WebApi.csproj"

WORKDIR "/src/PhysicsWars.WebApi"
RUN dotnet build "PhysicsWars.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhysicsWars.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhysicsWars.WebApi.dll"]
