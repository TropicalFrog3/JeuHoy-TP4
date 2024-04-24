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
    public interface IGestionFichierTexte
    {
        string LireFichier(string sNomFichier);
        string EcrireFichier(string sNomFichier, Dictionary<int, Dictionary<JointType, List<Point>>> bodyContenu);
    }
}
