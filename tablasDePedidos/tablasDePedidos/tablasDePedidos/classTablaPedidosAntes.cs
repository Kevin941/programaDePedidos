using System;
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
        string pathArchivoExcelOrigenPedidos = "";
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
                dialogoParaArchivo.Filter = "Excel Files|*.xlsx;";
                //dialogoParaArchivo.InitialDirectory = @"C:\";
                dialogoParaArchivo.Title = "Selección de archivo de pedidos";
                dialogoParaArchivo.CheckFileExists = true;
                dialogoParaArchivo.CheckPathExists = true;



                if (dialogoParaArchivo.ShowDialog() == DialogResult.OK)
                {

                    pathArchivoExcelOrigenPedidos = dialogoParaArchivo.FileName;
                    //MessageBox.Show("El path es : " + pathArchivoExcelOrigenPedidos);
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
        public void getTablaPedidos()
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                MessageBox.Show("Asegurese que la tabla de origen de pedidos se llame 'Cristobal' ");
            }

        }

        
    }
}
