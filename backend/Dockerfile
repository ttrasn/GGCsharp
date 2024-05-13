FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /backend

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /backend
COPY --from=build-env /backend/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "backend.dll"]