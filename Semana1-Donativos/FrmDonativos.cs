using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Semana1_Donativos.Models;
using Semana1_Donativos.Repositories;
using Semana1_Donativos.Utils;


namespace Semana1_Donativos
{
    public partial class Donativos : Form
    {
        private readonly DonativoRepository _repo = new DonativoRepository();
        private bool _isBinding = false;
        private int? _selectedId = null; //evita dupli
        private bool _suppressSelect = false;
        private bool _isClearing = false;


        public Donativos()
        {
            InitializeComponent();

            //ui
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Value = DateTime.Today;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AllowUserToAddRows = false;
            grid.MultiSelect = false; //Evitar multi-select

            //longitud
            txtOperativo.MaxLength = 70;
            txtDescripcion.MaxLength = 70;
            txtLote.MaxLength = 100;
            txtCantidad.MaxLength = 100;

            //filtros
            txtLote.KeyPress += txtSoloEnteros_KeyPress;
            txtCantidad.KeyPress += txtSoloEnteros_KeyPress;
            txtOperativo.KeyPress += txtAlfaNumSeguro_KeyPress;
            txtDescripcion.KeyPress += txtAlfaNumSeguro_KeyPress;

            //cmb estado
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new object[] { "Pendiente", "Aprobado", "Rechazado" });
            cmbEstado.SelectedIndex = 0;

            //botones
            btnEditar.Visible = false;
            btnEliminar.Visible = false;

            //eventos
            grid.SelectionChanged += grid_SelectionChanged;
            grid.DataBindingComplete += grid_DataBindingComplete;
        }

        private string GetPais()
        {
            if (rbtnJamaica.Checked) return "Jamaica";
            if (rbtnCuba.Checked) return "Cuba";
            if (rbtnHaiti.Checked) return "Haiti";
            return string.Empty;
        }

