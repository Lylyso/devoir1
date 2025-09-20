/*
    Programmeur:    Lydianne, Mohamed, Labib
    Date:           Septembre

    Solution:       BibliothequeParentForm.sln
    Projet:         BibliothequeParentForm.csproj
    Namespace:      {BibliothequeParentForm}
    Assembly:       BibliothequeParentForm.exe
    Classe:         BibliothequeParentForm.cs

*/
/*
    Programmeur : Mohamed Kach
    Date        : Septembre 2025
    Classe      : Parent (MDIParent)
    Rôle        : Gestion des enfants (nouveau, ouvrir, enregistrer, fermer, quitter)
*/

using GestionBibliotheque;
using System;
using System.IO;                // Pour Path.GetExtension
using System.Linq;
using System.Windows.Forms;

namespace GestionBibliotheque
{
    public partial class BibliothequeParentForm : Form
    {
        #region Variables

        private int nbClient = 1;
        private OpenFileDialog ofd;

        #endregion

        #region Constructeur

        public BibliothequeParentForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Load

        private void Parent_Load(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Rich Text Format (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*";
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "rtf";
        }

        #endregion

        #region Nouveau Client

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LivreEnfantForm oEnfant = new LivreEnfantForm();
            oEnfant.MdiParent = this;
            oEnfant.Text = "Client " + nbClient.ToString();
            nbClient++;
            oEnfant.Show();
        }

        #endregion

        #region Ouvrir Client

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;

                    // Vérification de l’extension
                    if (Path.GetExtension(filePath).ToLower() != ".rtf")
                    {
                        MessageBox.Show("Erreur : vous devez sélectionner un fichier RTF.",
                                        "Extension invalide",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }

                    LivreEnfantForm oEnfant = new LivreEnfantForm();
                    oEnfant.MdiParent = this;
                    oEnfant.Text = filePath;
                    oEnfant.Show();

                    // Charger dans un RichTextBox temporaire
                    RichTextBox ortf = new RichTextBox();
                    ortf.LoadFile(filePath);

                    // Remplir les zones de texte de l’enfant
                    if (ortf.Lines.Length >= 4)
                    {
                        oEnfant.nomEtudiantTextBox.Text = ortf.Lines[0];
                        oEnfant.classeTextBox.Text = ortf.Lines[1];
                        oEnfant.titreLivreTextBox.Text = ortf.Lines[2];
                        oEnfant.nbrJoursTextBox.Text = ortf.Lines[3];

                        // Supprimer les 4 lignes
                        int longueur = oEnfant.nomEtudiantTextBox.Text.Length +
                                       oEnfant.classeTextBox.Text.Length +
                                       oEnfant.titreLivreTextBox.Text.Length +
                                       oEnfant.nbrJoursTextBox.Text.Length + 4;

                        ortf.SelectionStart = 0;
                        ortf.SelectionLength = longueur;
                        ortf.SelectedText = string.Empty;
                    }

                    // Reste du texte → zone "Raison de l’emprunt"
                    oEnfant.raisonTextBox5.Rtf = ortf.Rtf;

                    // Propriétés logiques
                    oEnfant.Enregistrement = true;
                    oEnfant.Modification = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’ouverture : " + ex.Message,
                                "Erreur",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Enregistrer / Enregistrer sous

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                LivreEnfantForm oEnfant = (LivreEnfantForm)this.ActiveMdiChild;
                oEnfant.Enregistrer();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                LivreEnfantForm oEnfant = (LivreEnfantForm)this.ActiveMdiChild;
                oEnfant.EnregistrerSous();
            }
        }

        #endregion

        #region Fermer un enfant

        private void fermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }

        #endregion

        #region Quitter application

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
