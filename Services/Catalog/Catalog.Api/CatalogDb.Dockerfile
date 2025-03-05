# Use the official SQL Server image
FROM mcr.microsoft.com/mssql/server:2022-latest AS sqlserver
ENV ACCEPT_EULA=Y 
ENV SA_PASSWORD=ewww@20230302

# Add optional environment variable for database name
ENV MSSQL_DBNAME=CatalogService
