using Npgsql;
using TecnoNovaCRUD.Models;

namespace TecnoNovaCRUD.Data;

// DAO: aquí están las consultas SQL, fuera del controlador.
public class ProductoDAO
{
    private readonly string _conexion;

    public ProductoDAO(IConfiguration config)
    {
        _conexion = config.GetConnectionString("TecnoNova")
            ?? throw new InvalidOperationException(
                "Configura la conexión con: dotnet user-secrets set \"ConnectionStrings:TecnoNova\" \"...\"");
    }

    public List<Categoria> ObtenerCategorias()
    {
        var categorias = new List<Categoria>();
        using var conexion = new NpgsqlConnection(_conexion);
        conexion.Open();
        using var comando = new NpgsqlCommand(
            "SELECT id, nombre FROM categorias ORDER BY nombre", conexion);
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            categorias.Add(new Categoria
            {
                Id = lector.GetInt32(0),
                Nombre = lector.GetString(1)
            });
        }
        return categorias;
    }

    public List<Producto> ObtenerTodos()
    {
        var productos = new List<Producto>();
        using var conexion = new NpgsqlConnection(_conexion);
        conexion.Open();
        using var comando = new NpgsqlCommand(@"
            SELECT p.id, p.sku, p.nombre, p.precio, p.stock,
                   p.categoria_id, c.nombre
            FROM productos p
            INNER JOIN categorias c ON c.id = p.categoria_id
            ORDER BY p.id", conexion);
        using var lector = comando.ExecuteReader();
        while (lector.Read())
            productos.Add(LeerProducto(lector));
        return productos;
    }

    public Producto? ObtenerPorId(int id)
    {
        using var conexion = new NpgsqlConnection(_conexion);
        conexion.Open();
        using var comando = new NpgsqlCommand(@"
            SELECT p.id, p.sku, p.nombre, p.precio, p.stock,
                   p.categoria_id, c.nombre
            FROM productos p
            INNER JOIN categorias c ON c.id = p.categoria_id
            WHERE p.id = @id", conexion);
        comando.Parameters.AddWithValue("id", id);
        using var lector = comando.ExecuteReader();
        return lector.Read() ? LeerProducto(lector) : null;
    }

    public void Insertar(Producto producto)
    {
        using var conexion = new NpgsqlConnection(_conexion);
        conexion.Open();
        using var comando = new NpgsqlCommand(@"
            INSERT INTO productos (sku, nombre, precio, stock, categoria_id)
            VALUES (@sku, @nombre, @precio, @stock, @categoria)", conexion);
        PonerParametros(comando, producto);
        comando.ExecuteNonQuery();
    }

    public bool Actualizar(Producto producto)
    {
        using var conexion = new NpgsqlConnection(_conexion);
        conexion.Open();
        using var comando = new NpgsqlCommand(@"
            UPDATE productos
            SET sku = @sku, nombre = @nombre, precio = @precio,
                stock = @stock, categoria_id = @categoria
            WHERE id = @id", conexion);
        comando.Parameters.AddWithValue("id", producto.Id);
        PonerParametros(comando, producto);
        return comando.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int id)
    {
        using var conexion = new NpgsqlConnection(_conexion);
        conexion.Open();
        using var comando = new NpgsqlCommand(
            "DELETE FROM productos WHERE id = @id", conexion);
        comando.Parameters.AddWithValue("id", id);
        return comando.ExecuteNonQuery() > 0;
    }

    private static void PonerParametros(NpgsqlCommand comando, Producto producto)
    {
        comando.Parameters.AddWithValue("sku", producto.Sku.Trim());
        comando.Parameters.AddWithValue("nombre", producto.Nombre.Trim());
        comando.Parameters.AddWithValue("precio", producto.Precio);
        comando.Parameters.AddWithValue("stock", producto.Stock);
        comando.Parameters.AddWithValue("categoria", producto.CategoriaId);
    }

    private static Producto LeerProducto(NpgsqlDataReader lector)
    {
        return new Producto
        {
            Id = lector.GetInt32(0),
            Sku = lector.GetString(1),
            Nombre = lector.GetString(2),
            Precio = lector.GetDecimal(3),
            Stock = lector.GetInt32(4),
            CategoriaId = lector.GetInt32(5),
            CategoriaNombre = lector.GetString(6)
        };
    }
}
