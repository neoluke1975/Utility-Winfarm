using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace Utility_Winfarm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        /*INSERIMENTO NOTE SUI PRODOTTI*/
        private void btn_note_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = new MySqlConnection("Server=localhost;Database=bf2000;Uid=root;Pwd=" + tbx_passwordLinfa.Text + ";");
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("select codiceprodotto,noteprodotto from bfnoteprodotto", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    string noteProdotto = "";
                    codice = lettore[0].ToString();
                    noteProdotto = lettore[1].ToString();
                    noteProdotto = noteProdotto.Replace("'", " ");
                    inserimento_note(codice, noteProdotto);
                    contatore++;
                }
                connesioneMysql.Close();
                MessageBox.Show("note inseriti :" + contatore.ToString());
            }
            catch
            {
                MessageBox.Show("problema1");
            }

        }

        private void inserimento_note(string codice, string noteProdotto)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set desc_not='" + noteProdotto + "' where km10='" + codice + "'", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
            }
            catch
            {
                MessageBox.Show("problema2");
            }

        }

        /*INSERIMENTO PREZZI DISCREZIONALI SOLO SE PREVALENTI---PREZZO DI LISTINO*/
        private void btn_prz_prev_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("select m.CodiceProdotto,m.PrezzoDiscrezionale from bf2000.bfmagazzino m where m.PrezzoDiscrezionalePrevalente = -1", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    double prezzo = 0;
                    codice = lettore[0].ToString();
                    prezzo = double.Parse(lettore[1].ToString());
                    inserimento_prezziDiscrezionaliPrevalenti(prezzo, codice);
                    contatore++;
                }
                connesioneMysql.Close();
                MessageBox.Show("prezzi inseriti :" + contatore.ToString());
            }
            catch
            {
                MessageBox.Show("problema1");
            }
        }

        private MySqlConnection connmysql()
        {
            return new MySqlConnection("Server=" + tbx_linfa.Text + ";Database=bf2000;Uid=" + tbx_utenteLinfa.Text + ";Pwd=" + tbx_passwordLinfa.Text + ";");
        }

        private void inserimento_prezziDiscrezionaliPrevalenti(double prezzo, string codice)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set e_prezzo_listino=" + prezzo + " where km10='" + codice + "'", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
            }
            catch
            {
                MessageBox.Show("problema 2");
            }

        }
        /* INSERIMENTO PREZZI DISCREZIONALI SE MAGGIORI DI ZERO----PREZZO DI LISTINO----PS:UTILIZZO LA FUNZIONE GIA' FATTA PRIMA...IMPLEMENTARE SE CAMBIA CAMPO*/
        private void btn_prevalenti_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("select m.CodiceProdotto,m.PrezzoDiscrezionale from bf2000.bfmagazzino m where m.PrezzoDiscrezionale > 0", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    double prezzo = 0;
                    codice = lettore[0].ToString();
                    prezzo = double.Parse(lettore[1].ToString());
                    inserimento_prezziDiscrezionaliPrevalenti(prezzo, codice);
                    contatore++;
                }
                connesioneMysql.Close();
                MessageBox.Show("prezzi inseriti :" + contatore.ToString());
            }
            catch
            {
                MessageBox.Show("problema1");
            }

        }



        /*INSERISCE LE SOGLIE*/
        private void btnsoglie_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("select m.CodiceProdotto,m.SogliaReintegro from bf2000.bfmagazzino m where m.SogliaReintegro > 0", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    int soglia = 0;
                    codice = lettore[0].ToString();
                    soglia = int.Parse(lettore[1].ToString());
                    inserimento_soglie(codice, soglia);
                    contatore++;
                }
                connesioneMysql.Close();
                MessageBox.Show("soglie inserite :" + contatore.ToString());
            }
            catch
            {
                MessageBox.Show("problema1");
            }

        }

        private void inserimento_soglie(string codice, int soglia)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set soglia_reintegro=" + soglia + " where km10='" + codice + "'", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
            }
            catch
            {
                MessageBox.Show("" + codice + " " + soglia + "");
            }
        }

        /*CONVERTO LE SOGLIE DI RIORDINO*/
        private void btn_riordino_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("select m.CodiceProdotto,m.TipoRiordino from bf2000.bfmagazzino m where m.TipoRiordino <> 'S'", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    string tipoRiordino = "";
                    codice = lettore[0].ToString();
                    tipoRiordino = lettore[1].ToString();
                    inserimento_tipoRiordino(codice, tipoRiordino);
                    contatore++;
                }
                connesioneMysql.Close();
                MessageBox.Show("riordini inseriti :" + contatore.ToString());
            }
            catch
            {
                MessageBox.Show("problema1");
            }
        }

        private void inserimento_tipoRiordino(string codice, string tipoRiordino)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set riordino='" + tipoRiordino + "' where km10='" + codice + "'", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }
        /*inserisco le offerte*/
        private void btn_offerte1_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("select o.CodiceProdotto,o.DataFineOfferta,max(o.DataInizioOfferta),o.GestionePezziOfferta,o.Offerta,o.PezziOfferta,o.QtaMinima,o.QtaOmaggio,o.TipoOfferta " +
                                                      "from bf2000.bfmagazzinoofferte o " +
                                                      "where o.DataFineOfferta >= now() " +
                                                      "group by o.CodiceProdotto, o.DataFineOfferta, o.GestionePezziOfferta, o.Offerta, o.PezziOfferta, o.QtaMinima, o.QtaOmaggio, o.TipoOfferta " +
                                                      "order by o.CodiceProdotto", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    DateTime dataFineofferta;
                    DateTime dataInizioOfferta;
                    double prezzoOfferta = 0;
                    int pezziOfferta = 0;

                    codice = lettore[0].ToString();
                    dataFineofferta = DateTime.Parse(lettore[1].ToString());
                    inserimento_offerte(codice, dataFineofferta);
                    contatore++;
                }
                connesioneMysql.Close();
                MessageBox.Show("soglie inserite :" + contatore.ToString());
            }
            catch
            {
                MessageBox.Show("problema1");
            }
        }

        private void inserimento_offerte(string codice, DateTime dataFineofferta)
        {

        }

        private void btn_cancellaPrezziVendita_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino m set m.E_PREZZO_FARMACIA = 0, m.PREZZO_FARMACIA = 0 where m.E_PREZZO_FARMACIA = (select a.E_PREZZO from anapro a where a.km10 = m.km10)", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void btn_cancellaprezziListino_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino m set m.E_PREZZO_LISTINO = 0 where m.E_PREZZO_LISTINO = (select a.E_PREZZO from anapro a where a.km10 = m.km10)", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void btn_spostaPrezziVendita_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino m set m.E_PREZZO_LISTINO = m.E_PREZZO_FARMACIA where m.E_PREZZO_LISTINO = 0 AND (select a.E_PREZZO from anapro a where a.km10 = m.km10)=0", connesioneFb);
                update.ExecuteNonQuery();
                FbCommand update2 = new FbCommand("update magazzino m set m.E_PREZZO_FARMACIA=0,m.PREZZO_FARMACIA=0 where m.E_PREZZO_LISTINO = m.E_PREZZO_FARMACIA", connesioneFb);
                update2.ExecuteNonQuery();
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }

        }

        private void btn_ubicazionetotale_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("select m.CodiceProdotto,(m.Ubicazione + (select u.Descrizione from bf2000.tabellaubicazioni u where u.Codice = m.CodiceProdotto)) from bf2000.bfmagazzino m ", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    string ubicazioneTotale;
                    codice = lettore[0].ToString();
                    ubicazioneTotale = lettore[1].ToString();
                    inserimento_ubicazioneTotale(codice, ubicazioneTotale);
                    contatore++;
                }
                connesioneMysql.Close();
                MessageBox.Show("ubicazioni inserite :" + contatore.ToString());
            }
            catch
            {
                MessageBox.Show("problema1");
            }
        }

        private void inserimento_ubicazioneTotale(string codice, string ubicazioneTotale)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set ubicazione='" + ubicazioneTotale + "' where km10='" + codice + "'", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void btn_testlinfa_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                connesioneMysql.Close();
                MessageBox.Show("connessione a mysql presente");
            }
            catch
            {
                MessageBox.Show("connessione a mysql assente");
            }
        }

        private void btn_testwinfarm_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                connesioneFb.Close();
                MessageBox.Show("connessione a firebird presente");

            }
            catch
            {
                MessageBox.Show("connessione a firebird assente");
            }

        }

        private FbConnection connessione()
        {
            return new FbConnection("User=" + tbx_utenteFirebird.Text + ";Password=" + tbx_passwordWinfarm.Text + ";Database=" + tbx_winfarmPercorso.Text + " ;DataSource=" + tbx_serverWinfarm.Text + ";Port = 3050; Dialect = 3; Charset = NONE; Role =; Connection lifetime = 15; Pooling = true;MinPoolSize = 0; MaxPoolSize = 50; Packet Size = 8192; ServerType = 0; ");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_ubicazioniSecondaParte_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("SELECT Codice,descrizione FROM bf2000.tabellaubicazioni", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    string ubicazioneTotale = "";
                    codice = lettore[0].ToString();
                    ubicazioneTotale = lettore[1].ToString();
                    inserimento_ubicazioneTotale(codice, ubicazioneTotale);
                    contatore++;
                }
                connesioneMysql.Close();
                MessageBox.Show("ubicazioni inserite :" + contatore.ToString());
            }
            catch
            {
                MessageBox.Show("problema1");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int contatore = 0;
                MySqlConnection connesioneMysql = connmysql();
                connesioneMysql.Open();
                MySqlCommand query = new MySqlCommand("select utente,passwordutente,passwordoperatore from bf2000.tabellagrossisti where Descrizione like  'COMIFAR%'", connesioneMysql);
                MySqlDataReader lettore = null;
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
            }
            catch
            {

            }

        }

        private void btn_testlinfa_Click_1(object sender, EventArgs e)
        {

        }

        //private void button2_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int contatore = 0;
        //        MySqlConnection connesioneMysql = connmysql();
        //        connesioneMysql.Open();
        //        MySqlCommand query = new MySqlCommand("SELECT WebCareUserName,WebCarePassword,WebCarePin,WebDpcUserName,WebDpcPassword,pin,webcareurl,webdpcurl FROM " + textBox10.Text + ".tabellaclientitariffazione;", connesioneMysql);
        //        MySqlDataReader lettore = null;
        //        lettore = query.ExecuteReader();
        //        while (lettore.Read())

        //        {

        //            string codice_webcare = lettore[0].ToString();
        //            string password_webcare = lettore[1].ToString();
        //            string pinwebcare = lettore[2].ToString();
        //            string urlWebcare = lettore[6].ToString();
        //            textBox6.Text = codice_webcare;
        //            textBox5.Text = password_webcare;
        //            textBox4.Text = pinwebcare;
        //            textBox11.Text = urlWebcare;

        //            string codice_webdpc = lettore[3].ToString();
        //            string password_webdpc = lettore[4].ToString();
        //            string pinwebdpc = lettore[5].ToString();
        //            string urlWebdpc = lettore[7].ToString();
        //            textBox9.Text = codice_webdpc;
        //            textBox8.Text = password_webdpc;
        //            textBox7.Text = pinwebdpc;
        //            textBox12.Text = urlWebdpc;




        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


    }
}
