FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["MusicStore.csproj", "./"]
RUN dotnet restore "MusicStore.csproj"

COPY . .
RUN dotnet publish "MusicStore.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

COPY MusicStore.db .

ENTRYPOINT ["dotnet", "MusicStore.dll"]
