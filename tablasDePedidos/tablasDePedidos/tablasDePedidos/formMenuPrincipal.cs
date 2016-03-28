using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tablasDePedidos
{
    public partial class formMenuPrincipal : Form
    {
        public classTablaEspecificaciones tablaEspecificaciones = new classTablaEspecificaciones();
        public classTablaPedidosAntes tablaPedidosAntes = new classTablaPedidosAntes();
        public classTablaPedidosDespues tablaPedidosDespues = new classTablaPedidosDespues();
        public formLoading ventanaCargando = new formLoading(); 

        //Se utiliza este delegado para modificar la interfaz gráfica a través del thread. 
        //Se especifica que este delegado tomara un objeto del tipo grid en el momento de su invocación "this.invoke()"
        private delegate void delegadoParaInterfaz(DataGridView grid); 

        public formMenuPrincipal()
        {
            InitializeComponent();
        }

        private void botonComenzar_Click(object sender, EventArgs e)
        {


            /*
            MessageBox.Show("Por favor selecciona el archivo de especificaciones.");
            if (!tablaEspecificaciones.getPathOrigenEspecificaciones())
            {
                return;
            }

            MessageBox.Show("Por favor selecciona el archivo de pedidos.");
            if (!tablaPedidosAnterior.getPathOrigenPedidos())
            {
                return;
            }
            //loadWindow.TopMost = true;  // make sure it doesn't get created behind other forms
            ventanaCargando = new formLoading();
            ventanaCargando.Show();
            */


            //Mostrar la página de cargando... Se ocultará en el momento de terminar el proceso
            ventanaCargando.Show(); 


            //Background worker es un thread. Se utilizará para realizar la creación de las tablas mientras aparece la página de cargando 
            BackgroundWorker worker = new BackgroundWorker();

            //Do work es el procedimiento que se realiza cuando el thread comienza a correr. 
            worker.DoWork += new DoWorkEventHandler(procedimientoPrincipal);
            //En el momento que se termina de ejecutar todo el procedimiento del thread se invoca "worker_RunWorkerCompleted"
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(procedimientoPrincipalTerminado);
            //Ejecuta el Thread
            worker.RunWorkerAsync();            
        }

        void procedimientoPrincipal(object sender, DoWorkEventArgs e)
        {
            //Aquí va a ir todo el procedimiento para las tablas
            //tablaEspecificaciones.getTablaEspecificaciones();
            //tablaPedidosAnterior.getTablaPedidos();

            tablaPedidosDespues.getTablaDePedidos();

            //Se crea un delegado para poder modificar la interfaz del usuario a través del thread
            Delegate delegado = new delegadoParaInterfaz(tablaPedidosDespues.mostrarPedidosEnGrid);
            this.Invoke(delegado, dataGridPedidos); 

            //tablaPedidosDespues.generarExcelDesdeDataTable(tablaPedidosAnterior.tablaPedidos); 
            
        }

        //Este procedimiento se realiza en e el thread principal (El de la interfaz del usuario) 
        void procedimientoPrincipalTerminado(object sender, RunWorkerCompletedEventArgs e)
        {
            // close loading window
            ventanaCargando.Hide();


        }

        private void pictureBoxMenuPrincipal_Click(object sender, EventArgs e)
        {
             
        }
    }

    public class classTablasDePrueba
    {
        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }

        public List<classTablasDePrueba> getListaDeNombresDeColumnasEspecificaciones()
        {
            classTablaEspecificaciones tablaEspecificaciones = new classTablaEspecificaciones(); 
            if (!tablaEspecificaciones.getPathOrigenEspecificaciones())
            {
                return new List<classTablasDePrueba>();
            }
            
            tablaEspecificaciones.getTablaEspecificaciones();
            string[] columnNames = tablaEspecificaciones.tablaEspecificaciones.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
            List<classTablasDePrueba> listaNombres = new List<classTablasDePrueba>();

            foreach (string nombre in columnNames)
            {
                classTablasDePrueba textoTemporal = new classTablasDePrueba();
                textoTemporal.Nombre = nombre;
                listaNombres.Add(textoTemporal);
            }
            return listaNombres; 
            //this.dataGridPedidos.DataSource = listaNombres;
            
        }

 
    }
}
