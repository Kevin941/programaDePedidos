using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public FolderBrowserDialog dialogFolder = new FolderBrowserDialog();
        public System.Data.DataTable foundRowsTable;
        public System.Data.DataTable foundRowsTableClientes;
        public System.Data.DataTable foundRowsTableMezclas;
        public System.Data.DataTable clienteActualTable;
        public formVentanaInteractiva ventanaInteractiva; 
        ArrayList arregloClavesNoEncontradas = new ArrayList();
        int indiceDefinitivoPorIteracion = 0;
        string claveActual = "";
        string clienteActual = "";
        string ExcelFilePath=""; 

        ArrayList componentesMezcla = new ArrayList(); 
        public DataRow[] foundRows;
        public DataRow[] foundRowsMezclas;
        
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


        private void agregaRegistroEnTabla(int indicePedidosAntes)
        {
            //Primero se agrega el registro al diccionario 
            agregarRegistroAlDiccionario(indicePedidosAntes); 
            
            //Luego se agrega ese registro del diccionario a la tabla. Al final la tabla es más facil exportarla a excel. 
            agregarDiccionarioEnTabla(); 

            foreach (int indiceComponente in componentesMezcla)
            {
                agregarRegistroAlDiccionario(indicePedidosAntes, indiceComponente);
                agregarDiccionarioEnTabla(); 
            }

            //Se  limpia el arreglo para la siguiente iteración. 
            componentesMezcla = new ArrayList();
        }

        private void agregarRegistroAlDiccionario(int indicePedidosAntes, int indiceComponente)
        {
            diccionarioDeColumnas["Nombre del Cliente"] = foundRowsTable.Rows[indiceComponente]["nombreDelCliente"].ToString();
            diccionarioDeColumnas["Cantidad_Kg"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Cantidad"].ToString();

            //En caso de que las unidades se encuentren en LB se tendrá que hacer la conversión a KG
            if ((tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "LB")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lb")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "Lb")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lB"))
            {
                diccionarioDeColumnas["Cantidad_Kg"] = Convert.ToString(Convert.ToDouble(diccionarioDeColumnas["Cantidad_Kg"]) * 0.453592);
                diccionarioDeColumnas["Unidad_Original"] = "LB";
            }
            else
            {
                diccionarioDeColumnas["Unidad_Original"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString();
            }


            diccionarioDeColumnas["Calibre"] = foundRowsTable.Rows[indiceComponente]["D"].ToString();
            diccionarioDeColumnas["Color"] = foundRowsTable.Rows[indiceComponente]["E"].ToString();
            diccionarioDeColumnas["Material"] = foundRowsTable.Rows[indiceComponente]["C"].ToString();
            diccionarioDeColumnas["Resina"] = (foundRowsTable.Rows[indiceComponente]["L"].ToString()) + (foundRowsTable.Rows[indiceComponente]["O"].ToString());
            diccionarioDeColumnas["Clave"] = foundRowsTable.Rows[indiceComponente]["clave"].ToString();
            diccionarioDeColumnas["Corte"] = foundRowsTable.Rows[indiceComponente]["F"].ToString();
            diccionarioDeColumnas["Lubricante"] = foundRowsTable.Rows[indiceComponente]["AU"].ToString();
            diccionarioDeColumnas["Orientación"] = foundRowsTable.Rows[indiceComponente]["G"].ToString();
            diccionarioDeColumnas["No pedido"] = tablaPedidosAntes.Rows[indicePedidosAntes]["No pedido"].ToString();

            string fecha = tablaPedidosAntes.Rows[indicePedidosAntes]["Fecha Entrega"].ToString();
            if (fecha.Length > 10)
            {
                fecha = fecha.Substring(0, 10);
                fecha = fecha.Replace(".", "/");
            }
            diccionarioDeColumnas["Fecha Entrega"] = fecha;
            diccionarioDeColumnas["ESP_SAE"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Especificaciones"].ToString();
            diccionarioDeColumnas["Rizado"] = foundRowsTable.Rows[indiceComponente]["I"].ToString() +
                foundRowsTable.Rows[indiceComponente]["J"].ToString() +
                foundRowsTable.Rows[indiceComponente]["K"].ToString();
            diccionarioDeColumnas["Perfil"] = foundRowsTable.Rows[indiceComponente]["H"].ToString();
            diccionarioDeColumnas["Aditivos"] = foundRowsTable.Rows[indiceComponente]["N"].ToString();
            diccionarioDeColumnas["Tipo de Mazo"] = foundRowsTable.Rows[indiceComponente]["AN"].ToString();
            diccionarioDeColumnas["Bastón_Espejo_Tina"] =
                "Bastón " + foundRowsTable.Rows[indiceComponente]["Q"].ToString() +
                "Espejo " + foundRowsTable.Rows[indiceComponente]["P"].ToString() +
                "Tina " + foundRowsTable.Rows[indiceComponente]["R"].ToString();

            diccionarioDeColumnas["Herramental"] = foundRowsTable.Rows[indiceComponente]["V"].ToString() +
                foundRowsTable.Rows[indiceComponente]["W"].ToString();
            diccionarioDeColumnas["Fabricar"] = foundRowsTable.Rows[indiceComponente]["AC"].ToString();
            diccionarioDeColumnas["Temple"] = foundRowsTable.Rows[indiceComponente]["AP"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AQ"].ToString();
            diccionarioDeColumnas["Horno"] = foundRowsTable.Rows[indiceComponente]["AS"].ToString();
            diccionarioDeColumnas["Teñido"] = foundRowsTable.Rows[indiceComponente]["AT"].ToString();
            diccionarioDeColumnas["Enfundado"] = foundRowsTable.Rows[indiceComponente]["AX"].ToString();
            diccionarioDeColumnas["Esp_Carretes"] = foundRowsTable.Rows[indiceComponente]["AD"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AE"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AF"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AG"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AH"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AI"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AJ"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AK"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AL"].ToString() +
                foundRowsTable.Rows[indiceComponente]["AM"].ToString();
        }

        private void agregarDiccionarioEnTabla()
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

        private void agregarRegistroAlDiccionario(int indicePedidosAntes)
        {
            diccionarioDeColumnas["Nombre del Cliente"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["nombreDelCliente"].ToString();
            diccionarioDeColumnas["Cantidad_Kg"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Cantidad"].ToString();

            //En caso de que las unidades se encuentren en LB se tendrá que hacer la conversión a KG
            if ((tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "LB")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lb")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "Lb")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lB"))
            {
                diccionarioDeColumnas["Cantidad_Kg"] = Convert.ToString(Convert.ToDouble(diccionarioDeColumnas["Cantidad_Kg"]) * 0.453592);
                diccionarioDeColumnas["Unidad_Original"] = "LB";
            }
            else
            {
                diccionarioDeColumnas["Unidad_Original"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString();
            }


            diccionarioDeColumnas["Calibre"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["D"].ToString();
            diccionarioDeColumnas["Color"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["E"].ToString();
            diccionarioDeColumnas["Material"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["C"].ToString();
            diccionarioDeColumnas["Resina"] = (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["L"].ToString()) + (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["O"].ToString());
            diccionarioDeColumnas["Clave"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["clave"].ToString();
            diccionarioDeColumnas["Corte"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["F"].ToString();
            diccionarioDeColumnas["Lubricante"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AU"].ToString();
            diccionarioDeColumnas["Orientación"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["G"].ToString();
            diccionarioDeColumnas["No pedido"] = tablaPedidosAntes.Rows[indicePedidosAntes]["No pedido"].ToString();

            string fecha = tablaPedidosAntes.Rows[indicePedidosAntes]["Fecha Entrega"].ToString();
            if (fecha.Length > 10)
            {
                fecha = fecha.Substring(0, 10);
                fecha = fecha.Replace(".", "/");
            }
            diccionarioDeColumnas["Fecha Entrega"] = fecha;
            diccionarioDeColumnas["ESP_SAE"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Especificaciones"].ToString();
            diccionarioDeColumnas["Rizado"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["I"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["J"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["K"].ToString();
            diccionarioDeColumnas["Perfil"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["H"].ToString();
            diccionarioDeColumnas["Aditivos"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["N"].ToString();
            diccionarioDeColumnas["Tipo de Mazo"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AN"].ToString();
            diccionarioDeColumnas["Bastón_Espejo_Tina"] =
                "Bastón " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["Q"].ToString() +
                "Espejo " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["P"].ToString() +
                "Tina " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["R"].ToString();

            diccionarioDeColumnas["Herramental"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["V"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["W"].ToString();
            diccionarioDeColumnas["Fabricar"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AC"].ToString();
            diccionarioDeColumnas["Temple"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AP"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AQ"].ToString();
            diccionarioDeColumnas["Horno"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AS"].ToString();
            diccionarioDeColumnas["Teñido"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AT"].ToString();
            diccionarioDeColumnas["Enfundado"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AX"].ToString();
            diccionarioDeColumnas["Esp_Carretes"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AD"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AE"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AF"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AG"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AH"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AI"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AJ"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AK"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AL"].ToString() +
                foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AM"].ToString();
        }

       
        public void generarExcelDesdeDataTable(DataTable Tbl)
        {
            
            
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

            //Se ajusta el alto de las filas
            workSheet.UsedRange.EntireRow.RowHeight = 15;

            //Se dibuja el borde de las celdas
            Excel.Range _range;
            _range = workSheet.UsedRange;
            //Get the borders collection.
            Excel.Borders borders = _range.Borders;
            //Set the hair lines style.
            borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Weight = 2d;


            //Se dibuja el borde gordo de las celdas primarias
            _range = workSheet.get_Range("A1", "Z1");
            //Get the borders collection.
            borders = _range.Borders;
            //Set the hair lines style.
            borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Weight = 3d;
            _range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
               
            
            // check fielpath
            if (ExcelFilePath != null && ExcelFilePath != "")
            {
                try
                {   
                    workSheet.SaveAs(ExcelFilePath);
                    excelApp.Quit();
                    MessageBox.Show(new Form() { TopMost = true }, "Archivo Guardado con éxito");
                }
                catch (Exception ex)
                {
                    throw new Exception("El archivo no se ha guardado. Verifica la dirección proporcionada.\n"
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
            MessageBox.Show(new Form() { TopMost = true }, ex.Message);
        }
    }

        public bool getDireccionDestino()
        {
            try
            {
                while (dialogFolder.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Error al intentar abrir la ruta especificada");
                    return false;

                }

                ExcelFilePath = dialogFolder.SelectedPath + "\\archivoPedidos.xlsx";
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false; 
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

        public int getPedidoActualRowInTable(DataRow fila)
        {
            clienteActualTable = new System.Data.DataTable();
            clienteActualTable = tablaPedidosAntes.Clone();
            clienteActualTable.ImportRow(fila);

            return clienteActualTable.Rows.Count;
        }

        public int getRegistrosByClaveAndClienteEnEspecificaciones(string clave, string cliente)
        {
            //Se copian los nombre de las columnas en la tabla foundRowsTable
            foundRowsTableClientes = new System.Data.DataTable();
            foundRowsTableClientes = tablaEspecificaciones.Clone();

            foundRows = tablaEspecificaciones.Select(
                "clave like " + "'%" + clave + "%'" + 
                " AND " + 
                "nombreDelCliente like " + "'%" + cliente + "%'"
                );
            foreach (DataRow row in foundRows)
            {
                foundRowsTableClientes.ImportRow(row);
            }
            return foundRowsTableClientes.Rows.Count; 
        }
        public void getTablaDePedidos()
        {            
            //Si funciona el showDialog aunque se invoque este método desde un hilo
            //formMenuPrincipal ventana = new formMenuPrincipal();
            //ventana.ShowDialog(); 
            //Creamos una nueva tabla
            tablaPedidosDespues = new DataTable();
            
            //Genera las columnas para la tabla
            generarColumnasParaLaTabla(); 


            //Aquí va el procedimiento para cada uno de los índices de la tabla de Pedidos anterior
            llenarValoresDeTablaPedidosDespues();
            generarExcelDesdeDataTable(tablaPedidosDespues); 

            //mostrarReporte(); 
                
        }

        private void llenarValoresDeTablaPedidosDespues()
        {
            for (int x = 0; x < tablaPedidosAntes.Rows.Count; x++)
            {


                //Se utiliza el número del indice en caso de que el nombre de la columna se modifique. 
                claveActual = tablaPedidosAntes.Rows[x][6].ToString();  //claveActual = tablaPedidosAntes.Rows[x]["Clave"].ToString(); 
                clienteActual = tablaPedidosAntes.Rows[x][0].ToString(); //clienteActual = tablaPedidosAntes.Rows[0]["Nombre del Cliente"].ToString();


                //Se obtienen todos los registros encontrados para la clave actual y se almacena en "foundRowsTable"
                int encontrados = getRegistrosByClaveEnEspecificaciones(claveActual);
                if (encontrados > 0)
                {


                    //Buscar en esa tabla los valores con el cliente específico y guardarlos en la tabla "foundRowsTableClientes"
                    encontrados = getRegistrosByClaveAndClienteEnEspecificaciones(claveActual, clienteActual);

                    //Caso 1: Se ha encontrado un cliente para esa clave
                    if (encontrados == 1)
                    {
                        //Tomar ese único registro encotrado 
                        //MessageBox.Show(new Form() { TopMost = true }, "Caso 1: Correcto. Se ha encontrado la clave y un solo cliente.");
                        indiceDefinitivoPorIteracion = 0; 
                    }
                    else
                        //Caso 2: Se ha encontrado más de un cliente(el mismo) para esa clave. 
                        if (encontrados > 1)
                        {
                            if (!verificarMezcla())
                            {
                                //Mostrar ventana interactiva 
                                mostrarVentanaInteractiva("Caso 2: Se ha encontrado más de un cliente '"+clienteActual + "'la clave. (Posible Mezcla) " + claveActual, x);
                                //MessageBox.Show(new Form() { TopMost = true }, "El indice seleccionado es: " + ventanaInteractiva.IndiceSeleccionado);
                                indiceDefinitivoPorIteracion = ventanaInteractiva.IndiceSeleccionado; 
                            }
                        }
                        //Caso 3: Clave encontrada; pero cliente no encontrado. 
                        else
                            if (encontrados == 0)
                            {
                                if (foundRowsTable.Rows.Count == 1)
                                {
                                    indiceDefinitivoPorIteracion = 0;
                                }
                                else
                                {
                                    //En este caso verificar si lo que seleccionó el cliente es una mezcla
                                    mostrarVentanaInteractiva("Caso 3: No se ha encontrado ningún cliente '" + clienteActual + "' para la clave " + claveActual, x);
                                    indiceDefinitivoPorIteracion = ventanaInteractiva.IndiceSeleccionado;
                                }
                                //Cuando se cierre el diálogo se debera de acceder al índice seleccionado por el cliente en la tabla de "foundRowsTable"
                            }

                }
                //Caso 4: No se encontró la clave
                else
                {
                    //MessageBox.Show(new Form() { TopMost = true }, "Caso 4: No se ha encontrado la clave"); 
                    arregloClavesNoEncontradas.Add(claveActual);
                    return;
                }


                agregaRegistroEnTabla(x); 
                

            }

        }

        private bool verificarMezcla()
        {
            componentesMezcla = new ArrayList(); 
            bool esMezcla = false; 
            //Se inicializa un string con una longitud muy larga. 
            string claveMasCorta = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"; 
            //Se busca la clave con la menor longitud 
            int indicePosibleMezcla = 0; 
            for (int row = 0; row < foundRows.Length; row++)
            {

                if (foundRows[row][0].ToString().Length < claveMasCorta.Length)
                {


                    claveMasCorta = foundRows[row][0].ToString();
                    indicePosibleMezcla = row; 
                }
            }
               

            //MessageBox.Show(new Form() { TopMost = true }, "La clave maestra de la mezcla es: " + claveMasCorta);

            string claveAnalizada; 
            //Buscar esa varible en la tabla con un espacio seguido de cualquiera de los siguientes caracteres: A, L, R, 3D, S 
            for (int row = 0; row < foundRowsTable.Rows.Count; row++)
            {
                bool matches; 
                claveAnalizada = foundRowsTable.Rows[row]["clave"].ToString();

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)*A(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)*B(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)*C(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)*D(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)*L(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)*R(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)*3D(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)*S(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }               
            }
            if (esMezcla)
            {
                indiceDefinitivoPorIteracion = indicePosibleMezcla; 
            }
            return esMezcla; 
            
        }

        private void mostrarVentanaInteractiva(string aviso, int indice)
        {
            MessageBox.Show(new Form() { TopMost = true }, aviso); 
            getPedidoActualRowInTable(tablaPedidosAntes.Rows[indice]);
            
            ventanaInteractiva = new formVentanaInteractiva(foundRowsTable, clienteActualTable);
            ventanaInteractiva.ShowDialog();
        }

        private void mostrarReporte()
        {
            formReportePrograma ventanaReporte = new formReportePrograma(arregloClavesNoEncontradas);
            ventanaReporte.ShowDialog(); 
        }
        
    }
}
