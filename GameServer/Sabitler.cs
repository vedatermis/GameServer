using System.Linq;
using System.Windows.Forms;

namespace GameServer
{
    public static class Sabitler
    {
        public static MainForm server = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
    }
}
