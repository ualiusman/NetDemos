  # syntax=docker/dockerfile:1
  FROM mcr.microsoft.com/dotnet/aspnet:5.0
  COPY dist app/
  WORKDIR /app
  EXPOSE 80/tcp
  ENTRYPOINT ["dotnet", "DockerApp.dll"]