FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers

# Copy everything else and build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

ENV ASPNETCORE_URLS=http://+:81
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build-env /app/out .
RUN mkdir -p /app/etc/keys
ENTRYPOINT ["dotnet", "DemoAPI.dll"]
