using JeuHoy_WPF_Natif.Modele;
using System;

namespace JeuHoy_WPF_Natif.Vue
{
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
