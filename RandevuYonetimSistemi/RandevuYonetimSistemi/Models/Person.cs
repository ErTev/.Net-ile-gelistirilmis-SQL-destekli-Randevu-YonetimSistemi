using System.ComponentModel.DataAnnotations;

namespace RandevuYonetimSistemi.Models
{
    // Person modelimiz, sistemdeki tüm kişilerin temel özelliklerini tutar (BASE CLASSIMIZDIR NEWLENEMEZ) 
    public abstract class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(30, ErrorMessage = "Maximum 30 karakter giriniz")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(30, ErrorMessage = "Maximum 30 karakter giriniz")]
        public string Soyadi { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(maximumLength: 11, MinimumLength = 11,
            ErrorMessage = "Geçerli Bir T.C giriniz")]
        public string Tc { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli Bir mail giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(maximumLength: 11, MinimumLength = 11,
            ErrorMessage = "Geçerli Bir Telefon Numarası giriniz")]
        public string Telefon { get; set; }
    }
}
