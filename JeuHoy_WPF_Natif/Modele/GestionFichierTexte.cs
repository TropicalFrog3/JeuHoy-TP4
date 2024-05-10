using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
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

        public List<Dictionary<int, Dictionary<JointType, List<Point>>>> LireFichier(string sNomFichier)
        {
            // in the file there is : 
            // Body 1
            // SpineBase X:	180,313148498535    Y: 109,63958164431
            // SpineMid X:	180,496101975441    Y: 81,0644703991008
            // Neck X:	180,72635948658     Y: 53,7541817719082
            // Head X:	181,775564551353    Y: 39,5592801076061
            // ShoulderLeft X:	155,66533446312     Y: 65,2679486544627
            // ElbowLeft X:	131,112887263298    Y: 46,1420642205004
            // WristLeft X:	144,052867889404    Y: 26,9682916605248
            // HandLeft X:	151,217147111893    Y: 19,9737663988797
            // ShoulderRight X:	205,198332071304    Y: 64,4933434252469
            // ElbowRight X:	231,080286502838    Y: 49,3538809722325
            // WristRight X:	222,265360355377    Y: 27,9393466013782
            // HandRight X:	217,885439395905    Y: 21,8381341898216
            // HipLeft X:	171,109031438828    Y: 109,55712372402
            // KneeLeft X:	165,697300434113    Y: 166,561285054909
            // AnkleLeft X:	164,53766644001     Y: 198,357981555867
            // FootLeft X:	163,785252571106    Y: 207,583076908903
            // HipRight X:	189,473313689232    Y: 109,717133719966
            // KneeRight X:	190,255806446075    Y: 160,960296055056
            // AnkleRight X:	185,578629970551    Y: 197,329757978331
            // FootRight X:	186,007027626038    Y: 206,373060694281
            // SpineShoulder X:	180,663347840309    Y: 60,4665130039431
            // HandTipLeft X:	160,087859630585    Y: 15,6349182128906
            // ThumbLeft X:	155,345112085342    Y: 21,9545011700324
            // HandTipRight X:	207,396469116211    Y: 15,759291738834
            // ThumbRight X:	209,526430368423    Y: 23,5909717487839

            string sResultat = "";
            StreamReader reader = null;
            sNomFichier = "../../../" + sNomFichier;
            if (File.Exists(sNomFichier))
            {
                try
                {
                    reader = new StreamReader(sNomFichier);
                    List<Dictionary<int, Dictionary<JointType, List<Point>>>> listeBody = new List<Dictionary<int, Dictionary<JointType, List<Point>>>>();
                    SkeletonData body = new SkeletonData();
                    int iBody = 0;
                    while (!reader.EndOfStream)
                    {
                        string sLine = reader.ReadLine();
                        if (sLine.Contains("Body"))
                        {
                            iBody = int.Parse(sLine.Split(' ')[1]);

                            body.AddBody(iBody, new Dictionary<JointType, List<Point>>());
                        }
                        else if (sLine == "")
                        {
                            continue;
                        }
                        else
                        {
                            string[] sJoint = sLine.Split('\t');
                            // remove the empty string
                            sJoint = sJoint.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                            JointType jointType = (JointType)Enum.Parse(typeof(JointType), sJoint[0]);

                            string[] sPointX = sJoint[2].Split('\t');
                            string[] sPointY = sJoint[4].Split('\t');


                            Point point = new Point(double.Parse(sPointX[0].Replace('.', ',')), double.Parse(sPointY[0].Replace('.', ',')));

                            body.AddJoint(iBody, jointType, new List<Point>());
                            body.AddPoint(iBody, jointType, point);
                        }
                        if (sLine.Contains("Body " + iBody))
                        {
                            listeBody.Add(body.GetData());
                        }
                    }
                    return listeBody;                   // /!\ BREAK POINT MARCHE PAS LA LISTE /!\
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

            return new List<Dictionary<int, Dictionary<JointType, List<Point>>>>();
        }
    }
}
