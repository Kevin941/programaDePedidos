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
            this.dataGridPedidos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMenuPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // botonComenzar
            // 
            this.botonComenzar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.botonComenzar.Location = new System.Drawing.Point(383, 341);
            this.botonComenzar.Name = "botonComenzar";
            this.botonComenzar.Size = new System.Drawing.Size(313, 55);
            this.botonComenzar.TabIndex = 1;
            this.botonComenzar.Text = "Comenzar";
            this.botonComenzar.UseVisualStyleBackColor = true;
            this.botonComenzar.Click += new System.EventHandler(this.botonComenzar_Click);
            // 
            // pictureBoxMenuPrincipal
            // 
            this.pictureBoxMenuPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxMenuPrincipal.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMenuPrincipal.Image")));
            this.pictureBoxMenuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMenuPrincipal.Name = "pictureBoxMenuPrincipal";
            this.pictureBoxMenuPrincipal.Size = new System.Drawing.Size(708, 408);
            this.pictureBoxMenuPrincipal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMenuPrincipal.TabIndex = 0;
            this.pictureBoxMenuPrincipal.TabStop = false;
            this.pictureBoxMenuPrincipal.Click += new System.EventHandler(this.pictureBoxMenuPrincipal_Click);
            // 
            // dataGridPedidos
            // 
            this.dataGridPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPedidos.Location = new System.Drawing.Point(0, 12);
            this.dataGridPedidos.Name = "dataGridPedidos";
            this.dataGridPedidos.Size = new System.Drawing.Size(578, 150);
            this.dataGridPedidos.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // formMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 408);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridPedidos);
            this.Controls.Add(this.botonComenzar);
            this.Controls.Add(this.pictureBoxMenuPrincipal);
            this.Name = "formMenuPrincipal";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMenuPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botonComenzar;
        private System.Windows.Forms.PictureBox pictureBoxMenuPrincipal;
        private System.Windows.Forms.DataGridView dataGridPedidos;
        private System.Windows.Forms.Label label1;
    }
}

