using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using SmartXLS;
using System.Diagnostics; 


namespace tablasDePedidos
{
    public class classTablaEspecificaciones
    {
        public string connectionStringEspecificaciones = ""; 
        public OpenFileDialog dialogoParaArchivo = new OpenFileDialog();
        public System.Data.DataTable tablaEspecificaciones = new System.Data.DataTable();
        public System.Data.DataTable foundRowsTable;
        public DataRow[] foundRows;
        public string pathArchivoExcelOrigenEspecificaciones = "";
        public formLoading loadWindow = new formLoading();
        ArrayList arregloindicesClavesUtilizadas = new ArrayList();
        ArrayList arregloClavesUtilizadas = new ArrayList();
        public int cutRow;
        public string progreso = ""; 
        public void getConnectionStringEspecificaciones()
        {
            //connectionStringEspecificaciones = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this.pathArchivoExcelOrigenEspecificaciones + "; Extended Properties=" + "\"" + "Excel 12.0 Macro;HDR=YES;IMEX=1" + "\"" + ";";
            connectionStringEspecificaciones = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this.pathArchivoExcelOrigenEspecificaciones + "; Extended Properties=" + "\"" + "Excel 12.0 Macro;HDR=YES;IMEX=1;" + "\"" + ";";
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
        public void getExcelDataSmart()
        {
            //Estudianes en el área de tecnologías de información. 
            //auditoria y analíticos. 12 mil Brutos más prestaciones. Full time: SantaFe, IberoAmericana
            WorkBook workBook = new WorkBook();
            Console.WriteLine(pathArchivoExcelOrigenEspecificaciones); 
            workBook.read(pathArchivoExcelOrigenEspecificaciones);
            tablaEspecificaciones = workBook.ExportDataTable(
            0, //first row
            0, //first col
            68898, //last row
            35567, //last col
            true, //first row as header
            false //convert to DateTime object if it match date pattern
            );
        }
        public void getExcelData()
        {
            string strSheetName = "Hoja de Especificaciones grales"; 
            
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(pathArchivoExcelOrigenEspecificaciones, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }

            ISheet sheet = hssfworkbook.GetSheet(strSheetName);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            while (rows.MoveNext())
            {
                IRow row = (HSSFRow)rows.Current;

                if (tablaEspecificaciones.Columns.Count == 0)
                {
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        tablaEspecificaciones.Columns.Add(row.GetCell(j).ToString());
                    }

                    continue;
                }

                DataRow dr = tablaEspecificaciones.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);

                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                tablaEspecificaciones.Rows.Add(dr);
            }

            
        }
        public void setArregloClavesUtilizadas(ArrayList listaClaves)
        {
            arregloClavesUtilizadas.Clear(); 
            foreach (string clave in listaClaves)
            {
                arregloClavesUtilizadas.Add(clave); 
            }
        }
        private static void MatarProcesosExcel()
        {
            try
            {
                Process[] proces = Process.GetProcessesByName("EXCEL");
                foreach (Process proc in proces)
                {
                    if (proc.MainWindowTitle == "")
                        proc.Kill();
                }
            }
            catch { }
        }

