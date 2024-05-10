using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace JeuHoy_WPF_Natif.Modele
{
    internal interface IGestionFichierTexte
    {
        List<Dictionary<int, Dictionary<JointType, List<Point>>>> LireFichier(string sNomFichier);
        string EcrireFichier(string sNomFichier, SkeletonData bodyContenu);
    }
}
