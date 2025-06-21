using System.ComponentModel.DataAnnotations;

namespace RandevuYonetimSistemi.Models
{
    //Doktor modelimiz, Person modelinden miras alır ve doktorlara özel özellikler ekler
        public class Doctor : Person
    {

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        public string Unvan { get; set; }


        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(maximumLength: 8, MinimumLength = 4,
            ErrorMessage = "Geçerli Bir Şifre giriniz")]
        public string Sifre { get; set; }
        public bool Aktiflik { get; set; }

        public ICollection<Appointment>? Randevular { get; set; }

    }
}
