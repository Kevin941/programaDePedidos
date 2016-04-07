using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Access = Microsoft.Office.Interop.Access;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Collections;
using System.ComponentModel;
using System.Threading; 


namespace tablasDePedidos
{
    public class classTablaEspecificaciones
    {
        public string connectionStringEspecificaciones = ""; 
        public OpenFileDialog dialogoParaArchivo = new OpenFileDialog();
        public System.Data.DataTable tablaEspecificaciones = new System.Data.DataTable();
        public System.Data.DataTable foundRowsTable;
        public DataRow[] foundRows;
        string pathArchivoExcelOrigenEspecificaciones = "";
        public formLoading loadWindow = new formLoading();
        public void getConnectionStringEspecificaciones()
        {
            connectionStringEspecificaciones = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ this.pathArchivoExcelOrigenEspecificaciones + "; Extended Properties="+"\""+ "Excel 12.0 Macro;HDR=YES" + "\"" + ";"; 
            //MessageBox.Show(connectionStringEspecificaciones); 
            return;
        }

        public void mostrarEspecificacionesEnGrid(DataGridView grid)
        {
            grid.DataSource = tablaEspecificaciones;

        }

        public void getRegistrosByClave(string clave, DataGridView grid)
        {
            foundRowsTable = tablaEspecificaciones.Clone(); 
            foundRows = tablaEspecificaciones.Select("clave like " + "'%"+ clave + "%'");
            foreach (DataRow row in foundRows)
            {
                foundRowsTable.ImportRow(row); 
            }

            grid.DataSource = foundRowsTable; 

        }
        public int getRegistrosByClave(string clave)
        {
            foundRowsTable = new System.Data.DataTable(); 
            foundRowsTable = tablaEspecificaciones.Clone();
            foundRows = tablaEspecificaciones.Select("clave like " + "'%" + clave + "%'");
            foreach (DataRow row in foundRows)
            {
                foundRowsTable.ImportRow(row);
            }
            return foundRowsTable.Rows.Count; 
        }

        public void normalizarClave(string clave)
        {
            int cantidadRegistros = getRegistrosByClave("papalopaltaoca"); 
        }
        public void getRegistroByClave(string clave, DataGridView grid)
        {
            int x=0; 
            for (x = 0; x < tablaEspecificaciones.Rows.Count; x++)
            {
                string valor1= tablaEspecificaciones.Rows[x][0].ToString();
                string valor2= tablaEspecificaciones.Rows[x]["F1"].ToString();
                if (valor2 == clave)
               {
                   break;
               }
            }

            grid.DataSource =  tablaEspecificaciones.Rows[x];

            return;
        }

        public bool getPathOrigenEspecificaciones()
        {
            try
            {
                dialogoParaArchivo.Filter = "Excel Files|*.xlsm";
                //dialogoParaArchivo.InitialDirectory = @"C:\";
                dialogoParaArchivo.Title = "Selección de archivo de especificaciones";
                dialogoParaArchivo.CheckFileExists = true;
                dialogoParaArchivo.CheckPathExists = true;



                if (dialogoParaArchivo.ShowDialog() == DialogResult.OK)
                {

                    pathArchivoExcelOrigenEspecificaciones = dialogoParaArchivo.FileName;
                    //MessageBox.Show("El path es : " + pathArchivoExcelOrigenEspecificaciones);
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
        public void getTablaEspecificaciones()
        {
            try
            {
                OleDbConnection conexion = new OleDbConnection();
                this.getConnectionStringEspecificaciones();
                conexion.ConnectionString = this.connectionStringEspecificaciones;
                OleDbCommand comando = new OleDbCommand();
                //F1 es el nombre de la primera columna del archivo
                comando.CommandText = "Select " +
                    "[F1] AS `clave`, " +
                    "[Generales] AS `nombreDelCliente`, " +
                    "[Generales2] AS `D`, " +
                    "[Generales3] AS `E`, " +
                    "[Generales1] AS `C`, " +
                    "[Materiales] AS `L`, " +
                    "[Hoja de Especificaciones grales$.Pigmentos] AS `O`, " +
                    "[Generales4] AS `F`, " +
                    "[Peinado / Lubricado] AS `AU`, " +
                    "[Generales5] AS `G`, " +
                    "[Rizado2] AS `K`, " +
                    "[Horno de secado] AS `M`, " +
                    "[Extrusión5] AS `U`, " +
                    "[Extrusión7] AS `W`, " +
                    "[Embobinado Carretes] AS `AD`, " +
                    "[Embobinado Carretes1] AS `AE`, " +
                    "[Embobinado Carretes2] AS `AF`, " +
                    "[Embobinado Carretes3] AS `AG`, " +
                    "[Embobinado Carretes4] AS `AH`, " +
                    "[Embobinado Carretes5] AS `AI`, " +
                    "[Embobinado Carretes6] AS `AJ`, " +
                    "[Embobinado Carretes7] AS `AK`, " +
                    "[Embobinado Carretes8] AS `AL`, " +
                    "[Embobinado Carretes9] AS `AM`, " +
                    "[Rizado] AS `I`, " +
                    "[Rizado1] AS `J`, " +
                    "[Generales6] AS `H`, " +
                    "[Aditivos] AS `N`, " +
                    "[Embobinado] AS `AN`, " +
                    "[Extrusión1] AS `Q`, " +
                    "[Extrusión] AS `P`, " +
                    "[Extrusión2] AS `R`, " +
                    "[Extrusión6] AS `V`, " +
                    "[Extrusión13] AS `AC`, " +
                    "[Templado] AS `AP`, " +
                    "[Templado1] AS `AQ`, " +
                    "[Templado / Horno de Mazos] AS `AS`, " +
                    "[Teñido] AS `AT`, " +
                    "[Enfundado / Forrado / Marcado] AS `AX` " +                                                        
                    "from [Hoja de Especificaciones grales$];";
                //comando.CommandText = "Select * from [Hoja de Especificaciones grales$A3] where NOT 'F1' = '';"; 
                comando.Connection = conexion;
                DataSet setDatos = new DataSet();
                OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
                adaptador.Fill(setDatos);
                tablaEspecificaciones = setDatos.Tables[0];
                //string llave = ""; 
                /*
                for (int x = 0; x < tablaEspecificaciones.Rows.Count; x++)
                {
                    if (tablaEspecificaciones.Rows[x]["F1"].ToString() == "E0036008")
                    {

                    
                        llave = tablaEspecificaciones.Rows[x]["F1"].ToString();
                    }
                    
                }
                  */
                //MessageBox.Show("La llave es " + llave); 
                /*
                //Ciclo para acceder a todos los elementos de la tabla
                for (int x = 0; x < tablaEspecificaciones.Rows.Count; x++)
                {
                    foreach (DataColumn column in tablaEspecificaciones.Columns)
                    {
                        
                       tablaEspecificaciones.Rows[x][column.ColumnName].ToString();
                    }
                }
                 */

                

               

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                MessageBox.Show("Asegurese que la tabla de origen de pedidos se llame 'Cristobal' ");
            }

        }

        
    }
}
