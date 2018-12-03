namespace _1to50Game_Client
{
    partial class Waiting_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Waiting_Form));
            this.label1 = new System.Windows.Forms.Label();
            this.record = new System.Windows.Forms.Label();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtTTS = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("굴림", 15F);
            this.label1.Location = new System.Drawing.Point(38, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID적기";
            // 
            // record
            // 
            this.record.AutoSize = true;
            this.record.BackColor = System.Drawing.Color.Transparent;
            this.record.Location = new System.Drawing.Point(38, 62);
            this.record.Name = "record";
            this.record.Size = new System.Drawing.Size(57, 12);
            this.record.TabIndex = 1;
            this.record.Text = "전적 적기";
            // 
            // txtChat
            // 
            this.txtChat.BackColor = System.Drawing.SystemColors.Window;
            this.txtChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChat.Location = new System.Drawing.Point(22, 116);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(534, 224);
            this.txtChat.TabIndex = 18;
            this.txtChat.WordWrap = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(562, 116);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(150, 196);
            this.listBox1.TabIndex = 19;
            // 
            // txtTTS
            // 
            this.txtTTS.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtTTS.Location = new System.Drawing.Point(22, 346);
            this.txtTTS.Name = "txtTTS";
            this.txtTTS.Size = new System.Drawing.Size(478, 21);
            this.txtTTS.TabIndex = 20;
            this.txtTTS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTTS_Enter);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("휴먼편지체", 11F);
            this.button1.Location = new System.Drawing.Point(506, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 22);
            this.button1.TabIndex = 21;
            this.button1.Text = "전송";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.SendData);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("휴먼편지체", 11F);
            this.button2.Location = new System.Drawing.Point(562, 319);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 38);
            this.button2.TabIndex = 22;
            this.button2.Text = "게임시작";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Game_start);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("휴먼편지체", 12F);
            this.button3.Location = new System.Drawing.Point(638, 319);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 38);
            this.button3.TabIndex = 23;
            this.button3.Text = "1:1대전";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.Button_VS);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("휴먼편지체", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button4.Location = new System.Drawing.Point(562, 24);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 86);
            this.button4.TabIndex = 24;
            this.button4.Text = "기록 보기";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.Record_View);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("휴먼편지체", 13F);
            this.button5.Location = new System.Drawing.Point(562, 369);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(144, 40);
            this.button5.TabIndex = 25;
            this.button5.Text = "사용자 게임";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Waiting_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(724, 442);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTTS);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.record);
            this.Controls.Add(this.label1);
            this.Name = "Waiting_Form";
            this.Text = "Waiting_Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Close_All);
            this.Load += new System.EventHandler(this.Waiting_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label record;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtTTS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}