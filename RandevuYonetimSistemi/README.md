# Diş Kliniği Randevu Yönetim Sistemi

ASP.NET Core MVC ve SQL Server kullanılarak geliştirilmiş web tabanlı bir diş kliniği randevu yönetim sistemidir.

Sistem; hastaların bilgilerini doğrulayarak randevu oluşturmasına, uygun doktor ve saatleri görüntülemesine, mevcut randevularını sorgulamasına ve kliniğin doktor, çalışan ve randevu süreçlerini yönetmesine olanak sağlar.

## Projenin Amacı

Bu projenin amacı, diş kliniklerinde kullanılan randevu ve personel yönetimi süreçlerini dijital ortama taşımaktır.

Sistem aşağıdaki temel kullanıcı gruplarını destekler:

- Hastalar
- Doktorlar
- Klinik çalışanları
- Yöneticiler

## Temel Özellikler

### Hasta İşlemleri

- Hasta bilgileriyle kayıt oluşturma
- E-posta üzerinden doğrulama kodu gönderme
- Doğrulama sonrasında randevu alma
- Doktor seçimi
- Tarih ve uygun saat seçimi
- Dolu randevu saatlerinin otomatik olarak engellenmesi
- T.C. kimlik numarasıyla mevcut randevuları sorgulama
- Randevu bilgilerinin e-posta ile gönderilmesi

### Randevu Yönetimi

- Doktora göre uygun saatlerin hesaplanması
- Seçilen tarihteki dolu saatlerin listeden çıkarılması
- Aynı doktora aynı tarih ve saatte birden fazla randevu verilmesinin önlenmesi
- Randevu tarihi için özel doğrulama kuralları
- Aktif doktorların randevu ekranında listelenmesi
- Yaklaşan randevuların görüntülenmesi

### Muayene Sırası

- Güncel ve yaklaşan randevuların görüntülenmesi
- Doktor bazlı hasta sıralarının oluşturulması
- Randevuların tarih ve saate göre sıralanması
- Her doktor için sıradaki hastaların listelenmesi

### Yönetici Paneli

- Yönetici girişi
- Session tabanlı oturum yönetimi
- Doktorları listeleme
- Doktor ekleme, güncelleme ve silme
- Klinik çalışanlarını listeleme
- Çalışan ekleme, güncelleme ve silme
- Aktif veya pasif personel durumunu yönetme

## Kullanılan Teknolojiler

- C#
- .NET 9
- ASP.NET Core MVC
- Entity Framework Core 9
- SQL Server
- SQL Server LocalDB
- Razor Views
- LINQ
- HTML
- CSS
- JavaScript
- AJAX
- Bootstrap
- SMTP e-posta servisi
- Session yönetimi

## Proje Mimarisi

Proje, ASP.NET Core MVC mimarisine göre geliştirilmiştir.

```text
RandevuYonetimSistemi/
│
├── Controllers/
│   ├── AdminController.cs
│   ├── BaseController.cs
│   ├── DoctorController.cs
│   ├── EmployeController.cs
│   ├── HomeController.cs
│   ├── PatientRowController.cs
│   └── SickPersonController.cs
│
├── Data/
│   └── RandevuDbContext.cs
│
├── Migrations/
│
├── Models/
│   ├── Admin.cs
│   ├── AdminPanelViewModel.cs
│   ├── Appointment.cs
│   ├── Doctor.cs
│   ├── Employe.cs
│   ├── PatientRow.cs
│   ├── Person.cs
│   └── SickPerson.cs
│
├── Services/
│   ├── EmailService.cs
│   ├── EmailSettings.cs
│   └── Validation/
│       └── DateInRangeAttribute.cs
│
├── Views/
│   ├── Admin/
│   ├── Doctor/
│   ├── Employe/
│   ├── Home/
│   ├── PatientRow/
│   ├── Shared/
│   └── SickPerson/
│
├── wwwroot/
├── appsettings.json
├── Program.cs
└── RandevuYonetimSistemi.csproj
```

## Veritabanı Yapısı

Sistemde Entity Framework Core Code First yaklaşımı kullanılmaktadır.

Temel veritabanı tabloları:

| Tablo | Açıklama |
|---|---|
| `Admins` | Yönetici hesaplarını tutar |
| `Doctors` | Doktor bilgilerini tutar |
| `Employes` | Klinik çalışanlarını tutar |
| `SickPeople` | Hasta bilgilerini tutar |
| `Appointments` | Oluşturulan randevuları tutar |