        //registrar
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOperativo.Text) ||
        string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
        string.IsNullOrWhiteSpace(txtLote.Text) ||
        string.IsNullOrWhiteSpace(txtCantidad.Text))
            { Notifier.Warning("Completa todos los campos."); return; }

            if (!int.TryParse(txtLote.Text.Trim(), out var lote))
            { Notifier.Warning("Lote debe ser entero."); return; }

            if (!int.TryParse(txtCantidad.Text.Trim(), out var cantidad) || cantidad <= 0)
            { Notifier.Warning("Cantidad debe ser entero > 0."); return; }

            var pais = GetPais();
            if (string.IsNullOrEmpty(pais))
            { Notifier.Warning("Selecciona un país."); return; }

            if (cmbEstado.SelectedItem == null)
            { Notifier.Warning("Selecciona un estado."); return; }

            var d = new Donativo
            {
                Operativo = txtOperativo.Text.Trim(),
                Pais = pais,
                Lote = lote,
                Descripcion = txtDescripcion.Text.Trim(),
                Cantidad = cantidad,
                Fecha_Ingreso = dtpFecha.Value.Date,
                Estado = cmbEstado.SelectedItem.ToString()
            };

            if (_repo.Exists(d))
            {
                Notifier.Info("El donativo ya existe (Operativo + País + Lote + Fecha).");
                return;
            }

            try
            {
                _repo.Insert(d);
                CargarDatos();
                Limpiar();
                Notifier.Success("Donativo registrado correctamente.");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) when (ex.Number == 1062)
            {
                Notifier.Info("Duplicado: ya existe un registro con Operativo+País+Lote+Fecha.");
            }
            catch (Exception ex)
            {
                Notifier.Error("Error al registrar: " + ex.Message);
            }
        }

        //cargar
        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {

            try
            {
                _isBinding = true;
                _suppressSelect = true; // ← bloquea selección automática

                grid.DataSource = _repo.GetAllAsDataTable();

                // Layout/format
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                if (grid.Columns.Contains("Fecha_Ingreso"))
                    grid.Columns["Fecha_Ingreso"].DefaultCellStyle.Format = "yyyy-MM-dd";

                if (grid.Columns.Contains("ID")) grid.Columns["ID"].Width = 30;
                if (grid.Columns.Contains("Operativo")) grid.Columns["Operativo"].Width = 100;
                if (grid.Columns.Contains("Pais")) grid.Columns["Pais"].Width = 70;
                if (grid.Columns.Contains("Lote")) grid.Columns["Lote"].Width = 35;
                if (grid.Columns.Contains("Descripcion")) grid.Columns["Descripcion"].Width = 190;
                if (grid.Columns.Contains("Cantidad")) grid.Columns["Cantidad"].Width = 70;
                if (grid.Columns.Contains("Fecha_Ingreso")) grid.Columns["Fecha_Ingreso"].Width = 90;
                if (grid.Columns.Contains("Estado")) grid.Columns["Estado"].Width = 78;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
            finally
            {
                _isBinding = false;

                this.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        grid.ClearSelection();
                        grid.CurrentCell = null;
                        foreach (DataGridViewRow r in grid.Rows) r.Selected = false;
                        _selectedId = null;
                        btnEditar.Visible = false;
                        btnEliminar.Visible = false;
                    }
                    finally
                    {
                        _suppressSelect = false;
                    }
                }));
            }
        }

        private void grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //
        }

        private void Limpiar()
        {
            _isClearing = true;
            try
            {
                // Inputs
                txtOperativo.Clear();
                txtDescripcion.Clear();
                txtLote.Clear();
                txtCantidad.Clear();

                // Estado / País / Fecha
                cmbEstado.SelectedIndex = 0;
                rbtnJamaica.Checked = rbtnCuba.Checked = rbtnHaiti.Checked = false;
                dtpFecha.Value = DateTime.Today;

                // Grid: sin selección inmediata
                grid.ClearSelection();
                grid.CurrentCell = null;

                // Botones contextuales y selección lógica
                _selectedId = null;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;

                // Focus inicial
                txtOperativo.Focus();
            }
            finally
            {
                this.BeginInvoke(new Action(() =>
                {
                    grid.ClearSelection();
                    grid.CurrentCell = null;
                    _isClearing = false;
                }));
            }
        }

        private void Donativos_Load(object sender, EventArgs e)
        {
            CargarDatos();
            this.BeginInvoke(new Action(() =>
            {
                grid.ClearSelection();
                grid.CurrentCell = null;
            }));
        }

        //editar
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_selectedId == null) { Notifier.Info("Selecciona un registro en la tabla."); return; }

            if (string.IsNullOrWhiteSpace(txtOperativo.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtLote.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text))
            { Notifier.Warning("Completa todos los campos."); return; }

            if (!int.TryParse(txtLote.Text.Trim(), out var lote)) { Notifier.Warning("Lote debe ser entero."); return; }
            if (!int.TryParse(txtCantidad.Text.Trim(), out var cantidad) || cantidad <= 0) { Notifier.Warning("Cantidad debe ser entero > 0."); return; }

            var pais = GetPais();
            if (string.IsNullOrEmpty(pais)) { Notifier.Warning("Selecciona un país."); return; }
            if (cmbEstado.SelectedItem == null) { Notifier.Warning("Selecciona un estado."); return; }

            var d = new Donativo
            {
                ID = _selectedId.Value,
                Operativo = txtOperativo.Text.Trim(),
                Pais = pais,
                Lote = lote,
                Descripcion = txtDescripcion.Text.Trim(),
                Cantidad = cantidad,
                Fecha_Ingreso = dtpFecha.Value.Date,
                Estado = cmbEstado.SelectedItem.ToString()
            };

            if (_repo.ExistsForUpdate(_selectedId.Value, d))
            {
                Notifier.Info("Duplicado: ya existe un registro con Operativo+País+Lote+Fecha.");
                return;
            }

            try
            {
                var n = _repo.Update(d);
                if (n > 0) { CargarDatos(); Notifier.Success("Registro actualizado."); }
                else Notifier.Info("No se actualizó ningún registro.");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) when (ex.Number == 1062)
            {
                Notifier.Info("Duplicado: ya existe un registro con Operativo+País+Lote+Fecha.");
            }
            catch (Exception ex)
            {
                Notifier.Error("Error al actualizar: " + ex.Message);
            }
        }

        //grid selecc
        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (_isBinding || _suppressSelect || _isClearing) return;

            if (grid.CurrentCell == null || grid.CurrentCell.RowIndex < 0)
            {
                _selectedId = null;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
                return;
            }

            var row = grid.Rows[grid.CurrentCell.RowIndex];
            if (row?.Cells == null || row.Cells["ID"].Value == null)
            {
                _selectedId = null;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
                return;
            }

            if (!int.TryParse(row.Cells["ID"].Value.ToString(), out var idSel))
            {
                _selectedId = null;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
                return;
            }

            _selectedId = idSel;
            btnEditar.Visible = true;
            btnEliminar.Visible = true;

            // Precarga de campos
            txtOperativo.Text = row.Cells["Operativo"].Value?.ToString();
            txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString();
            txtLote.Text = row.Cells["Lote"].Value?.ToString();
            txtCantidad.Text = row.Cells["Cantidad"].Value?.ToString();

            var pais = row.Cells["Pais"].Value?.ToString();
            rbtnJamaica.Checked = string.Equals(pais, "Jamaica", StringComparison.OrdinalIgnoreCase);
            rbtnHaiti.Checked = string.Equals(pais, "Haiti", StringComparison.OrdinalIgnoreCase);
            rbtnCuba.Checked = string.Equals(pais, "Cuba", StringComparison.OrdinalIgnoreCase);

            if (DateTime.TryParse(row.Cells["Fecha_Ingreso"].Value?.ToString(), out var f))
                dtpFecha.Value = f;

            var estado = row.Cells["Estado"].Value?.ToString();
            if (!string.IsNullOrEmpty(estado))
            {
                
                var match = cmbEstado.Items.Cast<object>()
                              .Select(x => x.ToString())
                              .FirstOrDefault(x => x.Equals(estado, StringComparison.OrdinalIgnoreCase));
                cmbEstado.SelectedItem = match ?? cmbEstado.Items[0];
            }
            else
            {
                cmbEstado.SelectedIndex = 0;
            }
        }

        //keypress
        private void txtSoloEnteros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtAlfaNumSeguro_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (char.IsControl(c)) return;
            if (char.IsLetterOrDigit(c) || c == ' ' || c == '.' || c == '-' || c == '_' || c == ',') return;
            e.Handled = true;
        }

        // Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //
            if (_selectedId == null)
            {
                MessageBox.Show("Selecciona un registro para eliminar.", "ℹ Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var filaTxt = $"ID: {_selectedId}\nOperativo: {txtOperativo.Text}";
            var ask = MessageBox.Show(
                $"¿Eliminar el registro?\n\n{filaTxt}",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ask != DialogResult.Yes) return;

            try
            {
                var n = _repo.Delete(_selectedId.Value);
                if (n > 0)
                {
                    CargarDatos();
                    Limpiar();
                    MessageBox.Show("Registro eliminado.", "✓ Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se eliminó ningún registro.", "ℹ Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "✖ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "✖ Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //nada, fue por error
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Notifier.Success("Formulario limpio exitosamente.");

        }
    }
}
