using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace Utility_Winfarm
{
    public partial class terminali : Form
    {
        public terminali()
        {
            InitializeComponent();
            ToolTip buttonToolTip = new ToolTip(); 
            buttonToolTip.ToolTipTitle="Attenzione"; 
            buttonToolTip.UseFading = true; 
            buttonToolTip.UseAnimation = true; 
            buttonToolTip.IsBalloon = true; 
            buttonToolTip.ShowAlways = true; 
            buttonToolTip.AutoPopDelay = 5000; 
            buttonToolTip.InitialDelay = 1000; 
            buttonToolTip.ReshowDelay = 500; 
            buttonToolTip.SetToolTip(button2, "");
        }
       
        private FbConnection connessione()
        {
            return new FbConnection("User=" + Properties.Settings.Default.utenteWinfarm.ToString() + ";Password=" + Properties.Settings.Default.pwWinfarm.ToString() + ";Database=" + Properties.Settings.Default.percorsoWinfarm.ToString() + " ;DataSource=" + Properties.Settings.Default.serverWinfarm.ToString() + ";Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true;MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0; ");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FbConnection conn = connessione();
            conn.Open();
            FbCommand query = new FbCommand("select nome,ip from terminali", conn);
            FbDataAdapter adatta = new FbDataAdapter(query);
            adatta.Fill(dataSet1, "terminali");
            adatta.Fill(dataSet2, "variazione");

            dataGridView1.DataMember = "terminali";
            dataGridView1.DataSource = dataSet1;
            dataGridView1.AutoResizeColumns();
            dataGridView2.DataMember = "variazione";
            dataGridView2.DataSource = dataSet2;
            dataGridView2.AutoResizeColumns();
            conn.Close();
        }
             
        
        private void button2_Click(object sender, EventArgs e)
        {
            FbConnection conn = connessione();
            conn.Open();
          
            for (int i = 0; i < dataGridView2.RowCount-1; i++)
            {
                FbCommand update_terminali = new FbCommand("update terminali set nome='" + (dataGridView2.Rows[i].Cells[0].Value).ToString().ToUpper() +"',ip='"+ (dataGridView2.Rows[i].Cells[1].Value).ToString() + "' where nome='" + (dataGridView1.Rows[i].Cells[0].Value).ToString().ToUpper() + "'",conn);
                update_terminali.ExecuteNonQuery();
            }
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
