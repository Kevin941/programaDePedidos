﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel; 
namespace tablasDePedidos
{
    public class classTablaPedidosDespues
    {
        #region Declaracion de variables 

        public System.Data.DataTable tablaPedidosDespues = new System.Data.DataTable();
        public System.Data.DataTable tablaPedidosAntes = new System.Data.DataTable();
        public System.Data.DataTable tablaEspecificaciones = new System.Data.DataTable();
        Dictionary<string, string> diccionarioDeColumnas = new Dictionary<string, string>();
        public System.Data.DataTable foundRowsTable;
        public DataRow[] foundRows;
        
        #endregion


        #region Funciones de configuación 
        private void generarColumnasParaLaTabla()
        {
            tablaPedidosDespues.Columns.Add("Nombre del Cliente", typeof(string));
            tablaPedidosDespues.Columns.Add("Cantidad_Kg", typeof(string));
            tablaPedidosDespues.Columns.Add("Unidad_Original", typeof(string));
            tablaPedidosDespues.Columns.Add("Calibre", typeof(string));
            tablaPedidosDespues.Columns.Add("Color", typeof(string));
            tablaPedidosDespues.Columns.Add("Material", typeof(string));
            tablaPedidosDespues.Columns.Add("Resina", typeof(string));
            tablaPedidosDespues.Columns.Add("Clave", typeof(string));
            tablaPedidosDespues.Columns.Add("Corte", typeof(string));
            tablaPedidosDespues.Columns.Add("Lubricante", typeof(string));
            tablaPedidosDespues.Columns.Add("Orientación", typeof(string));
            tablaPedidosDespues.Columns.Add("No pedido", typeof(string));
            tablaPedidosDespues.Columns.Add("Fecha Entrega", typeof(string));
            tablaPedidosDespues.Columns.Add("ESP_SAE", typeof(string));
            tablaPedidosDespues.Columns.Add("Rizado", typeof(string));
            tablaPedidosDespues.Columns.Add("Perfil", typeof(string));
            tablaPedidosDespues.Columns.Add("Aditivos", typeof(string));
            tablaPedidosDespues.Columns.Add("Tipo de Mazo", typeof(string));
            tablaPedidosDespues.Columns.Add("Bastón_Espejo_Tina", typeof(string));
            tablaPedidosDespues.Columns.Add("Herramental", typeof(string));
            tablaPedidosDespues.Columns.Add("Fabricar", typeof(string));
            tablaPedidosDespues.Columns.Add("Temple", typeof(string));
            tablaPedidosDespues.Columns.Add("Horno", typeof(string));
            tablaPedidosDespues.Columns.Add("Teñido", typeof(string));
            tablaPedidosDespues.Columns.Add("Enfundado", typeof(string));
            tablaPedidosDespues.Columns.Add("Esp_Carretes", typeof(string));
        }

        public void copiarTablas(System.Data.DataTable tablaPedidosAntes, System.Data.DataTable tablaEspecificaciones)
        {
            this.tablaPedidosAntes = tablaPedidosAntes.Copy();
            this.tablaEspecificaciones = tablaEspecificaciones.Copy();
        }
        public classTablaPedidosDespues()
        {
            inicializarDiccionario();
        }

        private void inicializarDiccionario()
        {
            diccionarioDeColumnas.Add("Nombre del Cliente", "");
            diccionarioDeColumnas.Add("Cantidad_Kg", "");
            diccionarioDeColumnas.Add("Unidad_Original", "");
            diccionarioDeColumnas.Add("Calibre", "");
            diccionarioDeColumnas.Add("Color", "");
            diccionarioDeColumnas.Add("Material", "");
            diccionarioDeColumnas.Add("Resina", "");
            diccionarioDeColumnas.Add("Clave", "");
            diccionarioDeColumnas.Add("Corte", "");
            diccionarioDeColumnas.Add("Lubricante", "");
            diccionarioDeColumnas.Add("Orientación", "");
            diccionarioDeColumnas.Add("No pedido", "");
            diccionarioDeColumnas.Add("Fecha Entrega", "");
            diccionarioDeColumnas.Add("ESP_SAE", "");
            diccionarioDeColumnas.Add("Rizado", "");
            diccionarioDeColumnas.Add("Perfil", "");
            diccionarioDeColumnas.Add("Aditivos", "");
            diccionarioDeColumnas.Add("Tipo de Mazo", "");
            diccionarioDeColumnas.Add("Bastón_Espejo_Tina", "");
            diccionarioDeColumnas.Add("Herramental", "");
            diccionarioDeColumnas.Add("Fabricar", "");
            diccionarioDeColumnas.Add("Temple", "");
            diccionarioDeColumnas.Add("Horno", "");
            diccionarioDeColumnas.Add("Teñido", "");
            diccionarioDeColumnas.Add("Enfundado", "");
            diccionarioDeColumnas.Add("Esp_Carretes", "");
        }


        static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        } 

        #endregion 


