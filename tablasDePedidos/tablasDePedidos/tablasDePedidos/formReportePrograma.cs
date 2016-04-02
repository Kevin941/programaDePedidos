using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tablasDePedidos
{
    public partial class formReportePrograma : Form
    {
        ArrayList arregloClavesNoEncontradas = new ArrayList();
        public formReportePrograma(ArrayList arregloClavesNoEncontradas)
        {
            this.arregloClavesNoEncontradas = arregloClavesNoEncontradas; 
            InitializeComponent();
        }

        private void formReportePrograma_Load(object sender, EventArgs e)
        {
            if (arregloClavesNoEncontradas.Count < 1)
            {
                richTextBox1.Text = "Todas las claves fueron encontradas y copiadas con éxito";
            }
            else
            {
                richTextBox1.Text += "Las siguientes claves no fueron encontradas: \n";
                foreach (Object obj in arregloClavesNoEncontradas)
                    richTextBox1.Text += obj + "\n";
            }
        }
    }
}
