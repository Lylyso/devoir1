/*
    Programmeur:    Lydianne, Mohamed, Labib
    Date:           Septembre

    Solution:       BibliothequeParentForm.sln
    Projet:         BibliothequeParentForm.csproj
    Namespace:      {BibliothequeParentForm}
    Assembly:       BibliothequeParentForm.exe
    Classe:         BibliothequeParentForm.cs

*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace GestionBibliotheque
{
    public partial class BibliothequeParentForm : Form
    {
        private OpenFileDialog openFileDialog;

        public BibliothequeParentForm()
        {
            InitializeComponent();
        }

        #region Load
        private void BibliothequeParentForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialiser la boîte de dialogue Ouvrir
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Documents RTF (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*";
                openFileDialog.AddExtension = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.DefaultExt = "rtf";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’initialisation : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Méthode Ouvrir
               private void OuvrirToolStripMenuItem_Click_1(object sender, EventArgs e)

        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fichier = openFileDialog.FileName;

                    // Vérification de l’extension
                    if (Path.GetExtension(fichier).ToLower() != ".rtf")
                    {
                        MessageBox.Show("Erreur : le fichier doit être au format RTF.",
                                        "Extension invalide",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Créer un nouveau formulaire enfant
                    LivreEnfantForm enfant = new LivreEnfantForm();
                    enfant.MdiParent = this;
                    enfant.Text = fichier;

                    // RichTextBox temporaire
                    RichTextBox temp = new RichTextBox();
                    temp.LoadFile(fichier);

                    // Placer les lignes dans les zones de texte
                    if (temp.Lines.Length >= 3)
                    {
                       
                        enfant.NomTextBox.Text = temp.Lines[0];
                        enfant.ClasseTextBox.Text = temp.Lines[1];
                        enfant.TitreTextBox.Text = temp.Lines[2];
                        enfant. JoursTextBox.Text = (temp.Lines.Length > 3) ? temp.Lines[3] : "0";

                    }

                    // Supprimer les 3 premières lignes
                    int longueur = enfant.NomTextBox.Text.Length +
                                   enfant.ClasseTextBox.Text.Length +
                                   enfant.TitreTextBox.Text.Length + 3;

                    temp.SelectionStart = 0;
                    temp.SelectionLength = longueur;
                    temp.SelectedText = string.Empty;

                    // Copier le contenu restant
                    enfant.RaisonRichTextBox.Rtf = temp.Rtf;

                    // Mettre à jour les propriétés
                    enfant.Modification = false;
                    enfant.Enregistrement = true;

                    // Afficher l’enfant
                    enfant.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’ouverture : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Méthode Fermer
        private void FermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)
                {
                    this.ActiveMdiChild.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la fermeture : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Méthode Quitter
        private void QuitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la fermeture de l’application : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        
    }
}
