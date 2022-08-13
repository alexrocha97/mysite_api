using System.ComponentModel;
namespace API.Domain.Enums
{
    public enum TipoUsuario
    {
        [DescriptionAttribute("Admin")]
        Admin = 1,
        [DescriptionAttribute("Comum")]
        Comum = 2
    }
}
