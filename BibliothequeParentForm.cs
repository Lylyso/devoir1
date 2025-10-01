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
            DesactiverOperationsMenusBarreOutils();
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

        #region style MenuStrip

        private void professionnelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //caster le sender en ToolStripMenuItem
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem == null) return;

            // Trouver le menu parent car on travail avec un sous-menu
            ToolStripMenuItem parentMenu = (ToolStripMenuItem)clickedItem.OwnerItem;

            // Trouver l’index de l’item cliqué dans le sous-menu
            int index = parentMenu.DropDownItems.IndexOf(clickedItem);

            // Enlever crochets 
            foreach (ToolStripMenuItem item in parentMenu.DropDownItems)
                item.Checked = false;

            clickedItem.Checked = true;

            // Changer le RenderMode selon l’index
            if (index == 0)
            {
                menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            }
            else if (index == 1)
            {
                menuStrip1.RenderMode = ToolStripRenderMode.System;
            }
            else if (index == 2)
            {
                menuStrip1.RenderMode = ToolStripRenderMode.ManagerRenderMode; // CORRECT
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

        #region Méthodes Sauvegarde

        // Méthode Enregistrée ( vérifie enfant actif
        private void EnregistréeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild is LivreEnfantForm enfant)
                {
                    if (enfant.Enregistrement) // déjà enregistré
                        Enregistrer(enfant, enfant.Text);
                    else
                        EnregistrerSous(enfant);
                }
                else
                {
                    MessageBox.Show("Aucun formulaire actif pour enregistrer.",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’enregistrement : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Méthode Enregistrer
        private void Enregistrer(LivreEnfantForm enfant, string fichier)
        {
            try
            {
                if (enfant.Modification)
                {
                    RichTextBox temp = new RichTextBox();

                    // copier les zones de texte dans temp
                    temp.Clear();
                    temp.AppendText(enfant.NomTextBox.Text + "\n");
                    temp.AppendText(enfant.ClasseTextBox.Text + "\n");
                    temp.AppendText(enfant.TitreTextBox.Text + "\n");
                    temp.AppendText(enfant.JoursTextBox.Text + "\n");

                    // ajouter le RTF de la raison
                    temp.Rtf += enfant.RaisonRichTextBox.Rtf;

                    temp.SaveFile(fichier, RichTextBoxStreamType.RichText);

                    enfant.Modification = false;
                    enfant.Enregistrement = true;

                    MessageBox.Show("Fichier enregistré avec succès.", "Succès",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur dans Enregistrer : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Méthode EnregistrerSous
        private void EnregistrerSous(LivreEnfantForm enfant)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Documents RTF (*.rtf)|*.rtf";
                saveFileDialog.DefaultExt = "rtf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fichier = saveFileDialog.FileName;

                    // Vérifier l’extension
                    if (Path.GetExtension(fichier).ToLower() != ".rtf")
                    {
                        MessageBox.Show("Le fichier doit avoir l’extension .rtf",
                                        "Erreur Extension", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Enregistrer(enfant, fichier);
                    enfant.Text = fichier;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur dans EnregistrerSous : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Edition_Click()
        private void EditionText_Click(object sender, EventArgs e)
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

        #region Alignement_Click()

        private void Alignement_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild is LivreEnfantForm oEnfant)
                {
                    RichTextBox oRichTextBox = oEnfant.RaisonRichTextBox;
                    if (sender == alignementGaucheToolStripButton)
                    {
                        oRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    }
                    else if (sender == alignementMilieuToolStripButtons)
                    {
                        oRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    }
                    else if (sender == alignementDroiteToolStripButton)
                    {
                        oRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur Allinement_Click : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Méthode DesactiverOperationsMenusBarreOutils
        private void DesactiverOperationsMenusBarreOutils()
        {
            try
            {
                // Parcourir tous les menus principaux
                foreach (ToolStripItem mainItem in menuStrip1.Items)
                {

                    if (mainItem is ToolStripMenuItem mainMenu)
                    {
                        // Si c'est le menu Fichier, on traite ses sous-menus
                        if (mainMenu.Name == "fichierToolStripMenuItem")
                        {
                            mainMenu.Enabled = true; // Le menu Fichier reste actif
                            foreach (ToolStripItem subItem in mainMenu.DropDownItems)
                            {
                                // On active seulement Nouveau et Ouvrir, on désactive les autres
                                if (subItem.Name == "NouveauToolStripMenuItem" || subItem.Name == "OuvrirToolStripMenuItem" 
                                    || subItem.Name == "quitterToolStripMenuItem")
                                    subItem.Enabled = true;
                                else
                                    subItem.Enabled = false;
                            }
                        }
                        else
                        {
                            // Les autres menus principaux sont désactivés
                            mainMenu.Enabled = false;
                            foreach (ToolStripItem subItem in mainMenu.DropDownItems)
                            {
                                subItem.Enabled = false;
                            }
                        }
                    }
                    
                }

                // Activer tous les boutons de la barre d’outils sauf Couper et Coller
                foreach (ToolStripItem bouton in sousMenuToolStrip.Items)
                {
                    if (bouton.Name == "newFichierToolStripButton"
                || bouton.Name == "ouvrirToolStripButton")
                        bouton.Enabled = true;
                    else
                        bouton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur dans DesactiverOperationsMenusBarreOutils : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Méthode Parent_MdiChildActivate
        private void BibliothequeParentForm_MdiChildActivate(object sender, EventArgs e)
        {
            // Vérifier si le formulaire enfant est nul
            if (this.ActiveMdiChild == null)
            {
                // Appel à la méthode de désactivation des menus/barres d’outils
                DesactiverOperationsMenusBarreOutils();
                
            }
        }
        #endregion


        #region Méthode ActiverOperationsMenusBarreOutils
        private void ActiverOperationsMenusBarreOutils()
        {
            // Activer tous les menus et sous-menus
            foreach (ToolStripMenuItem oMainToolStripItem in menuStrip1.Items)
            {
                oMainToolStripItem.Enabled = true;
                // Boucle pour passer à travers les sous-menus si nécessaire
                foreach (object oCourantToolStripItem in oMainToolStripItem.DropDownItems)
                {
                    if (oCourantToolStripItem is ToolStripMenuItem sousMenu)
                    {
                        sousMenu.Enabled = true;
                    }
                }
            }

            // Activer tous les boutons de la barre d’outils
            foreach (ToolStripItem boutonToolStripItem in sousMenuToolStrip.Items)
            {
                boutonToolStripItem.Enabled = true;
            }

            // Désactiver les boutons Copier/Couper si pas de sélection, Coller si pas de texte dans le presse-papiers
            copierToolStripMenuItem.Enabled = false;
            couperToolStripMenuItem.Enabled = false;
            copierToolStripButton.Enabled = false;
            couperToolStripButton.Enabled = false;
            collerToolStripMenuItem.Enabled = Clipboard.ContainsText();
            collerToolStripButton.Enabled = Clipboard.ContainsText();
        }

        #endregion
    }
}
        

