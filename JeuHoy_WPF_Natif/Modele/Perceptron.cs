using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace JeuHoy_WPF_Natif.Modele
{
    public class Perceptron
    {
        private const double VITESSE_APPRENTISSAGE = 0.1f;

        private SkeletonData _data;
        private List<double> _poids;
        private float fThreshold = 0.5f;

        public Perceptron(SkeletonData data)
        {
            _data = data;
            _poids = new List<double>(new double[_data.Count()]);
        }

        public bool Train(int iterations)
        {
            try
            {
                for (int i = 0; i < iterations; i++)
                {
                    foreach (var iBody in _data.DataProp.Keys)
                    {
                        bool output = Process(_data.GetAllPoints(iBody));
                        int error = iBody - (output ? 1 : 0);

                        for (int k = 0; k < _poids.Count; k++)
                        {
                            for (int j = 0; j < _data.GetAllPoints(iBody).Count; j++)
                            {
                                _poids[k] += VITESSE_APPRENTISSAGE * error * _data.GetAllPoints(iBody)[j].X;
                                _poids[k] += VITESSE_APPRENTISSAGE * error * _data.GetAllPoints(iBody)[j].Y;
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Step(double weightedSum)
        {
            return weightedSum > fThreshold;
        }

        private bool Process(List<Point> points)
        {
            double fWeightedSum = 0.0d;

            for (int i = 0; i < points.Count; i++)
            {
                fWeightedSum += points[i].X * _poids[i];
                fWeightedSum += points[i].Y * _poids[i];
            }
            return Step(fWeightedSum);
        }
    }
}
