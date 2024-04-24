using JeuHoy_WPF_Natif.Modele;
using JeuHoy_WPF_Natif.Vue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuHoy_WPF_Natif.Presentation
{
    public class PresentateurwEntrainement
    {
        private IwEntrainement _vue;
        private GestionFichierTexte _gestionFichierTexte;

        public PresentateurwEntrainement(IwEntrainement vue)
        {
            _vue = vue;
            _vue.LectureFichierEvt += Vue_LectureFichierEvt;
            _vue.EcritureFichierEvt += Vue_EcritureFichierEvt;
            _vue.FermetureEvt += Vue_FermetureEvt;
            _gestionFichierTexte = new GestionFichierTexte();
        }

        private void Vue_FermetureEvt(object sender, EventArgs e)
        {
            _vue.Close();
        }

        private void Vue_EcritureFichierEvt(object sender, EventArgs e)
        {
            string sNomFichier = _vue.NomFichier;
            _vue.Console = _gestionFichierTexte.EcrireFichier(sNomFichier, _vue.ContenuFichier);
          
        }

        private void Vue_LectureFichierEvt(object sender, EventArgs e)
        {
            string sNomFichier = _vue.NomFichier;
            _vue.Console = _gestionFichierTexte.LireFichier(sNomFichier);
        }


    }
}
