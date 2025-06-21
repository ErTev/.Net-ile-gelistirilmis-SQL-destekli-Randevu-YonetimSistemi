using System.ComponentModel.DataAnnotations;

namespace RandevuYonetimSistemi.Services.Validation
{
    //Kendi özel ValidationAttribute sınıfımızı oluşturuyoruz.
    //Bu sınıf, belirli bir tarih aralığında olup olmadığını kontrol eder.

    public class DateInRangeAttribute : ValidationAttribute
    {
        private readonly int _minDaysFromToday;//Min tarih
        private readonly int _maxDaysFromToday;//Max tarih

        //Min ve Max tarihi yapıcı metoddan alarak hata mesajını oluşturuyoruz.
        public DateInRangeAttribute(int minDaysFromToday, int maxDaysFromToday)
        {
            _minDaysFromToday = minDaysFromToday;
            _maxDaysFromToday = maxDaysFromToday;
            ErrorMessage = $"Tarih, {minDaysFromToday} gün ile {maxDaysFromToday} gün arasında olmalıdır.";

        }

        //Model doğrulamasını belirleyen metot
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime selectedDate)
            {
                return new ValidationResult("Geçerli Bir Tarih Aralığı Gitriniz.");
            }
            var minDate = DateTime.Today.AddDays(_minDaysFromToday);
            var maxDate = DateTime.Today.AddDays(_maxDaysFromToday);

            if (selectedDate < minDate || selectedDate > maxDate)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
