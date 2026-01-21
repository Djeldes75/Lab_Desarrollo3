using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace App_EntregaCedula
{
    public partial class FrmVisor : Form
    {
        public FrmVisor()
        {
            InitializeComponent();
        }
        public void CargarReporte(string nombreRpt, DataTable datos)
        {
            try
            {
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreRpt);
                if (!File.Exists(ruta)) ruta = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\" + nombreRpt));

                reportViewer1.LocalReport.ReportPath = ruta;

                reportViewer1.LocalReport.DataSources.Clear();

                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", datos));

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message);
            }
        }

        private void FrmVisor_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}