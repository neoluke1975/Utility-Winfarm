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
            buttonToolTip.ToolTipTitle="Atenzione"; 
            buttonToolTip.UseFading = true; 
            buttonToolTip.UseAnimation = true; 
            buttonToolTip.IsBalloon = true; 
            buttonToolTip.ShowAlways = true; 
            buttonToolTip.AutoPopDelay = 5000; 
            buttonToolTip.InitialDelay = 1000; 
            buttonToolTip.ReshowDelay = 500; 
            buttonToolTip.SetToolTip(button1, "la funzione importa da un file in c:/codici.txt che abbia solo un elenco di codici minsan da portare a SI come gestione robot");
            buttonToolTip.SetToolTip(button2, "prova");
        }
       
        private FbConnection connessione()
        {
            return new FbConnection("User=" + tbx_utenteFirebird.Text + ";Password=" + tbx_passwordWinfarm.Text + ";Database=" + tbx_winfarmPercorso.Text + " ;DataSource=" + tbx_serverWinfarm.Text + ";Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true;MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0; ");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FbConnection conn = connessione();
            conn.Open();
            FbCommand query = new FbCommand("select nome from terminali", conn);
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

     
        private void btn_testwinfarm_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                connesioneFb.Close();
                btn_testwinfarm.BackColor = System.Drawing.Color.Green;
                MessageBox.Show("connessione a firebird presente");

            }
            catch
            {
                MessageBox.Show("connessione a firebird assente");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FbConnection conn = connessione();
            conn.Open();
          
            for (int i = 0; i < dataGridView2.RowCount-1; i++)
            {
                FbCommand update_terminali = new FbCommand("update terminali set nome='" + (dataGridView2.Rows[i].Cells[0].Value).ToString() + "' where nome='" + (dataGridView1.Rows[i].Cells[0].Value).ToString() + "'",conn);
                update_terminali.ExecuteNonQuery();
            }
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void terminali_Load(object sender, EventArgs e)
        {
            tbx_passwordWinfarm.Text = Utility_Winfarm.Properties.Settings.Default.pwWinfarm;
            tbx_serverWinfarm.Text = Utility_Winfarm.Properties.Settings.Default.serverWinfarm;
            tbx_utenteFirebird.Text = Utility_Winfarm.Properties.Settings.Default.utenteWinfarm;
            tbx_winfarmPercorso.Text = Utility_Winfarm.Properties.Settings.Default.percorsoWinfarm;
        }
    }
}
