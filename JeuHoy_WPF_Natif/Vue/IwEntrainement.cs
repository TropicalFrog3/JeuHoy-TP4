using JeuHoy_WPF_Natif.Modele;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace JeuHoy_WPF_Natif.Vue
{
    public interface IwEntrainement
    {
        string Console { get; set; }
        string NomFichier { get; }
        SkeletonData Data { get; }
        void Close();
        event EventHandler LectureFichierEvt;
        event EventHandler EcritureFichierEvt;
        event EventHandler FermetureEvt;
        event EventHandler EntrainementEvt;

    }
}
