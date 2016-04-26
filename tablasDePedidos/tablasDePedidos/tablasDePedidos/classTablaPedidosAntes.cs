using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tablasDePedidos
{
    public class classTablaPedidosAntes
    {
        public string connectionStringPedidos = "";
        public OpenFileDialog dialogoParaArchivo = new OpenFileDialog();
        public System.Data.DataTable tablaPedidos = new System.Data.DataTable();
        public string pathArchivoExcelOrigenPedidos = "";
        public string nombreDelArchivo ="";
        public ArrayList arregloClavesUtilizadas = new ArrayList(); 
        public void getConnectionStringPedidos()
        {
            connectionStringPedidos = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this.pathArchivoExcelOrigenPedidos + "; Extended Properties=" + "\"" + "Excel 12.0 Xml;HDR=YES" + "\"";
            return;
        }

        public void mostrarPedidosEnGrid(DataGridView grid)
        {
            grid.DataSource = tablaPedidos;
        }

        public bool getPathOrigenPedidos()
        {
            try
            {
                dialogoParaArchivo.Filter = "Excel Files|*.xlsx;*.xls";
                //dialogoParaArchivo.InitialDirectory = @"C:\";
                dialogoParaArchivo.Title = "Selección de archivo de pedidos";
                dialogoParaArchivo.CheckFileExists = true;
                dialogoParaArchivo.CheckPathExists = true;



                if (dialogoParaArchivo.ShowDialog() == DialogResult.OK)
                {

                    pathArchivoExcelOrigenPedidos = dialogoParaArchivo.FileName;
                    //MessageBox.Show("El path es : " + pathArchivoExcelOrigenPedidos);
                    string [] nombreDelArchivoConExtension = dialogoParaArchivo.SafeFileName.Split('.');
                    nombreDelArchivo = nombreDelArchivoConExtension[0]; 
                    //MessageBox.Show("El path es : " + nombreDelArchivo[0]);
                    
                    return true;
                }
                else
                {
                    MessageBox.Show("Error al intentar abrir el archivo de Origen");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public bool getTablaPedidos()
        {
            try
            {
                OleDbConnection conexion = new OleDbConnection();
                this.getConnectionStringPedidos();
                conexion.ConnectionString = this.connectionStringPedidos;
                OleDbCommand comando = new OleDbCommand();
                comando.CommandText = "select * from [Cristobal$]";
                comando.Connection = conexion;
                DataSet setDatos = new DataSet();
                OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
                adaptador.Fill(setDatos);
                tablaPedidos = setDatos.Tables[0];
                return true; 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                MessageBox.Show("Asegurese que la tabla de origen de pedidos se llame 'Cristobal' ");
                return false; 
            }

        }
        public void getArregloClavesUtilizadas()
        {
            for (int x = 0; x < tablaPedidos.Rows.Count; x++)
            {
                string stringTemporal = tablaPedidos.Rows[x]["Clave"].ToString();
                if (!arregloClavesUtilizadas.Contains(stringTemporal))
                {
                    arregloClavesUtilizadas.Add(stringTemporal);
                }
             }
                
        }
        
        public bool getTablaPedidosFormatoNuevo()
        {
            try
            {
                OleDbConnection conexion = new OleDbConnection();
                this.getConnectionStringPedidos();
                conexion.ConnectionString = this.connectionStringPedidos;
                OleDbCommand comando = new OleDbCommand();
                comando.CommandText = "select F1 as `Nombre del Cliente`, " +
                    "F9 as `Cantidad`,  " +
                    "F10 as `Unidad`,  " +
                    "F33 as `Fecha Entrega`,  " +
                    "F31 as `No pedido`,  " +
                    "F2 as `Especificaciones`,  " +
                    "F21 as `Clave`  " +
                                    "from [Sheet1$] WHERE F21 NOT like \"Clave\"";
                comando.Connection = conexion;
                DataSet setDatos = new DataSet();
                //MessageBox.Show(comando.CommandText); 
                OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
                adaptador.Fill(setDatos);
                tablaPedidos = setDatos.Tables[0];
                return true; 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                MessageBox.Show("Asegurese que la tabla de origen de pedidos se llame 'Sheet1' ");
                return false; 
            }

        }

        
    }
}
