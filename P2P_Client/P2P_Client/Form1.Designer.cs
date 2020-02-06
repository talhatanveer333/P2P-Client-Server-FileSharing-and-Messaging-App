namespace P2P_Client
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.GroupChat = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SingleChat = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.srvrIP = new System.Windows.Forms.TextBox();
            this.destinPortNo = new System.Windows.Forms.ComboBox();
            this.srvrPortNo = new System.Windows.Forms.MaskedTextBox();
            this.ServerPort = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerIP = new System.Windows.Forms.Label();
            this.downloadBtn = new System.Windows.Forms.Button();
            this.destinPeer = new System.Windows.Forms.ComboBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.GroupChat.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SingleChat.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SingleChat);
            this.tabControl1.Controls.Add(this.GroupChat);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 592);
            this.tabControl1.TabIndex = 8;
            // 
            // GroupChat
            // 
            this.GroupChat.Controls.Add(this.panel4);
            this.GroupChat.Controls.Add(this.label3);
            this.GroupChat.Controls.Add(this.textBox2);
            this.GroupChat.Controls.Add(this.listBox2);
            this.GroupChat.Location = new System.Drawing.Point(4, 22);
            this.GroupChat.Name = "GroupChat";
            this.GroupChat.Padding = new System.Windows.Forms.Padding(3);
            this.GroupChat.Size = new System.Drawing.Size(880, 566);
            this.GroupChat.TabIndex = 1;
            this.GroupChat.Text = "Group Chat Room";
            this.GroupChat.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Enter your Message :";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(92, 7);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(518, 13);
            this.textBox2.TabIndex = 10;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(224)))));
            this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 17;
            this.listBox2.Location = new System.Drawing.Point(5, 47);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(605, 510);
            this.listBox2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 592);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(888, 592);
            this.panel3.TabIndex = 9;
            // 
            // SingleChat
            // 
            this.SingleChat.BackgroundImage = global::P2P_Client.Properties.Resources.aleksandr_ledogorov_G_JJy_Yv_dA_unsplash;
            this.SingleChat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SingleChat.Controls.Add(this.label1);
            this.SingleChat.Controls.Add(this.panel1);
            this.SingleChat.Controls.Add(this.downloadBtn);
            this.SingleChat.Controls.Add(this.destinPeer);
            this.SingleChat.Controls.Add(this.sendBtn);
            this.SingleChat.Controls.Add(this.listBox1);
            this.SingleChat.Location = new System.Drawing.Point(4, 22);
            this.SingleChat.Name = "SingleChat";
            this.SingleChat.Padding = new System.Windows.Forms.Padding(3);
            this.SingleChat.Size = new System.Drawing.Size(880, 566);
            this.SingleChat.TabIndex = 0;
            this.SingleChat.Text = "Single Chat";
            this.SingleChat.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(35, 421);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "Peers : ";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.srvrIP);
            this.panel1.Controls.Add(this.destinPortNo);
            this.panel1.Controls.Add(this.srvrPortNo);
            this.panel1.Controls.Add(this.ServerPort);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ServerIP);
            this.panel1.ForeColor = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(646, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 558);
            this.panel1.TabIndex = 14;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 62);
            this.button1.TabIndex = 1;
            this.button1.Text = "Connect to server";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // srvrIP
            // 
            this.srvrIP.BackColor = System.Drawing.Color.IndianRed;
            this.srvrIP.Location = new System.Drawing.Point(12, 67);
            this.srvrIP.Name = "srvrIP";
            this.srvrIP.Size = new System.Drawing.Size(100, 20);
            this.srvrIP.TabIndex = 0;
            // 
            // destinPortNo
            // 
            this.destinPortNo.BackColor = System.Drawing.Color.IndianRed;
            this.destinPortNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destinPortNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.destinPortNo.FormattingEnabled = true;
            this.destinPortNo.Location = new System.Drawing.Point(11, 274);
            this.destinPortNo.Name = "destinPortNo";
            this.destinPortNo.Size = new System.Drawing.Size(183, 21);
            this.destinPortNo.TabIndex = 3;
            // 
            // srvrPortNo
            // 
            this.srvrPortNo.BackColor = System.Drawing.Color.IndianRed;
            this.srvrPortNo.Location = new System.Drawing.Point(12, 169);
            this.srvrPortNo.Mask = "00000";
            this.srvrPortNo.Name = "srvrPortNo";
            this.srvrPortNo.Size = new System.Drawing.Size(75, 20);
            this.srvrPortNo.TabIndex = 1;
            // 
            // ServerPort
            // 
            this.ServerPort.AutoSize = true;
            this.ServerPort.BackColor = System.Drawing.Color.Transparent;
            this.ServerPort.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerPort.Location = new System.Drawing.Point(9, 135);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(110, 19);
            this.ServerPort.TabIndex = 1;
            this.ServerPort.Text = "Server Port # :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Client Address :";
            // 
            // ServerIP
            // 
            this.ServerIP.AutoSize = true;
            this.ServerIP.BackColor = System.Drawing.Color.Transparent;
            this.ServerIP.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerIP.Location = new System.Drawing.Point(9, 31);
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Size = new System.Drawing.Size(142, 19);
            this.ServerIP.TabIndex = 11;
            this.ServerIP.Text = "Server IP Address:";
            // 
            // downloadBtn
            // 
            this.downloadBtn.BackColor = System.Drawing.Color.Transparent;
            this.downloadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.downloadBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadBtn.ForeColor = System.Drawing.Color.White;
            this.downloadBtn.Location = new System.Drawing.Point(5, 459);
            this.downloadBtn.Name = "downloadBtn";
            this.downloadBtn.Size = new System.Drawing.Size(123, 40);
            this.downloadBtn.TabIndex = 13;
            this.downloadBtn.Text = "Download File";
            this.downloadBtn.UseVisualStyleBackColor = false;
            this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
            // 
            // destinPeer
            // 
            this.destinPeer.BackColor = System.Drawing.Color.IndianRed;
            this.destinPeer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destinPeer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.destinPeer.FormattingEnabled = true;
            this.destinPeer.Location = new System.Drawing.Point(114, 421);
            this.destinPeer.Name = "destinPeer";
            this.destinPeer.Size = new System.Drawing.Size(136, 21);
            this.destinPeer.TabIndex = 9;
            // 
            // sendBtn
            // 
            this.sendBtn.BackColor = System.Drawing.Color.Transparent;
            this.sendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sendBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendBtn.ForeColor = System.Drawing.Color.White;
            this.sendBtn.Location = new System.Drawing.Point(143, 459);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(123, 40);
            this.sendBtn.TabIndex = 5;
            this.sendBtn.Text = "Send File";
            this.sendBtn.UseVisualStyleBackColor = false;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.IndianRed;
            this.listBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(395, 382);
            this.listBox1.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::P2P_Client.Properties.Resources.lucas_silva_pinheiro_santos_mpVzB3421lk_unsplash;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(616, 7);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(256, 547);
            this.panel4.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 592);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.GroupChat.ResumeLayout(false);
            this.GroupChat.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.SingleChat.ResumeLayout(false);
            this.SingleChat.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label ServerIP;
        private System.Windows.Forms.Label ServerPort;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SingleChat;
        private System.Windows.Forms.TabPage GroupChat;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ComboBox destinPortNo;
        private System.Windows.Forms.ComboBox destinPeer;
        private System.Windows.Forms.TextBox srvrIP;
        private System.Windows.Forms.MaskedTextBox srvrPortNo;
        private System.Windows.Forms.Button downloadBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}

