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

        #region Methode afficher images
        private void AfficherIcones()
        {
            //utilisation d'un bloc try catch pour gerer les erreurs
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

        #region nouveau livre

        int compt = 1;    //on initialise le compteur
        private void nouveauLivre()
        {
            //utilisation d'un bloc try catch pour gerer les erreurs
            try
            {
                LivreEnfantForm livreForm = new LivreEnfantForm();
                livreForm.Text = "Nouveau Livre" + compt++; //on change le titre du form child
                livreForm.MdiParent = this;     //set le form parent
                livreForm.Show();   //afficher le form child
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création d'un nouveau livre : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        //on appelle la methode nouveauLivre lors du click sur le menu
        private void NouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nouveauLivre();
        }

        #endregion

        #region Methode controlAdded

        private void QuatrePaneaux_ControlAdded(object sender, ControlEventArgs e)
        {

            if (e.Control is MenuStrip menu)
            {
                // Si c’est un menu, changer son style
                menu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                menu.TextDirection = ToolStripTextDirection.Horizontal;

                // Exemple : afficher une ComboBox spéciale
                toolStripComboBox3.Visible = true;
            }
            else if (e.Control is ToolStrip toolbar)
            {
                // Si c’est une barre d’outils, masquer la ComboBox
                toolStripComboBox3.Visible = false;
            }

            // Vérifier dans quel panneau le contrôle a été ajouté
            if (sender is ToolStripPanel panel)
            {
                if (panel.Dock == DockStyle.Top)
                    MessageBox.Show("Contrôle ajouté en haut");
                else if (panel.Dock == DockStyle.Bottom)
                    MessageBox.Show("Contrôle ajouté en bas");
                else if (panel.Dock == DockStyle.Left)
                    MessageBox.Show("Contrôle ajouté à gauche");
                else if (panel.Dock == DockStyle.Right)
                    MessageBox.Show("Contrôle ajouté à droite");
            }

        }

        #endregion

        #region reorganiser fenetre
        private void fenetreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem clickedItem)
            {
                // Décoche tous les sous-menus
                foreach (ToolStripMenuItem item in fenetreToolStripMenuItem.DropDownItems)
                    item.Checked = false;

                // Cocher uniquement celui qu'on a cliqué
                clickedItem.Checked = true;

                // Appliquer l’organisation selon le choix
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
        private void OuvrirToolStripMenuItem_Click_1(object sender, EventArgs e)

        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fichier = OuvrirFileOpenFileDialog.FileName;       

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
                        enfant.JoursTextBox.Text = (temp.Lines.Length > 3) ? temp.Lines[3] : "0";

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
        #region Méthodes Sauvegarde

        // Méthode Enregistrée ( vérifie enfant actif
      
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

        #endregion

        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enregistrerToolStripMenuItem_Click(sender, e);
        }

        private void EnregistrerSousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is LivreEnfantForm enfant)
                EnregistrerSous(enfant);

        }
    }
}