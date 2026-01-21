using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_FiestaIntec
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //ignorar lol
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //toolStripStatusLabel2
            toolStripStatusLabel2.Text = "📅 " + DateTime.Now.ToString("dd/MM/yyyy  ⏰ hh:mm:ss tt");
        }

        private void nuevoInvitadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRegistro hijo = new FrmRegistro();
            hijo.MdiParent = this;
            hijo.Show();
        }

        private void listadoGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRegistro.VerReporteGeneral();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿De verdad quieres salir del sistema?",
                "Confirmar Salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
