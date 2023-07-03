using System.ComponentModel.DataAnnotations;
namespace Przychodnia.Models
{
    public class EdytujHaslo
    {
        public string password { get; set; } = string.Empty;
        public string repeatPassword { get; set; } = string.Empty;

    }
}