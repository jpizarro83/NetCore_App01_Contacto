1) saber la version de netcore
dotnet --version

2) instalar complementos para visual studio
  C# -> ms-dotnettools.csharp
  Conectarse a la BD Sql Server -> ms-mssql.mssql
  Cliente Rest -> humao.rest-client

3) crear proyecto:
 -> dotnet new webapi -o Contactos

4) Instalar entityframework
dotnet add package Microsoft.EntityFrameworkCore --version 3.1.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 3.1.5

5) instalar dotnet para usar entity framwork: par amigracion por ejemplo:
dotnet tool install --global dotnet-ef

6) crear la primera migracion
	dotnet ef migrations add MigracionInicial

7) ejecutar la migración:
	dotnet ef database update