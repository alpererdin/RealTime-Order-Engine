FROM mcr.microsoft.com/dotnet/sdk:10.0.101 AS build
WORKDIR /src
COPY src .

RUN dotnet publish RealTimeOrderEngine.Client/RealTimeOrderEngine.Client.csproj -c Release -o /client-out

RUN dotnet publish RealTimeOrderEngine.Api/RealTimeOrderEngine.Api.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
COPY --from=build /client-out/wwwroot ./wwwroot

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "RealTimeOrderEngine.Api.dll"]