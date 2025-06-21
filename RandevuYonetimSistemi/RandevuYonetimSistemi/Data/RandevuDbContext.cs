using Microsoft.EntityFrameworkCore;

using RandevuYonetimSistemi.Models;

namespace RandevuYonetimSistemi.Data
{
    //Veritabanı bağlamımızı tanımlıyoruz DbContext sınıfından türetiyoruz.
    public class RandevuDbContext : DbContext
    {
        public RandevuDbContext(DbContextOptions<RandevuDbContext> opt) : base(opt)
        {

        }
        //Entitylerimiz DbSet olarak tanımlanır.
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<SickPerson> SickPeople { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        // DbContext'in yapılandırma ayarlarını yapıyoruz,model oluştuğu gibi
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


        //Model oluşturma işlemi yapılırken bazı veriler ve ilişkiler ekleniyor.
        [Obsolete]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SickPerson>()
            .HasIndex(s => s.Tc)
            .IsUnique()
            .HasName("Index_Tc_Unique");
            modelBuilder.Entity<Doctor>().HasData(
              new Doctor()
              {
                  Adi = "Hamza",
                  Soyadi = "Çakmakçi",
                  Tc = "12345678911",
                  Email = "hamza@gmail.com",
                  Sifre = "123456",
                  Id = 1,
                  Telefon = "05553654718",
                  Unvan = "dk.",

              });
            modelBuilder.Entity<Admin>().HasData(
                new Admin()
                {
                    Email = "admin@gmail.com",
                    Sifre = "admin123",
                    Id = 1,
                });
            modelBuilder.Entity<SickPerson>().HasData(new SickPerson()
            {
                Adi = "fırat",
                Soyadi = "Eren",
                Id = 1,
                Email = "fırat@gmail.com",
                Tc = "12456789123",
                Telefon = "05336649874",




            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment()
            {
                DoctorId = 1,
                Id = 1,
                HastaId = 1,
                RandevuGirisSaati = new DateTime(2024, 01, 01, 10, 00, 00),
                RandevuTarihi = new DateTime(2024, 01, 01, 10, 00, 00)


            });

        }
    }
}
