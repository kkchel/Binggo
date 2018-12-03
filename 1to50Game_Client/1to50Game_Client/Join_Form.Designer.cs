namespace _1to50Game_Client
{
    partial class Join_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Join_Form));
            this.complete_Join = new System.Windows.Forms.Button();
            this.DuplicateConfirm = new System.Windows.Forms.Button();
            this.conPW = new System.Windows.Forms.TextBox();
            this.newPW = new System.Windows.Forms.TextBox();
            this.newID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // complete_Join
            // 
            this.complete_Join.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.complete_Join.FlatAppearance.BorderSize = 0;
            this.complete_Join.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.complete_Join.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.complete_Join.Font = new System.Drawing.Font("휴먼편지체", 11F);
            this.complete_Join.Location = new System.Drawing.Point(137, 130);
            this.complete_Join.Name = "complete_Join";
            this.complete_Join.Size = new System.Drawing.Size(75, 24);
            this.complete_Join.TabIndex = 43;
            this.complete_Join.Text = "가입하기";
            this.complete_Join.UseVisualStyleBackColor = false;
            this.complete_Join.Click += new System.EventHandler(this.complete_Join_Click);
            // 
            // DuplicateConfirm
            // 
            this.DuplicateConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DuplicateConfirm.FlatAppearance.BorderSize = 0;
            this.DuplicateConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DuplicateConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DuplicateConfirm.Font = new System.Drawing.Font("휴먼편지체", 11F);
            this.DuplicateConfirm.Location = new System.Drawing.Point(245, 26);
            this.DuplicateConfirm.Name = "DuplicateConfirm";
            this.DuplicateConfirm.Size = new System.Drawing.Size(75, 23);
            this.DuplicateConfirm.TabIndex = 42;
            this.DuplicateConfirm.Text = "중복 확인";
            this.DuplicateConfirm.UseVisualStyleBackColor = false;
            this.DuplicateConfirm.Click += new System.EventHandler(this.DuplicateConfirm_Click);
            // 
            // conPW
            // 
            this.conPW.Location = new System.Drawing.Point(127, 98);
            this.conPW.Name = "conPW";
            this.conPW.Size = new System.Drawing.Size(100, 21);
            this.conPW.TabIndex = 41;
            // 
            // newPW
            // 
            this.newPW.Location = new System.Drawing.Point(127, 64);
            this.newPW.Name = "newPW";
            this.newPW.Size = new System.Drawing.Size(100, 21);
            this.newPW.TabIndex = 40;
            // 
            // newID
            // 
            this.newID.Location = new System.Drawing.Point(127, 28);
            this.newID.Name = "newID";
            this.newID.Size = new System.Drawing.Size(100, 21);
            this.newID.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("굴림", 10F);
            this.label5.Location = new System.Drawing.Point(13, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 14);
            this.label5.TabIndex = 38;
            this.label5.Text = "비밀번호 확인";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("굴림", 10F);
            this.label4.Location = new System.Drawing.Point(28, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 37;
            this.label4.Text = "비밀번호";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 10F);
            this.label3.Location = new System.Drawing.Point(36, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 36;
            this.label3.Text = "아이디";
            // 
            // Join_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(344, 178);
            this.Controls.Add(this.complete_Join);
            this.Controls.Add(this.DuplicateConfirm);
            this.Controls.Add(this.conPW);
            this.Controls.Add(this.newPW);
            this.Controls.Add(this.newID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "Join_Form";
            this.Text = "Join_Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button complete_Join;
        private System.Windows.Forms.Button DuplicateConfirm;
        private System.Windows.Forms.TextBox conPW;
        private System.Windows.Forms.TextBox newPW;
        private System.Windows.Forms.TextBox newID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}