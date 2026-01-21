using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Types;

namespace App_InscripcionAI
{
    public partial class Form1 : Form
    {
        static string cnString = ConfigurationManager.ConnectionStrings["cnString"].ConnectionString;

        string rutaFoto = "";
        byte[] bytesFoto = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
        }

        private void btnCargarFoto_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Imágenes|*.jpg;*.png;*.jpeg";

            if (open.ShowDialog() == DialogResult.OK)
            {
                rutaFoto = open.FileName;
                pbFoto.ImageLocation = rutaFoto;

                using (FileStream fs = new FileStream(rutaFoto, FileMode.Open, FileAccess.Read))
                {
                    bytesFoto = new byte[fs.Length];
                    fs.Read(bytesFoto, 0, (int)fs.Length);
                }
            }
        }

        private void btnInscribir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rutaFoto)) { MessageBox.Show("¡Falta la foto!"); return; }
            if (string.IsNullOrEmpty(txtDocumento.Text)) { MessageBox.Show("¡Falta el documento!"); return; }
            if (string.IsNullOrEmpty(cbCarrera.Text)) { MessageBox.Show("¡Selecciona una carrera!"); return; }

            try
            {
                AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(
                    "AKIAW5PNQJW25XYCGSVR",
                    "QNUorqJJqB/ECj8jr3zyNN1EESH0ml8bsoar9zT1",
                    RegionEndpoint.USEast1);

                Amazon.Rekognition.Model.Image imageAWS = new Amazon.Rekognition.Model.Image();

                MemoryStream ms = new MemoryStream(bytesFoto);
                imageAWS.Bytes = ms;

                var modRequest = new DetectModerationLabelsRequest() { Image = imageAWS, MinConfidence = 60 };
                var modResponse = rekognitionClient.DetectModerationLabels(modRequest);

                if (modResponse.ModerationLabels.Count > 0)
                {
                    string motivos = "";
                    foreach (var m in modResponse.ModerationLabels) motivos += m.Name + ", ";
                    MessageBox.Show($"⛔ ALERTA: Foto rechazada por contenido inapropiado.\nDetectado: {motivos}", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                var celebRequest = new RecognizeCelebritiesRequest() { Image = imageAWS };
                var celebResponse = rekognitionClient.RecognizeCelebrities(celebRequest);

                decimal monto = decimal.Parse(txtMonto.Text);
                string estado = "Inscrito";
                string mensajeExtra = "";

                if (celebResponse.CelebrityFaces.Count > 0)
                {
                    string famoso = celebResponse.CelebrityFaces[0].Name;
                    monto = monto * 0.90m; // 10% de descuento
                    estado = "Inscrito VIP (Celebridad)";
                    mensajeExtra = $"\n🌟 ¡Celebridad detectada ({famoso})! Descuento aplicado.";
                }

                int idGenerado = 0;
                using (SqlConnection con = new SqlConnection(cnString))
                {
                    con.Open();
                    string query = @"INSERT INTO tblEstudiantes 
                        (tipodocumento, documento, nombres, apellidos, fechanacimiento, fechaingreso, carrera, sexo, MontoPagoInscripcion, Estado, Foto)
                        VALUES (@td, @doc, @nom, @ape, @fn, @fi, @car, @sex, @monto, @est, @foto);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@td", cbTipoDoc.Text);
                        cmd.Parameters.AddWithValue("@doc", txtDocumento.Text);
                        cmd.Parameters.AddWithValue("@nom", txtNombres.Text);
                        cmd.Parameters.AddWithValue("@ape", txtApellidos.Text);
                        cmd.Parameters.AddWithValue("@fn", dtpNacimiento.Value);
                        cmd.Parameters.AddWithValue("@fi", DateTime.Now);
                        cmd.Parameters.AddWithValue("@car", cbCarrera.Text);
                        cmd.Parameters.AddWithValue("@sex", cbSexo.Text);
                        cmd.Parameters.AddWithValue("@monto", monto);
                        cmd.Parameters.AddWithValue("@est", estado);
                        cmd.Parameters.AddWithValue("@foto", bytesFoto);

                        idGenerado = (int)cmd.ExecuteScalar();
                    }
                }

                MessageBox.Show($"✅ Estudiante inscrito correctamente.{mensajeExtra}");

                AbrirComprobante(idGenerado, $"{txtNombres.Text} {txtApellidos.Text}");
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        //fin

        //entrada
        void Limpiar()
        {
            txtDocumento.Clear(); txtNombres.Clear(); txtApellidos.Clear();
            cbCarrera.SelectedIndex = -1;
            pbFoto.Image = null; rutaFoto = "";
        }
        //fin

        //entrada

        // Método para abrir el reporte
        void AbrirComprobante(int id, string nombre)
        {
            string sql = $"SELECT * FROM tblEstudiantes WHERE id={id}";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cnString))
            {
                con.Open();
                new SqlDataAdapter(sql, con).Fill(dt);
            }

            FrmVisor visor = new FrmVisor();
            visor.Text = "Comprobante: " + nombre;
            visor.CargarReporte("rptComprobante.rdlc", dt);
            visor.ShowDialog();
        }
        //fin
    }
}
