namespace Answers_API_LeonardoCortes.Models.DTOs
{
    public class UserDTO
    {
        public int IDUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? NumeroTelefono { get; set; }
        public string Contrasennia { get; set; } = null!;
        public int CantidadStrike { get; set; }
        public string CorreoRespaldo { get; set; } = null!;
        public string? DescripcionTrabajo { get; set; }
        public int IDEstatusUsuario { get; set; }
        public int IDPais { get; set; }
        public int IDRol { get; set; }
    }
}
