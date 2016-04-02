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
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPedidoActual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClavesEncontradas)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridPedidoActual
            // 
            this.dataGridPedidoActual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPedidoActual.Location = new System.Drawing.Point(12, 38);
            this.dataGridPedidoActual.Name = "dataGridPedidoActual";
            this.dataGridPedidoActual.Size = new System.Drawing.Size(813, 150);
            this.dataGridPedidoActual.TabIndex = 0;
            // 
            // dataGridClavesEncontradas
            // 
            this.dataGridClavesEncontradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridClavesEncontradas.Location = new System.Drawing.Point(12, 194);
            this.dataGridClavesEncontradas.Name = "dataGridClavesEncontradas";
            this.dataGridClavesEncontradas.Size = new System.Drawing.Size(813, 150);
            this.dataGridClavesEncontradas.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(600, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 101);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // formVentanaInteractiva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 463);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridClavesEncontradas);
            this.Controls.Add(this.dataGridPedidoActual);
            this.Name = "formVentanaInteractiva";
            this.Text = "formVentanaInteractiva";
            this.Load += new System.EventHandler(this.formVentanaInteractiva_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPedidoActual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClavesEncontradas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridPedidoActual;
        private System.Windows.Forms.DataGridView dataGridClavesEncontradas;
        private System.Windows.Forms.Button button1;
    }
}