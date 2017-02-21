namespace Utility_Winfarm
{
    partial class terminali
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(terminali));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_testwinfarm = new System.Windows.Forms.Button();
            this.tbx_serverWinfarm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_passwordWinfarm = new System.Windows.Forms.TextBox();
            this.tbx_winfarmPercorso = new System.Windows.Forms.TextBox();
            this.tbx_utenteFirebird = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataSet1 = new System.Data.DataSet();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataSet2 = new System.Data.DataSet();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DodgerBlue;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btn_testwinfarm);
            this.groupBox1.Controls.Add(this.tbx_serverWinfarm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbx_passwordWinfarm);
            this.groupBox1.Controls.Add(this.tbx_winfarmPercorso);
            this.groupBox1.Controls.Add(this.tbx_utenteFirebird);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(845, 85);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametri";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 2;
            this.label7.Text = "Server Winfarm";
            // 
            // btn_testwinfarm
            // 
            this.btn_testwinfarm.BackColor = System.Drawing.Color.Red;
            this.btn_testwinfarm.Location = new System.Drawing.Point(757, 25);
            this.btn_testwinfarm.Name = "btn_testwinfarm";
            this.btn_testwinfarm.Size = new System.Drawing.Size(75, 23);
            this.btn_testwinfarm.TabIndex = 1;
            this.btn_testwinfarm.Text = "Test";
            this.btn_testwinfarm.UseVisualStyleBackColor = false;
            this.btn_testwinfarm.Click += new System.EventHandler(this.btn_testwinfarm_Click);
            // 
            // tbx_serverWinfarm
            // 
            this.tbx_serverWinfarm.Location = new System.Drawing.Point(107, 54);
            this.tbx_serverWinfarm.Name = "tbx_serverWinfarm";
            this.tbx_serverWinfarm.Size = new System.Drawing.Size(146, 20);
            this.tbx_serverWinfarm.TabIndex = 3;
            this.tbx_serverWinfarm.Text = "localhost";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Percorso Winfarm";
            // 
            // tbx_passwordWinfarm
            // 
            this.tbx_passwordWinfarm.Location = new System.Drawing.Point(651, 28);
            this.tbx_passwordWinfarm.Name = "tbx_passwordWinfarm";
            this.tbx_passwordWinfarm.Size = new System.Drawing.Size(100, 20);
            this.tbx_passwordWinfarm.TabIndex = 3;
            this.tbx_passwordWinfarm.Text = "masterkey";
            // 
            // tbx_winfarmPercorso
            // 
            this.tbx_winfarmPercorso.Location = new System.Drawing.Point(107, 28);
            this.tbx_winfarmPercorso.Name = "tbx_winfarmPercorso";
            this.tbx_winfarmPercorso.Size = new System.Drawing.Size(432, 20);
            this.tbx_winfarmPercorso.TabIndex = 4;
            this.tbx_winfarmPercorso.Text = "c:/program files (x86)/winfarm/archivi/arc2000.phs";
            // 
            // tbx_utenteFirebird
            // 
            this.tbx_utenteFirebird.Location = new System.Drawing.Point(545, 28);
            this.tbx_utenteFirebird.Name = "tbx_utenteFirebird";
            this.tbx_utenteFirebird.Size = new System.Drawing.Size(100, 20);
            this.tbx_utenteFirebird.TabIndex = 7;
            this.tbx_utenteFirebird.Text = "SYSDBA";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 217);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(403, 420);
            this.dataGridView1.TabIndex = 13;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 63);
            this.button1.TabIndex = 14;
            this.button1.Text = "Dati Winfarm(tabella terminali)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(447, 217);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(410, 420);
            this.dataGridView2.TabIndex = 15;
            // 
            // dataSet2
            // 
            this.dataSet2.DataSetName = "NewDataSet";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(447, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 62);
            this.button2.TabIndex = 16;
            this.button2.Text = "Aggiorna";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(740, 104);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 62);
            this.button3.TabIndex = 18;
            this.button3.Text = "Uscita";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // terminali
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button3;
            this.ClientSize = new System.Drawing.Size(872, 649);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "terminali";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "terminali";
            this.Load += new System.EventHandler(this.terminali_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_testwinfarm;
        private System.Windows.Forms.TextBox tbx_serverWinfarm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_passwordWinfarm;
        private System.Windows.Forms.TextBox tbx_winfarmPercorso;
        private System.Windows.Forms.TextBox tbx_utenteFirebird;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Data.DataSet dataSet2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}