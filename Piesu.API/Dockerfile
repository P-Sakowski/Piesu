FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Piesu.API.csproj", "./"]
RUN dotnet restore "./Piesu.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Piesu.API.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Piesu.API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Piesu.API.dll"]