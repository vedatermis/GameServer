using System;
using System.Windows.Forms;

namespace GameServer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            richTextBox1.AppendText("Server Starting...");
            richTextBox2.AppendText("Server Starting...");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            General.StartGameServer();
            button1.Enabled = false;
        }
    }
}
