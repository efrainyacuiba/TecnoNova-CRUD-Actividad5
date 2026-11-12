using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TecnoNovaCRUD.Models;

public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El cÃ³digo es obligatorio.")]
    [StringLength(30, ErrorMessage = "MÃ¡ximo 30 caracteres.")]
    public string Sku { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "MÃ¡ximo 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "99999999", ParseLimitsInInvariantCulture = true, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }

    [Range(0, 999999, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecciona una categorÃ­a.")]
    public int CategoriaId { get; set; }

    // Solo muestra el nombre de la categorÃ­a; no se exige al validar formularios.
    [ValidateNever]
    public string CategoriaNombre { get; set; } = string.Empty;
}

