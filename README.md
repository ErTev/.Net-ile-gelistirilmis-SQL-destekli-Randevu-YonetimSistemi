# Diş Kliniği Randevu Yönetim Sistemi

ASP.NET Core MVC ve SQL Server kullanılarak geliştirilmiş web tabanlı bir diş kliniği randevu yönetim sistemidir.

Uygulama; hastaların e-posta doğrulaması yaptıktan sonra uygun doktor ve saatleri görüntüleyerek randevu oluşturmasını, yaklaşan randevularını sorgulamasını ve klinik personelinin doktor, çalışan ve randevu süreçlerini yönetmesini sağlar.

## Projenin Amacı

Bu projenin amacı, diş kliniklerinde yürütülen randevu, hasta ve personel yönetimi süreçlerini dijital ortama taşımaktır.

Sistem temel olarak aşağıdaki kullanıcı gruplarına yönelik hazırlanmıştır:

- Hastalar
- Doktorlar
- Klinik çalışanları
- Yöneticiler

## Temel Özellikler

### Hasta İşlemleri

- Hasta bilgileriyle kayıt oluşturma
- E-posta adresine doğrulama kodu gönderme
- Doğrulama sonrasında randevu oluşturma
- Aktif doktorları görüntüleme
- Doktor ve tarih seçimi
- Seçilen tarihteki uygun saatleri görüntüleme
- T.C. kimlik numarasıyla yaklaşan randevuları sorgulama
- Oluşturulan randevu bilgilerini e-posta ile gönderme

### Randevu Yönetimi

- Doktora ve tarihe göre uygun saatleri hesaplama
- Dolu randevu saatlerini otomatik olarak listeden çıkarma
- Aynı doktora aynı tarih ve saatte ikinci randevu verilmesini engelleme
- Randevu tarihi için doğrulama kuralları
- Aktif doktorları randevu ekranında listeleme
- Yaklaşan randevuları görüntüleme
- Randevuları tarih ve saate göre sıralama

### Muayene Sırası

- Güncel ve yaklaşan randevuları görüntüleme
- Doktor bazlı hasta sırası oluşturma
- Randevuları tarih ve saate göre sıralama
- Her doktor için sıradaki hastaları listeleme

### Yönetici Paneli

- Yönetici girişi
- Session tabanlı oturum yönetimi
- Yönetici çıkış işlemi
- Doktorları listeleme
- Doktor ekleme, güncelleme ve silme
- Klinik çalışanlarını listeleme
- Çalışan ekleme, güncelleme ve silme
- Doktor ve çalışanların aktiflik durumunu yönetme

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
- Bootstrap
- SMTP e-posta servisi
- Session yönetimi

## Proje Mimarisi

Proje, ASP.NET Core MVC mimarisine göre geliştirilmiştir.

