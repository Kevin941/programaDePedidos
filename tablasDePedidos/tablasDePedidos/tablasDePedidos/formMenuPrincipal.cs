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
        public bool formatoNuevo = false; 
        public formLoading ventanaCargando = new formLoading();
        //Es un thread para poner el pacman a bailar mientras carga el programa
        BackgroundWorker worker = new BackgroundWorker();

        //Se utiliza este delegado para modificar la interfaz gráfica a través del thread. 
        //Se especifica que este delegado tomara un objeto del tipo grid en el momento de su invocación "this.invoke()"
        private delegate void delegadoParaUtilizarGrid(DataGridView grid);
        private delegate void delegadoParaUtilizarDosGrids(DataGridView grid, DataGridView grid2);
        private delegate void delegadoGridConString(string clave, DataGridView grid);
        private delegate void delegadoProgressConLabel(Label etiquetaProceso, ProgressBar barraDeProgreso); 
        public formMenuPrincipal()
        {
            InitializeComponent();
        }

        private void botonComenzar_Click(object sender, EventArgs e)
        {
            formatoNuevo = false; 
            tablaEspecificaciones.pathArchivoExcelOrigenEspecificaciones = Application.StartupPath + "\\ESPECIFICACIONES.xlsm";
            try
            {
                string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\rutas.txt");
                tablaEspecificaciones.pathArchivoExcelOrigenEspecificaciones = lines[0]; 
            }
            catch
            {
                MessageBox.Show("No se pudo leer el archivo de rutas.");
                return; 
            }
            /*
            MessageBox.Show("Por favor selecciona el archivo de especificaciones.");
            if (!tablaEspecificaciones.getPathOrigenEspecificaciones())
            {
                return;
            }*/
            
            MessageBox.Show("Por favor selecciona el archivo de pedidos.");

            if (!tablaPedidosAntes.getPathOrigenPedidos())
            {
                return;
            }
            tablaPedidosDespues.nombreDelArchivo = tablaPedidosAntes.nombreDelArchivo; 

            MessageBox.Show("Por favor selecciona el destino del archivo resultante");
            if (!tablaPedidosDespues.getDireccionDestino())
            {
                return; 
            }


            invocarProcedimientoPrincipalParaProgressBar();           
        }

        void invocarProcedimientoPrincipalParaProgressBar()
        {
            backgroundWorker1.RunWorkerAsync(); 
        }

        void invocarProcedimientoPrincipal()
        {
            ventanaCargando = new formLoading(); 
            ventanaCargando.Show();


            //Background worker es un thread. Se utilizará para realizar la creación de las tablas mientras aparece la página de cargando 
            worker = new BackgroundWorker();

            //Do work es el procedimiento que se realiza cuando el thread comienza a correr. 
            worker.DoWork += new DoWorkEventHandler(procedimientoPrincipal);
            //En el momento que se termina de ejecutar todo el procedimiento del thread se invoca "worker_RunWorkerCompleted"
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(procedimientoPrincipalTerminado);
            //Ejecuta el Thread
            worker.RunWorkerAsync();  
        }
        void procedimientoPrincipal(object sender, DoWorkEventArgs e)
        {
            //Se obtienen las tablas desde los excel. Los path ya se pidieron antes. 
            

            //if (tablaPedidosAntes.tablaPedidos.Rows.Count < 1)
            if (formatoNuevo == false)
            {
                if (!tablaPedidosAntes.getTablaPedidos())
                {
                    return; 
                }
            }
            if (formatoNuevo == true)
            {
                if (!tablaPedidosAntes.getTablaPedidosFormatoNuevo())
                {
                    return; 
                } 
            }

            tablaPedidosAntes.getArregloClavesUtilizadas();
            tablaEspecificaciones.setArregloClavesUtilizadas(tablaPedidosAntes.arregloClavesUtilizadas); 
            tablaEspecificaciones.getTablaEspecificacionesInterop(backgroundWorker1);
           
            tablaPedidosDespues.copiarTablas(tablaPedidosAntes.tablaPedidos, tablaEspecificaciones.tablaEspecificaciones);
            tablaPedidosDespues.getTablaDePedidos(backgroundWorker1); 

             

            //Delegate delegado = new delegadoParaInterfaz(tablaPedidosDespues.mostrarPedidosEnGrid);
            //this.Invoke(delegado, dataGridPedidos); 

            //Delegate delegado = new delegadoParaUtilizarDosGrids(tablaPedidosDespues.getTablaDePedidos);
            //this.Invoke(delegado, dataGridPedidos, dataGridPedidos); 
            //Delegate delegado = new delegadoGridConString(tablaEspecificaciones.getRegistrosByClave);
            //this.Invoke(delegado, ".17A55104E0", dataGridPedidos);       
            
            //Se crea un delegado para poder modificar la interfaz del usuario a través del thread
            //Delegate delegado = new delegadoParaInterfaz(tablaPedidosDespues.mostrarPedidosEnGrid);
            //this.Invoke(delegado, dataGridPedidos); 

            //tablaPedidosDespues.generarExcelDesdeDataTable(tablaPedidosAnterior.tablaPedidos); 
            
        }

        //Este procedimiento se realiza en e el thread principal (El de la interfaz del usuario) 
        void procedimientoPrincipalTerminado(object sender, RunWorkerCompletedEventArgs e)
        {
            // close loading window
            ventanaCargando.Hide();
            worker.Dispose(); 


        }

        private void pictureBoxMenuPrincipal_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            classTablasDePrueba nueva = new classTablasDePrueba();
           // dataGridPedidos.DataSource = nueva.getListaDeNombresDeColumnasEspecificaciones();

        }

        private void formMenuPrincipal_Load(object sender, EventArgs e)
        {
            /*label1.Parent = pictureBoxMenuPrincipal;
            label2.Parent = pictureBoxMenuPrincipal; 
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;

            label2.BackColor = Color.Transparent; 
            */
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            formValidacionSesion ventanaValidar = new formValidacionSesion();
            ventanaValidar.Show(); 
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            formatoNuevo = true; 
            tablaEspecificaciones.pathArchivoExcelOrigenEspecificaciones = Application.StartupPath + "\\ESPECIFICACIONES.xlsm";
            /*
            MessageBox.Show("Por favor selecciona el archivo de especificaciones.");
            if (!tablaEspecificaciones.getPathOrigenEspecificaciones())
            {
                return;
            }*/

            MessageBox.Show("Por favor selecciona el archivo de pedidos.");

            if (!tablaPedidosAntes.getPathOrigenPedidos())
            {
                return;
            }
            tablaPedidosDespues.nombreDelArchivo = tablaPedidosAntes.nombreDelArchivo;

            MessageBox.Show("Por favor selecciona el destino del archivo resultante");
            if (!tablaPedidosDespues.getDireccionDestino())
            {
                return;
            }


            invocarProcedimientoPrincipalParaProgressBar();           
        }

        private void procedimientoPrincipalBarra(object sender, DoWorkEventArgs e)
        {

            if (formatoNuevo == false)
            {
                if (!tablaPedidosAntes.getTablaPedidos())
                {
                    return; 
                }
            }
            if (formatoNuevo == true)
            {
                if (!tablaPedidosAntes.getTablaPedidosFormatoNuevo())
                {
                    return; 
                }
            }

            tablaPedidosAntes.getArregloClavesUtilizadas();
            tablaEspecificaciones.setArregloClavesUtilizadas(tablaPedidosAntes.arregloClavesUtilizadas);
            tablaEspecificaciones.getTablaEspecificacionesInterop(backgroundWorker1);
            
            tablaPedidosDespues.copiarTablas(tablaPedidosAntes.tablaPedidos, tablaEspecificaciones.tablaEspecificaciones);
            tablaPedidosDespues.getTablaDePedidos(backgroundWorker1); 
        }

        private void procedimientoPrincipalBarraCambiada(object sender, ProgressChangedEventArgs e)
        {
            progressBarPrincipal.Value = e.ProgressPercentage;
            if (progressBarPrincipal.Value <= 70)
            {
                labelProceso.Text = tablaEspecificaciones.progreso;
            }
            else
            {
                labelProceso.Text = tablaPedidosDespues.progreso; 
            }
            labelPorcentaje.Text = e.ProgressPercentage.ToString() + "%"; 
            //labelProceso.Text = e.ProgressPercentage.ToString() + "% completado. "; 
        }

        private void procedimientoPrincipalBarraTerminado(object sender, RunWorkerCompletedEventArgs e)
        {
            labelProceso.Text = "Bienvenido.";
            labelPorcentaje.Text = "";
            //MessageBox.Show("Terminado"); 
            backgroundWorker1.Dispose();
            tablaEspecificaciones = new classTablaEspecificaciones();
            tablaPedidosAntes = new classTablaPedidosAntes();
            tablaPedidosDespues = new classTablaPedidosDespues(); 
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