        public void getTablaEspecificacionesInterop(BackgroundWorker backgroundWorker1)
        {
            backgroundWorker1.ReportProgress(0);

            progreso = "Deteniendo procesos de excel... ";
            MatarProcesosExcel();
            backgroundWorker1.ReportProgress(3);
            try
            {
                progreso = "Leyendo base de datos...";
                Microsoft.Office.Interop.Excel.Application ExcelObj = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook theWorkbook = ExcelObj.Workbooks.Open(pathArchivoExcelOrigenEspecificaciones, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, false, Microsoft.Office.Interop.Excel.XlCorruptLoad.xlNormalLoad);
                Microsoft.Office.Interop.Excel.Sheets sheets = theWorkbook.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);
                backgroundWorker1.ReportProgress(30);
            
            //Obtenemos la la celda con el último registro. 

            progreso = "Leyendo base de datos...";
            cutRow = obtieneCeldaDeCorte(worksheet);
            backgroundWorker1.ReportProgress(40); //Ya va el 10% 

            //Primero obtenemos todos los valores de las claves por medio de un select y lo guardamos en la tablaEspecificaciones 
            progreso = "Buscando claves...";
            getTablaEspecificacionesClaves();
            llenarArregloindicesClavesUtilizadas();
            backgroundWorker1.ReportProgress(50);
            

            //'If you are not sure of the range of data, use UsedRange, but make sure you handle the null values explicitly.

            
            //System.Data.DataTable tablaEspecificaciones = new System.Data.DataTable();
            tablaEspecificaciones = new System.Data.DataTable();
            tablaEspecificaciones.Columns.Add("clave", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("nombreDelCliente", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("D", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("E", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("C", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("L", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("O", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("F", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AU", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("G", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("K", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("M", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("U", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("W", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AD", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AE", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AF", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AG", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AH", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AI", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AJ", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AK", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AL", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AM", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("I", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("J", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("H", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("N", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AN", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("Q", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("P", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("R", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("V", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AC", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AP", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AQ", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AR", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AO", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AS", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AX", System.Type.GetType("System.String"));
            tablaEspecificaciones.Columns.Add("AT", System.Type.GetType("System.String")); //Teñido 


            DataRow myDataRow;
            Microsoft.Office.Interop.Excel.Range range;
            //tablaEspecificaciones = new System.Data.DataTable(); 

            progreso = "Copiando registros de la base de datos a la memoria... "; 
            //Add the Rows to the DataTable.
            foreach (int rowCounter in arregloindicesClavesUtilizadas)
            {
                myDataRow = tablaEspecificaciones.NewRow();

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 1]);
                myDataRow["clave"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 2]);
                myDataRow["nombreDelCliente"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 4]);
                myDataRow["D"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 5]);
                myDataRow["E"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 3]);
                myDataRow["C"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 12]);
                myDataRow["L"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 15]);
                myDataRow["O"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 6]);
                myDataRow["F"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 47]);
                myDataRow["AU"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 7]);
                myDataRow["G"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 11]);
                myDataRow["K"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 13]);
                myDataRow["M"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 21]);
                myDataRow["U"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 23]);
                myDataRow["W"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 30]);
                myDataRow["AD"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 31]);
                myDataRow["AE"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 32]);
                myDataRow["AF"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 33]);
                myDataRow["AG"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 34]);
                myDataRow["AH"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 35]);
                myDataRow["AI"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 36]);
                myDataRow["AJ"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 37]);
                myDataRow["AK"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 38]);
                myDataRow["AL"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 39]);
                myDataRow["AM"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 9]);
                myDataRow["I"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 10]);
                myDataRow["J"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 8]);
                myDataRow["H"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 14]);
                myDataRow["N"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 40]);
                myDataRow["AN"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 17]);
                myDataRow["Q"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 16]);
                myDataRow["P"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 18]);
                myDataRow["R"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 22]);
                myDataRow["V"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 29]);
                myDataRow["AC"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 42]);
                myDataRow["AP"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 43]);
                myDataRow["AQ"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 44]);
                myDataRow["AR"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 41]);
                myDataRow["AO"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 45]);
                myDataRow["AS"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 50]);
                myDataRow["AX"] = range.Text;

                range = (Microsoft.Office.Interop.Excel.Range)(worksheet.Cells[rowCounter, 46]);
                myDataRow["AT"] = range.Text;


                
                
                //Agregamos el registro a la tabla
                tablaEspecificaciones.Rows.Add(myDataRow);

            }
            backgroundWorker1.ReportProgress(68);

            progreso = "Cerrando archivos"; 
            MatarProcesosExcel();
            backgroundWorker1.ReportProgress(70);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos. Verifica la ruta especificada. \n\n" + ex.ToString());                 
                return;
            }

        }


        private void llenarArregloindicesClavesUtilizadas()
        {
            arregloindicesClavesUtilizadas = new ArrayList(); 
            foreach (string clave in arregloClavesUtilizadas)
            {
                DataRow[] filasEncontradas;
                filasEncontradas = tablaEspecificaciones.Select("clave like " + "'%" + clave + "%' AND clave not like '%OBSOLETO%' AND clave not like 'Cliente'");
                foreach (DataRow registroEncontrado in filasEncontradas)                
                {
                    int indiceEntabla = tablaEspecificaciones.Rows.IndexOf(registroEncontrado);
                    if (indiceEntabla < cutRow)
                    {
                        arregloindicesClavesUtilizadas.Add(indiceEntabla+2); 
                    }
                }
               
            }
            Console.WriteLine();  
            
        }

        private int obtieneCeldaDeCorte(Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Range range;
                range = xlWorkSheet.UsedRange;
                int catidadColumnas = range.Columns.Count;
                int cantidadFilas = range.Rows.Count;
                int contadorRegistros = 2;
                string valorCeldaActual = Convert.ToString((range.Cells[contadorRegistros, 1] as Range).Value2);

                while ((valorCeldaActual != "") && (valorCeldaActual != null))
                {

                    contadorRegistros++;
                    valorCeldaActual = Convert.ToString((range.Cells[contadorRegistros, 1] as Range).Value2);
                    //Console.WriteLine(valorCeldaActual);

                }
                return contadorRegistros;
            }
            catch (Exception E)
            {
                Console.WriteLine("\n" + E.ToString());
                return 0;
            }



        }
        private void getTablaEspecificacionesClaves(){
            try
            {
                OleDbConnection conexion = new OleDbConnection();
                this.getConnectionStringEspecificaciones();
                conexion.ConnectionString = this.connectionStringEspecificaciones;
                OleDbCommand comando = new OleDbCommand();
                //F1 es el nombre de la primera columna del archivo
                comando.CommandText = "Select " +
                    "[F1] AS `clave` " +
                    "from [Hoja de Especificaciones grales$];";
               // MessageBox.Show(comando.CommandText); 
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
                    "[Horno de Mazos] AS `AR`, " +
                    "[Embobinado1] AS `AO`, " +           
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
