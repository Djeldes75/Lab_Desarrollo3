namespace Semana1_Donativos
{
    partial class Donativos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOperativo = new System.Windows.Forms.TextBox();
            this.rbtnJamaica = new System.Windows.Forms.RadioButton();
            this.rbtnCuba = new System.Windows.Forms.RadioButton();
            this.rbtnHaiti = new System.Windows.Forms.RadioButton();
            this.lblPais = new System.Windows.Forms.Label();
            this.lblOperativo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFecha = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.lblLote = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.btnEditar = new System.Windows.Forms.Button();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // txtOperativo
            // 
            this.txtOperativo.Location = new System.Drawing.Point(36, 137);
            this.txtOperativo.Name = "txtOperativo";
            this.txtOperativo.Size = new System.Drawing.Size(223, 20);
            this.txtOperativo.TabIndex = 0;
            this.txtOperativo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlfaNumSeguro_KeyPress);
            // 
            // rbtnJamaica
            // 
            this.rbtnJamaica.AutoSize = true;
            this.rbtnJamaica.Location = new System.Drawing.Point(36, 217);
            this.rbtnJamaica.Name = "rbtnJamaica";
            this.rbtnJamaica.Size = new System.Drawing.Size(64, 17);
            this.rbtnJamaica.TabIndex = 1;
            this.rbtnJamaica.TabStop = true;
            this.rbtnJamaica.Text = "Jamaica";
            this.rbtnJamaica.UseVisualStyleBackColor = true;
            // 
            // rbtnCuba
            // 
            this.rbtnCuba.AutoSize = true;
            this.rbtnCuba.Location = new System.Drawing.Point(36, 251);
            this.rbtnCuba.Name = "rbtnCuba";
            this.rbtnCuba.Size = new System.Drawing.Size(50, 17);
            this.rbtnCuba.TabIndex = 2;
            this.rbtnCuba.TabStop = true;
            this.rbtnCuba.Text = "Cuba";
            this.rbtnCuba.UseVisualStyleBackColor = true;
            // 
            // rbtnHaiti
            // 
            this.rbtnHaiti.AutoSize = true;
            this.rbtnHaiti.Location = new System.Drawing.Point(36, 285);
            this.rbtnHaiti.Name = "rbtnHaiti";
            this.rbtnHaiti.Size = new System.Drawing.Size(46, 17);
            this.rbtnHaiti.TabIndex = 3;
            this.rbtnHaiti.TabStop = true;
            this.rbtnHaiti.Text = "Haiti";
            this.rbtnHaiti.UseVisualStyleBackColor = true;
            // 
            // lblPais
            // 
            this.lblPais.AutoSize = true;
            this.lblPais.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPais.Location = new System.Drawing.Point(33, 182);
            this.lblPais.Name = "lblPais";
            this.lblPais.Size = new System.Drawing.Size(41, 18);
            this.lblPais.TabIndex = 4;
            this.lblPais.Text = "Pais";
            // 
            // lblOperativo
            // 
            this.lblOperativo.AutoSize = true;
            this.lblOperativo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperativo.Location = new System.Drawing.Point(33, 99);
            this.lblOperativo.Name = "lblOperativo";
            this.lblOperativo.Size = new System.Drawing.Size(81, 18);
            this.lblOperativo.TabIndex = 5;
            this.lblOperativo.Text = "Operativo";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(245, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(301, 46);
            this.lblTitulo.TabIndex = 6;
            this.lblTitulo.Text = "Registro de Donativos";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Location = new System.Drawing.Point(320, 99);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(98, 18);
            this.lblDescripcion.TabIndex = 7;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(323, 137);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(223, 20);
            this.txtDescripcion.TabIndex = 8;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlfaNumSeguro_KeyPress);
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(323, 214);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 9;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(320, 182);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(169, 18);
            this.lblFecha.TabIndex = 10;
            this.lblFecha.Text = "Seleccione una fecha";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(611, 264);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(131, 42);
            this.btnRegistrar.TabIndex = 11;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // txtLote
            // 
            this.txtLote.Location = new System.Drawing.Point(611, 137);
            this.txtLote.Name = "txtLote";
            this.txtLote.Size = new System.Drawing.Size(131, 20);
            this.txtLote.TabIndex = 12;
            this.txtLote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoloEnteros_KeyPress);
            // 
            // lblLote
            // 
            this.lblLote.AutoSize = true;
            this.lblLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLote.Location = new System.Drawing.Point(608, 99);
            this.lblLote.Name = "lblLote";
            this.lblLote.Size = new System.Drawing.Size(89, 18);
            this.lblLote.TabIndex = 13;
            this.lblLote.Text = "Lote/Batch";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(611, 217);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(131, 20);
            this.txtCantidad.TabIndex = 14;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoloEnteros_KeyPress);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(608, 182);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(74, 18);
            this.lblCantidad.TabIndex = 15;
            this.lblCantidad.Text = "Cantidad";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(320, 255);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(61, 18);
            this.lblEstado.TabIndex = 17;
            this.lblEstado.Text = "Estado";
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(642, 586);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(100, 34);
            this.btnCargar.TabIndex = 18;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(36, 321);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(706, 259);
            this.grid.TabIndex = 19;
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(536, 586);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 34);
            this.btnEditar.TabIndex = 20;
            this.btnEditar.Text = "Actualizar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(323, 285);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(200, 21);
            this.cmbEstado.TabIndex = 21;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(430, 586);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 34);
            this.btnEliminar.TabIndex = 22;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(36, 586);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 34);
            this.btnLimpiar.TabIndex = 23;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // Donativos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 628);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.lblLote);
            this.Controls.Add(this.txtLote);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblOperativo);
            this.Controls.Add(this.lblPais);
            this.Controls.Add(this.rbtnHaiti);
            this.Controls.Add(this.rbtnCuba);
            this.Controls.Add(this.rbtnJamaica);
            this.Controls.Add(this.txtOperativo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Donativos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Donativos";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOperativo;
        private System.Windows.Forms.RadioButton rbtnJamaica;
        private System.Windows.Forms.RadioButton rbtnCuba;
        private System.Windows.Forms.RadioButton rbtnHaiti;
        private System.Windows.Forms.Label lblPais;
        private System.Windows.Forms.Label lblOperativo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.Label lblLote;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
    }
}

