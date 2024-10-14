FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CharSuiRelator/CharSuiRelator.csproj", "CharSuiRelator/"]
RUN dotnet restore "CharSuiRelator/CharSuiRelator.csproj"
COPY . .
WORKDIR "/src/CharSuiRelator"
RUN dotnet build "CharSuiRelator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CharSuiRelator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CharSuiRelator.dll"]
