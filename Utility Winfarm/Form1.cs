using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using System.Diagnostics;

namespace Utility_Winfarm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "Atenzione";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;
            buttonToolTip.SetToolTip(btn_prz_prev, "La Procedura importa i prezzi di vendita discrezionali prevalenti da Linfa/Concept e li inserisce in Prezzo di Listino su Evolution");
            buttonToolTip.SetToolTip(btn_prevalenti, "La Procedura importa i prezzi di vendita discrezionali maggiori di 0 su Evolution");
            buttonToolTip.SetToolTip(btnSoglie, "Vengono importate le soglie e il contatore del reintegro");
            buttonToolTip.SetToolTip(btn_note, "Importazione delle Note sui prodotti");
            buttonToolTip.SetToolTip(btn_riordino, "Importa riordino prodotto Si/No/Rappresentante");
            buttonToolTip.SetToolTip(button2, "Importa da un file c:/codici.txt tramite i codici minsan i prodotti gestiti a root");
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
                MySqlCommand query = new MySqlCommand("select m.CodiceProdotto,m.SogliaReintegro,m.contatorereintegro from bf2000.bfmagazzino m where m.SogliaReintegro > 0", connesioneMysql);
                MySqlDataReader lettore = null;
                lettore = query.ExecuteReader();
                while (lettore.Read())

                {

                    string codice = "";
                    int soglia = 0;
                    int contatore_reintegro = 0;
                    codice = lettore[0].ToString();
                    soglia = int.Parse(lettore[1].ToString());
                    contatore_reintegro = int.Parse(lettore[2].ToString());
                    inserimento_soglie(codice, soglia,contatore_reintegro);
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

        private void inserimento_soglie(string codice, int soglia,int contatore_reintegro)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set soglia_reintegro=" + soglia + ",soglia_vendite="+contatore_reintegro+" where km10='" + codice + "'", connesioneFb);
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
                FbCommand update = new FbCommand("update magazzino set e_prezzo_listino = 0 where(select v_euro from vero_prezzo('TODAY', magazzino.km10, 4)) > 0 and e_prezzo_listino > 0 and(select v_euro from vero_prezzo('TODAY', magazzino.km10, 4)) = e_prezzo_listino", connesioneFb);
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
                btn_testlinfa.BackColor = System.Drawing.Color.Green;
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
                btn_testwinfarm.BackColor = System.Drawing.Color.Green;
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

        

      
        //importo da file in c:/clienti.txt con dentro solo minsan i prodotti gestiti a robot
        private void button2_Click_1(object sender, EventArgs e)
        {
            int contatore = 0;
                string minsan;
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();

            
            try
            {
                FbCommand updatereset = new FbCommand("update magazzino set prodotto_robot = 'N'");
                updatereset.ExecuteNonQuery();
                using (StreamReader lettore = new StreamReader("c:/codici.txt"))
                {
                    while ((minsan = lettore.ReadLine()) != null)
                    {
                        FbCommand updaterobot = new FbCommand("update magazzino set prodotto_robot='S' where km10='" + minsan + "'", connesioneFb);
                        updaterobot.ExecuteNonQuery();
                        contatore++;
                    }
                    MessageBox.Show("aggiornati " + contatore.ToString() + " prodotti");
                }
                connesioneFb.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Problema con importazione");
              
            }
           
          }

        private void button3_Click(object sender, EventArgs e)
        {
            Form terminali = new terminali();
            terminali.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form dati_utili = new dati_utili();
            dati_utili.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnAzzeramentoPuntiCard_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set punti_catena='0'", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbx_linfa.Text = Utility_Winfarm.Properties.Settings.Default.percorsoLinfaServer;
            tbx_passwordLinfa.Text = Utility_Winfarm.Properties.Settings.Default.pwLinfa;
            tbx_passwordWinfarm.Text = Utility_Winfarm.Properties.Settings.Default.pwWinfarm;
            tbx_utenteFirebird.Text = Utility_Winfarm.Properties.Settings.Default.utenteWinfarm;
            tbx_utenteLinfa.Text = Utility_Winfarm.Properties.Settings.Default.utenteLinfa;
            tbx_winfarmPercorso.Text = Utility_Winfarm.Properties.Settings.Default.percorsoWinfarm;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Utility_Winfarm.Properties.Settings.Default.percorsoLinfaServer= tbx_linfa.Text;
            Utility_Winfarm.Properties.Settings.Default.pwLinfa= tbx_passwordLinfa.Text;
            Utility_Winfarm.Properties.Settings.Default.pwWinfarm= tbx_passwordWinfarm.Text;
            Utility_Winfarm.Properties.Settings.Default.utenteWinfarm = tbx_utenteFirebird.Text;
            Utility_Winfarm.Properties.Settings.Default.utenteLinfa= tbx_utenteLinfa.Text;
            Utility_Winfarm.Properties.Settings.Default.percorsoWinfarm= tbx_winfarmPercorso.Text;
            Utility_Winfarm.Properties.Settings.Default.Save();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("UPDATE ANAFORN SET ANAFORN.ATTIVA_FIDELITY='S' WHERE ANAFORN.COD_BADGE  > 0", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void btnFidelityDisattiva_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("UPDATE ANAFORN SET ANAFORN.ATTIVA_FIDELITY='N' WHERE ANAFORN.COD_BADGE  = 0", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("UPDATE ANAFORN SET ANAFORN.PUNTI_CATENA='0' WHERE ANAFORN.ATTIVA_FIDELITY='N'", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }

        }

        private void btnEfidelity_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update anaforn set codice_card = f_mid(1000000000 + cod_badge, 1, 9) where cod_badge <> 0 and codice_card = ''", connesioneFb);
                update.ExecuteNonQuery();
                FbCommand delete = new FbCommand("update anaforn set cod_badge = 0 where codice_card <> ''", connesioneFb);
                update.ExecuteNonQuery();
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }

        }

        private void btnApreGrid_Click(object sender, EventArgs e)
        {
            try
            {
                vista.Visible = true;
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand query = new FbCommand("select codkey,deskey,codice_card,cod_badge,punti_catena,f_mid(1000000000 + cod_badge, 1, 9) from anaforn where cod_badge <> 0 or codice_card <> '' order by codice_card, cod_badge", connesioneFb);
                FbDataAdapter adatta = new FbDataAdapter(query);
                adatta.Fill(dataSet1, "query");
                              
                vista.Height = 602;
                vista.Width = 855;
                vista.Visible = Enabled;
                vista.DataSource = dataSet1;
                vista.DataMember = "query";
                vista.AutoResizeColumns();
                button7.Visible = true;
                

                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            vista.Visible = false;
            button7.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set e_prezzo_listino = 0 where(select v_euro from vero_prezzo('TODAY', magazzino.km10, 4)) > 0 and e_prezzo_listino > 0 and(select v_euro from vero_prezzo('TODAY', magazzino.km10, 4)) > e_prezzo_listino", connesioneFb);
                update.ExecuteNonQuery();
               
                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set e_prezzo_listino = 0 where(select v_euro from vero_prezzo('TODAY', magazzino.km10, 4)) > 0 and e_prezzo_listino > 0", connesioneFb);
                update.ExecuteNonQuery();

                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set e_prezzo_farmacia = 0, prezzo_farmacia = 0 where(select v_euro from vero_prezzo('TODAY', magazzino.km10, 4)) > 0 and e_prezzo_farmacia >= (select v_euro from vero_prezzo('TODAY', magazzino.km10, 4))", connesioneFb);
                update.ExecuteNonQuery();

                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set e_prezzo_listino =0 where e_prezzo_farmacia = e_prezzo_listino and e_prezzo_farmacia > 0", connesioneFb);
                update.ExecuteNonQuery();

                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set magazzino.prezzo_farmacia=0,magazzino.e_prezzo_farmacia = 0 where(km10 in (select magazzino.km10 from magazzino inner join anapro on (magazzino.km10 = anapro.km10) where F_LRTRIM((SELECT V_CLASSE2 FROM VERA_CONC('TODAY', Anapro.KM10))) = 'A')) and (magazzino.e_prezzo_farmacia > 0)", connesioneFb);
                update.ExecuteNonQuery();

                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var data = (dateTimePicker1.Value).ToString("MM/dd/yyyy");
            try
                {
                    
                    FbConnection connesioneFb = connessione();
                    connesioneFb.Open();
                    FbCommand update = new FbCommand("delete from magazzino m where m.km10 in (Select m.km10 FROM MAGAZZINO m, ANAPRO A WHERE A.KM10 = m.KM10 AND m.giac_totale > 0 and m.ult_data_vendita <='"+data+"' and m.ult_data_acquisto <= m.ult_data_vendita)", connesioneFb);
                    update.ExecuteNonQuery();

                    connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
                
          
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {

                FbConnection connesioneFb = connessione();
                connesioneFb.Open();
                FbCommand update = new FbCommand("update magazzino set costo_medio=0, costo_ultimo=0, e_costo_medio=0, e_costo_ultimo=0 where km10 not in (SELECT KM10 FROM ORD_RIGHE)", connesioneFb);
                update.ExecuteNonQuery();

                connesioneFb.Close();
                MessageBox.Show("update eseguito correttamente");
            }
            catch
            {
                MessageBox.Show("problema2");
            }
        }
    }
}
