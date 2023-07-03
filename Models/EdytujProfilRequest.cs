using System.ComponentModel.DataAnnotations;
namespace Przychodnia.Models
{
    public class EdytujProfilRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string pesel { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string PrzyjmowaneLeki { get; set; }
        public string Alergie { get; set; }
    }
}