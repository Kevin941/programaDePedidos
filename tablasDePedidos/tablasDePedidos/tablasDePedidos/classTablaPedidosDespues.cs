using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string progreso = ""; 
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
        public string nombreDelArchivo; 
        ArrayList arregloClavesNoEncontradas = new ArrayList();
        int indiceDefinitivoPorIteracion = 0;
        string claveActual = "";
        string clienteActual = "";
        string ExcelFilePath="";
        string rangoEncabezadoInicial;
        string rangoEncabezadoFinal;
        int cantidadComponentesMezcla = 0; 

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
            tablaPedidosDespues.Columns.Add("TODO", typeof(string));
            tablaPedidosDespues.Columns.Add("ESP_SAE", typeof(string));
            tablaPedidosDespues.Columns.Add("Rizado", typeof(string));
            tablaPedidosDespues.Columns.Add("Perfil", typeof(string));
            tablaPedidosDespues.Columns.Add("Aditivos", typeof(string));
            tablaPedidosDespues.Columns.Add("Pigmentos", typeof(string));
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
            diccionarioDeColumnas.Add("TODO", "");
            diccionarioDeColumnas.Add("ESP_SAE", "");
            diccionarioDeColumnas.Add("Rizado", "");
            diccionarioDeColumnas.Add("Perfil", "");
            diccionarioDeColumnas.Add("Aditivos", "");
            diccionarioDeColumnas.Add("Pigmentos", "");
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
                cantidadComponentesMezcla++; 
            }
            
            //Se  limpia el arreglo para la siguiente iteración. 
            componentesMezcla = new ArrayList();
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
               diccionarioDeColumnas["TODO"],
               diccionarioDeColumnas["ESP_SAE"],
               diccionarioDeColumnas["Rizado"],
               diccionarioDeColumnas["Perfil"],
               diccionarioDeColumnas["Aditivos"],
               diccionarioDeColumnas["Pigmentos"],
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
            limpiarDiccionario();
            if (indiceDefinitivoPorIteracion == -1)
            {
                diccionarioDeColumnas["Clave"] = tablaPedidosAntes.Rows[indicePedidosAntes]["clave"].ToString();
                //diccionarioDeColumnas["Nombre del Cliente"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["nombreDelCliente"].ToString();
                try
                {
                    diccionarioDeColumnas["Cantidad_Kg"] = (Math.Round(Convert.ToDouble(tablaPedidosAntes.Rows[indicePedidosAntes]["Cantidad"].ToString()), 2)).ToString();
                }
                catch
                {

                }
                //En caso de que las unidades se encuentren en LB se tendrá que hacer la conversión a KG
                if ((tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "LB")
                    || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lb")
                    || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "Lb")
                    || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lB"))
                {
                    diccionarioDeColumnas["Cantidad_Kg"] = Convert.ToString(Math.Round(Convert.ToDouble(diccionarioDeColumnas["Cantidad_Kg"]) * 0.453592, 2));
                    diccionarioDeColumnas["Unidad_Original"] = "LB";
                }
                else
                {
                    diccionarioDeColumnas["Unidad_Original"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString();
                }

                /*
                diccionarioDeColumnas["Calibre"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["D"].ToString();
                diccionarioDeColumnas["Color"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["E"].ToString();
                diccionarioDeColumnas["Pigmentos"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["O"].ToString();
                diccionarioDeColumnas["Material"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["C"].ToString();
                diccionarioDeColumnas["Resina"] = (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["L"].ToString());
                diccionarioDeColumnas["Clave"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["clave"].ToString();
                diccionarioDeColumnas["Corte"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["F"].ToString();
                diccionarioDeColumnas["Lubricante"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AU"].ToString();
                diccionarioDeColumnas["Orientación"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["G"].ToString();
                 * */
                diccionarioDeColumnas["No pedido"] = tablaPedidosAntes.Rows[indicePedidosAntes]["No pedido"].ToString();

                string fecha = tablaPedidosAntes.Rows[indicePedidosAntes]["Fecha Entrega"].ToString();
                if (fecha.Length > 10)
                {
                    fecha = fecha.Substring(0, 10);
                    fecha = fecha.Replace(".", "/");
                }
                diccionarioDeColumnas["Fecha Entrega"] = fecha;
                diccionarioDeColumnas["ESP_SAE"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Especificaciones"].ToString();
                /*
                diccionarioDeColumnas["Rizado"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["I"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["J"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["K"].ToString();
                diccionarioDeColumnas["Perfil"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["H"].ToString();
                diccionarioDeColumnas["Aditivos"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["N"].ToString();
                diccionarioDeColumnas["Tipo de Mazo"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AN"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AO"].ToString();
                string cadenaBastonEspejoTina = "";
                if (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["Q"].ToString() != "")
                {
                    cadenaBastonEspejoTina += "Bastón: " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["Q"].ToString();
                }

                if (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["P"].ToString() != "")
                {
                    cadenaBastonEspejoTina += ", Espejo: " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["P"].ToString();
                }

                if (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["R"].ToString() != "")
                {
                    cadenaBastonEspejoTina += ", Tina: " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["R"].ToString();
                }

                diccionarioDeColumnas["Bastón_Espejo_Tina"] = cadenaBastonEspejoTina;

                diccionarioDeColumnas["Herramental"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["V"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["W"].ToString();
                diccionarioDeColumnas["Fabricar"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AC"].ToString();
                diccionarioDeColumnas["Temple"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AP"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AQ"].ToString();
                diccionarioDeColumnas["Horno"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AR"].ToString() + ", " +
                     foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AS"].ToString();
                diccionarioDeColumnas["Teñido"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AT"].ToString();
                diccionarioDeColumnas["Enfundado"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AX"].ToString();
                diccionarioDeColumnas["Esp_Carretes"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AD"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AE"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AF"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AG"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AH"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AI"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AJ"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AK"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AL"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AM"].ToString();
                 * */
                diccionarioDeColumnas["TODO"] =
                diccionarioDeColumnas["Perfil"] + ", " +
                diccionarioDeColumnas["Rizado"] + ", " +
                diccionarioDeColumnas["Aditivos"] + ", " +
                diccionarioDeColumnas["Pigmentos"] + ", " +
                diccionarioDeColumnas["Herramental"] + ", " +
                diccionarioDeColumnas["Tipo de Mazo"] + ", " +
                diccionarioDeColumnas["Bastón_Espejo_Tina"] + ", " +
                diccionarioDeColumnas["Temple"] + ", " +
                diccionarioDeColumnas["Horno"] + ", " +
                diccionarioDeColumnas["Teñido"] + ", " +
                diccionarioDeColumnas["Enfundado"] + ", " +
                diccionarioDeColumnas["Esp_Carretes"] + ", ";

                quitarComas();
            }
            else
            {
                if (foundRowsTable.Rows.Count <= indiceDefinitivoPorIteracion)
                {
                    return;
                }
                diccionarioDeColumnas["Nombre del Cliente"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["nombreDelCliente"].ToString();
                try
                {
                    diccionarioDeColumnas["Cantidad_Kg"] = (Math.Round(Convert.ToDouble(tablaPedidosAntes.Rows[indicePedidosAntes]["Cantidad"].ToString()), 2)).ToString();
                }
                catch
                {

                }
                //En caso de que las unidades se encuentren en LB se tendrá que hacer la conversión a KG
                if ((tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "LB")
                    || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lb")
                    || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "Lb")
                    || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lB"))
                {
                    diccionarioDeColumnas["Cantidad_Kg"] = Convert.ToString(Math.Round(Convert.ToDouble(diccionarioDeColumnas["Cantidad_Kg"]) * 0.453592, 2));
                    diccionarioDeColumnas["Unidad_Original"] = "LB";
                }
                else
                {
                    diccionarioDeColumnas["Unidad_Original"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString();
                }


                diccionarioDeColumnas["Calibre"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["D"].ToString();
                diccionarioDeColumnas["Color"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["E"].ToString();
                diccionarioDeColumnas["Pigmentos"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["O"].ToString();
                diccionarioDeColumnas["Material"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["C"].ToString();
                diccionarioDeColumnas["Resina"] = (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["L"].ToString());
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
                diccionarioDeColumnas["Rizado"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["I"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["J"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["K"].ToString();
                diccionarioDeColumnas["Perfil"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["H"].ToString();
                diccionarioDeColumnas["Aditivos"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["N"].ToString();
                diccionarioDeColumnas["Tipo de Mazo"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AN"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AO"].ToString();
                string cadenaBastonEspejoTina = "";
                if (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["Q"].ToString() != "")
                {
                    cadenaBastonEspejoTina += "Bastón: " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["Q"].ToString();
                }

                if (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["P"].ToString() != "")
                {
                    cadenaBastonEspejoTina += ", Espejo: " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["P"].ToString();
                }

                if (foundRowsTable.Rows[indiceDefinitivoPorIteracion]["R"].ToString() != "")
                {
                    cadenaBastonEspejoTina += ", Tina: " + foundRowsTable.Rows[indiceDefinitivoPorIteracion]["R"].ToString();
                }

                diccionarioDeColumnas["Bastón_Espejo_Tina"] = cadenaBastonEspejoTina;

                diccionarioDeColumnas["Herramental"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["V"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["W"].ToString();
                diccionarioDeColumnas["Fabricar"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AC"].ToString();
                diccionarioDeColumnas["Temple"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AP"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AQ"].ToString();
                diccionarioDeColumnas["Horno"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AR"].ToString() + ", " +
                     foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AS"].ToString();
                diccionarioDeColumnas["Teñido"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AT"].ToString();
                diccionarioDeColumnas["Enfundado"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AX"].ToString();
                diccionarioDeColumnas["Esp_Carretes"] = foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AD"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AE"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AF"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AG"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AH"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AI"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AJ"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AK"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AL"].ToString() + ", " +
                    foundRowsTable.Rows[indiceDefinitivoPorIteracion]["AM"].ToString();
                string stringHorneado = "";
                if ((diccionarioDeColumnas["Horno"] != "") && (diccionarioDeColumnas["Horno"] != ", "))
                {
                    stringHorneado = "Horneado: " + diccionarioDeColumnas["Horno"]; 
                }
                diccionarioDeColumnas["TODO"] =
                diccionarioDeColumnas["Perfil"] + ", " +
                diccionarioDeColumnas["Rizado"] + ", " +
                diccionarioDeColumnas["Aditivos"] + ", " +
                diccionarioDeColumnas["Pigmentos"] + ", " +
                diccionarioDeColumnas["Herramental"] + ", " +
                diccionarioDeColumnas["Tipo de Mazo"] + ", " +
                diccionarioDeColumnas["Bastón_Espejo_Tina"] + ", " +
                diccionarioDeColumnas["Temple"] + ", " +
                stringHorneado + ", " +
                diccionarioDeColumnas["Teñido"] + ", " +
                diccionarioDeColumnas["Enfundado"] + ", " +
                diccionarioDeColumnas["Esp_Carretes"] + ", ";

                quitarComas();
            }
        }

        private void limpiarDiccionario()
        {
            diccionarioDeColumnas["Nombre del Cliente"] = "";
            diccionarioDeColumnas["Cantidad_Kg"] = "";
            diccionarioDeColumnas["Unidad_Original"] = "";
            diccionarioDeColumnas["Calibre"] = "";
            diccionarioDeColumnas["Color"] = "";
            diccionarioDeColumnas["Material"] = "";
            diccionarioDeColumnas["Resina"] = "";
            diccionarioDeColumnas["Clave"] = "";
            diccionarioDeColumnas["Corte"] = "";
            diccionarioDeColumnas["Lubricante"] = "";
            diccionarioDeColumnas["Orientación"] = "";
            diccionarioDeColumnas["No pedido"] = "";
            diccionarioDeColumnas["Fecha Entrega"] = "";
            diccionarioDeColumnas["TODO"] = "";
            diccionarioDeColumnas["ESP_SAE"] = "";
            diccionarioDeColumnas["Rizado"] = "";
            diccionarioDeColumnas["Perfil"] = "";
            diccionarioDeColumnas["Aditivos"] = "";
            diccionarioDeColumnas["Pigmentos"] = "";
            diccionarioDeColumnas["Tipo de Mazo"] = "";
            diccionarioDeColumnas["Bastón_Espejo_Tina"] = "";
            diccionarioDeColumnas["Herramental"] = "";
            diccionarioDeColumnas["Fabricar"] = "";
            diccionarioDeColumnas["Temple"] = "";
            diccionarioDeColumnas["Horno"] = "";
            diccionarioDeColumnas["Teñido"] = "";
            diccionarioDeColumnas["Enfundado"] = "";
            diccionarioDeColumnas["Esp_Carretes"] = ""; 
        }

        private void quitarComas()
        {
            for (int x = 0; x < 30; x++)
            {
                diccionarioDeColumnas["Nombre del Cliente"] = diccionarioDeColumnas["Nombre del Cliente"].Replace(",,", ",");
                diccionarioDeColumnas["Nombre del Cliente"] = diccionarioDeColumnas["Nombre del Cliente"].Replace(", ,", ",");
                diccionarioDeColumnas["Cantidad_Kg"] = diccionarioDeColumnas["Cantidad_Kg"].Replace(",,", ",");
                diccionarioDeColumnas["Cantidad_Kg"] = diccionarioDeColumnas["Cantidad_Kg"].Replace(", ,", ",");
                diccionarioDeColumnas["Unidad_Original"] = diccionarioDeColumnas["Unidad_Original"].Replace(",,", ",");
                diccionarioDeColumnas["Unidad_Original"] = diccionarioDeColumnas["Unidad_Original"].Replace(", ,", ",");
                diccionarioDeColumnas["Calibre"] = diccionarioDeColumnas["Calibre"].Replace(",,", ",");
                diccionarioDeColumnas["Calibre"] = diccionarioDeColumnas["Calibre"].Replace(", ,", ",");
                diccionarioDeColumnas["Color"] = diccionarioDeColumnas["Color"].Replace(",,", ",");
                diccionarioDeColumnas["Color"] = diccionarioDeColumnas["Color"].Replace(", ,", ",");
                diccionarioDeColumnas["Pigmentos"] = diccionarioDeColumnas["Pigmentos"].Replace(",,", ",");
                diccionarioDeColumnas["Pigmentos"] = diccionarioDeColumnas["Pigmentos"].Replace(", ,", ",");
                diccionarioDeColumnas["Material"] = diccionarioDeColumnas["Material"].Replace(",,", ",");
                diccionarioDeColumnas["Material"] = diccionarioDeColumnas["Material"].Replace(", ,", ",");
                diccionarioDeColumnas["Resina"] = diccionarioDeColumnas["Resina"].Replace(",,", ",");
                diccionarioDeColumnas["Resina"] = diccionarioDeColumnas["Resina"].Replace(", ,", ",");
                diccionarioDeColumnas["Clave"] = diccionarioDeColumnas["Clave"].Replace(",,", ",");
                diccionarioDeColumnas["Clave"] = diccionarioDeColumnas["Clave"].Replace(", ,", ",");
                diccionarioDeColumnas["Corte"] = diccionarioDeColumnas["Corte"].Replace(",,", ",");
                diccionarioDeColumnas["Corte"] = diccionarioDeColumnas["Corte"].Replace(", ,", ",");
                diccionarioDeColumnas["Lubricante"] = diccionarioDeColumnas["Lubricante"].Replace(",,", ",");
                diccionarioDeColumnas["Lubricante"] = diccionarioDeColumnas["Lubricante"].Replace(", ,", ",");
                diccionarioDeColumnas["Orientación"] = diccionarioDeColumnas["Orientación"].Replace(",,", ",");
                diccionarioDeColumnas["Orientación"] = diccionarioDeColumnas["Orientación"].Replace(", ,", ",");
                diccionarioDeColumnas["No pedido"] = diccionarioDeColumnas["No pedido"].Replace(",,", ",");
                diccionarioDeColumnas["No pedido"] = diccionarioDeColumnas["No pedido"].Replace(", ,", ",");
                diccionarioDeColumnas["Fecha Entrega"] = diccionarioDeColumnas["Fecha Entrega"].Replace(",,", ",");
                diccionarioDeColumnas["Fecha Entrega"] = diccionarioDeColumnas["Fecha Entrega"].Replace(", ,", ",");
                diccionarioDeColumnas["TODO"] = diccionarioDeColumnas["TODO"].Replace(",,", ",");                
                diccionarioDeColumnas["TODO"] = diccionarioDeColumnas["TODO"].Replace(", ,", ",");
                diccionarioDeColumnas["ESP_SAE"] = diccionarioDeColumnas["ESP_SAE"].Replace(",,", ",");
                diccionarioDeColumnas["ESP_SAE"] = diccionarioDeColumnas["ESP_SAE"].Replace(", ,", ",");
                diccionarioDeColumnas["Rizado"] = diccionarioDeColumnas["Rizado"].Replace(",,", ",");
                diccionarioDeColumnas["Rizado"] = diccionarioDeColumnas["Rizado"].Replace(", ,", ",");
                diccionarioDeColumnas["Perfil"] = diccionarioDeColumnas["Perfil"].Replace(",,", ",");
                diccionarioDeColumnas["Perfil"] = diccionarioDeColumnas["Perfil"].Replace(", ,", ",");
                diccionarioDeColumnas["Aditivos"] = diccionarioDeColumnas["Aditivos"].Replace(",,", ",");
                diccionarioDeColumnas["Aditivos"] = diccionarioDeColumnas["Aditivos"].Replace(", ,", ",");
                diccionarioDeColumnas["Tipo de Mazo"] = diccionarioDeColumnas["Tipo de Mazo"].Replace(",,", ",");
                diccionarioDeColumnas["Tipo de Mazo"] = diccionarioDeColumnas["Tipo de Mazo"].Replace(", ,", ",");
                diccionarioDeColumnas["Bastón_Espejo_Tina"] = diccionarioDeColumnas["Bastón_Espejo_Tina"].Replace(",,", ",");
                diccionarioDeColumnas["Bastón_Espejo_Tina"] = diccionarioDeColumnas["Bastón_Espejo_Tina"].Replace(", ,", ",");
                diccionarioDeColumnas["Herramental"] = diccionarioDeColumnas["Herramental"].Replace(",,", ",");
                diccionarioDeColumnas["Herramental"] = diccionarioDeColumnas["Herramental"].Replace(", ,", ",");
                diccionarioDeColumnas["Fabricar"] = diccionarioDeColumnas["Fabricar"].Replace(",,", ",");
                diccionarioDeColumnas["Fabricar"] = diccionarioDeColumnas["Fabricar"].Replace(", ,", ",");
                diccionarioDeColumnas["Temple"] = diccionarioDeColumnas["Temple"].Replace(",,", ",");
                diccionarioDeColumnas["Temple"] = diccionarioDeColumnas["Temple"].Replace(", ,", ",");
                diccionarioDeColumnas["Horno"] = diccionarioDeColumnas["Horno"].Replace(",,", ",");
                diccionarioDeColumnas["Horno"] = diccionarioDeColumnas["Horno"].Replace(", ,", ",");
                diccionarioDeColumnas["Teñido"] = diccionarioDeColumnas["Teñido"].Replace(",,", ",");
                diccionarioDeColumnas["Teñido"] = diccionarioDeColumnas["Teñido"].Replace(", ,", ",");
                diccionarioDeColumnas["Enfundado"] = diccionarioDeColumnas["Enfundado"].Replace(",,", ",");
                diccionarioDeColumnas["Enfundado"] = diccionarioDeColumnas["Enfundado"].Replace(", ,", ",");
                diccionarioDeColumnas["Esp_Carretes"] = diccionarioDeColumnas["Esp_Carretes"].Replace(",,", ",");
                diccionarioDeColumnas["Esp_Carretes"] = diccionarioDeColumnas["Esp_Carretes"].Replace(", ,", ",");
            }

            	if(diccionarioDeColumnas["Nombre del Cliente"]==", "){
                	diccionarioDeColumnas["Nombre del Cliente"] = ""; 
                }
                if(diccionarioDeColumnas["Cantidad_Kg"]==", "){
                	diccionarioDeColumnas["Cantidad_Kg"] = ""; 
                }
                if(diccionarioDeColumnas["Cantidad_Kg"]==", "){
                	diccionarioDeColumnas["Cantidad_Kg"] = ""; 
                }
                if(diccionarioDeColumnas["Unidad_Original"]==", "){
                	diccionarioDeColumnas["Unidad_Original"] = ""; 
                }
                if(diccionarioDeColumnas["Unidad_Original"]==", "){
                	diccionarioDeColumnas["Unidad_Original"] = ""; 
                }
                if(diccionarioDeColumnas["Calibre"]==", "){
                	diccionarioDeColumnas["Calibre"] = ""; 
                }
                if(diccionarioDeColumnas["Calibre"]==", "){
                	diccionarioDeColumnas["Calibre"] = ""; 
                }
                if(diccionarioDeColumnas["Color"]==", "){
                	diccionarioDeColumnas["Color"] = ""; 
                }
                if(diccionarioDeColumnas["Color"]==", "){
                	diccionarioDeColumnas["Color"] = ""; 
                }
                if(diccionarioDeColumnas["Pigmentos"]==", "){
                	diccionarioDeColumnas["Pigmentos"] = ""; 
                }
                if(diccionarioDeColumnas["Pigmentos"]==", "){
                	diccionarioDeColumnas["Pigmentos"] = ""; 
                }
                if(diccionarioDeColumnas["Material"]==", "){
                	diccionarioDeColumnas["Material"] = ""; 
                }
                if(diccionarioDeColumnas["Material"]==", "){
                	diccionarioDeColumnas["Material"] = ""; 
                }
                if(diccionarioDeColumnas["Resina"]==", "){
                	diccionarioDeColumnas["Resina"] = ""; 
                }
                if(diccionarioDeColumnas["Resina"]==", "){
                	diccionarioDeColumnas["Resina"] = ""; 
                }
                if(diccionarioDeColumnas["Clave"]==", "){
                	diccionarioDeColumnas["Clave"] = ""; 
                }
                if(diccionarioDeColumnas["Clave"]==", "){
                	diccionarioDeColumnas["Clave"] = ""; 
                }
                if(diccionarioDeColumnas["Corte"]==", "){
                	diccionarioDeColumnas["Corte"] = ""; 
                }
                if(diccionarioDeColumnas["Corte"]==", "){
                	diccionarioDeColumnas["Corte"] = ""; 
                }
                if(diccionarioDeColumnas["Lubricante"]==", "){
                	diccionarioDeColumnas["Lubricante"] = ""; 
                }
                if(diccionarioDeColumnas["Lubricante"]==", "){
                	diccionarioDeColumnas["Lubricante"] = ""; 
                }
                if(diccionarioDeColumnas["Orientación"]==", "){
                	diccionarioDeColumnas["Orientación"] = ""; 
                }
                if(diccionarioDeColumnas["Orientación"]==", "){
                	diccionarioDeColumnas["Orientación"] = ""; 
                }
                if(diccionarioDeColumnas["No pedido"]==", "){
                	diccionarioDeColumnas["No pedido"] = ""; 
                }
                if(diccionarioDeColumnas["No pedido"]==", "){
                	diccionarioDeColumnas["No pedido"] = ""; 
                }
                if(diccionarioDeColumnas["Fecha Entrega"]==", "){
                	diccionarioDeColumnas["Fecha Entrega"] = ""; 
                }
                if(diccionarioDeColumnas["Fecha Entrega"]==", "){
                	diccionarioDeColumnas["Fecha Entrega"] = ""; 
                }
                if(diccionarioDeColumnas["TODO"]==", "){
                	diccionarioDeColumnas["TODO"] = ""; 
                }
                if(diccionarioDeColumnas["TODO"]==", "){
                	diccionarioDeColumnas["TODO"] = ""; 
                }
                if(diccionarioDeColumnas["ESP_SAE"]==", "){
                	diccionarioDeColumnas["ESP_SAE"] = ""; 
                }
                if(diccionarioDeColumnas["ESP_SAE"]==", "){
                	diccionarioDeColumnas["ESP_SAE"] = ""; 
                }
                if(diccionarioDeColumnas["Rizado"]==", "){
                	diccionarioDeColumnas["Rizado"] = ""; 
                }
                if(diccionarioDeColumnas["Rizado"]==", "){
                	diccionarioDeColumnas["Rizado"] = ""; 
                }
                if(diccionarioDeColumnas["Perfil"]==", "){
                	diccionarioDeColumnas["Perfil"] = ""; 
                }
                if(diccionarioDeColumnas["Perfil"]==", "){
                	diccionarioDeColumnas["Perfil"] = ""; 
                }
                if(diccionarioDeColumnas["Aditivos"]==", "){
                	diccionarioDeColumnas["Aditivos"] = ""; 
                }
                if(diccionarioDeColumnas["Aditivos"]==", "){
                	diccionarioDeColumnas["Aditivos"] = ""; 
                }
                if(diccionarioDeColumnas["Tipo de Mazo"]==", "){
                	diccionarioDeColumnas["Tipo de Mazo"] = ""; 
                }
                if(diccionarioDeColumnas["Tipo de Mazo"]==", "){
                	diccionarioDeColumnas["Tipo de Mazo"] = ""; 
                }
                if(diccionarioDeColumnas["Bastón_Espejo_Tina"]==", "){
                	diccionarioDeColumnas["Bastón_Espejo_Tina"] = ""; 
                }
                if(diccionarioDeColumnas["Bastón_Espejo_Tina"]==", "){
                	diccionarioDeColumnas["Bastón_Espejo_Tina"] = ""; 
                }
                if(diccionarioDeColumnas["Herramental"]==", "){
                	diccionarioDeColumnas["Herramental"] = ""; 
                }
                if(diccionarioDeColumnas["Herramental"]==", "){
                	diccionarioDeColumnas["Herramental"] = ""; 
                }
                if(diccionarioDeColumnas["Fabricar"]==", "){
                	diccionarioDeColumnas["Fabricar"] = ""; 
                }
                if(diccionarioDeColumnas["Fabricar"]==", "){
                	diccionarioDeColumnas["Fabricar"] = ""; 
                }
                if(diccionarioDeColumnas["Temple"]==", "){
                	diccionarioDeColumnas["Temple"] = ""; 
                }
                if(diccionarioDeColumnas["Temple"]==", "){
                	diccionarioDeColumnas["Temple"] = ""; 
                }
                if(diccionarioDeColumnas["Horno"]==", "){
                	diccionarioDeColumnas["Horno"] = ""; 
                }
                if(diccionarioDeColumnas["Horno"]==", "){
                	diccionarioDeColumnas["Horno"] = ""; 
                }
                if(diccionarioDeColumnas["Teñido"]==", "){
                	diccionarioDeColumnas["Teñido"] = ""; 
                }
                if(diccionarioDeColumnas["Teñido"]==", "){
                	diccionarioDeColumnas["Teñido"] = ""; 
                }
                if(diccionarioDeColumnas["Enfundado"]==", "){
                	diccionarioDeColumnas["Enfundado"] = ""; 
                }
                if(diccionarioDeColumnas["Enfundado"]==", "){
                	diccionarioDeColumnas["Enfundado"] = ""; 
                }
                if(diccionarioDeColumnas["Esp_Carretes"]==", "){
                	diccionarioDeColumnas["Esp_Carretes"] = ""; 
                }
                if(diccionarioDeColumnas["Esp_Carretes"]==", "){
                	diccionarioDeColumnas["Esp_Carretes"] = ""; 
                }

                quitarComasAlInicio();
                quitarComasAlFinal(); 

                
        }

        private void quitarComasAlFinal()
        {
            List<string> keys = new List<string>(diccionarioDeColumnas.Keys);
            foreach (string key in keys)
            {
                try
                {
                    if (diccionarioDeColumnas[key][diccionarioDeColumnas[key].Length - 2] == ',')
                    {
                        diccionarioDeColumnas[key] = diccionarioDeColumnas[key].Remove(diccionarioDeColumnas[key].Length - 2);

                    }
                }
                catch
                {

                }
                    // do something with entry.Value or entry.Key
            }
            
        }

        private void quitarComasAlInicio()
        {
            string str = "";
            List<string> keys = new List<string>(diccionarioDeColumnas.Keys);
            foreach (string key in keys) 
            {
                if (diccionarioDeColumnas[key].Length > 1)
                {
                    if (diccionarioDeColumnas[key][0] == ',')
                    {
                        str = diccionarioDeColumnas[key];
                        diccionarioDeColumnas[key] = str.Substring(2, diccionarioDeColumnas[key].Length-2);
                    }                  
                }
            }
        }
    
        
    
     private void agregarRegistroAlDiccionario(int indicePedidosAntes, int indiceComponente)
        {
            limpiarDiccionario(); 
            if (foundRowsTable.Rows.Count <= indiceComponente)
            {
                return; 
            }
            diccionarioDeColumnas["Nombre del Cliente"] = foundRowsTable.Rows[indiceComponente]["nombreDelCliente"].ToString();
            try
            {
                diccionarioDeColumnas["Cantidad_Kg"] = (Math.Round(Convert.ToDouble(tablaPedidosAntes.Rows[indicePedidosAntes]["Cantidad"].ToString()), 2)).ToString();
            }
            catch
            {

            }
            //En caso de que las unidades se encuentren en LB se tendrá que hacer la conversión a KG
            if ((tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "LB")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lb")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "Lb")
                || (tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString() == "lB"))
            {
                diccionarioDeColumnas["Cantidad_Kg"] = Convert.ToString(Math.Round(Convert.ToDouble(diccionarioDeColumnas["Cantidad_Kg"]) * 0.453592, 2));
                diccionarioDeColumnas["Unidad_Original"] = "LB";
            }
            else
            {
                diccionarioDeColumnas["Unidad_Original"] = tablaPedidosAntes.Rows[indicePedidosAntes]["Unidad"].ToString();
            }


            diccionarioDeColumnas["Calibre"] = foundRowsTable.Rows[indiceComponente]["D"].ToString();
            diccionarioDeColumnas["Color"] = foundRowsTable.Rows[indiceComponente]["E"].ToString();
            diccionarioDeColumnas["Pigmentos"] = foundRowsTable.Rows[indiceComponente]["O"].ToString();
            diccionarioDeColumnas["Material"] = foundRowsTable.Rows[indiceComponente]["C"].ToString();
            diccionarioDeColumnas["Resina"] = (foundRowsTable.Rows[indiceComponente]["L"].ToString());
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
            diccionarioDeColumnas["Rizado"] = foundRowsTable.Rows[indiceComponente]["I"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["J"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["K"].ToString();
            diccionarioDeColumnas["Perfil"] = foundRowsTable.Rows[indiceComponente]["H"].ToString();
            diccionarioDeColumnas["Aditivos"] = foundRowsTable.Rows[indiceComponente]["N"].ToString();
            diccionarioDeColumnas["Tipo de Mazo"] = foundRowsTable.Rows[indiceComponente]["AN"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AO"].ToString();
            string cadenaBastonEspejoTina = "";
            if (foundRowsTable.Rows[indiceComponente]["Q"].ToString() != "")
            {
                cadenaBastonEspejoTina += "Bastón: " + foundRowsTable.Rows[indiceComponente]["Q"].ToString();
            }

            if (foundRowsTable.Rows[indiceComponente]["P"].ToString() != "")
            {
                cadenaBastonEspejoTina += ", Espejo: " + foundRowsTable.Rows[indiceComponente]["P"].ToString();
            }

            if (foundRowsTable.Rows[indiceComponente]["R"].ToString() != "")
            {
                cadenaBastonEspejoTina += ", Tina: " + foundRowsTable.Rows[indiceComponente]["R"].ToString();
            }

            diccionarioDeColumnas["Bastón_Espejo_Tina"] = cadenaBastonEspejoTina;

            diccionarioDeColumnas["Herramental"] = foundRowsTable.Rows[indiceComponente]["V"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["W"].ToString();
            diccionarioDeColumnas["Fabricar"] = foundRowsTable.Rows[indiceComponente]["AC"].ToString();
            diccionarioDeColumnas["Temple"] = foundRowsTable.Rows[indiceComponente]["AP"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AQ"].ToString();
            diccionarioDeColumnas["Horno"] = foundRowsTable.Rows[indiceComponente]["AR"].ToString() + ", " +
                 foundRowsTable.Rows[indiceComponente]["AS"].ToString();
            diccionarioDeColumnas["Teñido"] = foundRowsTable.Rows[indiceComponente]["AT"].ToString();
            diccionarioDeColumnas["Enfundado"] = foundRowsTable.Rows[indiceComponente]["AX"].ToString();
            diccionarioDeColumnas["Esp_Carretes"] = foundRowsTable.Rows[indiceComponente]["AD"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AE"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AF"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AG"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AH"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AI"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AJ"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AK"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AL"].ToString() + ", " +
                foundRowsTable.Rows[indiceComponente]["AM"].ToString();

            string stringHorneado = "";
            if ((diccionarioDeColumnas["Horno"] != "") && (diccionarioDeColumnas["Horno"] != ", ")) 
            {
                stringHorneado = "Horneado: " + diccionarioDeColumnas["Horno"];
            }
            diccionarioDeColumnas["TODO"] =
            diccionarioDeColumnas["Perfil"] + ", " +
            diccionarioDeColumnas["Rizado"] + ", " +
            diccionarioDeColumnas["Aditivos"] + ", " +
            diccionarioDeColumnas["Pigmentos"] + ", " +
            diccionarioDeColumnas["Herramental"] + ", " +
            diccionarioDeColumnas["Tipo de Mazo"] + ", " +
            diccionarioDeColumnas["Bastón_Espejo_Tina"] + ", " +
            diccionarioDeColumnas["Temple"] + ", " +
            stringHorneado + ", " +
            diccionarioDeColumnas["Teñido"] + ", " +
            diccionarioDeColumnas["Enfundado"] + ", " +
            diccionarioDeColumnas["Esp_Carretes"] + ", ";

            quitarComas(); 
     }

       
        public void generarExcelDesdeDataTable(DataTable Tbl)
        {
            string[] lines = new string[0];
            bool configuracionDefecto = false; 
            try
            {
                lines = System.IO.File.ReadAllLines(@"" + Application.StartupPath + "\\archivoConfiguracion.txt");
            }
            catch
            {
                MessageBox.Show("No se pudo leer la configuración. Se utilizará la configuración por defecto.");
                configuracionDefecto = true; 
            }

            if (configuracionDefecto == false)
            {
                //Aquí se quitaran las columnas que no se quieran 
                for (int x = 0; x < lines.Length; x++)
                {
                    string [] configuracionDeColumna = lines[x].Split('|');
                    if (configuracionDeColumna[1] == "False")
                    {
                        Tbl.Columns.Remove(configuracionDeColumna[0]);
                    }
                }
            }
            else
            {
                Tbl.Columns.Remove("ESP_SAE");
            }
            try
        {
            if (Tbl == null || Tbl.Columns.Count == 0)
                throw new Exception("ExportToExcel: Null or empty input table!\n");

            // load excel, and create a new workbook
            Excel.Application excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            // single worksheet
            Excel._Worksheet workSheet = excelApp.ActiveSheet;

            //Establecemos toda la hoja como formato de texto
            //workSheet.Range["A:A"].NumberFormat = "@";
          

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
                    workSheet.Cells[(i + 2), (j + 1)].NumberFormat = "@"; 
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

            obtenerRangoEncabezados(Tbl.Columns.Count); 
            //Se dibuja el borde gordo de las celdas primarias
            _range = workSheet.get_Range(rangoEncabezadoInicial, rangoEncabezadoFinal);
                    
            

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
                    excelApp.ActiveWorkbook.Close(true); 
                    excelApp.Quit();
                    int pedidosLeidos = tablaPedidosAntes.Rows.Count;
                    int pedidosGenerados = tablaPedidosDespues.Rows.Count - cantidadComponentesMezcla;
                    MessageBox.Show(new Form() { TopMost = true }, "Archivo Guardado con éxito. \nPedidos leídos: " + pedidosLeidos + "\nPedidos generados: " + pedidosGenerados);
                    if (arregloClavesNoEncontradas.Count > 0)
                    {
                        string clavesNoEncontradas = "";
                        for (int x = 0; x < arregloClavesNoEncontradas.Count; x++)
                        {
                            clavesNoEncontradas += "\"" + arregloClavesNoEncontradas[x].ToString() + "\""+ "\n"; 
                        }
                            MessageBox.Show("Las siguientes claves no fueron encontradas: \n" + clavesNoEncontradas + "Favor de contactar a Aseguramiento de Calidad.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                    }
                    if (pedidosLeidos > pedidosGenerados)
                    {
                        MessageBox.Show("Hay pedidos que no se generaron. Favor de contactar a Aseguramiento de Calidad","Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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

        private void obtenerRangoEncabezados(int p)
        {
            if (p > 0)
            {
                rangoEncabezadoInicial = "A1";
            }
            switch (p)
            {
                case 1:
                    rangoEncabezadoFinal = "A1"; 
                    break;
                case 2:
                    rangoEncabezadoFinal = "B1";
                    break; 
                case 3:
                    rangoEncabezadoFinal = "C1";
                    break; 
                case 4:
                    rangoEncabezadoFinal = "D1";
                    break; 
                case 5:
                    rangoEncabezadoFinal = "E1";
                    break;
                case 6:
                    rangoEncabezadoFinal = "F1";
                    break;
                case 7:
                    rangoEncabezadoFinal = "G1";
                    break;
                case 8:
                    rangoEncabezadoFinal = "H1";
                    break;
                case 9:
                    rangoEncabezadoFinal = "I1";
                    break;
                case 10:
                    rangoEncabezadoFinal = "J1";
                    break;
                case 11:
                    rangoEncabezadoFinal = "K1";
                    break;
                case 12:
                    rangoEncabezadoFinal = "L1";
                    break;
                case 13:
                    rangoEncabezadoFinal = "M1";
                    break;
                case 14:
                    rangoEncabezadoFinal = "N1";
                    break;
                case 15:
                    rangoEncabezadoFinal = "O1";
                    break;
                case 16:
                    rangoEncabezadoFinal = "P1";
                    break;
                case 17:
                    rangoEncabezadoFinal = "Q1";
                    break;
                case 18:
                    rangoEncabezadoFinal = "R1";
                    break;
                case 19:
                    rangoEncabezadoFinal = "S1";
                    break;
                case 20:
                    rangoEncabezadoFinal = "T1";
                    break;
                case 21:
                    rangoEncabezadoFinal = "U1";
                    break;
                case 22:
                    rangoEncabezadoFinal = "V1";
                    break;
                case 23:
                    rangoEncabezadoFinal = "W1";
                    break;
                case 24:
                    rangoEncabezadoFinal = "X1";
                    break;
                case 25:
                    rangoEncabezadoFinal = "Y1";
                    break;
                case 26:
                    rangoEncabezadoFinal = "Z1";
                    break;
                case 27:
                    rangoEncabezadoFinal = "AA1";
                    break;
                case 28:
                    rangoEncabezadoFinal = "AB1";
                    break;
                case 29:
                    rangoEncabezadoFinal = "AC1";
                    break;
                case 30:
                    rangoEncabezadoFinal = "AD1";
                    break;
                case 31:
                    rangoEncabezadoFinal = "AF1";
                    break; 
                case 32:
                    rangoEncabezadoFinal = "AG1";
                    break; 

                default:
                    rangoEncabezadoFinal = "AZ1";
                    break; 
                    
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

                ExcelFilePath = dialogFolder.SelectedPath + "\\" + nombreDelArchivo + "_SAESP.xlsx";
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
            
            foundRows = tablaEspecificaciones.Select("clave like " + "'%" + clave + "%' AND clave not like '%OBSOLETO%'");
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
            cliente = cliente.Replace("'", "''"); 
            foundRows = tablaEspecificaciones.Select(
                "clave like " + "'%" + clave + "%'" + 
                " AND " +
                "clave NOT like " + "'%OBSOLETO%'" +
                " AND " +
                "nombreDelCliente like " + "'%" + cliente + "%'"
                );
            foreach (DataRow row in foundRows)
            {
                foundRowsTableClientes.ImportRow(row);
            }
            return foundRowsTableClientes.Rows.Count; 
        }
        public void getTablaDePedidos(DataGridView data1, DataGridView data2)
        {
            return; 
        }
        public void getTablaDePedidos(BackgroundWorker backgroundWorker1)
        {            
            //Si funciona el showDialog aunque se invoque este método desde un hilo
            //formMenuPrincipal ventana = new formMenuPrincipal();
            //ventana.ShowDialog(); 
            //Creamos una nueva tabla
            tablaPedidosDespues = new DataTable();
            
            //Genera las columnas para la tabla
            generarColumnasParaLaTabla(); 


            //Aquí va el procedimiento para cada uno de los índices de la tabla de Pedidos anterior
            llenarValoresDeTablaPedidosDespues(backgroundWorker1);
            generarExcelDesdeDataTable(tablaPedidosDespues);
            backgroundWorker1.ReportProgress(100); 
            //mostrarReporte(); 
                
        }

        private void llenarValoresDeTablaPedidosDespues(BackgroundWorker backgroundWorker1)
        {

            for (int x = 0; x < tablaPedidosAntes.Rows.Count; x++)
            {

                progreso = "Generando tabla de pedidos. " + x + " de " + tablaPedidosAntes.Rows.Count + "." ; 
                //Se utiliza el número del indice en caso de que el nombre de la columna se modifique. 
                claveActual = tablaPedidosAntes.Rows[x]["Clave"].ToString();  //claveActual = tablaPedidosAntes.Rows[x][6].ToString(); 
                clienteActual = tablaPedidosAntes.Rows[x]["Nombre del Cliente"].ToString(); //clienteActual = tablaPedidosAntes.Rows[0][0].ToString();

                clienteActual = normalizacionCliente(clienteActual);
                if (claveActual == "Clave")
                    continue; 
                //Se obtienen todos los registros encontrados para la clave actual y se almacena en "foundRowsTable"
                int encontrados = getRegistrosByClaveEnEspecificaciones(claveActual);
                if (encontrados > 0)
                {


                    //Buscar en esa tabla los valores con el cliente específico y guardarlos en la tabla "foundRowsTableClientes"
                    int encontradosClientes = getRegistrosByClaveAndClienteEnEspecificaciones(claveActual, clienteActual);

                    //Caso 1: Se ha encontrado un cliente para esa clave
                    if (encontradosClientes == 1)
                    {
                        if (encontrados == 1)
                            indiceDefinitivoPorIteracion = 0; 
                        else
                            if (encontrados > 1)
                            {
                                getIndiceClienteUnico(claveActual, clienteActual);  
                            }
                        //Tomar ese único registro encotrado 
                        //MessageBox.Show(new Form() { TopMost = true }, "Caso 1: Correcto. Se ha encontrado la clave y un solo cliente.");
                        
                    }
                    else
                        //Caso 2: Se ha encontrado más de un cliente(el mismo) para esa clave. 
                        if (encontradosClientes > 1)
                        {
                            if (!verificarMezcla())
                            {
                                // En este caso ya se habrán encontrado dos clientes o más (puede ser el mismo cliente) para una sola búsqueda. 
                                //Se concatenará a la clave las iniciales correspondientes para realizar la normalización. 
                                //claveActual = convertirClaveCasosEspeciales(claveActual, clienteActual); 
                                if (!claveEsExacta(claveActual))
                                {
                                    if (!eligeCasoEspecial(claveActual, clienteActual))
                                    {
                                        //Mostrar ventana interactiva 
                                        mostrarVentanaInteractiva("Caso 2: Se ha encontrado más de un cliente '" + clienteActual + "'la clave. (Posible Mezcla) " + claveActual, x);
                                        //MessageBox.Show(new Form() { TopMost = true }, "El indice seleccionado es: " + ventanaInteractiva.IndiceSeleccionado);
                                        indiceDefinitivoPorIteracion = ventanaInteractiva.IndiceSeleccionado;
                                    }
                                }
                                else
                                {
                                    verificarMezcla(indiceDefinitivoPorIteracion); 
                                }
                                
                            }
                        }
                        //Caso 3: Clave encontrada; pero cliente no encontrado. 
                        
                        else
                            if (encontradosClientes == 0)
                            {
                                if (foundRowsTable.Rows.Count == 1)
                                {
                                    //Aquí se encontró una sola clave; pero el cliente es diferente. En esta situación se cambia el nombre del 
                                    //cliente del pedido por el que esta en la base de datos. 
                                    indiceDefinitivoPorIteracion = 0;
                                }
                                else
                                {
                                    if (!claveEsExacta(claveActual))
                                    {
                                        if (!eligeCasoEspecial(claveActual, clienteActual))
                                        {
                                            mostrarVentanaInteractiva("Caso 3: No se ha encontrado ningún cliente '" + clienteActual + "' para la clave " + claveActual, x);

                                            if (!verificarMezcla(ventanaInteractiva.IndiceSeleccionado))
                                            {
                                                indiceDefinitivoPorIteracion = ventanaInteractiva.IndiceSeleccionado;
                                            }
                                        }
                                    }
                                }
                                //Cuando se cierre el diálogo se debera de acceder al índice seleccionado por el cliente en la tabla de "foundRowsTable"
                            }

                }
                //Caso 4: No se encontró la clave
                else
                {
                    //MessageBox.Show(new Form() { TopMost = true }, "Caso 4: No se ha encontrado la clave"); 
                    arregloClavesNoEncontradas.Add(claveActual);
                    indiceDefinitivoPorIteracion = -1; 
                    
                }


                agregaRegistroEnTabla(x);
                backgroundWorker1.ReportProgress(70 + Convert.ToInt16(Math.Round((x * 20.00) / tablaPedidosAntes.Rows.Count))); 

            }

        }

        private bool verificarMezcla(int indiceAnalizar)
        {
            componentesMezcla = new ArrayList();
            bool esMezcla = false;
            //Se inicializa un string con una longitud muy larga. 
            string claveMasCorta = foundRowsTable.Rows[indiceAnalizar]["clave"].ToString(); 


            //MessageBox.Show(new Form() { TopMost = true }, "La clave maestra de la mezcla es: " + claveMasCorta);

            string claveAnalizada;
            //Buscar esa varible en la tabla con un espacio seguido de cualquiera de los siguientes caracteres: A, L, R, 3D, S 
            for (int row = 0; row < foundRowsTable.Rows.Count; row++)
            {
                bool matches;
                claveAnalizada = foundRowsTable.Rows[row]["clave"].ToString();

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*A(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "A");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*B(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "B");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*C(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "C");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*D(.)*");
                if (matches == true)
                {
                    matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*3D(.)*");
                    if (matches == false)
                    {
                        //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                        componentesMezcla.Add(row);
                        esMezcla = true;
                    }

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "D");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "E");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }



                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*L(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "L");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*R(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "R");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*3D(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "3D");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*S(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "S");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
            }
            if (esMezcla)
            {
                indiceDefinitivoPorIteracion = indiceAnalizar;
            }
            return esMezcla; 
        }

        private bool eligeCasoEspecial(string claveActual, string clienteActual)
        {
            string sufijoDeBusqueda = "";
            switch (clienteActual)
            {
                case "Anderson Brighton de México, S.A. de C.V.":
                    //Buscará aqui el caso especial y regresará verdadero en caso de encontrarlo. 
                    sufijoDeBusqueda = " AN"; 
                    break; 
                case "The Mill Rose Company":
                    sufijoDeBusqueda = " M"; 
                    break; 
            }

            //Aquí buscará la clave con el sufijo especificado
            for (int x = 0; x < foundRowsTable.Rows.Count; x++)
            {
                if ((foundRowsTable.Rows[x]["clave"].ToString() == (claveActual + sufijoDeBusqueda)))
                {
                    indiceDefinitivoPorIteracion = x;
                    return true;
                }

            }
            return false;           
        }

        private string convertirClaveCasosEspeciales(string claveActual, string clienteActual)
        {
            if ((claveActual == "C-200411") && (clienteActual == "Anderson Brighton de México, S.A. de C.V."))
            {
                claveActual = "C-200411 AN";
            }
            if ((claveActual == "C-100822") && (clienteActual == "Anderson Brighton de México, S.A. de C.V."))
            {
                claveActual = "C-100822 AN";
            }

            return claveActual; 
        }

        
        private bool claveEsExacta(string clave)
        {
            for (int x = 0; x < foundRowsTable.Rows.Count; x++)
            {
                if ((foundRowsTable.Rows[x]["clave"].ToString() == clave))
                {
                    indiceDefinitivoPorIteracion = x;
                    return true; 
                }

            }

            return false; 
        }

        private void getIndiceClienteUnico(string claveActual, string clienteActual)
        {
            
            //Se copian los nombre de las columnas en la tabla foundRowsTable
            for (int x = 0; x < foundRowsTable.Rows.Count; x++ )
            {
                if((foundRowsTable.Rows[x]["nombreDelCliente"].ToString()==clienteActual))
                {
                    indiceDefinitivoPorIteracion = x; 
                    break; 
                }              

            }
        }

        private string normalizacionCliente(string clienteActual)
        {
            switch (clienteActual)
            {
                case "PLASCENCIA FLORES LAURA":
                    clienteActual = "Plascencia Flores Laura";
                    break; 
                case "PLASCENCIA FLORES JEANETTE ESTHELA":
                    clienteActual = "Plascencia Flores Jeanette Esthela";
                    break; 
                case "PLASTICOS PLASA DE GUADALAJARA, S.A. DE C.V.":
                    clienteActual = "Plásticos Plasa de Guadalajara S. A. de C. V."; 
                    break;
                case "ANDERSON BRIGHTON DE MEXICO, S.A. DE C.V.":
                    clienteActual = "Anderson Brighton de México, S.A. de C.V.";
                    break;
                case "THE MILL ROSE COMPANY":
                    clienteActual = "The Mill Rose Company"; 
                    break; 
                case "PORTILLO MARTINEZ ERNESTO":
                    clienteActual = "Portillo Martínez Ernesto";
                    break;
                case "TANIS FDL":
                    clienteActual = "Tanis Incorporated";
                    break;
                case "PROCTER & GAMBLE MANUFACTURING GmbH":
                    clienteActual = "P&G Manufacturing GmbH";
                    break;
                case "SUNSTAR AMERICAS, INC.":
                    clienteActual = "Sunstar Americas Inc.";
                    break;
                case "GORDON BRUSH MFG. CO. INC.":
                    clienteActual = "Gordon Brush Mfg. Co., Inc.";
                    break;  

            }
          

            return clienteActual; 
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

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*A(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "A");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*B(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "B");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*C(.)*");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "C");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*D(.)*");
                if (matches == true)
                {
                    matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*3D(.)*");
                    if (matches == false)
                    {
                        //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                        componentesMezcla.Add(row);
                        esMezcla = true;
                    }

                }
                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "D");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "3D");
                if (matches == true)
                {
                    //Se agrega el índice del componente de la mezcla para poder agregarlo en el método de "agregaRegistroEnTabla"
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }
                

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*L(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "L");
                if (matches == true)
                {
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*R(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "R");
                if (matches == true)
                {
                    componentesMezcla.Add(row);
                    esMezcla = true;

                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*3D(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "(.)* (.)*S(.)*");
                if (matches == true)
                {
                    componentesMezcla.Add(row); 
                    esMezcla = true;
                    
                }

                matches = Regex.IsMatch(claveAnalizada, "^" + claveMasCorta + "S");
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
