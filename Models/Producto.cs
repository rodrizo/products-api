namespace ProductoCRUD.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Marca { get; set; }
        public string? Categoria { get; set; }
        public decimal Precio { get; set; }
        public int? Cantidad { get; set; }
    }
}
