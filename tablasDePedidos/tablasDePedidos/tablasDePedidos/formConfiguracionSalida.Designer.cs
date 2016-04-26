namespace tablasDePedidos
{
    partial class formConfiguracionSalida
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formConfiguracionSalida));
            this.dataGridColumnasConfiguracion = new System.Windows.Forms.DataGridView();
            this.columna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonGuardarConfiguracion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridColumnasConfiguracion)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridColumnasConfiguracion
            // 
            this.dataGridColumnasConfiguracion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridColumnasConfiguracion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columna,
            this.estado});
            this.dataGridColumnasConfiguracion.Location = new System.Drawing.Point(12, 12);
            this.dataGridColumnasConfiguracion.Name = "dataGridColumnasConfiguracion";
            this.dataGridColumnasConfiguracion.Size = new System.Drawing.Size(498, 258);
            this.dataGridColumnasConfiguracion.TabIndex = 0;
            // 
            // columna
            // 
            this.columna.HeaderText = "Columna del archivo";
            this.columna.Name = "columna";
            // 
            // estado
            // 
            this.estado.HeaderText = "Activar/Desactivar";
            this.estado.Name = "estado";
            this.estado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // buttonGuardarConfiguracion
            // 
            this.buttonGuardarConfiguracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.buttonGuardarConfiguracion.Location = new System.Drawing.Point(12, 276);
            this.buttonGuardarConfiguracion.Name = "buttonGuardarConfiguracion";
            this.buttonGuardarConfiguracion.Size = new System.Drawing.Size(498, 56);
            this.buttonGuardarConfiguracion.TabIndex = 1;
            this.buttonGuardarConfiguracion.Text = "Guardar Configuración";
            this.buttonGuardarConfiguracion.UseVisualStyleBackColor = true;
            this.buttonGuardarConfiguracion.Click += new System.EventHandler(this.buttonGuardarConfiguracion_Click);
            // 
            // formConfiguracionSalida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(522, 344);
            this.Controls.Add(this.buttonGuardarConfiguracion);
            this.Controls.Add(this.dataGridColumnasConfiguracion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formConfiguracionSalida";
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.formConfiguracionSalida_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridColumnasConfiguracion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridColumnasConfiguracion;
        private System.Windows.Forms.DataGridViewTextBoxColumn columna;
        private System.Windows.Forms.DataGridViewCheckBoxColumn estado;
        private System.Windows.Forms.Button buttonGuardarConfiguracion;
    }
}