using JeuHoy_WPF_Natif.Modele;
using System;

namespace JeuHoy_WPF_Natif.Vue
{
    /// <summary>
    /// Auteur : nicolas lajoie, Arthur 
    /// description : Interface pour les classes d'entrainement
    /// date : 2020-05-12
    /// </summary>
    public interface IwEntrainement
    {
        
        string Console { get; set; }
        string NomFichier { get; }
        SkeletonData Data { get; }
        SkeletonData CurrentData { get; }
        void Close();
        event EventHandler LectureFichierEvt;
        event EventHandler EcritureFichierEvt;
        event EventHandler FermetureEvt;
        event EventHandler EntrainementEvt;
        event EventHandler TestEvt;
    }
}
