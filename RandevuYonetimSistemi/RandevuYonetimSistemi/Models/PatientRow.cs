namespace RandevuYonetimSistemi.Models
{
    //Mevcut güne ait randevu sıralarını tutan modelimiz
    public class PatientRow
    {
        public int Id { get; set; }


        public int HastaId { get; set; }
        public SickPerson Hasta { get; set; }


        public int DoktorId { get; set; }
        public Doctor Doktor { get; set; }

        public DateTime SıraTarihi { get; set; }

        public string Durum { get; set; }
    }
}
