namespace tablasDePedidos
{
    partial class formMenuAdministrador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMenuAdministrador));
            this.buttonConfigurarSalida = new System.Windows.Forms.Button();
            this.buttonCambiarRutas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConfigurarSalida
            // 
            this.buttonConfigurarSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.buttonConfigurarSalida.Location = new System.Drawing.Point(12, 62);
            this.buttonConfigurarSalida.Name = "buttonConfigurarSalida";
            this.buttonConfigurarSalida.Size = new System.Drawing.Size(314, 130);
            this.buttonConfigurarSalida.TabIndex = 0;
            this.buttonConfigurarSalida.Text = "Configuración del archivo de salida";
            this.buttonConfigurarSalida.UseVisualStyleBackColor = true;
            this.buttonConfigurarSalida.Click += new System.EventHandler(this.buttonConfigurarSalida_Click);
            // 
            // buttonCambiarRutas
            // 
            this.buttonCambiarRutas.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.buttonCambiarRutas.Location = new System.Drawing.Point(333, 62);
            this.buttonCambiarRutas.Name = "buttonCambiarRutas";
            this.buttonCambiarRutas.Size = new System.Drawing.Size(314, 130);
            this.buttonCambiarRutas.TabIndex = 1;
            this.buttonCambiarRutas.Text = "Cambiar rutas";
            this.buttonCambiarRutas.UseVisualStyleBackColor = true;
            this.buttonCambiarRutas.Click += new System.EventHandler(this.buttonCambiarRutas_Click);
            // 
            // formMenuAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 277);
            this.Controls.Add(this.buttonCambiarRutas);
            this.Controls.Add(this.buttonConfigurarSalida);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formMenuAdministrador";
            this.Text = "Panel de administración";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonConfigurarSalida;
        private System.Windows.Forms.Button buttonCambiarRutas;
    }
}