# Corrección de compatibilidad decimal (Windows 11)

Este proyecto evita el error `FormatException: The input string '0.01' was not in a correct format` en las pantallas Nuevo producto / Editar.

Cambios:

- En `Models/Producto.cs`, `RangeAttribute` usa `ParseLimitsInInvariantCulture = true`. Así, los límites como `0.01` siempre se interpretan correctamente independientemente del idioma de Windows.
- En `Views/Productos/Create.cshtml` y `Edit.cshtml`, Precio usa `type="text"` con `inputmode="decimal"`, para aceptar números con coma en equipos donde la configuración regional utiliza coma decimal.

Para iniciar:

1. Asegúrate de tener PostgreSQL y la base `tecnonova_crud` ya configurada.
2. En la carpeta del proyecto, configura tu contraseña local con `dotnet user-secrets` si aún no lo hiciste.
3. Ejecuta `dotnet restore`, `dotnet build` y `dotnet run --launch-profile http`.
4. En un Windows con separador decimal coma, escribe precios como `4255,92` (sin separador de miles) en los formularios.

**No vuelvas a ejecutar el script SQL si ya creaste las tablas y datos.**

No subas contraseñas ni archivos `bin/`, `obj/` a GitHub. Las credenciales locales almacenadas con user-secrets no están dentro del ZIP.
