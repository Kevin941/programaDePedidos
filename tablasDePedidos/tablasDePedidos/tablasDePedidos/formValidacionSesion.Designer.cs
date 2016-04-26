namespace tablasDePedidos
{
    partial class formValidacionSesion
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxContraseña = new System.Windows.Forms.TextBox();
            this.textBoxUsuario = new System.Windows.Forms.TextBox();
            this.botonValidar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.Location = new System.Drawing.Point(51, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label2.Location = new System.Drawing.Point(11, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Contraseña";
            // 
            // textBoxContraseña
            // 
            this.textBoxContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.textBoxContraseña.Location = new System.Drawing.Point(153, 62);
            this.textBoxContraseña.Name = "textBoxContraseña";
            this.textBoxContraseña.PasswordChar = '•';
            this.textBoxContraseña.Size = new System.Drawing.Size(313, 35);
            this.textBoxContraseña.TabIndex = 1;
            this.textBoxContraseña.UseSystemPasswordChar = true;
            // 
            // textBoxUsuario
            // 
            this.textBoxUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.textBoxUsuario.Location = new System.Drawing.Point(153, 21);
            this.textBoxUsuario.Name = "textBoxUsuario";
            this.textBoxUsuario.Size = new System.Drawing.Size(313, 35);
            this.textBoxUsuario.TabIndex = 1;
            // 
            // botonValidar
            // 
            this.botonValidar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.botonValidar.Location = new System.Drawing.Point(16, 114);
            this.botonValidar.Name = "botonValidar";
            this.botonValidar.Size = new System.Drawing.Size(450, 54);
            this.botonValidar.TabIndex = 2;
            this.botonValidar.Text = "Validar";
            this.botonValidar.UseVisualStyleBackColor = true;
            this.botonValidar.Click += new System.EventHandler(this.botonValidar_Click);
            // 
            // formValidacionSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 196);
            this.Controls.Add(this.botonValidar);
            this.Controls.Add(this.textBoxUsuario);
            this.Controls.Add(this.textBoxContraseña);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "formValidacionSesion";
            this.Text = "Inicio de Sesion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxContraseña;
        private System.Windows.Forms.TextBox textBoxUsuario;
        private System.Windows.Forms.Button botonValidar;
    }
}