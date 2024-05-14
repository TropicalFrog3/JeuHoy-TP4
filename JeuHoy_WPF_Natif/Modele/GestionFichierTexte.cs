using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace JeuHoy_WPF_Natif.Modele
{
    /// <summary>
    /// Auteur : nicolas lajoie, Arthur
    ///     description : Classe de gestion des fichiers texte
    ///     date : 2020-05-12
    /// </summary>
    public class GestionFichierTexte : IGestionFichierTexte
    {
        /// <summary>
        /// écriture du fichier
        /// </summary>
        /// <param name="sNomFichier"></param>
        /// <param name="bodyContenu"></param>
        /// <returns></returns>
        public string EcrireFichier(string sNomFichier, SkeletonData bodyContenu)
        {
            string sResultat;
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
                            if (point.X.ToString().Length < 16)
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

        /// <summary>
        /// lecture du fichier
        /// </summary>
        /// <param name="sNomFichier"></param>
        /// <returns></returns>
        public SkeletonData LireFichier(string sNomFichier)
        {
            string sResultat = "";
            StreamReader reader = null;
            sNomFichier = "../../../" + sNomFichier;
            if (File.Exists(sNomFichier))
            {
                try
                {
                    reader = new StreamReader(sNomFichier);
                    SkeletonData allBody = new SkeletonData();
                    int iBody = 0;
                    while (!reader.EndOfStream)
                    {
                        string sLine = reader.ReadLine();
                        if (sLine.Contains("Body"))
                        {
                            iBody = int.Parse(sLine.Split(' ')[1]);

                            allBody.AddBody(iBody, new Dictionary<JointType, List<Point>>());
                        }
                        else if (sLine == "")
                        {
                            continue;
                        }
                        else
                        {
                            string[] sJoint = sLine.Split('\t');
                            sJoint = sJoint.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                            JointType jointType = (JointType)Enum.Parse(typeof(JointType), sJoint[0]);

                            string[] sPointX = sJoint[2].Split('\t');
                            string[] sPointY = sJoint[4].Split('\t');

                            Point point = new Point(double.Parse(sPointX[0].Replace('.', ',')), double.Parse(sPointY[0].Replace('.', ',')));

                            allBody.AddJoint(iBody, jointType, new List<Point>());
                            allBody.AddPoint(iBody, jointType, point);
                        }
                    }
                    return allBody;
                }
                catch (Exception e)
                {
                    sResultat = e.Message;
                }
            }
            else
            {
                sResultat = "Fichier inexistant";
            }

            if (reader != null)
                reader.Close();

            return new SkeletonData();
        }
    }
}
