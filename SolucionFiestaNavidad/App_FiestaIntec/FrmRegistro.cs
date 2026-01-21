using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace App_FiestaIntec
{
    public partial class FrmRegistro : Form
    {
        // 1. CAMBIADO: Usamos "cnString" como está en tu App.config
        static string connStr = ConfigurationManager.ConnectionStrings["cnString"].ConnectionString;

        public FrmRegistro()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Por favor llena los datos."); return;
            }

            try
            {
                int tipo = 1;
                if (cmbTipo.Text != null && cmbTipo.Text.Contains("2")) tipo = 2;

                int idGenerado = 0;

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    // 2. CAMBIADO: Tabla "Invitados"
                    string query = @"INSERT INTO Invitados (TipoDoc, Documento, Nombres, Apellidos, Sexo, FechaNacimiento, Nota) 
                                     VALUES (@t, @d, @n, @a, @s, @f, @nt);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@t", tipo);
                        cmd.Parameters.AddWithValue("@d", long.Parse(txtDocumento.Text));
                        cmd.Parameters.AddWithValue("@n", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@a", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@s", cmbSexo.Text);
                        cmd.Parameters.AddWithValue("@f", dtpNacimiento.Value);
                        cmd.Parameters.AddWithValue("@nt", txtNota.Text);

                        idGenerado = (int)cmd.ExecuteScalar();
                    }
                }

                MessageBox.Show("¡Guardado! Generando Ticket...");

                // Muestra el Ticket
                AbrirReporte("rptTicket.rdlc", $"SELECT * FROM Invitados WHERE Id={idGenerado}");

                // 3. NUEVO: ¡Limpiar todo!
                LimpiarCampos();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // Método para borrar el formulario
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDocumento.Clear();
            txtNota.Clear();
            cmbTipo.SelectedIndex = -1;
            cmbSexo.SelectedIndex = -1;
            dtpNacimiento.Value = DateTime.Now;
            txtNombre.Focus(); // Pone el cursor en Nombre para seguir rápido
        }

        public static void VerReporteGeneral()
        {
            AbrirReporte("rptListado.rdlc", "SELECT * FROM Invitados");
        }

        private static void AbrirReporte(string nombreRpt, string sql)
        {
            Form visor = new Form();
            visor.Text = "Vista Previa";
            visor.WindowState = FormWindowState.Maximized;

            ReportViewer rv = new ReportViewer();
            rv.Dock = DockStyle.Fill;

            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreRpt);
            if (!File.Exists(ruta)) ruta = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\" + nombreRpt));

            rv.LocalReport.ReportPath = ruta;

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                new SqlDataAdapter(sql, con).Fill(dt);
            }

            rv.LocalReport.DataSources.Clear();
            rv.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            rv.RefreshReport();

            visor.Controls.Add(rv);
            visor.ShowDialog();
        }
    }
}
