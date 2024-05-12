using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Dictionary<int, Dictionary<JointType, List<Point>>> DataProp
        {
            get { return Data; }
        }
        public void AddBody(int iBody, Dictionary<JointType, List<Point>> body)
        {
            if (!Data.ContainsKey(iBody))
            {
                Data.Add(iBody, body);
            }
        }
        public void AddJoint(int iBody, JointType jointType, List<Point> joint)
        {
            if (!Data[iBody].ContainsKey(jointType))
                Data[iBody].Add(jointType, joint);
            else
                Console.WriteLine();
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

        public int Count()
        {
            return Data.Count();
        }
    }
}
