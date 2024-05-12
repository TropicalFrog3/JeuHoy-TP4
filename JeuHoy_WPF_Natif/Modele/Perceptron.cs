using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace JeuHoy_WPF_Natif.Modele
{
    public class Perceptron
    {
        private const double LEARNING_RATE = 0.1;

        private List<double> _weights;
        private SkeletonData _skeletonData;
        private double threshold = 0.5;

        public SkeletonData data
        {
            get { return _skeletonData; }
        }

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
                        _weights[j] += LEARNING_RATE * error * inputs[i][j].X;
                        _weights[j] += LEARNING_RATE * error * inputs[i][j].Y;
                    }
                }
                threshold += LEARNING_RATE;
            }
        }

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

        private bool Step(double weightedSum)
        {
            return weightedSum > threshold;
        }
    }
}