```text
.Net-ile-gelistirilmis-SQL-destekli-Randevu-YonetimSistemi/
│
├── README.md
├── .gitignore
│
└── RandevuYonetimSistemi/
    │
    ├── RandevuYonetimSistemi.sln
    │
    └── RandevuYonetimSistemi/
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

Projede Entity Framework Core kullanılarak SQL Server veritabanı bağlantısı sağlanmaktadır.

Temel veritabanı tabloları:

| Tablo | Açıklama |
|---|---|
| `Admins` | Yönetici hesaplarını tutar |
| `Doctors` | Doktor bilgilerini tutar |
| `Employes` | Klinik çalışanlarının bilgilerini tutar |
| `SickPeople` | Hasta bilgilerini tutar |
| `Appointments` | Oluşturulan randevuları tutar |

Hasta T.C. kimlik numarası alanında benzersiz indeks kullanılmaktadır. Böylece aynı T.C. kimlik numarasıyla tekrar eden hasta kayıtlarının oluşturulması engellenir.

## Gereksinimler

Projeyi çalıştırmak için aşağıdaki bileşenler gereklidir:

- .NET 9 SDK
- .NET 9 destekleyen Visual Studio veya Visual Studio Code
- SQL Server veya SQL Server LocalDB
- Entity Framework Core araçları
- Git

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

SQL Server LocalDB için örnek bağlantı:

```json
"ConnectionStrings": {
  "defaultconnection": "Server=(localdb)\\MSSQLLocalDB;Database=RandevuDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 5. E-posta ayarlarını yapılandırın

`appsettings.json` dosyasında güvenlik amacıyla örnek e-posta bilgileri bulunmaktadır:

```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderName": "Diş Kliniği",
  "SenderEmail": "your-email@example.com",
  "SenderPassword": "your-app-password"
}
```

Uygulamayı çalıştırmadan önce bu alanları kendi SMTP bilgilerinizle düzenleyin.

Gerçek e-posta adresleri, parolalar ve uygulama şifreleri herkese açık GitHub depolarına yüklenmemelidir.

### 6. Entity Framework aracını yükleyin

Bilgisayarınızda `dotnet-ef` kurulu değilse aşağıdaki komutu çalıştırın:

```bash
dotnet tool install --global dotnet-ef
```

Araç zaten kuruluysa güncellemek için:

```bash
dotnet tool update --global dotnet-ef
```

### 7. Veritabanını oluşturun

```bash
dotnet ef database update
```

### 8. Projeyi çalıştırın

```bash
dotnet run
```

Terminalde gösterilen yerel adresi tarayıcıda açarak uygulamaya erişebilirsiniz.

## Hasta Randevu Akışı

Hasta randevu alma işlemi aşağıdaki sırayla ilerler:

1. Hasta kişisel bilgilerini girer.
2. Girilen e-posta adresine dört haneli doğrulama kodu gönderilir.
3. Hasta doğrulama kodunu sisteme girer.
4. Sistem aktif doktorları listeler.
5. Hasta doktor ve tarih seçer.
6. Sistem dolu saatleri çıkararak uygun saatleri gösterir.
7. Seçilen randevu veritabanına kaydedilir.
8. Randevu bilgileri hastaya e-posta ile gönderilir.

## Güvenlik Notları

- Gerçek SMTP parolaları GitHub’a yüklenmemelidir.
- Üretim ortamında parolalar environment variable veya .NET Secret Manager ile saklanmalıdır.
- Yönetici ve kullanıcı şifreleri düz metin yerine güvenli parola özetleme yöntemleriyle saklanmalıdır.
- `EnableSensitiveDataLogging()` üretim ortamında kapalı tutulmalıdır.
- Örnek kullanıcı bilgileri gerçek kişisel veriler içermemelidir.
- Form işlemlerinde yetkilendirme ve CSRF koruması uygulanmalıdır.
- Üretim ortamında HTTPS kullanılmalıdır.

## Proje Durumu

Proje eğitim ve portföy amacıyla geliştirilmiştir.

Mevcut sürümde:

- Hasta kayıt sistemi bulunmaktadır.
- E-posta doğrulama sistemi bulunmaktadır.
- Dinamik randevu saati yönetimi bulunmaktadır.
- Yönetici paneli bulunmaktadır.
- Doktor ve çalışan yönetimi bulunmaktadır.
- SQL Server veritabanı entegrasyonu bulunmaktadır.
- E-posta bildirim sistemi bulunmaktadır.
- Muayene sırası görüntüleme sistemi bulunmaktadır.

## Planlanan Geliştirmeler

- ASP.NET Core Identity entegrasyonu
- Parolaların güvenli şekilde saklanması
- Rol tabanlı yetkilendirme
- Randevu iptal etme ve güncelleme
- Doktor çalışma takvimi yönetimi
- Birden fazla klinik ve bölüm desteği
- SMS bildirim sistemi
- Mobil uyumlu arayüz iyileştirmeleri
- Otomatik testler
- Docker desteği
- REST API desteği
- Merkezi hata ve log yönetimi

## Hazırlayan

**Ertuğrul EVLİYAOĞLU**
