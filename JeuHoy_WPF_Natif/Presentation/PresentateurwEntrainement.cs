using JeuHoy_WPF_Natif.Modele;
using JeuHoy_WPF_Natif.Vue;
using System;

namespace JeuHoy_WPF_Natif.Presentation
{
    public class PresentateurwEntrainement
    {
        #region Champs
        private IwEntrainement _vue;
        private GestionFichierTexte _gestionFichierTexte;
        private Perceptron _perceptron;
        #endregion

        public PresentateurwEntrainement(IwEntrainement vue)
        {
            _vue = vue;
            _vue.LectureFichierEvt += Vue_LectureFichierEvt;
            _vue.EcritureFichierEvt += Vue_EcritureFichierEvt;
            _vue.FermetureEvt += Vue_FermetureEvt;
            _vue.EntrainementEvt += Vue_EntrainementEvt;
            _gestionFichierTexte = new GestionFichierTexte();
        }

        private void Vue_FermetureEvt(object sender, EventArgs e)
        {
            _vue.Close();
        }

        private void Vue_EcritureFichierEvt(object sender, EventArgs e)
        {
            string sNomFichier = _vue.NomFichier;
            
            _vue.Console = _gestionFichierTexte.EcrireFichier(sNomFichier, _vue.Data);
        }

        private void Vue_LectureFichierEvt(object sender, EventArgs e)
        {
            string sNomFichier = _vue.NomFichier;

            _perceptron = new Perceptron(_gestionFichierTexte.LireFichier(sNomFichier));
        }

        private void Vue_EntrainementEvt(object sender, EventArgs e)
        {
            bool bResultat = _perceptron.Process();

            if (bResultat)
            {
                _vue.Console += bResultat.ToString();
            }
            else
            {
                _vue.Console += bResultat.ToString();
            }
            _vue.Console += "\n";
        }
    }
}
