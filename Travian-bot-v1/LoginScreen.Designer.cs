namespace Travian_bot_v1
{
    partial class LoginScreen
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
            this.server = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.launchbtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trav = new System.Windows.Forms.Label();
            this.domain = new System.Windows.Forms.TextBox();
            this.http = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.passBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(48, 32);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(35, 20);
            this.server.TabIndex = 0;
            this.server.Text = "ts19";
            this.server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URL";
            // 
            // launchbtn
            // 
            this.launchbtn.Location = new System.Drawing.Point(175, 58);
            this.launchbtn.Name = "launchbtn";
            this.launchbtn.Size = new System.Drawing.Size(139, 23);
            this.launchbtn.TabIndex = 6;
            this.launchbtn.Text = "Launch";
            this.launchbtn.UseVisualStyleBackColor = true;
            this.launchbtn.Click += new System.EventHandler(this.launchbtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trav);
            this.groupBox1.Controls.Add(this.domain);
            this.groupBox1.Controls.Add(this.http);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.passBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nameBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.launchbtn);
            this.groupBox1.Controls.Add(this.server);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 95);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // trav
            // 
            this.trav.AutoSize = true;
            this.trav.Location = new System.Drawing.Point(81, 35);
            this.trav.Name = "trav";
            this.trav.Size = new System.Drawing.Size(45, 13);
            this.trav.TabIndex = 12;
            this.trav.Text = ".travian.";
            // 
            // domain
            // 
            this.domain.Location = new System.Drawing.Point(125, 32);
            this.domain.Name = "domain";
            this.domain.Size = new System.Drawing.Size(34, 20);
            this.domain.TabIndex = 10;
            this.domain.Text = "lt";
            this.domain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // http
            // 
            this.http.AutoSize = true;
            this.http.Location = new System.Drawing.Point(8, 35);
            this.http.Name = "http";
            this.http.Size = new System.Drawing.Size(43, 13);
            this.http.TabIndex = 11;
            this.http.Text = "https://";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(320, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Login";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Check for cookies";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(360, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "PASSWORD";
            // 
            // passBox
            // 
            this.passBox.Location = new System.Drawing.Point(320, 32);
            this.passBox.Name = "passBox";
            this.passBox.PasswordChar = '*';
            this.passBox.Size = new System.Drawing.Size(139, 20);
            this.passBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "LOGIN";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(175, 32);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(139, 20);
            this.nameBox.TabIndex = 3;
            this.nameBox.Text = "herqs";
            // 
            // LoginScreen
            // 
            this.AcceptButton = this.launchbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 110);
            this.Controls.Add(this.groupBox1);
            this.Name = "LoginScreen";
            this.Text = "T4Bot by Herqs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginScreenClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button launchbtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label trav;
        private System.Windows.Forms.TextBox domain;
        private System.Windows.Forms.Label http;
    }
}

