using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tablasDePedidos
{
    public partial class formConfiguracionSalida : Form
    {
        public formConfiguracionSalida()
        {
            InitializeComponent();
        }

        private void formConfiguracionSalida_Load(object sender, EventArgs e)
        {
            this.dataGridColumnasConfiguracion.DefaultCellStyle.Font = new Font("Tahoma", 13);
            this.dataGridColumnasConfiguracion.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 13);
            string[] lines;
            try
            {
                //Leemos el archivo de configuracion 
                lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\archivoConfiguracion.txt");
            }
            catch
            {
                MessageBox.Show("No se pudo cargar la configuración actual. Se realizará una nueva configuración.");
                lines = new string[27];
                lines[0] = "Nombre del Cliente|True";
                lines[1] = "Cantidad_Kg|True";
                lines[2] = "Unidad_Original|True";
                lines[3] = "Calibre|True";
                lines[4] = "Color|True";
                lines[5] = "Material|True";
                lines[6] = "Resina|True";
                lines[7] = "Clave|True";
                lines[8] = "Corte|True";
                lines[9] = "Lubricante|True";
                lines[10] = "Orientación|True";
                lines[11] = "No pedido|True";
                lines[12] = "Fecha Entrega|True";
                lines[13] = "TODO|True";
                lines[14] = "ESP_SAE|False";
                lines[15] = "Rizado|True";
                lines[16] = "Perfil|True";
                lines[17] = "Aditivos|True";
                lines[18] = "Tipo de Mazo|True";
                lines[19] = "Bastón_Espejo_Tina|True";
                lines[20] = "Herramental|True";
                lines[21] = "Fabricar|True";
                lines[22] = "Temple|True";
                lines[23] = "Horno|True";
                lines[24] = "Teñido|True";
                lines[25] = "Enfundado|True";
                lines[26] = "Esp_Carretes|True";
            }

            for (int x = 0; x < lines.Length; x++)
            {
                string[] datosSplit = lines[x].Split('|');
                string nombreColumna = datosSplit[0];
                bool activado = false;
                if (datosSplit[1] == "True")
                    activado = true;
                else
                    activado = false;
                dataGridColumnasConfiguracion.Rows.Add(nombreColumna, activado);
            }

            autosizeGrid();

        }
        

        private void autosizeGrid()
        {

            dataGridColumnasConfiguracion.AllowUserToOrderColumns = true;
            dataGridColumnasConfiguracion.AllowUserToResizeColumns = true;

            dataGridColumnasConfiguracion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridColumnasConfiguracion.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        private void generarArchivoDeConfiguracion()
        {
            //Se pone false para indicar que se sobreescribira el archivo en caso de existir
            using (StreamWriter escritor = new StreamWriter(Application.StartupPath + "\\archivoConfiguracion.txt", false))
            {
                escritor.WriteLine("Nombre del Cliente" 		+ "|"  + dataGridColumnasConfiguracion.Rows[0].Cells[1].Value.ToString());
 				escritor.WriteLine("Cantidad_Kg" 				+ "|"  + dataGridColumnasConfiguracion.Rows[1].Cells[1].Value.ToString());
                escritor.WriteLine("Unidad_Original" 			+ "|"  + dataGridColumnasConfiguracion.Rows[2].Cells[1].Value.ToString());
                escritor.WriteLine("Calibre" 					+ "|"  + dataGridColumnasConfiguracion.Rows[3].Cells[1].Value.ToString());
                escritor.WriteLine("Color" 						+ "|"  + dataGridColumnasConfiguracion.Rows[4].Cells[1].Value.ToString());
                escritor.WriteLine("Material" 					+ "|"  + dataGridColumnasConfiguracion.Rows[5].Cells[1].Value.ToString());
                escritor.WriteLine("Resina" 					+ "|"  + dataGridColumnasConfiguracion.Rows[6].Cells[1].Value.ToString());
                escritor.WriteLine("Clave" 						+ "|"  + dataGridColumnasConfiguracion.Rows[7].Cells[1].Value.ToString());
                escritor.WriteLine("Corte"                      + "|"  + dataGridColumnasConfiguracion.Rows[8].Cells[1].Value.ToString());
                escritor.WriteLine("Lubricante" 				+ "|"  + dataGridColumnasConfiguracion.Rows[9].Cells[1].Value.ToString());
                escritor.WriteLine("Orientación" 				+ "|"  + dataGridColumnasConfiguracion.Rows[10].Cells[1].Value.ToString());
                escritor.WriteLine("No pedido" 					+ "|"  + dataGridColumnasConfiguracion.Rows[11].Cells[1].Value.ToString());
                escritor.WriteLine("Fecha Entrega" 				+ "|"  + dataGridColumnasConfiguracion.Rows[12].Cells[1].Value.ToString());
                escritor.WriteLine("TODO"       				+ "|"  + dataGridColumnasConfiguracion.Rows[13].Cells[1].Value.ToString());
                escritor.WriteLine("ESP_SAE" 					+ "|"  + dataGridColumnasConfiguracion.Rows[14].Cells[1].Value.ToString());
                escritor.WriteLine("Rizado" 					+ "|"  + dataGridColumnasConfiguracion.Rows[15].Cells[1].Value.ToString());
                escritor.WriteLine("Perfil" 					+ "|"  + dataGridColumnasConfiguracion.Rows[16].Cells[1].Value.ToString());
                escritor.WriteLine("Aditivos" 					+ "|"  + dataGridColumnasConfiguracion.Rows[17].Cells[1].Value.ToString());
                escritor.WriteLine("Tipo de Mazo" 				+ "|"  + dataGridColumnasConfiguracion.Rows[18].Cells[1].Value.ToString());
                escritor.WriteLine("Bastón_Espejo_Tina" 		+ "|"  + dataGridColumnasConfiguracion.Rows[19].Cells[1].Value.ToString());
                escritor.WriteLine("Herramental" 				+ "|"  + dataGridColumnasConfiguracion.Rows[20].Cells[1].Value.ToString());
                escritor.WriteLine("Fabricar" 					+ "|"  + dataGridColumnasConfiguracion.Rows[21].Cells[1].Value.ToString());	
                escritor.WriteLine("Temple" 					+ "|"  + dataGridColumnasConfiguracion.Rows[22].Cells[1].Value.ToString());	
                escritor.WriteLine("Horno" 					    + "|"  + dataGridColumnasConfiguracion.Rows[23].Cells[1].Value.ToString());	
                escritor.WriteLine("Teñido" 					+ "|"  + dataGridColumnasConfiguracion.Rows[24].Cells[1].Value.ToString());	
                escritor.WriteLine("Enfundado" 					+ "|"  + dataGridColumnasConfiguracion.Rows[25].Cells[1].Value.ToString());	
                escritor.WriteLine("Esp_Carretes" 				+ "|"  + dataGridColumnasConfiguracion.Rows[26].Cells[1].Value.ToString());	
               
                
            }
        }

        private void buttonGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            try
            {
                generarArchivoDeConfiguracion();
                bool valor = Convert.ToBoolean(dataGridColumnasConfiguracion.Rows[1].Cells[1].Value);
                MessageBox.Show("Configuración guardada");
                this.Hide(); 
                this.Dispose(); 
            }
            catch(Exception Ex){
                MessageBox.Show(Ex.ToString()); 
            }

        }
    }
}
