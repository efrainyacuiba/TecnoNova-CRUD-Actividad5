# Ejecutar desde la raiz del proyecto TecnoNova_CRUD_Actividad5.
# Primero detener dotnet run (Ctrl+C).
$ErrorActionPreference = "Stop"
$producto = Join-Path (Get-Location) 'Models\Producto.cs'
if (-not (Test-Path $producto)) { throw 'No estas dentro de la carpeta TecnoNova_CRUD_Actividad5.' }
$original = '[Range(typeof(decimal), "0.01", "99999999", ErrorMessage = "El precio debe ser mayor que cero.")]'
$actualizado = '[Range(typeof(decimal), "0.01", "99999999", ParseLimitsInInvariantCulture = true, ErrorMessage = "El precio debe ser mayor que cero.")]'
$contenido = Get-Content $producto -Raw -Encoding UTF8
if ($contenido.Contains($original)) {
    $contenido.Replace($original, $actualizado) | Set-Content $producto -Encoding UTF8
} elseif (-not $contenido.Contains($actualizado)) {
    throw 'No se encontro la validacion original del precio. Revisa Models/Producto.cs.'
}
foreach ($nombre in @('Create','Edit')) {
    $vista = Join-Path (Get-Location) "Views\Productos\$nombre.cshtml"
    $contenido = Get-Content $vista -Raw -Encoding UTF8
    $antes = '<input asp-for="Precio" type="number" min="0.01" step="0.01" />'
    $despues = '<input asp-for="Precio" type="text" inputmode="decimal" placeholder="Ej. 4255,92" />'
    if ($contenido.Contains($antes)) {
        $contenido.Replace($antes, $despues) | Set-Content $vista -Encoding UTF8
    } elseif (-not $contenido.Contains($despues)) {
        throw "No se encontro el campo Precio en $vista"
    }
}
Write-Host 'Correccion aplicada. Ejecuta dotnet build y dotnet run --launch-profile http.' -ForegroundColor Green
