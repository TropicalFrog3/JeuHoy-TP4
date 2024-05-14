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
    public class Perceptron
    {
        #region Champs
        private const double LEARNING_RATE = 0.01;

        private List<double> _weights;
        private SkeletonData _skeletonData;

        public SkeletonData data
        {
            get { return _skeletonData; }
        }
        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="skeletonData"></param>
        public Perceptron(SkeletonData skeletonData)
        {
            _skeletonData = skeletonData;
            _weights = new List<double>();
            Random random = new Random();

            List<List<Point>> inputData = new List<List<Point>>();
            foreach (var body in skeletonData.DataProp)
            {
                inputData.AddRange(body.Value.Values.ToList());
            }

            for (int i = 0; i < inputData.Count(); i++)
            {
                _weights.Add(random.NextDouble());
            }
        }

        /// <summary>
        /// entrainement du perceptron
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="targets"></param>
        /// <param name="iterations"></param>
        public void Train(List<List<Point>> inputs, List<int> targets, int iterations)
        {
            for (int iter = 0; iter < iterations; iter++)
            {
                for (int i = 0; i < inputs.Count; i++)
                {
                    double prediction = Process(inputs[i])[0];
                    double error = targets[i] - prediction;

                    for (int j = 0; j < _weights.Count; j++)
                    {
                        for(int k=0; k < inputs[i].Count; k++)
                        {
                            _weights[j] += LEARNING_RATE * error * inputs[i][k].X;
                            _weights[j] += LEARNING_RATE * error * inputs[i][k].Y;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Processus de prédiction
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public List<double> Process(List<Point> inputs)
        {
            List<double> result = new List<double>();
            double sum = 0.0;

            for (int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i].X * _weights[i];
                sum += inputs[i].Y * _weights[i];
            }

            result.Add(Step(sum) ? 1 : 0);
            result.Add(sum);
            return result;
        }
        /// <summary>
        /// Fonction d'activation
        /// </summary>
        /// <param name="weightedSum"></param>
        /// <returns></returns>
        private bool Step(double weightedSum)
        {
            return (weightedSum >= 0 ? true : false);
        }
    }
}
