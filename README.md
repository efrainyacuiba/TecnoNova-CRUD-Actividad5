# TecnoNova Bolivia - CRUD básico (Actividad 5)

Prototipo académico con **ASP.NET Core MVC (.NET 10), PostgreSQL y Npgsql**. Es una versión sencilla de TecnoNova Bolivia: administra productos y consulta categorías.

## Iniciar en Windows 11

**Lee `GUIA_WINDOWS_11.md`** para instalar .NET 10, PostgreSQL, crear la base de datos, configurar la conexión y subir el proyecto a GitHub.

Resumen (desde PowerShell, una vez creada la BD `tecnonova_crud` y ejecutado el SQL en pgAdmin):

```powershell
dotnet user-secrets set "ConnectionStrings:TecnoNova" "Host=localhost;Port=5432;Database=tecnonova_crud;Username=postgres;Password=TU_CONTRASEÑA"
dotnet restore
dotnet build
dotnet run --launch-profile http
```

Luego visita `http://localhost:5071`.

## Iniciar en Ubuntu

```bash
sudo -u postgres createdb tecnonova_crud
sudo -u postgres psql -d tecnonova_crud -f sql/01_tablas_y_datos.sql
dotnet user-secrets set "ConnectionStrings:TecnoNova" "Host=localhost;Port=5432;Database=tecnonova_crud;Username=postgres;Password=TU_CONTRASEÑA"
dotnet restore && dotnet build && dotnet run
```

La contraseña debe coincidir con la configurada para el usuario PostgreSQL. Requiere SDK .NET 10.

## Estructura

```text
Controllers/ProductosController.cs    - Controlador (acciones CRUD)
Models/Producto.cs, Categoria.cs      - Modelos C#
Data/ProductoDAO.cs                   - Acceso a PostgreSQL y SQL
Views/Productos/                     - Pantallas Index, Create, Edit, Delete
sql/01_tablas_y_datos.sql            - Crear tablas y cargar datos de ejemplo
docs/                                - Historias de usuario y diagramas de diseño
GUIA_WINDOWS_11.md                   - Instalación completa Windows 11
```

No hay conexión ni contraseñas guardadas en el ZIP. Configúralas con `dotnet user-secrets`; no se suben al repositorio. Las capturas de pruebas y el enlace real de GitHub deben añadirse al informe de entrega tras probar el sistema.
