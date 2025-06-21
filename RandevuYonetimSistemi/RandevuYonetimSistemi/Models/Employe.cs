using System.ComponentModel.DataAnnotations;

namespace RandevuYonetimSistemi.Models
{
    //Çalışan modelimiz, Person modelinden miras alır ve çalışanlara özel özellikler ekler
    public class Employe : Person
    {

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        public string Unvanı { get; set; }


        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(maximumLength: 8, MinimumLength = 4,
            ErrorMessage = "Geçerli Bir Şifre giriniz")]
        public string Sifre { get; set; }

        public bool Aktiflik {  get; set; }
    }
}
