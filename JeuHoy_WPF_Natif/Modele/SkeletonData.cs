using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JeuHoy_WPF_Natif.Modele
{
    public class SkeletonData
    {
        private Dictionary<int, Dictionary<JointType, List<Point>>> Data;

        public SkeletonData()
        {
            Data = new Dictionary<int, Dictionary<JointType, List<Point>>>();
        }

        public void AddBody(int iBody, Dictionary<JointType, List<Point>> body)
        {
            Data.Add(iBody, body);
        }

        public void AddJoint(int iBody, JointType jointType, List<Point> joint)
        {
            Data[iBody].Add(jointType, joint);
        }

        public void AddPoint(int iBody, JointType jointType, Point point)
        {
            Data[iBody][jointType].Add(point);
        }
        public bool ContainsKey(int iBody)
        {
            return Data.ContainsKey(iBody);
        }

        public bool ContainsKey(int iBody, JointType jointType)
        {
            return Data[iBody].ContainsKey(jointType);
        }

        public Dictionary<int, Dictionary<JointType, List<Point>>> GetData()
        {
            return Data;
        }

        public void Clear()
        {
            Data.Clear();
        }

        public override string ToString()
        {
            string sResultat = "";
            foreach (KeyValuePair<int, Dictionary<JointType, List<Point>>> body in Data)
            {
                sResultat += "Body " + body.Key + "\n";
                foreach (KeyValuePair<JointType, List<Point>> joint in body.Value)
                {
                    sResultat += joint.Key + "\n";
                    foreach (Point point in joint.Value)
                    {
                        sResultat += "X: " + point.X + "\tY: " + point.Y + "\n";
                    }
                }
            }
            return sResultat;
        }
    }
}
