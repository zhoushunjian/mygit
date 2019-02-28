#FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
#WORKDIR /src
#COPY CoreBackend/CoreBackend.csproj CoreBackend/
#RUN dotnet restore CoreBackend/CoreBackend.csproj
#COPY . .
#WORKDIR /src/CoreBackend
#RUN dotnet build CoreBackend.csproj -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish CoreBackend.csproj -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "CoreBackend.dll"]
#

FROM microsoft/aspnetcore:latest
WORKDIR /app
COPY ./
ENTRYPOINT ["dotnet", "CoreBackend.dll"]