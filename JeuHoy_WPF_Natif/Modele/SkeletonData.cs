using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace JeuHoy_WPF_Natif.Modele
{
    /// <summary>
    /// Auteur : nicolas lajoie, Arthur
    /// description : Classe de données pour les squelettes
    /// date : 2020-05-12
    /// </summary>
    public class SkeletonData
    {
        /// <summary>
        /// Dictionnaire de données
        /// </summary>
        private Dictionary<int, Dictionary<JointType, List<Point>>> Data;

        /// <summary>
        /// Constructeur
        /// </summary>
        public SkeletonData()
        {
            Data = new Dictionary<int, Dictionary<JointType, List<Point>>>();
        }
        /// <summary>
        /// Propriété de données
        /// </summary>
        public Dictionary<int, Dictionary<JointType, List<Point>>> DataProp
        {
            get { return Data; }
        }
        /// <summary>
        /// Ajout d'un squelette
        /// </summary>
        /// <param name="iBody"></param>
        /// <param name="body"></param>
        public void AddBody(int iBody, Dictionary<JointType, List<Point>> body)
        {
            if (!Data.ContainsKey(iBody))
            {
                Data.Add(iBody, body);
            }
        }
        /// <summary>
        /// Ajout d'une jointure
        /// </summary>
        /// <param name="iBody"></param>
        /// <param name="jointType"></param>
        /// <param name="joint"></param>
        public void AddJoint(int iBody, JointType jointType, List<Point> joint)
        {
            if (!Data[iBody].ContainsKey(jointType))
                Data[iBody].Add(jointType, joint);
            else
                Console.WriteLine();
        }
        /// <summary>
        /// Ajout d'un point
        /// </summary>
        /// <param name="iBody"></param>
        /// <param name="jointType"></param>
        /// <param name="point"></param>
        public void AddPoint(int iBody, JointType jointType, Point point)
        {
            Data[iBody][jointType].Add(point);
        }
        /// <summary>
        /// Vérifie si le squelette existe
        /// </summary>
        /// <param name="iBody"></param>
        /// <returns></returns>
        public bool ContainsKey(int iBody)
        {
            return Data.ContainsKey(iBody);
        }
        /// <summary>
        /// Vérifie si la jointure existe
        /// </summary>
        /// <param name="iBody"></param>
        /// <param name="jointType"></param>
        /// <returns></returns>
        public bool ContainsKey(int iBody, JointType jointType)
        {
            return Data[iBody].ContainsKey(jointType);
        }
        /// <summary>
        /// Vérifie si le point existe
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Dictionary<JointType, List<Point>>> GetData()
        {
            return Data;
        }
        /// <summary>
        /// Vérifie si le point existe
        /// </summary>
        public void Clear()
        {
            Data.Clear();
        }
        /// <summary>
        /// Compte le nombre de squelettes
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return Data.Count();
        }
    }
}
