#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update && \
apt-get install -yq --no-install-recommends \
libgdiplus libc6-dev 
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Perform_OCR_Docker/Perform_OCR_Docker.csproj", "Perform_OCR_Docker/"]
RUN dotnet restore "Perform_OCR_Docker/Perform_OCR_Docker.csproj"
COPY . .
WORKDIR "/src/Perform_OCR_Docker"
RUN dotnet build "Perform_OCR_Docker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Perform_OCR_Docker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Perform_OCR_Docker.dll"]