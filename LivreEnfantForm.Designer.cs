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
            this.nomLabel = new System.Windows.Forms.Label();
            this.classeLabel = new System.Windows.Forms.Label();
            this.titreLivreLabel = new System.Windows.Forms.Label();
            this.nbrsJoursLabel = new System.Windows.Forms.Label();
            this.nomTextBox = new System.Windows.Forms.TextBox();
            this.classeTextBox = new System.Windows.Forms.TextBox();
            this.titreLivreTextBox = new System.Windows.Forms.TextBox();
            this.nbrJoursTextBox = new System.Windows.Forms.TextBox();
            this.raisonLabel = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // nomLabel
            // 
            this.nomLabel.AutoSize = true;
            this.nomLabel.Location = new System.Drawing.Point(10, 31);
            this.nomLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nomLabel.Name = "nomLabel";
            this.nomLabel.Size = new System.Drawing.Size(106, 20);
            this.nomLabel.TabIndex = 0;
            this.nomLabel.Text = "Nom Etudiant";
            // 
            // classeLabel
            // 
            this.classeLabel.AutoSize = true;
            this.classeLabel.Location = new System.Drawing.Point(10, 72);
            this.classeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.classeLabel.Name = "classeLabel";
            this.classeLabel.Size = new System.Drawing.Size(57, 20);
            this.classeLabel.TabIndex = 1;
            this.classeLabel.Text = "Classe";
            // 
            // titreLivreLabel
            // 
            this.titreLivreLabel.AutoSize = true;
            this.titreLivreLabel.Location = new System.Drawing.Point(10, 114);
            this.titreLivreLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titreLivreLabel.Name = "titreLivreLabel";
            this.titreLivreLabel.Size = new System.Drawing.Size(93, 20);
            this.titreLivreLabel.TabIndex = 2;
            this.titreLivreLabel.Text = "Titre du livre";
            // 
            // nbrsJoursLabel
            // 
            this.nbrsJoursLabel.AutoSize = true;
            this.nbrsJoursLabel.Location = new System.Drawing.Point(10, 152);
            this.nbrsJoursLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nbrsJoursLabel.Name = "nbrsJoursLabel";
            this.nbrsJoursLabel.Size = new System.Drawing.Size(125, 20);
            this.nbrsJoursLabel.TabIndex = 3;
            this.nbrsJoursLabel.Text = "Nombre de jours";
            // 
            // nomTextBox
            // 
            this.nomTextBox.Location = new System.Drawing.Point(227, 31);
            this.nomTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.nomTextBox.Name = "nomTextBox";
            this.nomTextBox.Size = new System.Drawing.Size(83, 26);
            this.nomTextBox.TabIndex = 4;
            // 
            // classeTextBox
            // 
            this.classeTextBox.Location = new System.Drawing.Point(227, 68);
            this.classeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.classeTextBox.Name = "classeTextBox";
            this.classeTextBox.Size = new System.Drawing.Size(83, 26);
            this.classeTextBox.TabIndex = 5;
            // 
            // titreLivreTextBox
            // 
            this.titreLivreTextBox.Location = new System.Drawing.Point(227, 111);
            this.titreLivreTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.titreLivreTextBox.Name = "titreLivreTextBox";
            this.titreLivreTextBox.Size = new System.Drawing.Size(83, 26);
            this.titreLivreTextBox.TabIndex = 6;
            // 
            // nbrJoursTextBox
            // 
            this.nbrJoursTextBox.Location = new System.Drawing.Point(227, 152);
            this.nbrJoursTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.nbrJoursTextBox.Name = "nbrJoursTextBox";
            this.nbrJoursTextBox.Size = new System.Drawing.Size(83, 26);
            this.nbrJoursTextBox.TabIndex = 7;
            // 
            // raisonLabel
            // 
            this.raisonLabel.AutoSize = true;
            this.raisonLabel.Location = new System.Drawing.Point(10, 220);
            this.raisonLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.raisonLabel.Name = "raisonLabel";
            this.raisonLabel.Size = new System.Drawing.Size(150, 20);
            this.raisonLabel.TabIndex = 8;
            this.raisonLabel.Text = "Raison de l\'emprunt";
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(14, 252);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(301, 115);
            this.richTextBox.TabIndex = 9;
            this.richTextBox.Text = "";
            // 
            // LivreEnfantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 394);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.raisonLabel);
            this.Controls.Add(this.nbrJoursTextBox);
            this.Controls.Add(this.titreLivreTextBox);
            this.Controls.Add(this.classeTextBox);
            this.Controls.Add(this.nomTextBox);
            this.Controls.Add(this.nbrsJoursLabel);
            this.Controls.Add(this.titreLivreLabel);
            this.Controls.Add(this.classeLabel);
            this.Controls.Add(this.nomLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LivreEnfantForm";
            this.Text = "Livre Bibliotheque ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LivreEnfantForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nomLabel;
        private System.Windows.Forms.Label classeLabel;
        private System.Windows.Forms.Label titreLivreLabel;
        private System.Windows.Forms.Label nbrsJoursLabel;
        private System.Windows.Forms.TextBox nomTextBox;
        private System.Windows.Forms.TextBox classeTextBox;
        private System.Windows.Forms.TextBox titreLivreTextBox;
        private System.Windows.Forms.TextBox nbrJoursTextBox;
        private System.Windows.Forms.Label raisonLabel;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}