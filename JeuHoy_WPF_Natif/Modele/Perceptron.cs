using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace JeuHoy_WPF_Natif.Modele
{
    public class Perceptron
    {
        //private const double VITESSE_APP = 0.1;
        //private double[] _poids;

        //public Perceptron()
        //{
        //    _poids = new double[20];
        //    for (int i = 0; i < 20; i++)
        //    {
        //        _poids[i] = 0;
        //    }
        //}

        ////public string

        private SkeletonData _data;
        private List<float> fInput = new List<float>();
        private List<float> fWeights = new List<float>();
        private float fThreshold = 0.5f;

        public Perceptron(SkeletonData data)
        {
            _data = data;

            for (int i =0; i <data.Count(); i++)
            {
                fWeights.Add(0.0f);
            }
        }

        private bool Step(double WeightedSum)
        {
            if (WeightedSum > fThreshold)
                return true;
            else
                return false;
        }

        public bool Process()
        {
            // exemple de traitement
            //fInput.Add(0.1f);
            //fInput.Add(0.5f);
            //fInput.Add(0.2f);

            //fWeights.Add(0.4f);
            //fWeights.Add(0.3f);
            //fWeights.Add(0.6f);

            //float fWeightedSum = 0.0f;
            //for (int i = 0; i < fInput.Count; i++)
            //{
            //    fWeightedSum += fInput[i] * fWeights[i];
            //    Console.WriteLine(fWeightedSum);
            //}
            //return Step(fWeightedSum);


            // exemple de traitement avec les données du squelette
            double fWeightedSum = 0.0d;

            //foreach (Dictionary<JointType, List<Point>> body in _data)
            //{
            //    for(int i = 0; i < body.Count(); i++)
            //    {
            //        // TODO: do the sum of each point with the corresponding weight
            //        fWeightedSum += body[i].Values.ElementAt(0).ElementAt(0).X;
            //        Console.WriteLine(fWeightedSum);
            //    }
            //}

            return Step(fWeightedSum);
        }
    }
}
