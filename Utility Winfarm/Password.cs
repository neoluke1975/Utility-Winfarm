using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility_Winfarm
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (textBox1.Text=="vivailcobol")
                {
                    Form1 form = new Form1();
                    this.Hide();
                    form.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hai cannato la password!!!!!Baluba");
                    this.Close();
                }
            }
        }
    }
}
