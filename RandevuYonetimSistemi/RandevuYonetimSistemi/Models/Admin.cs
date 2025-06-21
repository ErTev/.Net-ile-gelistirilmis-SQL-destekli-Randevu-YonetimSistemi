using System.ComponentModel.DataAnnotations;

namespace RandevuYonetimSistemi.Models
{
    //Admin ve propertylerini belirleyen modelimiz
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli Bir mail giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(maximumLength: 8, MinimumLength = 4,
          ErrorMessage = "Geçerli Bir Şifre giriniz")]
        public string Sifre { get; set; }


    }
}
