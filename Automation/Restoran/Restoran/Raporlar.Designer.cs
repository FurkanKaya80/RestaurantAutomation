namespace Restoran
{
    partial class Raporlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Raporlar));
            this.dTP1 = new System.Windows.Forms.DateTimePicker();
            this.dTP2 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDessert = new System.Windows.Forms.Button();
            this.btnSalad = new System.Windows.Forms.Button();
            this.btnSoup = new System.Windows.Forms.Button();
            this.btnDrinks = new System.Windows.Forms.Button();
            this.btnFastfood = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.graf = new Gigasoft.ProEssentials.Pepco();
            this.btnAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGeri = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dTP1
            // 
            this.dTP1.CalendarFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dTP1.CustomFormat = "dd/MM/yyyy";
            this.dTP1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dTP1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP1.Location = new System.Drawing.Point(945, 112);
            this.dTP1.Name = "dTP1";
            this.dTP1.Size = new System.Drawing.Size(263, 33);
            this.dTP1.TabIndex = 0;
            this.dTP1.Value = new System.DateTime(2022, 10, 26, 14, 30, 13, 0);
            // 
            // dTP2
            // 
            this.dTP2.CalendarFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dTP2.CustomFormat = "dd/MM/yyyy";
            this.dTP2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dTP2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP2.Location = new System.Drawing.Point(945, 159);
            this.dTP2.Name = "dTP2";
            this.dTP2.Size = new System.Drawing.Size(263, 33);
            this.dTP2.TabIndex = 1;
            this.dTP2.Value = new System.DateTime(2022, 10, 26, 14, 30, 9, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnDessert);
            this.groupBox1.Controls.Add(this.btnSalad);
            this.groupBox1.Controls.Add(this.btnSoup);
            this.groupBox1.Controls.Add(this.btnDrinks);
            this.groupBox1.Controls.Add(this.btnFastfood);
            this.groupBox1.Controls.Add(this.btnMain);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(51, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 512);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Menü";
            // 
            // btnDessert
            // 
            this.btnDessert.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDessert.BackgroundImage")));
            this.btnDessert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDessert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDessert.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDessert.Location = new System.Drawing.Point(254, 363);
            this.btnDessert.Name = "btnDessert";
            this.btnDessert.Size = new System.Drawing.Size(193, 107);
            this.btnDessert.TabIndex = 0;
            this.btnDessert.UseVisualStyleBackColor = true;
            this.btnDessert.Click += new System.EventHandler(this.btnDessert_Click);
            // 
            // btnSalad
            // 
            this.btnSalad.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSalad.BackgroundImage")));
            this.btnSalad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSalad.Location = new System.Drawing.Point(30, 363);
            this.btnSalad.Name = "btnSalad";
            this.btnSalad.Size = new System.Drawing.Size(193, 107);
            this.btnSalad.TabIndex = 0;
            this.btnSalad.UseVisualStyleBackColor = true;
            this.btnSalad.Click += new System.EventHandler(this.btnSalad_Click);
            // 
            // btnSoup
            // 
            this.btnSoup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSoup.BackgroundImage")));
            this.btnSoup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSoup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSoup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSoup.Location = new System.Drawing.Point(254, 215);
            this.btnSoup.Name = "btnSoup";
            this.btnSoup.Size = new System.Drawing.Size(193, 107);
            this.btnSoup.TabIndex = 0;
            this.btnSoup.UseVisualStyleBackColor = true;
            this.btnSoup.Click += new System.EventHandler(this.btnSoup_Click);
            // 
            // btnDrinks
            // 
            this.btnDrinks.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrinks.BackgroundImage")));
            this.btnDrinks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDrinks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDrinks.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDrinks.Location = new System.Drawing.Point(30, 215);
            this.btnDrinks.Name = "btnDrinks";
            this.btnDrinks.Size = new System.Drawing.Size(193, 107);
            this.btnDrinks.TabIndex = 0;
            this.btnDrinks.UseVisualStyleBackColor = true;
            this.btnDrinks.Click += new System.EventHandler(this.btnDrinks_Click);
            // 
            // btnFastfood
            // 
            this.btnFastfood.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFastfood.BackgroundImage")));
            this.btnFastfood.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFastfood.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFastfood.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFastfood.Location = new System.Drawing.Point(254, 70);
            this.btnFastfood.Name = "btnFastfood";
            this.btnFastfood.Size = new System.Drawing.Size(193, 107);
            this.btnFastfood.TabIndex = 0;
            this.btnFastfood.UseVisualStyleBackColor = true;
            this.btnFastfood.Click += new System.EventHandler(this.btnFastfood_Click);
            // 
            // btnMain
            // 
            this.btnMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMain.BackgroundImage")));
            this.btnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMain.Location = new System.Drawing.Point(30, 70);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(193, 107);
            this.btnMain.TabIndex = 0;
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // graf
            // 
            this.graf.BackColor = System.Drawing.SystemColors.Control;
            this.graf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.graf.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.graf.Location = new System.Drawing.Point(704, 236);
            this.graf.Name = "graf";
            this.graf.Size = new System.Drawing.Size(683, 433);
            this.graf.TabIndex = 4;
            this.graf.Text = "pepco1";
            this.graf.Visible = false;
            this.graf.PeDataHotSpot += new Gigasoft.ProEssentials.Pepco.DataHotSpotEventHandler(this.graf_PeDataHotSpot);
            this.graf.Click += new System.EventHandler(this.graf_Click);
            // 
            // btnAll
            // 
            this.btnAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAll.BackgroundImage")));
            this.btnAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAll.Location = new System.Drawing.Point(51, 636);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(193, 107);
            this.btnAll.TabIndex = 0;
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(713, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "Başlangıç Tarihi: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(713, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 30);
            this.label2.TabIndex = 5;
            this.label2.Text = "Bitiş Tarihi: ";
            // 
            // btnGeri
            // 
            this.btnGeri.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGeri.BackgroundImage")));
            this.btnGeri.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGeri.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeri.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGeri.Location = new System.Drawing.Point(1445, 673);
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(66, 48);
            this.btnGeri.TabIndex = 6;
            this.btnGeri.UseVisualStyleBackColor = true;
            this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // btnCikis
            // 
            this.btnCikis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCikis.BackgroundImage")));
            this.btnCikis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCikis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCikis.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCikis.Location = new System.Drawing.Point(1545, 673);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(66, 48);
            this.btnCikis.TabIndex = 6;
            this.btnCikis.UseVisualStyleBackColor = true;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // Raporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1940, 1047);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnGeri);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.graf);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dTP2);
            this.Controls.Add(this.dTP1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Raporlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raporlar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Raporlar_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateTimePicker dTP1;
        private DateTimePicker dTP2;
        private GroupBox groupBox1;
        private Gigasoft.ProEssentials.Pepco graf;
        private Button btnMain;
        private Button btnDessert;
        private Button btnSalad;
        private Button btnSoup;
        private Button btnDrinks;
        private Button btnFastfood;
        private Button btnAll;
        private Label label1;
        private Label label2;
        private Button btnGeri;
        private Button btnCikis;
    }
}