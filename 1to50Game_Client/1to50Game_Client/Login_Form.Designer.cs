namespace _1to50Game_Client
{
    partial class Login_Form
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Form));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.TextBox();
            this.Game_Start = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.Join_Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("굴림", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(164, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("굴림", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(189, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "ID";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(247, 58);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(100, 21);
            this.Password.TabIndex = 16;
            // 
            // ID
            // 
            this.ID.Location = new System.Drawing.Point(247, 12);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(100, 21);
            this.ID.TabIndex = 15;
            // 
            // Game_Start
            // 
            this.Game_Start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Game_Start.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Game_Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Game_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Game_Start.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.Game_Start.Location = new System.Drawing.Point(375, 15);
            this.Game_Start.Name = "Game_Start";
            this.Game_Start.Size = new System.Drawing.Size(89, 67);
            this.Game_Start.TabIndex = 14;
            this.Game_Start.Text = "Battle Start";
            this.Game_Start.UseVisualStyleBackColor = false;
            this.Game_Start.Click += new System.EventHandler(this.Game_Start_Click_1);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(31, 13);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 21);
            this.txtIP.TabIndex = 28;
            this.txtIP.Visible = false;
            // 
            // Join_Start
            // 
            this.Join_Start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Join_Start.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Join_Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Join_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Join_Start.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Join_Start.Location = new System.Drawing.Point(375, 175);
            this.Join_Start.Name = "Join_Start";
            this.Join_Start.Size = new System.Drawing.Size(89, 70);
            this.Join_Start.TabIndex = 29;
            this.Join_Start.Text = "회원가입";
            this.Join_Start.UseVisualStyleBackColor = false;
            this.Join_Start.Click += new System.EventHandler(this.Join_Start_Click_1);
            // 
            // Login_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(487, 390);
            this.Controls.Add(this.Join_Start);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.Game_Start);
            this.Name = "Login_Form";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Login_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Game_Start;
        public System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button Join_Start;
        public System.Windows.Forms.TextBox ID;
        public System.Windows.Forms.TextBox Password;
    }
}

