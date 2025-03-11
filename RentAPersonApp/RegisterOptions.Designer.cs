namespace RentAPersonApp
{
    partial class RegisterOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterOptions));
            this.buttonBackToLogin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonJoinAsUser = new System.Windows.Forms.Button();
            this.buttonJoinAsProvider = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBackToLogin
            // 
            this.buttonBackToLogin.BackColor = System.Drawing.Color.Gold;
            this.buttonBackToLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBackToLogin.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBackToLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonBackToLogin.Location = new System.Drawing.Point(331, 455);
            this.buttonBackToLogin.Name = "buttonBackToLogin";
            this.buttonBackToLogin.Size = new System.Drawing.Size(137, 37);
            this.buttonBackToLogin.TabIndex = 74;
            this.buttonBackToLogin.Text = "Back";
            this.buttonBackToLogin.UseVisualStyleBackColor = false;
            this.buttonBackToLogin.Click += new System.EventHandler(this.buttonBackToLogin_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 122);
            this.panel1.TabIndex = 102;
            // 
            // closeButton
            // 
            this.closeButton.Image = global::RentAPersonApp.Properties.Resources.close_buton;
            this.closeButton.Location = new System.Drawing.Point(768, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(34, 31);
            this.closeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.closeButton.TabIndex = 3;
            this.closeButton.TabStop = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(20, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "RentAPerson";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::RentAPersonApp.Properties.Resources.debu_logo;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 18);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::RentAPersonApp.Properties.Resources.debucari;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttonJoinAsUser
            // 
            this.buttonJoinAsUser.BackgroundImage = global::RentAPersonApp.Properties.Resources.Capture;
            this.buttonJoinAsUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonJoinAsUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonJoinAsUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJoinAsUser.Location = new System.Drawing.Point(418, 177);
            this.buttonJoinAsUser.Name = "buttonJoinAsUser";
            this.buttonJoinAsUser.Size = new System.Drawing.Size(345, 236);
            this.buttonJoinAsUser.TabIndex = 44;
            this.buttonJoinAsUser.UseVisualStyleBackColor = true;
            this.buttonJoinAsUser.Click += new System.EventHandler(this.buttonJoinAsUser_Click);
            // 
            // buttonJoinAsProvider
            // 
            this.buttonJoinAsProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonJoinAsProvider.BackgroundImage")));
            this.buttonJoinAsProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonJoinAsProvider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonJoinAsProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonJoinAsProvider.Location = new System.Drawing.Point(32, 176);
            this.buttonJoinAsProvider.Name = "buttonJoinAsProvider";
            this.buttonJoinAsProvider.Size = new System.Drawing.Size(345, 236);
            this.buttonJoinAsProvider.TabIndex = 43;
            this.buttonJoinAsProvider.UseVisualStyleBackColor = true;
            this.buttonJoinAsProvider.Click += new System.EventHandler(this.buttonJoinAsProvider_Click);
            // 
            // RegisterOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(800, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonBackToLogin);
            this.Controls.Add(this.buttonJoinAsUser);
            this.Controls.Add(this.buttonJoinAsProvider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonJoinAsProvider;
        private System.Windows.Forms.Button buttonJoinAsUser;
        private System.Windows.Forms.Button buttonBackToLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox closeButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}