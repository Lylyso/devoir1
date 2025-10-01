using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GestionBibliotheque
{
    
    // Formulaire enfant pour la gestion des livres d'enfants.
  
    public partial class LivreEnfantForm : Form
    {
        #region Propriétés
        public bool Enregistrement { get; set; } = false;
        public bool Modification { get; set; } = false;

        // Accès publics aux contrôles
        public TextBox NomTextBox { get { return nomTextBox; } }
        public TextBox ClasseTextBox { get { return classeTextBox; } }
        public TextBox TitreTextBox { get { return titreLivreTextBox; } }
        public TextBox JoursTextBox { get { return nbrJoursTextBox; } }
        public RichTextBox RaisonRichTextBox { get { return richTextBox; } }

        private string cheminFichier;
        #endregion

        #region Constructeur
        public LivreEnfantForm()
        {
            InitializeComponent();
            Modification = false;
            Enregistrement = false;

            // Détecter modifications
            nomTextBox.TextChanged += Zones_TextChanged;
            classeTextBox.TextChanged += Zones_TextChanged;
            titreLivreTextBox.TextChanged += Zones_TextChanged;
            nbrJoursTextBox.TextChanged += Zones_TextChanged;
            richTextBox.TextChanged += Zones_TextChanged;

            this.FormClosing += LivreEnfantForm_FormClosing;
            this.Activated += LivreEnfantForm_Activated;
            richTextBox.SelectionChanged += RichTextBox_SelectionChanged;
        }
        #endregion

        #region Méthode SelectionChanged
        
        // Gère la sélection dans le RichTextBox et met à jour l'état des boutons du parent.
       
        private void RichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // MdiParent pour capter le formulaire principal
                var parent = this.MdiParent as BibliothequeParentForm;
                if (parent == null) return;

                // Boutons Gras, Italique, Souligné
                var font = richTextBox.SelectionFont ?? richTextBox.Font;
                parent.boldToolStripButton.Checked = font.Bold;
                parent.italicToolStripButton.Checked = font.Italic;
                parent.underlineToolStripButton.Checked = font.Underline;

                // Vérifier le contenu du presse-papiers (texte)
                bool hasClipboardText = Clipboard.ContainsText();
                parent.collerToolStripMenuItem.Enabled = hasClipboardText;
                parent.collerToolStripButton.Enabled = hasClipboardText;

                // Bouton Copier/Couper activé si sélection > 0
                bool hasSelection = richTextBox.SelectionLength > 0;
                parent.copierToolStripMenuItem.Enabled = hasSelection;
                parent.copierToolStripButton.Enabled = hasSelection;
                parent.couperToolStripMenuItem.Enabled = hasSelection;
                parent.couperToolStripButton.Enabled = hasSelection;

                // Vérifier l'alignement
                parent.alignementGaucheToolStripButton.Checked = (richTextBox.SelectionAlignment == HorizontalAlignment.Left);
                parent.alignementMilieuToolStripButtons.Checked = (richTextBox.SelectionAlignment == HorizontalAlignment.Center);
                parent.alignementDroiteToolStripButton.Checked = (richTextBox.SelectionAlignment == HorizontalAlignment.Right);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur RichTextBox_SelectionChanged : " + ex.Message);
            }
        }
        #endregion

        #region Méthode ClientActivated
      
        // Appelée lors de l'activation du formulaire enfant.
      
        private void LivreEnfantForm_Activated(object sender, EventArgs e)
        {
            // Appel direct à SelectionChanged (null comme paramètres)
            RichTextBox_SelectionChanged(richTextBox, EventArgs.Empty);
        }
        #endregion

        #region Méthode ChangerAttributsPolice

        public void ChangerAttributsPolice(FontStyle style)
        {
            try
            {
                // Vérifier si le style est disponible
                if (richTextBox.SelectionFont != null)
                {

                    Font currentFont = richTextBox.SelectionFont;
                    FontStyle newFontStyle;

                    if(currentFont.Style.HasFlag(style))
                    {
                        // Retirer le style
                        newFontStyle = currentFont.Style & ~style;
                    }
                    else
                    {
                        // Ajouter le style
                        newFontStyle = currentFont.Style | style;
                    }
                    richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, style);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur ChangerAttributsPolice : " + ex.Message);
            }
        }
        #endregion

        #region Méthode Zones_TextChanged
        private void Zones_TextChanged(object sender, EventArgs e)
        {
            Modification = true;
        }
        #endregion

        #region Méthode FormClosing
        private void LivreEnfantForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Modification)
                {
                    DialogResult result = MessageBox.Show(
                        "Voulez-vous enregistrer les modifications ?",
                        "Enregistrement",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Enregistrer();
                        this.Dispose();
                    }
                    else if (result == DialogResult.No)
                    {
                        this.Dispose();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la fermeture : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Méthodes Enregistrer/EnregistrerSous
        public void Enregistrer()
        {
            try
            {
                if (Modification)
                {
                    if (!Enregistrement)
                    {
                        EnregistrerSous();
                    }
                    else
                    {
                        RichTextBox temp = new RichTextBox();
                        temp.Rtf = richTextBox.Rtf;

                        temp.SelectionStart = 0;
                        temp.SelectionLength = 0;
                        temp.SelectedText = nomTextBox.Text + Environment.NewLine +
                                            classeTextBox.Text + Environment.NewLine +
                                            titreLivreTextBox.Text + Environment.NewLine +
                                            nbrJoursTextBox.Text + Environment.NewLine;

                        temp.SaveFile(cheminFichier, RichTextBoxStreamType.RichText);

                        Modification = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’enregistrement : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EnregistrerSous()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Documents RTF (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*";
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = "rtf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fichier = saveFileDialog.FileName;

                    if (Path.GetExtension(fichier).ToLower() != ".rtf")
                    {
                        MessageBox.Show("Erreur : le fichier doit être au format RTF.",
                                        "Extension invalide",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    RichTextBox temp = new RichTextBox();
                    temp.Rtf = richTextBox.Rtf;

                    temp.SelectionStart = 0;
                    temp.SelectionLength = 0;
                    temp.SelectedText = nomTextBox.Text + Environment.NewLine +
                                        classeTextBox.Text + Environment.NewLine +
                                        titreLivreTextBox.Text + Environment.NewLine +
                                        nbrJoursTextBox.Text + Environment.NewLine;

                    temp.SaveFile(fichier, RichTextBoxStreamType.RichText);

                    cheminFichier = fichier;
                    this.Text = Path.GetFileName(fichier);
                    Enregistrement = true;
                    Modification = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’enregistrement sous : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
