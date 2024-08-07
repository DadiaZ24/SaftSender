namespace saft_sender
{
    partial class SaftSenderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaftSenderForm));
            this.submit_button = new System.Windows.Forms.Button();
            this.exit_button = new System.Windows.Forms.Button();
            this.opensaft_button = new System.Windows.Forms.Button();
            this.nif = new System.Windows.Forms.Label();
            this.opensaft_dialog = new System.Windows.Forms.OpenFileDialog();
            this.nif_txtbox = new System.Windows.Forms.TextBox();
            this.pass_txtbox = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.Label();
            this.year_txtbox = new System.Windows.Forms.TextBox();
            this.year = new System.Windows.Forms.Label();
            this.month_txtbox = new System.Windows.Forms.TextBox();
            this.month = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.title1 = new System.Windows.Forms.Label();
            this.title2 = new System.Windows.Forms.Label();
            this.downloadjar_button = new System.Windows.Forms.Button();
            this.resumesaft_button = new System.Windows.Forms.Button();
            this.loading = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // submit_button
            // 
            this.submit_button.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submit_button.Location = new System.Drawing.Point(176, 431);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(134, 41);
            this.submit_button.TabIndex = 0;
            this.submit_button.Text = "Submeter";
            this.submit_button.UseVisualStyleBackColor = true;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
            // 
            // exit_button
            // 
            this.exit_button.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_button.Location = new System.Drawing.Point(18, 431);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(134, 41);
            this.exit_button.TabIndex = 1;
            this.exit_button.Text = "Sair";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // opensaft_button
            // 
            this.opensaft_button.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opensaft_button.Location = new System.Drawing.Point(113, 325);
            this.opensaft_button.Name = "opensaft_button";
            this.opensaft_button.Size = new System.Drawing.Size(89, 34);
            this.opensaft_button.TabIndex = 5;
            this.opensaft_button.Text = "Abrir saft";
            this.opensaft_button.UseVisualStyleBackColor = true;
            this.opensaft_button.Click += new System.EventHandler(this.opensaft_button_Click);
            // 
            // nif
            // 
            this.nif.AutoSize = true;
            this.nif.BackColor = System.Drawing.SystemColors.Control;
            this.nif.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nif.Location = new System.Drawing.Point(61, 122);
            this.nif.Name = "nif";
            this.nif.Size = new System.Drawing.Size(36, 18);
            this.nif.TabIndex = 6;
            this.nif.Text = "NIF";
            // 
            // opensaft_dialog
            // 
            this.opensaft_dialog.FileName = "opensaft_dialog";
            // 
            // nif_txtbox
            // 
            this.nif_txtbox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nif_txtbox.Location = new System.Drawing.Point(113, 119);
            this.nif_txtbox.Name = "nif_txtbox";
            this.nif_txtbox.Size = new System.Drawing.Size(135, 27);
            this.nif_txtbox.TabIndex = 7;
            // 
            // pass_txtbox
            // 
            this.pass_txtbox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pass_txtbox.Location = new System.Drawing.Point(113, 168);
            this.pass_txtbox.Name = "pass_txtbox";
            this.pass_txtbox.Size = new System.Drawing.Size(135, 27);
            this.pass_txtbox.TabIndex = 9;
            // 
            // pass
            // 
            this.pass.AutoSize = true;
            this.pass.BackColor = System.Drawing.SystemColors.Control;
            this.pass.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pass.Location = new System.Drawing.Point(13, 171);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(84, 18);
            this.pass.TabIndex = 8;
            this.pass.Text = "Senha AT";
            // 
            // year_txtbox
            // 
            this.year_txtbox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.year_txtbox.Location = new System.Drawing.Point(113, 224);
            this.year_txtbox.Name = "year_txtbox";
            this.year_txtbox.Size = new System.Drawing.Size(135, 27);
            this.year_txtbox.TabIndex = 11;
            // 
            // year
            // 
            this.year.AutoSize = true;
            this.year.BackColor = System.Drawing.SystemColors.Control;
            this.year.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.year.Location = new System.Drawing.Point(58, 227);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(39, 18);
            this.year.TabIndex = 10;
            this.year.Text = "Ano";
            // 
            // month_txtbox
            // 
            this.month_txtbox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.month_txtbox.Location = new System.Drawing.Point(113, 275);
            this.month_txtbox.Name = "month_txtbox";
            this.month_txtbox.Size = new System.Drawing.Size(135, 27);
            this.month_txtbox.TabIndex = 13;
            // 
            // month
            // 
            this.month.AutoSize = true;
            this.month.BackColor = System.Drawing.SystemColors.Control;
            this.month.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.month.Location = new System.Drawing.Point(58, 278);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(40, 18);
            this.month.TabIndex = 12;
            this.month.Text = "Mês";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(316, -16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(482, 473);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // title1
            // 
            this.title1.AutoSize = true;
            this.title1.BackColor = System.Drawing.Color.Transparent;
            this.title1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.title1.Location = new System.Drawing.Point(12, 9);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(277, 32);
            this.title1.TabIndex = 15;
            this.title1.Text = "Envio automático";
            // 
            // title2
            // 
            this.title2.AutoSize = true;
            this.title2.BackColor = System.Drawing.Color.Transparent;
            this.title2.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.title2.Location = new System.Drawing.Point(107, 51);
            this.title2.Name = "title2";
            this.title2.Size = new System.Drawing.Size(83, 32);
            this.title2.TabIndex = 16;
            this.title2.Text = "A AT";
            // 
            // downloadjar_button
            // 
            this.downloadjar_button.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadjar_button.Location = new System.Drawing.Point(46, 378);
            this.downloadjar_button.Name = "downloadjar_button";
            this.downloadjar_button.Size = new System.Drawing.Size(89, 34);
            this.downloadjar_button.TabIndex = 17;
            this.downloadjar_button.Text = "Download JAR";
            this.downloadjar_button.UseVisualStyleBackColor = true;
            this.downloadjar_button.Click += new System.EventHandler(this.downloadjar_button_Click);
            // 
            // resumesaft_button
            // 
            this.resumesaft_button.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resumesaft_button.Location = new System.Drawing.Point(176, 378);
            this.resumesaft_button.Name = "resumesaft_button";
            this.resumesaft_button.Size = new System.Drawing.Size(89, 34);
            this.resumesaft_button.TabIndex = 18;
            this.resumesaft_button.Text = "Guardar saft resumido";
            this.resumesaft_button.UseVisualStyleBackColor = true;
            this.resumesaft_button.Click += new System.EventHandler(this.resumesaft_button_Click);
            // 
            // loading
            // 
            this.loading.Location = new System.Drawing.Point(292, 279);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(322, 23);
            this.loading.TabIndex = 19;
            this.loading.Click += new System.EventHandler(this.loading_Click);
            this.loading.Visible = false;
            // 
            // SaftSenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(797, 485);
            this.Controls.Add(this.loading);
            this.Controls.Add(this.resumesaft_button);
            this.Controls.Add(this.downloadjar_button);
            this.Controls.Add(this.title2);
            this.Controls.Add(this.title1);
            this.Controls.Add(this.month_txtbox);
            this.Controls.Add(this.month);
            this.Controls.Add(this.year_txtbox);
            this.Controls.Add(this.year);
            this.Controls.Add(this.pass_txtbox);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.nif_txtbox);
            this.Controls.Add(this.nif);
            this.Controls.Add(this.opensaft_button);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "SaftSenderForm";
            this.Text = "Saft Sender v1.0";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Button opensaft_button;
        private System.Windows.Forms.Label nif;
        private System.Windows.Forms.OpenFileDialog opensaft_dialog;
        private System.Windows.Forms.TextBox nif_txtbox;
        private System.Windows.Forms.TextBox pass_txtbox;
        private System.Windows.Forms.Label pass;
        private System.Windows.Forms.TextBox year_txtbox;
        private System.Windows.Forms.Label year;
        private System.Windows.Forms.TextBox month_txtbox;
        private System.Windows.Forms.Label month;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label title1;
        private System.Windows.Forms.Label title2;
        private System.Windows.Forms.Button downloadjar_button;
        private System.Windows.Forms.Button resumesaft_button;
        private System.Windows.Forms.ProgressBar loading;
    }
}

