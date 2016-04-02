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
        private int indiceSeleccionado = -1;

        public int IndiceSeleccionado
        {
            get { return indiceSeleccionado; }
            set { indiceSeleccionado = value; }
        }
        public formVentanaInteractiva(DataTable tablaClavesEncontradas, DataTable tablaPedidoActual)
        {
            InitializeComponent();
            dataGridPedidoActual.DataSource = tablaPedidoActual;
            dataGridClavesEncontradas.DataSource = tablaClavesEncontradas; 
        }

        private void formVentanaInteractiva_Load(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (indiceSeleccionado == -1)
            {
                MessageBox.Show("Por favor selecciona una fila para continuar");
                e.Cancel = true; 
                return;
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (indiceSeleccionado == -1)
            {
                MessageBox.Show("Por favor selecciona un elemento de la tabla de pedidos");
                return; 
            }
            this.Hide();
            this.Dispose(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridClavesEncontradas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indiceSeleccionado = e.RowIndex;
            

            
            
            
            
            
            
        }
    }
}
