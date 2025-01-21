FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env

WORKDIR /src
COPY src/Orderee.Api/Orderee.Api.csproj ./src/Orderee.Api/
RUN dotnet restore ./src/Orderee.Api/Orderee.Api.csproj

COPY . .
RUN dotnet publish src/Orderee.Api/Orderee.Api.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build-env /out .

# Faz a aplicação escutar na porta 5000
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "Orderee.Api.dll"]