        private void agregaRegistroEnTabla()
        {
            tablaPedidosDespues.Rows.Add(
               diccionarioDeColumnas["Nombre del Cliente"],
               diccionarioDeColumnas["Cantidad_Kg"],
               diccionarioDeColumnas["Unidad_Original"],
               diccionarioDeColumnas["Calibre"],
               diccionarioDeColumnas["Color"],
               diccionarioDeColumnas["Material"],
               diccionarioDeColumnas["Resina"],
               diccionarioDeColumnas["Clave"],
               diccionarioDeColumnas["Corte"],
               diccionarioDeColumnas["Lubricante"],
               diccionarioDeColumnas["Orientación"],
               diccionarioDeColumnas["No pedido"],
               diccionarioDeColumnas["Fecha Entrega"],
               diccionarioDeColumnas["ESP_SAE"],
               diccionarioDeColumnas["Rizado"],
               diccionarioDeColumnas["Perfil"],
               diccionarioDeColumnas["Aditivos"],
               diccionarioDeColumnas["Tipo de Mazo"],
               diccionarioDeColumnas["Bastón_Espejo_Tina"],
               diccionarioDeColumnas["Herramental"],
               diccionarioDeColumnas["Fabricar"],
               diccionarioDeColumnas["Temple"],
               diccionarioDeColumnas["Horno"],
               diccionarioDeColumnas["Teñido"],
               diccionarioDeColumnas["Enfundado"],
               diccionarioDeColumnas["Esp_Carretes"]);
        }

        public void generarExcelDesdeDataTable2(DataTable tabla)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.Add(tabla, "tablaPedidosDespues");
            //Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //Lineas para generar un nuevo archivo



            //Obtener celda de corte 
            //Aqui va todo el procedimiento 


            xlWorkBook.SaveAs("your-file-name.xls");
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            Console.ReadKey();


        }

        

        


        public void generarExcelDesdeDataTable(DataTable Tbl, string ExcelFilePath = null)
        {
            ExcelFilePath = "C:\\Users\\Juan\\Desktop\\programaDePedidos\\archivo.xls"; 
            try
        {
            if (Tbl == null || Tbl.Columns.Count == 0)
                throw new Exception("ExportToExcel: Null or empty input table!\n");

            // load excel, and create a new workbook
            Excel.Application excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            // single worksheet
            Excel._Worksheet workSheet = excelApp.ActiveSheet;

            // column headings
            for (int i = 0; i < Tbl.Columns.Count; i++)
            {
                workSheet.Cells[1, (i+1)] = Tbl.Columns[i].ColumnName;
            }

            // rows
            for (int i = 0; i < Tbl.Rows.Count; i++)
            {
                // to do: format datetime values before printing
                for (int j = 0; j < Tbl.Columns.Count; j++)
                {
                    workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                }
            }

            // check fielpath
            if (ExcelFilePath != null && ExcelFilePath != "")
            {
                try
                {
                    workSheet.SaveAs(ExcelFilePath);
                    excelApp.Quit();
                    MessageBox.Show("Excel file saved!");
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                        + ex.Message);
                }
            }
            else    // no filepath is given
            {
                excelApp.Visible = true;
            }
        }
        catch(Exception ex)
        {
            throw new Exception("ExportToExcel: \n" + ex.Message);
        }
    }

        public void mostrarPedidosEnGrid(DataGridView grid)
        {
            grid.DataSource = tablaPedidosAntes; 
        }

        public int getRegistrosByClaveEnEspecificaciones(string clave)
        {
            //Se copian los nombre de las columnas en la tabla foundRowsTable
            foundRowsTable = new System.Data.DataTable();
            foundRowsTable = tablaEspecificaciones.Clone();

            foundRows = tablaEspecificaciones.Select("clave like " + "'%" + clave + "%'");
            foreach (DataRow row in foundRows)
            {
                foundRowsTable.ImportRow(row);
            }
            return foundRowsTable.Rows.Count; 
        }
        public void getTablaDePedidos()
        {
            //Creamos una nueva tabla
            tablaPedidosDespues = new DataTable();
            
            //Genera las columnas para la tabla
            generarColumnasParaLaTabla(); 



            //Se actualizan los valores del arreglo para introducirlo a la tabla 
            diccionarioDeColumnas["Nombre del Cliente"]= "valor"; 
            diccionarioDeColumnas["Cantidad_Kg"]= "valor"; 
            diccionarioDeColumnas["Unidad_Original"]= "valor"; 
            diccionarioDeColumnas["Calibre"]= "valor"; 
            diccionarioDeColumnas["Color"]= "valor"; 
            diccionarioDeColumnas["Material"]= "valor"; 
            diccionarioDeColumnas["Resina"]= "valor"; 
            diccionarioDeColumnas["Clave"]= "valor"; 
            diccionarioDeColumnas["Corte"]= "valor"; 
            diccionarioDeColumnas["Lubricante"]= "valor"; 
            diccionarioDeColumnas["Orientación"]= "valor"; 
            diccionarioDeColumnas["No pedido"]= "valor"; 
            diccionarioDeColumnas["Fecha Entrega"]= "valor"; 
            diccionarioDeColumnas["ESP_SAE"]= "valor"; 
            diccionarioDeColumnas["Rizado"]= "valor"; 
            diccionarioDeColumnas["Perfil"]= "valor"; 
            diccionarioDeColumnas["Aditivos"]= "valor"; 
            diccionarioDeColumnas["Tipo de Mazo"]= "valor"; 
            diccionarioDeColumnas["Bastón_Espejo_Tina"]= "valor"; 
            diccionarioDeColumnas["Herramental"]= "valor"; 
            diccionarioDeColumnas["Fabricar"]= "valor"; 
            diccionarioDeColumnas["Temple"]= "valor"; 
            diccionarioDeColumnas["Horno"]= "valor"; 
            diccionarioDeColumnas["Teñido"]= "valor"; 
            diccionarioDeColumnas["Enfundado"]= "valor"; 
            diccionarioDeColumnas["Esp_Carretes"]= "valor"; 

            //Meter el valor actual del diccionario a la tabla de pedidos
            agregaRegistroEnTabla(); 

            //Agremamos tantos registros como queramos 
           
            
            

            
        }
        
    }
}
