using JeuHoy_WPF_Natif.Modele;
using JeuHoy_WPF_Natif.Vue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace JeuHoy_WPF_Natif.Presentation
{
    /// <summary>
    /// auteur : nicolas lajoie, Arthur
    /// description : Classe de présentation pour l'interface d'entraînement
    /// date : 2020-05-12
    /// </summary>
    public class PresentateurwEntrainement
    {
        #region Champs
        private IwEntrainement _vue;
        private GestionFichierTexte _gestionFichierTexte;
        private Perceptron _perceptron;
        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="vue"></param>
        public PresentateurwEntrainement(IwEntrainement vue)
        {
            _vue = vue;
            _vue.LectureFichierEvt += Vue_LectureFichierEvt;
            _vue.EcritureFichierEvt += Vue_EcritureFichierEvt;
            _vue.FermetureEvt += Vue_FermetureEvt;
            _vue.EntrainementEvt += Vue_EntrainementEvt;
            _vue.TestEvt += Vue_TestEvt;
            _gestionFichierTexte = new GestionFichierTexte();
        }

        /// <summary>
        /// Fermeture de la vue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vue_FermetureEvt(object sender, EventArgs e)
        {
            _vue.Close();
        }

        /// <summary>
        /// Écriture du fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vue_EcritureFichierEvt(object sender, EventArgs e)
        {
            string sNomFichier = _vue.NomFichier;

            _vue.Console = _gestionFichierTexte.EcrireFichier(sNomFichier, _vue.Data);
        }

        /// <summary>
        /// Lecture du fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vue_LectureFichierEvt(object sender, EventArgs e)
        {
            string sNomFichier = _vue.NomFichier;

            _perceptron = new Perceptron(_gestionFichierTexte.LireFichier(sNomFichier));
        }

        /// <summary>
        /// Entraînement du modèle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vue_EntrainementEvt(object sender, EventArgs e)
        {
            try
            {
                if (_perceptron == null || _perceptron.data == null)
                {
                    _vue.Console += "Veuillez charger un fichier avant d'entraîner le modèle.\n";
                    return;
                }

                List<List<Point>> inputList = new List<List<Point>>();
                List<int> targetList = new List<int>();
                foreach (var body in _perceptron.data.DataProp)
                {
                    
                    int target = body.Key; 
                    foreach (var joint in body.Value)
                    {
                        inputList.Add(joint.Value);
                        targetList.Add(target);
                    }
                }

                int iterations = 10; 
                _perceptron.Train(inputList, targetList, iterations);

                _vue.Console += "Entraînement terminé.\n";
            }
            catch (Exception ex)
            {
                _vue.Console += $"Erreur lors de l'entraînement : {ex.Message}\n";
            }
        }

        /// <summary>
        /// Test du modèle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vue_TestEvt(object sender, EventArgs e)
        {
            try
            {
                if (_perceptron.data.Count() == 0)
                {
                    _vue.Console = "Veuillez entraîner le modèle avant de le tester.\n";
                    return;
                }

                SkeletonData skeletonData = _vue.CurrentData;

                if (skeletonData == null || skeletonData.Count() == 0)
                {
                    _vue.Console = "Aucun squelette à afficher.\n";
                    return;
                }

                foreach (var body in skeletonData.DataProp)
                {
                    List<Point> inputList = body.Value.Values.SelectMany(j => j).ToList();

                    List<double> target = _perceptron.Process(inputList);

                    double predictedPosture = target[0];

                    _vue.Console = $"La position prédite pour le corps {body.Key} est {(predictedPosture == 1 ? "Correcte" : "Incorrecte")}.\n";
                    _vue.Console += $"La Somme est: {target[1]}\n";
                }
            }
            catch (Exception ex)
            {
                _vue.Console = $"Erreur lors du test : {ex.Message}\n";
            }
        }
    }
}
