using System.ComponentModel.DataAnnotations.Schema;

namespace RandevuYonetimSistemi.Models
{
    // SickPerson modelimiz, Person modelinden miras alır ve hastalara özel özellikler ekler (Randevu alırlar)
    public class SickPerson : Person
    {
        public bool CezaDurumu { get; set; } = false;

    }
}
