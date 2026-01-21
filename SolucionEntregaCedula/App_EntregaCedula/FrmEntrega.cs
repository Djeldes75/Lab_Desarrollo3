using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace App_EntregaCedula
{
    public partial class FrmEntrega : Form
    {
        static string cnString = ConfigurationManager.ConnectionStrings["cnString"].ConnectionString;

        public FrmEntrega()
        {
            InitializeComponent();
        }

        private void FrmEntrega_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEntregar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCedula.Text) || string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                MessageBox.Show("❌ Por favor digite la cédula y el nombre.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idGenerado = 0;

                using (SqlConnection con = new SqlConnection(cnString))
                {
                    con.Open();

                    string query = @"INSERT INTO Entregas (Cedula, Nombres, Apellidos, FechaEntrega, Observacion) 
                                     VALUES (@c, @n, @a, @f, @o);
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (!long.TryParse(txtCedula.Text, out long cedulaNumerica))
                        {
                            MessageBox.Show("⚠️ La cédula debe ser numérica sin guiones.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        cmd.Parameters.AddWithValue("@c", cedulaNumerica);
                        cmd.Parameters.AddWithValue("@n", txtNombres.Text);
                        cmd.Parameters.AddWithValue("@a", txtApellidos.Text);
                        cmd.Parameters.AddWithValue("@f", DateTime.Now);
                        cmd.Parameters.AddWithValue("@o", txtObservacion.Text);

                        idGenerado = (int)cmd.ExecuteScalar();
                    }

                    string sqlReporte = $"SELECT * FROM Entregas WHERE Id={idGenerado}";
                    DataTable dt = new DataTable();
                    new SqlDataAdapter(sqlReporte, con).Fill(dt);

                    FrmVisor visor = new FrmVisor();

                    visor.Text = "Comprobante de Entrega - " + txtNombres.Text;
                    visor.StartPosition = FormStartPosition.CenterScreen;

                    visor.CargarReporte("rptComprobante.rdlc", dt);

                    visor.ShowDialog();
                }

                MessageBox.Show("✅ Entrega realizada con éxito.");
                Limpiar();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("⛔ Esta cédula YA FUE ENTREGADA anteriormente.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Error SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Limpiar()
        {
            txtCedula.Clear(); txtNombres.Clear(); txtApellidos.Clear(); txtObservacion.Clear();
            txtCedula.Focus();
        }
    }
}