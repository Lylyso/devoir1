namespace GestionBibliotheque
{
    partial class LivreEnfantForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nomTextBox1 = new System.Windows.Forms.TextBox();
            this.classeTextBox2 = new System.Windows.Forms.TextBox();
            this.titreLivreTextBox3 = new System.Windows.Forms.TextBox();
            this.nbrJoursTextBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom Etudiant";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Classe";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Titre du livre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nombre de jours";
            // 
            // nomTextBox1
            // 
            this.nomTextBox1.Location = new System.Drawing.Point(227, 31);
            this.nomTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.nomTextBox1.Name = "nomTextBox1";
            this.nomTextBox1.Size = new System.Drawing.Size(83, 26);
            this.nomTextBox1.TabIndex = 4;
            // 
            // classeTextBox2
            // 
            this.classeTextBox2.Location = new System.Drawing.Point(227, 68);
            this.classeTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.classeTextBox2.Name = "classeTextBox2";
            this.classeTextBox2.Size = new System.Drawing.Size(83, 26);
            this.classeTextBox2.TabIndex = 5;
            // 
            // titreLivreTextBox3
            // 
            this.titreLivreTextBox3.Location = new System.Drawing.Point(227, 111);
            this.titreLivreTextBox3.Margin = new System.Windows.Forms.Padding(2);
            this.titreLivreTextBox3.Name = "titreLivreTextBox3";
            this.titreLivreTextBox3.Size = new System.Drawing.Size(83, 26);
            this.titreLivreTextBox3.TabIndex = 6;
            // 
            // nbrJoursTextBox4
            // 
            this.nbrJoursTextBox4.Location = new System.Drawing.Point(227, 152);
            this.nbrJoursTextBox4.Margin = new System.Windows.Forms.Padding(2);
            this.nbrJoursTextBox4.Name = "nbrJoursTextBox4";
            this.nbrJoursTextBox4.Size = new System.Drawing.Size(83, 26);
            this.nbrJoursTextBox4.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 220);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Raison de l\'emprunt";
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(12, 243);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(298, 113);
            this.richTextBox.TabIndex = 10;
            this.richTextBox.Text = "";
            // 
            // LivreEnfantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 394);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nbrJoursTextBox4);
            this.Controls.Add(this.titreLivreTextBox3);
            this.Controls.Add(this.classeTextBox2);
            this.Controls.Add(this.nomTextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LivreEnfantForm";
            this.Text = "Livre Bibliotheque ";
            this.Load += new System.EventHandler(this.LivreEnfantForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nomTextBox1;
        private System.Windows.Forms.TextBox classeTextBox2;
        private System.Windows.Forms.TextBox titreLivreTextBox3;
        private System.Windows.Forms.TextBox nbrJoursTextBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}