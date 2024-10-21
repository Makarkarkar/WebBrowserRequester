FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebBrowserRequester/WebBrowserRequester.csproj", "WebBrowserRequester/"]
RUN dotnet restore "WebBrowserRequester/WebBrowserRequester.csproj"
COPY . .
WORKDIR "/src/WebBrowserRequester"
RUN dotnet build "WebBrowserRequester.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBrowserRequester.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBrowserRequester.dll"]
