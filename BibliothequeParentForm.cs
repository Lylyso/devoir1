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
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
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

        #region Variables
        private int nbClient = 1;
        private OpenFileDialog ofd;
        #endregion

        #region Load
        private void BibliothequeParentForm_Load(object sender, EventArgs e)
        {
            AfficherIcones();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*";
            openFileDialog.AddExtension = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = "rtf";
        }
        #endregion

        #region Afficher icônes
        private void AfficherIcones()
        {
            try
            {
                NouveauToolStripMenuItem.Image = Properties.Resources.page;
                OuvrirToolStripMenuItem.Image = Properties.Resources.dossier;
                EnregistrerSousToolStripMenuItem.Image = Properties.Resources.enregistrer;
                couperToolStripMenuItem.Image = Properties.Resources.cut;
                copierToolStripMenuItem.Image = Properties.Resources.copie;
                collerToolStripMenuItem.Image = Properties.Resources.coller;
                rechercherToolStripMenuItem.Image = Properties.Resources.rechercher;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement icônes : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Nouveau livre
        int compt = 1;
        private void nouveauLivre()
        {
            try
            {
                LivreEnfantForm livreForm = new LivreEnfantForm();
                livreForm.Text = "Nouveau Livre " + compt++;
                livreForm.MdiParent = this;
                livreForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création d'un nouveau livre : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nouveauLivre();
        }
        #endregion

        #region Réorganiser fenêtre
        private void fenetreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem clickedItem)
            {
                foreach (ToolStripMenuItem item in fenetreToolStripMenuItem.DropDownItems)
                    item.Checked = false;

                clickedItem.Checked = true;

                if (clickedItem == cascadeToolStripMenuItem)
                    this.LayoutMdi(MdiLayout.Cascade);
                else if (clickedItem == mosaiqueHToolStripMenuItem)
                    this.LayoutMdi(MdiLayout.TileHorizontal);
                else if (clickedItem == mosaiqueVToolStripMenuItem)
                    this.LayoutMdi(MdiLayout.TileVertical);
                else if (clickedItem == iconesToolStripMenuItem)
                    this.LayoutMdi(MdiLayout.ArrangeIcons);
            }
        }
        #endregion

        #region Méthode Ouvrir
        private void OuvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fichier = openFileDialog.FileName;

                    if (Path.GetExtension(fichier).ToLower() != ".rtf")
                    {
                        MessageBox.Show("Erreur : le fichier doit être au format RTF.",
                                        "Extension invalide",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    LivreEnfantForm enfant = new LivreEnfantForm();
                    enfant.MdiParent = this;
                    enfant.Text = fichier;

                    RichTextBox temp = new RichTextBox();
                    temp.LoadFile(fichier);

                    if (temp.Lines.Length >= 3)
                    {
                        enfant.NomTextBox.Text = temp.Lines[0];
                        enfant.ClasseTextBox.Text = temp.Lines[1];
                        enfant.TitreTextBox.Text = temp.Lines[2];
                        enfant.JoursTextBox.Text = (temp.Lines.Length > 3) ? temp.Lines[3] : "0";
                    }

                    enfant.RaisonRichTextBox.Rtf = temp.Rtf;
                    enfant.Modification = false;
                    enfant.Enregistrement = true;

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

        #region Fermer / Quitter
        private void FermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)
                    this.ActiveMdiChild.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la fermeture : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

      
        #region Edition_Click()
        private void Edition_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild is LivreEnfantForm oEnfant)
                {
                    // On récupère le RichTextBox de l’enfant
                    RichTextBox oRichTextBox = oEnfant.RaisonRichTextBox;

                    if (sender == couperToolStripMenuItem)
                    {
                        oRichTextBox.Cut();
                    }
                    else if (sender == copierToolStripMenuItem)
                    {
                        oRichTextBox.Copy();
                    }
                    else if (sender == collerToolStripMenuItem)
                    {
                        oRichTextBox.Paste();
                    }
                    else if (sender == effacerToolStripMenuItem)
                    {
                        oRichTextBox.Clear();
                    }
                    else if (sender == toutSelectionnerToolStripMenuItem)
                    {
                        oRichTextBox.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur Edition_Click : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region StylePolice_Click()
        private void StylePolice_Click(object sender, EventArgs e)
        {
            try
            {
                LivreEnfantForm oEnfant = (LivreEnfantForm)this.ActiveMdiChild;

                if (oEnfant != null)
                {
                    if (sender == boldToolStripButton)
                    {
                        oEnfant.ChangerAttributsPolice(FontStyle.Bold);
                    }
                    else if (sender == italicToolStripButton)
                    {
                        oEnfant.ChangerAttributsPolice(FontStyle.Italic);
                    }
                    else if (sender == underlineToolStripButton)
                    {
                        oEnfant.ChangerAttributsPolice(FontStyle.Underline);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur StylePolice_Click : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    #endregion


        #endregion

        #endregion

        #region enregistrer
        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enregistrerToolStripMenuItem_Click(sender, e);
        }

        private void EnregistrerSousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is LivreEnfantForm enfant)
                EnregistrerSous(enfant);

        }

        #endregion


        #region Allignement texte
        private void allignementGaucheToolStripButton_Click(object sender, EventArgs e)
        {
            //utilisation d'un bloc try catch pour gerer les erreurs

        }
        #endregion

    }
}

