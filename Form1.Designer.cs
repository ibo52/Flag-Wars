namespace CWG
{
    partial class deneme
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(deneme));
            this.connect = new System.Windows.Forms.Button();
            this.listen = new System.Windows.Forms.Button();
            this.results = new System.Windows.Forms.ListBox();
            this.gameInfo = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sendit = new System.Windows.Forms.Button();
            this.warMap = new System.Windows.Forms.PictureBox();
            this.newText = new System.Windows.Forms.TextBox();
            this.musicCtrl = new System.Windows.Forms.Button();
            this.userInformText = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.warMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(15, 26);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(75, 23);
            this.connect.TabIndex = 0;
            this.connect.Text = "connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.ButtonConnectOnClick);
            // 
            // listen
            // 
            this.listen.Location = new System.Drawing.Point(96, 26);
            this.listen.Name = "listen";
            this.listen.Size = new System.Drawing.Size(73, 23);
            this.listen.TabIndex = 1;
            this.listen.Text = "create srvr";
            this.listen.UseVisualStyleBackColor = true;
            this.listen.Click += new System.EventHandler(this.ButtonListenOnClick);
            // 
            // results
            // 
            this.results.FormattingEnabled = true;
            this.results.Location = new System.Drawing.Point(12, 126);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(228, 121);
            this.results.TabIndex = 2;
            // 
            // gameInfo
            // 
            this.gameInfo.FormattingEnabled = true;
            this.gameInfo.Location = new System.Drawing.Point(12, 285);
            this.gameInfo.Name = "gameInfo";
            this.gameInfo.Size = new System.Drawing.Size(228, 121);
            this.gameInfo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "chatBOX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Game Progression Screen";
            // 
            // sendit
            // 
            this.sendit.Location = new System.Drawing.Point(165, 84);
            this.sendit.Name = "sendit";
            this.sendit.Size = new System.Drawing.Size(75, 23);
            this.sendit.TabIndex = 6;
            this.sendit.Text = "send";
            this.sendit.UseVisualStyleBackColor = true;
            this.sendit.Click += new System.EventHandler(this.ButtonSendOnClick);
            // 
            // warMap
            // 
            this.warMap.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.warMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.warMap.Image = global::CWG.Properties.Resources.warMap;
            this.warMap.Location = new System.Drawing.Point(269, 95);
            this.warMap.Name = "warMap";
            this.warMap.Size = new System.Drawing.Size(629, 311);
            this.warMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.warMap.TabIndex = 7;
            this.warMap.TabStop = false;
            this.warMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Mouse_clicked);
            // 
            // newText
            // 
            this.newText.Location = new System.Drawing.Point(15, 87);
            this.newText.Name = "newText";
            this.newText.Size = new System.Drawing.Size(144, 20);
            this.newText.TabIndex = 8;
            // 
            // musicCtrl
            // 
            this.musicCtrl.Location = new System.Drawing.Point(807, 12);
            this.musicCtrl.Name = "musicCtrl";
            this.musicCtrl.Size = new System.Drawing.Size(91, 23);
            this.musicCtrl.TabIndex = 9;
            this.musicCtrl.Text = "stop the music";
            this.musicCtrl.UseVisualStyleBackColor = true;
            this.musicCtrl.Click += new System.EventHandler(this.musicCtrl_Click);
            // 
            // userInformText
            // 
            this.userInformText.AutoSize = true;
            this.userInformText.BackColor = System.Drawing.SystemColors.Control;
            this.userInformText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.userInformText.Location = new System.Drawing.Point(265, 26);
            this.userInformText.Name = "userInformText";
            this.userInformText.Size = new System.Drawing.Size(155, 24);
            this.userInformText.TabIndex = 10;
            this.userInformText.Text = "Flag War Game";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(15, -3);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(75, 23);
            this.axWindowsMediaPlayer1.TabIndex = 11;
            this.axWindowsMediaPlayer1.Visible = false;
            // 
            // deneme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(907, 450);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.userInformText);
            this.Controls.Add(this.musicCtrl);
            this.Controls.Add(this.newText);
            this.Controls.Add(this.warMap);
            this.Controls.Add(this.sendit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameInfo);
            this.Controls.Add(this.results);
            this.Controls.Add(this.listen);
            this.Controls.Add(this.connect);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "deneme";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.deneme_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.warMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Button listen;
        private System.Windows.Forms.ListBox results;
        private System.Windows.Forms.ListBox gameInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sendit;
        private System.Windows.Forms.PictureBox warMap;
        private System.Windows.Forms.TextBox newText;
        private System.Windows.Forms.Button musicCtrl;
        private System.Windows.Forms.Label userInformText;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}

