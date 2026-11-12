# TecnoNova Bolivia - Configuración en Windows 11

Proyecto de laboratorio: C# / ASP.NET Core MVC / PostgreSQL / Npgsql.
**Para la Actividad 5.** No hace falta instalar Ubuntu ni migrar el proyecto original de la tienda.

## 1) Instalar los programas en Windows 11

1. **.NET 10 SDK (Windows x64)**: https://dotnet.microsoft.com/en-us/download/dotnet/10.0
   - Instala el **SDK**, no solamente el Runtime.
   - Verifica en PowerShell: `dotnet --version` (debe comenzar con 10).
2. **PostgreSQL con pgAdmin**: https://www.postgresql.org/download/windows/
   - En el instalador conserva PostgreSQL Server y pgAdmin 4.
   - Puerto habitual: `5432`.
   - Usuario administrador: `postgres`.
   - Elige una contraseña propia y consérvala; **no la subas a GitHub**.
3. **Editor**: Visual Studio Code + extensión C# Dev Kit, o Visual Studio 2026 con la carga «ASP.NET y desarrollo web».
4. **Git**: https://git-scm.com/downloads/win (para subir al repositorio).

Para este proyecto con `net10.0`, si usas Visual Studio (no VS Code), utiliza Visual Studio 2026 o posterior.

## 2) Descomprimir y abrir la carpeta

Extrae el ZIP, entra en la carpeta `TecnoNova_CRUD_Actividad5` y abre una terminal **PowerShell** desde esa carpeta (Shift + clic derecho o barra de direcciones del Explorador: escribir `powershell`).

Debes ver el archivo `TecnoNovaCRUD.csproj` al ejecutar:

```powershell
Get-ChildItem TecnoNovaCRUD.csproj
```

## 3) Crear las tablas con pgAdmin 4

1. Abre **pgAdmin 4**.
2. Conéctate al servidor PostgreSQL local (introduce la contraseña de `postgres` cuando te la solicite).
3. En el panel izquierdo: `Servers > PostgreSQL > Databases`.
4. Clic derecho en `Databases` > `Create` > `Database...`.
5. Escribe `tecnonova_crud` y guarda.
6. Selecciona la nueva base `tecnonova_crud` y abre **Query Tool** (Herramienta de consulta).
7. Abre el archivo `sql/01_tablas_y_datos.sql` en Query Tool o pega allí su contenido.
8. Pulsa **Execute** (F5). Se crearán `categorias` y `productos` con datos de ejemplo.
9. Para comprobarlo, ejecuta:

```sql
SELECT * FROM categorias ORDER BY id;
SELECT * FROM productos ORDER BY id;
```

**Importante:** ejecuta el script *dentro* de `tecnonova_crud`, no dentro de la base `postgres`.

## 4) Configurar la conexión SIN subir contraseñas

Desde PowerShell en la carpeta del proyecto:

```powershell
dotnet user-secrets set "ConnectionStrings:TecnoNova" "Host=localhost;Port=5432;Database=tecnonova_crud;Username=postgres;Password=TU_CONTRASEÑA"
```

Cambia `TU_CONTRASEÑA` por la contraseña real que configuraste al instalar PostgreSQL. Esta configuración se guarda en tu perfil de Windows, **fuera del proyecto**.

**No compartas tu contraseña, ni capturas donde se vea.** Si tu contraseña contiene punto y coma (`;`), usa una que no lo contenga para este ejemplo simplificado o escápala correctamente en la cadena de conexión.

Para ver si hay una conexión guardada (cuidado: puede mostrar la contraseña):

```powershell
dotnet user-secrets list
```

No incluyas la salida de este último comando en las evidencias de clase.

## 5) Restaurar, compilar y ejecutar

```powershell
dotnet --version
dotnet restore
dotnet build
dotnet run --launch-profile http
```

Abre en el navegador: **http://localhost:5071**.

Si el navegador no abre automáticamente, copia esa dirección. Para detener el servidor presiona `Ctrl + C` en PowerShell.

## 6) Pruebas que necesitas fotografiar / capturar

1. **CREATE**: registra un producto con SKU no repetido; captura formulario y lista.
2. **READ**: captura la tabla de productos en el navegador.
3. **UPDATE**: modifica precio o stock; captura resultado.
4. **DELETE**: elimina un producto de prueba; captura el resultado.
5. **Base de datos**: en Query Tool verifica `SELECT * FROM productos ORDER BY id;`.
6. **Validación**: precio 0 o SKU duplicado; debe rechazarse.

Guarda capturas propias, tomadas tras ejecutar el programa. El archivo Word del informe es para completar con pruebas reales.

## 7) Subir el código a GitHub

Primero crea en GitHub un repositorio **público y vacío** (sin README ni .gitignore; esos archivos ya están en este proyecto).

En PowerShell desde la carpeta del proyecto:

```powershell
git init
git add .
git status
git commit -m "Actividad 5: TecnoNova CRUD en C# MVC y PostgreSQL"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/TU_REPOSITORIO.git
git push -u origin main
```

Reemplaza la URL de ejemplo por la **URL real** de tu repositorio. GitHub puede pedir iniciar sesión. Si ya hay un `origin`, cambia su URL con `git remote set-url origin URL_REAL` en vez de usar `git remote add`.

**Sí subir**: `.cs`, `.cshtml`, `.csproj`, `sql/`, `docs/`, `wwwroot/`, `README.md`, `GUIA_WINDOWS_11.md`, `.gitignore`.

**No subir**: contraseñas, `bin/`, `obj/`, `.vs/`, archivos privados o capturas con información sensible.

## 8) Solución de errores comunes

- **`dotnet` no se reconoce**: reinicia PowerShell después de instalar el SDK, y confirma la instalación de .NET 10.
- **`No SDK found` o `NETSDK1045`**: falta SDK 10; instala el SDK correcto.
- **`password authentication failed`**: la contraseña en user-secrets no coincide con la del usuario `postgres`.
- **`database "tecnonova_crud" does not exist`**: crea la base con ese nombre exacto en pgAdmin.
- **`relation "productos" does not exist`**: ejecuta el archivo `sql/01_tablas_y_datos.sql` dentro de `tecnonova_crud`.
- **`connection refused`**: verifica que el servicio de PostgreSQL esté iniciado y use el puerto 5432.
- **Puerto 5071 ocupado**: detén el programa anterior con Ctrl+C; si usas otro puerto, abre la URL indicada en consola.

## Alcance

Se incluyen categorías de referencia y CRUD **de productos**. No implementa login, compras, pedidos ni ventas. Eso es intencional: mantiene la tarea de laboratorio simple y alineada con la rúbrica.
