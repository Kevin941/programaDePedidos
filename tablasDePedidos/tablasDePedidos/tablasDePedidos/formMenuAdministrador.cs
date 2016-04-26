using System;
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
    public partial class formMenuAdministrador : Form
    {
        public formMenuAdministrador()
        {
            InitializeComponent();
        }

        private void buttonConfigurarSalida_Click(object sender, EventArgs e)
        {
            formConfiguracionSalida ventanaConfiguracionSalida = new formConfiguracionSalida();
            ventanaConfiguracionSalida.Show();
        }

        private void buttonCambiarRutas_Click(object sender, EventArgs e)
        {
            formCambiarRutas ventanaRutas = new formCambiarRutas();
            ventanaRutas.Show(); 

        }
    }
}
