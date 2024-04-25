using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace JeuHoy_WPF_Natif.Modele
{
    public class GestionFichierTexte : IGestionFichierTexte
    {
        public string EcrireFichier(string sNomFichier, SkeletonData bodyContenu)
        {
            string sResultat = "";
            StreamWriter writer = null;
            sNomFichier = "../../../" + sNomFichier;
            try
            {
                writer = new StreamWriter(sNomFichier, true);

                foreach (KeyValuePair<int, Dictionary<JointType, List<Point>>> body in bodyContenu.GetData())
                {
                    writer.WriteLine("Body " + body.Key);
                    foreach (KeyValuePair<JointType, List<Point>> joint in body.Value)
                    {
                        writer.Write(joint.Key);
                        if (joint.Key.ToString().Length < 8)
                            writer.Write("\t");

                        foreach (Point point in joint.Value)
                        {
                            writer.Write("\tX:\t" + point.X);
                            if(point.X.ToString().Length < 16)
                                writer.Write("\t");
                            writer.WriteLine("\tY:\t" + point.Y);
                        }
                    }
                }

                writer.WriteLine();

                sResultat = "Fichier écrit";
            }
            catch (Exception e)
            {
                sResultat = e.Message;
            }

            if (writer != null)
                writer.Close();
            return sResultat;
        }

        public string LireFichier(string sNomFichier)
        {
            string sResultat = "";
            StreamReader reader = null;
            sNomFichier = "../../../" + sNomFichier;
            if (File.Exists(sNomFichier))
            {
                reader = new StreamReader(sNomFichier);
                if (!reader.EndOfStream)
                    sResultat = reader.ReadToEnd();
            }
            else
            {
                sResultat = "Fichier inexistant";
            }

            if (reader != null)
                reader.Close();
            return sResultat;
        }
    }
}
