# Actividad 5 — TecnoNova Bolivia: CRUD de Productos

## Carátula (completar)
- Materia: ____________________
- Docente: ____________________
- Equipo: ____________________
- Integrantes: ____________________
- Fecha: ____________________

## 1. Problema de contexto
TecnoNova Bolivia es una tienda tecnológica que necesita llevar un registro sencillo de sus productos. Si la información de precios y cantidades se maneja manualmente, es fácil cometer errores o perder tiempo buscando datos. Por eso proponemos una aplicación web básica que permita registrar, consultar, actualizar y eliminar productos en una base de datos PostgreSQL.

Esta versión es un prototipo de laboratorio, no la tienda online completa del proyecto original.

## 2. Historias de usuario
**HU01 — Registrar productos.** Como administrador de TecnoNova, quiero registrar productos con código, nombre, categoría, precio y stock para tenerlos organizados en el sistema.

**HU02 — Consultar productos.** Como administrador, quiero ver una lista con los productos y sus cantidades disponibles para conocer qué artículos tengo registrados.

**HU03 — Mantener el catálogo.** Como administrador, quiero editar los datos o eliminar productos que ya no vendo para mantener actualizado el catálogo.

## 3. Diseño UML
Incluir en el PDF la imagen `UML_TecnoNova.png` que viene en esta carpeta. Un producto pertenece a una categoría; una categoría puede tener varios productos.

## 4. Modelo relacional de PostgreSQL
- `categorias(id PK, nombre UNIQUE)`
- `productos(id PK, sku UNIQUE, nombre, precio, stock, categoria_id FK -> categorias.id)`

Los campos del formulario coinciden con los atributos del modelo C#. El script está en `sql/01_tablas_y_datos.sql` y contiene ejemplos simplificados del catálogo original.

**Evidencia necesaria:** captura real de las dos tablas en PostgreSQL o captura del script ejecutado.

## 5. Arquitectura del prototipo
- **Modelo:** `Models/Producto.cs` y `Models/Categoria.cs`.
- **Vista:** archivos `.cshtml` de `Views/Productos/`.
- **Controlador:** `Controllers/ProductosController.cs`.
- **DAO:** `Data/ProductoDAO.cs`, donde están las consultas SQL y la conexión PostgreSQL mediante Npgsql.

## 6. Documento de pruebas (llenar con resultados reales)
| Caso | Pasos | Resultado esperado | Resultado real y captura |
| --- | --- | --- | --- |
| CP01 - Crear | Registrar un producto nuevo con un SKU que no exista. | El producto se ve en la tabla web y en PostgreSQL. | [Añadir captura propia] |
| CP02 - Leer | Abrir la página principal. | Se muestran los productos y su stock. | [Añadir captura propia] |
| CP03 - Editar | Cambiar precio o stock de un producto y guardar. | El valor nuevo aparece en la web y PostgreSQL. | [Añadir captura propia] |
| CP04 - Eliminar | Seleccionar Eliminar y confirmar. | El producto ya no aparece en la lista ni en PostgreSQL. | [Añadir captura propia] |
| CP05 - Validación | Intentar guardar un producto con precio igual a 0. | Se muestra mensaje de validación y no se guarda. | [Añadir captura propia] |

**Importante:** no declarar pruebas como aprobadas sin ejecutarlas realmente.

## 7. GitHub
- URL pública del repositorio: ____________________
- Captura de `git status` y/o `git log --oneline`: ____________________

## 8. Conclusión
Se implementó un prototipo sencillo de gestión de productos con las cuatro operaciones CRUD y separación de responsabilidades usando MVC y DAO. **Adaptar esta conclusión solo después de confirmar que el prototipo funciona y documentar sus pruebas.**
