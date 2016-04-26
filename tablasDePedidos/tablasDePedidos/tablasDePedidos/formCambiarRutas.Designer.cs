namespace tablasDePedidos
{
    partial class formCambiarRutas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formCambiarRutas));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.buttonSeleccionarRuta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(795, 35);
            this.textBox1.TabIndex = 0;
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.buttonAceptar.Location = new System.Drawing.Point(239, 53);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(221, 38);
            this.buttonAceptar.TabIndex = 1;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // buttonSeleccionarRuta
            // 
            this.buttonSeleccionarRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.buttonSeleccionarRuta.Location = new System.Drawing.Point(12, 53);
            this.buttonSeleccionarRuta.Name = "buttonSeleccionarRuta";
            this.buttonSeleccionarRuta.Size = new System.Drawing.Size(221, 38);
            this.buttonSeleccionarRuta.TabIndex = 1;
            this.buttonSeleccionarRuta.Text = "Seleccionar ruta";
            this.buttonSeleccionarRuta.UseVisualStyleBackColor = true;
            this.buttonSeleccionarRuta.Click += new System.EventHandler(this.buttonSeleccionarRuta_Click);
            // 
            // formCambiarRutas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 111);
            this.Controls.Add(this.buttonSeleccionarRuta);
            this.Controls.Add(this.buttonAceptar);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formCambiarRutas";
            this.Text = "Cambiar rutas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.Button buttonSeleccionarRuta;
    }
}