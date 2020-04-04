using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GameServer
{
    public static class Sabitler
    {
        public static MainForm server = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
        public static Dictionary<int, Client> bagliClient = new Dictionary<int, Client>();

        public static void Oyuncu_Baglandi(string connectionId)
        {
            BagliKullaniciSayisi++;
            Yazi.Log_yaz("Kullanıcı Bağlandı : " + connectionId);
            Sabitler.server.listBox1.Items.Add(connectionId);
        }

        public static void Oyuncu_Cikti(string connectionId)
        {
            BagliKullaniciSayisi--;
            Yazi.Log_yaz("Kullanıcı Ayrıldı : " + connectionId);
            Sabitler.server.listBox1.Items.Remove(connectionId);
        }

        private static int _bagliKullaniciSayisi = 0;

        public static int BagliKullaniciSayisi
        {
            get => _bagliKullaniciSayisi;
            set
            {
                _bagliKullaniciSayisi = value;
                server.label1.Text = "Bağlı Kullanıcı Sayısı : " + BagliKullaniciSayisi;
            }
        }
    }
}
