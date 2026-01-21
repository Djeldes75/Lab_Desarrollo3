using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_EntregaCedula
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            //Ignorar
        }

        private void realizarEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEntrega hijo = new FrmEntrega();
            hijo.MdiParent = this;
            hijo.StartPosition = FormStartPosition.CenterScreen;
            hijo.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar el sistema?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = "📅 " + DateTime.Now.ToString("dd/MM/yyyy  ⏰ hh:mm:ss tt");
        }

        private void verHistorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHistorial historial = new FrmHistorial();

            historial.MdiParent = this;

            historial.StartPosition = FormStartPosition.CenterScreen;
            historial.Show();
        }
    }
}
