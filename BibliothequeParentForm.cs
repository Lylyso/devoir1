using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBibliotheque
{
    public partial class BibliothequeParentForm : Form
    {
        public BibliothequeParentForm()
        {
            InitializeComponent();
        }

        #region Load
        private void BibliothequeParentForm_Load(object sender, EventArgs e)
        {
            AfficherIcones();
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

        
    }
}
