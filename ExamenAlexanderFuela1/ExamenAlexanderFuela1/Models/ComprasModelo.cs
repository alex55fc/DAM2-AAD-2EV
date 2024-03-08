namespace ExamenAlexanderFuela1.Models
{
    public class ComprasModelo
    {
        public int Id { get; set; }
        public int PrecioU { get; set; }
        public int Cantidad { get; set; }   
        public DateTime Fecha { get; set; }
        // 
        public ProductoModelo Producto { get; set; }
        public int ProductoId { get; set; }

        public ProveedorModelo Proveedor { get; set; }
        public int ProveedorId { get; set; }

    }
}