Hasta T.C. kimlik numarası üzerinde benzersiz indeks kullanılmaktadır. Böylece aynı hasta için tekrar eden kayıtların önüne geçilir.

## Gereksinimler

Projeyi çalıştırmak için aşağıdaki bileşenler gereklidir:

- .NET 9 SDK
- Visual Studio 2022 veya Visual Studio Code
- SQL Server veya SQL Server LocalDB
- Entity Framework Core araçları

## Kurulum

### 1. Depoyu klonlayın

```bash
git clone https://github.com/ErTev/.Net-ile-gelistirilmis-SQL-destekli-Randevu-YonetimSistemi.git
```

### 2. Proje klasörüne girin

```bash
cd .Net-ile-gelistirilmis-SQL-destekli-Randevu-YonetimSistemi
cd RandevuYonetimSistemi/RandevuYonetimSistemi
```

### 3. NuGet paketlerini yükleyin

```bash
dotnet restore
```

### 4. Veritabanı bağlantısını yapılandırın

`appsettings.json` dosyasındaki bağlantı cümlesini kendi SQL Server yapılandırmanıza göre düzenleyin.

SQL Server LocalDB için örnek:

```json
"ConnectionStrings": {
  "defaultconnection": "Server=(localdb)\\MSSQLLocalDB;Database=RandevuDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 5. E-posta yapılandırmasını düzenleyin

`appsettings.json` dosyasında güvenlik nedeniyle örnek bilgiler bulunmaktadır:

```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderName": "Diş Kliniği",
  "SenderEmail": "your-email@example.com",
  "SenderPassword": "your-app-password"
}
```

Gerçek e-posta adresi ve uygulama şifresi herkese açık bir depoya yüklenmemelidir.

### 6. Entity Framework aracını yükleyin

Sistemde `dotnet-ef` kurulu değilse:

```bash
dotnet tool install --global dotnet-ef
```

### 7. Veritabanını oluşturun

```bash
dotnet ef database update
```

### 8. Projeyi çalıştırın

```bash
dotnet run
```

Terminalde gösterilen yerel adresi tarayıcıda açarak sisteme erişebilirsiniz.

## Çalışma Akışı

Hasta randevu alma işlemi genel olarak şu sırayla ilerler:

1. Hasta kişisel bilgilerini girer.
2. Sisteme girilen e-posta adresine doğrulama kodu gönderilir.
3. Hasta doğrulama kodunu girer.
4. Aktif doktorlar listelenir.
5. Hasta doktor ve tarih seçer.
6. Sistem dolu saatleri çıkararak uygun saatleri gösterir.
7. Randevu veritabanına kaydedilir.
8. Randevu bilgileri hastaya e-posta ile gönderilir.

## Güvenlik Notları

- Gerçek SMTP parolaları GitHub’a yüklenmemelidir.
- Üretim ortamında parolalar environment variable veya Secret Manager ile saklanmalıdır.
- Kullanıcı şifreleri düz metin yerine güvenli bir parola özetleme yöntemiyle saklanmalıdır.
- Hassas veritabanı loglaması üretim ortamında kapalı tutulmalıdır.
- Örnek kullanıcı bilgileri gerçek kişisel bilgilerle değiştirilmemelidir.

## Proje Durumu

Proje eğitim ve portföy amacıyla geliştirilmiştir.

Mevcut sürümde:

- Hasta kayıt ve doğrulama sistemi bulunmaktadır.
- Dinamik randevu saati yönetimi bulunmaktadır.
- Yönetici paneli bulunmaktadır.
- Doktor ve çalışan yönetimi bulunmaktadır.
- SQL Server veritabanı entegrasyonu bulunmaktadır.
- E-posta bildirim sistemi bulunmaktadır.
- Muayene sırası görüntüleme sistemi bulunmaktadır.

## Gelecek Geliştirmeler

- ASP.NET Core Identity entegrasyonu
- Parolaların güvenli biçimde saklanması
- Rol tabanlı yetkilendirme
- Randevu iptal etme ve güncelleme
- Doktor çalışma takvimi yönetimi
- Klinik ve bölüm desteği
- SMS bildirimi
- Responsive arayüz iyileştirmeleri
- Otomatik testler
- Docker desteği
- REST API desteği
- Merkezi hata ve log yönetimi

## Hazırlayan

**Ertuğrul EVLİYAOĞLU**
