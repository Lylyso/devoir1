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

        #region Form_Closing
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

        #region Méthode Enregistrer
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

                        // Insérer infos de l’étudiant + livre
                        temp.SelectionStart = 0;
                        temp.SelectionLength = 0;
                        temp.SelectedText = nomTextBox.Text + Environment.NewLine +
                                            classeTextBox.Text + Environment.NewLine +
                                            titreLivreTextBox.Text + Environment.NewLine +
                                            nbrJoursTextBox.Text + Environment.NewLine;

                        temp.SaveFile(this.Text, RichTextBoxStreamType.RichText);

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
        #endregion
        //logic for saving the file with a SaveFileDialog

        #region Méthode EnregistrerSous
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

                    this.Text = fichier;
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

        #region Méthode TextChanged
        private void Zones_TextChanged(object sender, EventArgs e)
        {
            Modification = true;
        }

        #endregion

        public void ChangerAttributsPolice(FontStyle style)
        {
            try
            {
                if (richTextBox.SelectionFont != null)
                {
                    // Création d'une nouvelle police avec le style choisi
                    Font currentFont = richTextBox.SelectionFont;
                    Font newFont = new Font(currentFont, style);
                    richTextBox.SelectionFont = newFont;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur ChangerAttributsPolice : " + ex.Message);
            }
        }

        private void RichTextBox_SelectionChanged(object sender, EventArgs e)
{
    try
    {
        BibliothequeParentForm parent = this.MdiParent as BibliothequeParentForm;
        if (parent == null) return;

        RichTextBox rtb = sender as RichTextBox;
        if (rtb == null) return;

        Font currentFont = rtb.SelectionFont;
        if (currentFont == null) return;

        // Mettre à jour les boutons du parent
        parent.boldToolStripButton.Checked = currentFont.Bold;
        parent.italiqueToolStripButton.Checked = currentFont.Italic;
        parent.souligneToolStripButton.Checked = currentFont.Underline;

        parent.couperToolStripMenuItem.Enabled = rtb.SelectionLength > 0;
        parent.copierToolStripMenuItem.Enabled = rtb.SelectionLength > 0;
        parent.collerToolStripMenuItem.Enabled = Clipboard.ContainsText();

        // Alignement
        switch (rtb.SelectionAlignment)
        {
            case HorizontalAlignment.Left:
                parent.gaucheToolStripButton.Checked = true;
                parent.centreToolStripButton.Checked = false;
                parent.droiteToolStripButton.Checked = false;
                break;
            case HorizontalAlignment.Center:
                parent.gaucheToolStripButton.Checked = false;
                parent.centreToolStripButton.Checked = true;
                parent.droiteToolStripButton.Checked = false;
                break;
            case HorizontalAlignment.Right:
                parent.gaucheToolStripButton.Checked = false;
                parent.centreToolStripButton.Checked = false;
                parent.droiteToolStripButton.Checked = true;
                break;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Erreur RichTextBox_SelectionChanged : " + ex.Message);
    }
}

        private void LivreEnfantForm_Activated(object sender, EventArgs e)
        {
            if (this.ActiveControl is RichTextBox rtb)
            {
                RichTextBox_SelectionChanged(rtb, EventArgs.Empty);
            }
        }
        private void ChangerAttributsPolice(FontStyle style, bool activer)
        {
            if (this.ActiveMdiChild is LivreEnfantForm enfant)
            {
                RichTextBox rtb = enfant.RaisonRichTextBox;

                if (rtb.SelectionFont != null)
                {
                    FontStyle newStyle = rtb.SelectionFont.Style;

                    if (activer)
                        newStyle |= style;   // Ajoute le style
                    else
                        newStyle &= ~style;  // Enlève le style

                    rtb.SelectionFont = new Font(rtb.SelectionFont, newStyle);
                }
            }
        }

        private void LivreEnfantForm_Load(object sender, EventArgs e)
        {
            try 
            {
                BibliothequeParentForm parent = new BibliothequeParentForm();
                if (parent != null)
                {
                    this.MdiParent = parent;
                    parent.Parent = this.MdiParent as BibliothequeParentForm;
                    parent.boldToolStripButton.Click += (s, ev) => ChangerAttributsPolice(FontStyle.Bold, parent.boldToolStripButton.Checked);
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur LivreEnfantForm_Load : " + ex.Message);
            }
    }
}
