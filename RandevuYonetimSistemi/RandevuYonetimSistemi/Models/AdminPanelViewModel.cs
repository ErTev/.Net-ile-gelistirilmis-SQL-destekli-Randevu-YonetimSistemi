namespace RandevuYonetimSistemi.Models
{
    //Admin sayfasında erişeceğimiz kullanıcıları tutan view modelimiz
    public class AdminPanelViewModel
    {
        public List<Admin> Adminler { get; set; }
        public List<Employe> Calisanlar { get; set; }
        public List<Doctor> Doktorlar { get; set; }
    }
}
