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
    public partial class formVentanaInteractiva : Form
    {
        public formVentanaInteractiva(DataTable tablaClavesEncontradas, DataTable tablaPedidoActual)
        {
            InitializeComponent();
            dataGridPedidoActual.DataSource = tablaPedidoActual;
            dataGridClavesEncontradas.DataSource = tablaClavesEncontradas; 
        }

        private void formVentanaInteractiva_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose(); 
        }
    }
}
