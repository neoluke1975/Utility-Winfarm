using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace Utility_Winfarm
{
    public partial class dati_utili : Form
    {
        public dati_utili()
        {
            InitializeComponent();
        }

        private void dati_utili_Load(object sender, EventArgs e)
        {
            tbx_linfa.Text = Utility_Winfarm.Properties.Settings.Default.percorsoLinfaServer;
            tbx_passwordLinfa.Text = Utility_Winfarm.Properties.Settings.Default.pwLinfa;
            tbx_passwordWinfarm.Text = Utility_Winfarm.Properties.Settings.Default.pwWinfarm;
            tbx_utenteFirebird.Text = Utility_Winfarm.Properties.Settings.Default.utenteWinfarm;
            tbx_utenteLinfa.Text = Utility_Winfarm.Properties.Settings.Default.utenteLinfa;
            tbx_winfarmPercorso.Text = Utility_Winfarm.Properties.Settings.Default.percorsoWinfarm;
        }
        private MySqlConnection connmysql()
        {
            return new MySqlConnection("Server=" + tbx_linfa.Text + ";Database=bf2000;Uid=" + tbx_utenteLinfa.Text + ";Pwd=" + tbx_passwordLinfa.Text + ";");
        }
        private FbConnection connessione()
        {
            return new FbConnection("User=" + tbx_utenteFirebird.Text + ";Password=" + tbx_passwordWinfarm.Text + ";Database=" + tbx_winfarmPercorso.Text + " ;DataSource=" + tbx_serverWinfarm.Text + ";Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true;MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0; ");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlDataReader lettore = null;
            MySqlConnection connesioneMysql = connmysql();
            connesioneMysql.Open();
            try
            {//chiedo il cliente tariffazione per prendere i dati
                
                MySqlCommand query_cliente_tariffazione = new MySqlCommand("SELECT ClienteTariffazione FROM bf2000.tabellaparametrivari", connesioneMysql);
               
                lettore = query_cliente_tariffazione.ExecuteReader();
                while (lettore.Read())
                {
                    string cliente_tariffazione = "ricette" + lettore[0].ToString();
                    textBox7.Text = cliente_tariffazione;
                }
                lettore.Close();
            }
            catch {
            }
            try
            { //chiedo i dati di comifar
                MySqlCommand query = new MySqlCommand("select utente,passwordutente,passwordoperatore from bf2000.tabellagrossisti where Descrizione like  'COMIFAR%'", connesioneMysql);
                lettore = query.ExecuteReader();
                while (lettore.Read())
                {
                    string codice_comifar = lettore[0].ToString();
                    string password1 = lettore[1].ToString();
                    string password2 = lettore[2].ToString();
                    textBox1.Text = codice_comifar;
                    textBox2.Text = password1;
                    textBox3.Text = password2;

                }
                lettore.Close();

            }
            catch 
            {

               
            }
            try
            {
                //chiedo i dati di webcare
                MySqlCommand query1 = new MySqlCommand("SELECT WebCareUserName,WebCarePassword,WebCarePin FROM " + textBox7.Text + ".tabellaclientitariffazione;", connesioneMysql);
                lettore = query1.ExecuteReader();
                while (lettore.Read())
                {
                    string codice_webcare = lettore[0].ToString();
                    string password_webcare = lettore[1].ToString();
                    string pin = lettore[2].ToString();
                    textBox6.Text = codice_webcare;
                    textBox5.Text = password_webcare;
                    textBox4.Text = pin;

                }
                lettore.Close();
            }
            catch 
            {
                
              
            }
            try
            {
                //chiedo i dati di webcare
                MySqlCommand query1 = new MySqlCommand("SELECT WebDpcUserName,WebDpcPassword,pin FROM " + textBox7.Text + ".tabellaclientitariffazione;", connesioneMysql);
                lettore = query1.ExecuteReader();
                while (lettore.Read())
                {
                    string codice_webdpc = lettore[0].ToString();
                    string password_webdpc = lettore[1].ToString();
                    string pin_dpc = lettore[2].ToString();
                    textBox10.Text = codice_webdpc;
                    textBox9.Text = password_webdpc;
                    textBox8.Text = pin_dpc;

                }
                lettore.Close();
            }
            catch
            {


            }
            try
            {
                //chiedo i dati di webcare
                MySqlCommand query1 = new MySqlCommand("SELECT ProvinciaFarmacia,CodASLSogei,CodiceASL,CodiceAslFarmacia FROM " + textBox7.Text + ".tabellaclientitariffazione;", connesioneMysql);
                lettore = query1.ExecuteReader();
                while (lettore.Read())
                {
                    string provincia = lettore[0].ToString();
                    string codice_sogei = lettore[1].ToString();
                    string codiceasl = lettore[2].ToString();
                    string codiceaslfarmacia = lettore[3].ToString();
                    textBox14.Text = provincia;
                    textBox13.Text = codice_sogei;
                    textBox12.Text = codiceasl;
                    textBox11.Text = codiceaslfarmacia;

                }
                lettore.Close();
            }
            catch
            {


            }
            try
            {
                //chiedo i dati di webcare
                MySqlCommand query1 = new MySqlCommand("SELECT t.DelegaSpeseSanitarieUser,t.DelegaSpeseSanitariePassword,t.CodiceFiscale,t.PartitaIva,t.SSErogatore FROM bf2000.tabellaparametrifarmacia t", connesioneMysql);
                lettore = query1.ExecuteReader();
                while (lettore.Read())
                {
                    string delega_user = lettore[0].ToString();
                    string delega_password = lettore[1].ToString();
                    string codice_fiscale = lettore[2].ToString();
                    string partita_iva = lettore[3].ToString();
                    string ss_erogatore = lettore[4].ToString();
                    textBox18.Text = delega_user;
                    textBox17.Text = delega_password;
                    textBox16.Text = codice_fiscale;
                    textBox15.Text = partita_iva;
                    textBox19.Text = ss_erogatore;

                }
                lettore.Close();
            }
            catch
            {


            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
