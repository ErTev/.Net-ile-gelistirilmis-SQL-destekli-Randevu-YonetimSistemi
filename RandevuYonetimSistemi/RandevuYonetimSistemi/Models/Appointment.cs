using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RandevuYonetimSistemi.Services.Validation;

namespace RandevuYonetimSistemi.Models
{
    // Appointment modelimiz, randevu bilgilerini tutar
    public class Appointment
    {

        public int Id { get; set; }

        [ForeignKey(nameof(SickPerson.Id))]
        public int HastaId { get; set; }

        public SickPerson Hasta { get; set; }


        [ForeignKey(nameof(Doctor.Id))]
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }


        public DateTime RandevuGirisSaati { get; set; }


        [Required(ErrorMessage = "lütfen muayene tarihi seçiniz")]
        [DateInRange(1,14)]
        [DataType(DataType.Date)]
        public DateTime RandevuTarihi { get; set; }

    }
}
