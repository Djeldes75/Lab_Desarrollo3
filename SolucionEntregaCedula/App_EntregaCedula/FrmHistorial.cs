using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace App_EntregaCedula
{
    public partial class FrmHistorial : Form
    {
        static string cnString = ConfigurationManager.ConnectionStrings["cnString"].ConnectionString;

        public FrmHistorial()
        {
            InitializeComponent();
        }

        private void FrmHistorial_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                string query = "SELECT * FROM Entregas";

                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(cnString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dt);
                }

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rptHistorial.rdlc");
                if (!File.Exists(ruta)) ruta = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\" + "rptHistorial.rdlc"));

                reportViewer1.LocalReport.ReportPath = ruta;

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial: " + ex.Message);
            }
        }
        private void reportViewer1_Load(object sender, EventArgs e)
        {
            //
        }

        private void reportViewer1_ReportRefresh(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CargarDatos();
            MessageBox.Show("Lista actualizada.");
        }
    }
}