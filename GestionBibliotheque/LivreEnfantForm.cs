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
        public TextBox NomTextBox { get { return nomTextBox1; } }
        public TextBox ClasseTextBox { get { return classeTextBox2; } }
        public TextBox TitreTextBox { get { return titreLivreTextBox3; } }
        public TextBox JoursTextBox { get { return nbrJoursTextBox4; } }
        public RichTextBox RaisonRichTextBox { get { return richTextBox; } }
        
        #endregion

        public LivreEnfantForm()
        {
            InitializeComponent();
            Modification = false;
            Enregistrement = false;
            // Détecter modifications
            nomTextBox1.TextChanged += Zones_TextChanged;
            classeTextBox2.TextChanged += Zones_TextChanged;
            titreLivreTextBox3.TextChanged += Zones_TextChanged;
            nbrJoursTextBox4.TextChanged += Zones_TextChanged;
            richTextBox.TextChanged += Zones_TextChanged;
        }

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
                        temp.SelectedText = nomTextBox1.Text + Environment.NewLine +
                                            classeTextBox2.Text + Environment.NewLine +
                                            titreLivreTextBox3.Text + Environment.NewLine +
                                            nbrJoursTextBox4.Text + Environment.NewLine;

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
                    temp.SelectedText = nomTextBox1.Text + Environment.NewLine +
                                        classeTextBox2.Text + Environment.NewLine +
                                        titreLivreTextBox3.Text + Environment.NewLine +
                                        nbrJoursTextBox4.Text + Environment.NewLine;

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

        private void LivreEnfantForm_Load(object sender, EventArgs e)
        {

        }
    }
}
