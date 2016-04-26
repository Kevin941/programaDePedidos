namespace tablasDePedidos
{
    partial class formMenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMenuPrincipal));
            this.botonComenzar = new System.Windows.Forms.Button();
            this.pictureBoxMenuPrincipal = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonConfigurarSalida = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBarPrincipal = new System.Windows.Forms.ProgressBar();
            this.labelProceso = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelPorcentaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMenuPrincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // botonComenzar
            // 
            this.botonComenzar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.botonComenzar.Location = new System.Drawing.Point(359, 386);
            this.botonComenzar.Name = "botonComenzar";
            this.botonComenzar.Size = new System.Drawing.Size(341, 55);
            this.botonComenzar.TabIndex = 1;
            this.botonComenzar.Text = "Comenzar";
            this.botonComenzar.UseVisualStyleBackColor = true;
            this.botonComenzar.Click += new System.EventHandler(this.botonComenzar_Click);
            // 
            // pictureBoxMenuPrincipal
            // 
            this.pictureBoxMenuPrincipal.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMenuPrincipal.Image")));
            this.pictureBoxMenuPrincipal.Location = new System.Drawing.Point(397, 115);
            this.pictureBoxMenuPrincipal.Name = "pictureBoxMenuPrincipal";
            this.pictureBoxMenuPrincipal.Size = new System.Drawing.Size(309, 178);
            this.pictureBoxMenuPrincipal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMenuPrincipal.TabIndex = 0;
            this.pictureBoxMenuPrincipal.TabStop = false;
            this.pictureBoxMenuPrincipal.Click += new System.EventHandler(this.pictureBoxMenuPrincipal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(18, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 80);
            this.label1.TabIndex = 2;
            this.label1.Text = "Versión 1.0.0\r\n\r\nProveedora Mexicana de Monofilamentos S.A de C.V\r\n\r\n22-04-2016";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(18, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(678, 36);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sistema para exportación de tablas de pedidos";
            // 
            // buttonConfigurarSalida
            // 
            this.buttonConfigurarSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.buttonConfigurarSalida.Location = new System.Drawing.Point(12, 386);
            this.buttonConfigurarSalida.Name = "buttonConfigurarSalida";
            this.buttonConfigurarSalida.Size = new System.Drawing.Size(341, 55);
            this.buttonConfigurarSalida.TabIndex = 3;
            this.buttonConfigurarSalida.Text = "Configuración";
            this.buttonConfigurarSalida.UseVisualStyleBackColor = true;
            this.buttonConfigurarSalida.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.button1.Location = new System.Drawing.Point(12, 447);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(688, 53);
            this.button1.TabIndex = 4;
            this.button1.Text = "Comenzar (Formato nuevo)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // progressBarPrincipal
            // 
            this.progressBarPrincipal.Location = new System.Drawing.Point(18, 308);
            this.progressBarPrincipal.Name = "progressBarPrincipal";
            this.progressBarPrincipal.Size = new System.Drawing.Size(688, 38);
            this.progressBarPrincipal.TabIndex = 5;
            // 
            // labelProceso
            // 
            this.labelProceso.AutoSize = true;
            this.labelProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.labelProceso.Location = new System.Drawing.Point(19, 349);
            this.labelProceso.Name = "labelProceso";
            this.labelProceso.Size = new System.Drawing.Size(134, 29);
            this.labelProceso.TabIndex = 6;
            this.labelProceso.Text = "Bienvenido";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.procedimientoPrincipalBarra);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.procedimientoPrincipalBarraCambiada);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.procedimientoPrincipalBarraTerminado);
            // 
            // labelPorcentaje
            // 
            this.labelPorcentaje.AutoSize = true;
            this.labelPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.labelPorcentaje.Location = new System.Drawing.Point(649, 349);
            this.labelPorcentaje.Name = "labelPorcentaje";
            this.labelPorcentaje.Size = new System.Drawing.Size(0, 29);
            this.labelPorcentaje.TabIndex = 7;
            // 
            // formMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 512);
            this.Controls.Add(this.labelPorcentaje);
            this.Controls.Add(this.labelProceso);
            this.Controls.Add(this.progressBarPrincipal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonConfigurarSalida);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.botonComenzar);
            this.Controls.Add(this.pictureBoxMenuPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formMenuPrincipal";
            this.Text = "Exportación de Tablas de Pedidos";
            this.Load += new System.EventHandler(this.formMenuPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMenuPrincipal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botonComenzar;
        private System.Windows.Forms.PictureBox pictureBoxMenuPrincipal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonConfigurarSalida;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBarPrincipal;
        private System.Windows.Forms.Label labelProceso;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelPorcentaje;
    }
}

