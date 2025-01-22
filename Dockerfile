FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
RUN dotnet publish API_GestionDominios -c Release -o ./publish /p:SelfContained=false /p:EnvironmentName=Development

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS prod
WORKDIR /App
COPY --from=build /App/publish .

ENTRYPOINT ["dotnet", "API_GestionDominios.dll"]