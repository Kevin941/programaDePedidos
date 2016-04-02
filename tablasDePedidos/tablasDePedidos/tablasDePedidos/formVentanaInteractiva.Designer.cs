namespace tablasDePedidos
{
    partial class formVentanaInteractiva
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
            this.dataGridPedidoActual = new System.Windows.Forms.DataGridView();
            this.dataGridClavesEncontradas = new System.Windows.Forms.DataGridView();
            this.botonAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPedidoActual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClavesEncontradas)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridPedidoActual
            // 
            this.dataGridPedidoActual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPedidoActual.Location = new System.Drawing.Point(12, 36);
            this.dataGridPedidoActual.Name = "dataGridPedidoActual";
            this.dataGridPedidoActual.Size = new System.Drawing.Size(813, 83);
            this.dataGridPedidoActual.TabIndex = 0;
            // 
            // dataGridClavesEncontradas
            // 
            this.dataGridClavesEncontradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridClavesEncontradas.Location = new System.Drawing.Point(12, 160);
            this.dataGridClavesEncontradas.Name = "dataGridClavesEncontradas";
            this.dataGridClavesEncontradas.Size = new System.Drawing.Size(813, 150);
            this.dataGridClavesEncontradas.TabIndex = 1;
            this.dataGridClavesEncontradas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridClavesEncontradas_CellClick);
            // 
            // botonAceptar
            // 
            this.botonAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.botonAceptar.Location = new System.Drawing.Point(600, 355);
            this.botonAceptar.Name = "botonAceptar";
            this.botonAceptar.Size = new System.Drawing.Size(225, 44);
            this.botonAceptar.TabIndex = 2;
            this.botonAceptar.Text = "Aceptar";
            this.botonAceptar.UseVisualStyleBackColor = true;
            this.botonAceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label1.Location = new System.Drawing.Point(8, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Archivo de especificaciones. ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Archivo de pedidos:";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(29, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(759, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Por favor selecciona el registro correcto de la tabla de especificaciones y pulsa" +
    " el botón de aceptar. ";
            // 
            // formVentanaInteractiva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 414);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.botonAceptar);
            this.Controls.Add(this.dataGridClavesEncontradas);
            this.Controls.Add(this.dataGridPedidoActual);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "formVentanaInteractiva";
            this.Text = "Ventana Interactiva";
            this.Load += new System.EventHandler(this.formVentanaInteractiva_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPedidoActual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClavesEncontradas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridPedidoActual;
        private System.Windows.Forms.DataGridView dataGridClavesEncontradas;
        private System.Windows.Forms.Button botonAceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}