using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tablasDePedidos
{
    public partial class formCambiarRutas : Form
    {
        public formCambiarRutas()
        {
            InitializeComponent();
        }

        private void buttonSeleccionarRuta_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogoParaArchivo = new OpenFileDialog(); 
            dialogoParaArchivo.Filter = "Excel Files|*.xlsm;";
            //dialogoParaArchivo.InitialDirectory = @"C:\";
            dialogoParaArchivo.Title = "Seleccion ESPECIFICACIONES.xlsm";
            dialogoParaArchivo.CheckFileExists = true;
            dialogoParaArchivo.CheckPathExists = true;
            if (dialogoParaArchivo.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialogoParaArchivo.FileName.ToString(); 
                
                
            }
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter escritor = new StreamWriter(Application.StartupPath + "\\rutas.txt", false))
                {
                    escritor.WriteLine(textBox1.Text);
                }
                MessageBox.Show("Ruta guardada exitosamente.");
                this.Hide();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
