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
    public partial class formValidacionSesion : Form
    {
        public formValidacionSesion()
        {
            InitializeComponent();
        }

        private void botonValidar_Click(object sender, EventArgs e)
        {
            if ((textBoxUsuario.Text == "AC") && (textBoxContraseña.Text == "JaglExp"))
            {
                formMenuAdministrador ventana = new formMenuAdministrador();
                ventana.Show();
                this.Hide();
                this.Dispose();
                return; 
            }
            if ((textBoxUsuario.Text == "admin") && (textBoxContraseña.Text == "conker"))
            {
                formMenuAdministrador ventana = new formMenuAdministrador();
                ventana.Show();
                this.Hide();
                this.Dispose();
                return;
            }
        }
    }
}
