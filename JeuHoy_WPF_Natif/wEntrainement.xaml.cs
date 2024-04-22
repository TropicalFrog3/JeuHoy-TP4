using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JeuHoy_WPF
{
    /// <summary>
    /// Auteur:      Hugo St-Louis
    /// Description: Permet de faire l'entrainement des différentes figures de danse.
    /// Date:        2023-04-17
    /// </summary>
    public partial class wEntrainement : Window
    {
      
        private Dictionary<string, BitmapImage> _dicImgFigure = new Dictionary<string, BitmapImage>();
        private JouerSon _son = new JouerSon();
        private int _positionEnCours = 1;
       

        /// <summary>
        /// Constructeur
        /// </summary>
        public wEntrainement()
        {
            InitializeComponent();

            for (int i = 1; i <= CstApplication.NBFIGURE; i++)
            {
                Uri uriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"./HoyContent/fig" + i + ".png", UriKind.Absolute);
                _dicImgFigure.Add("fig" + i, new BitmapImage(uriSource));
            }

            lblNbPositions.Content = "/ " + CstApplication.NBFIGURE.ToString();
            ChargerFigure();
            _son.JouerSonAsync(@"./HoyContent/hoy.wav");
        }

        /// <summary>
        /// Dessine un ellipse pour chacune des jointure du squelette détecté.
        /// </summary>
        /// <param name="joueur">Le joueur détecté</param>
        /// <param name="sensor">Le sensor Kinect</param>
        private void DessinerSquelette(Body body, KinectSensor sensor)
        {
            try
            {
                if (body != null)
                {
                    Joint[] joints = body.Joints.Values.ToArray();
                    for (int i = 0; i < joints.Count(); i++)
                        DrawJoint(sensor, joints[i], CstApplication.BODY_ELLIPSE_SIZE, pDessinSquelette);
                }
            }
            catch (Exception ex)
            {
                txtConsole.Text = ex.Message;
            }
        }


        /// <summary>
        /// Dessine le joint d'un squellete d'un senseur Kinect sur le canvas passé en paramètre
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="joint"></param>
        /// <param name="size"></param>
        /// <param name="canvas"></param>
        private void DrawJoint(KinectSensor sensor, Joint joint, int size, Canvas canvas)
        {
            if (joint.Position.X != 0 && joint.Position.Y != 0 && joint.Position.Z != 0)
            {
                // Convertir la position du joint en coordonnées d'écran
                System.Windows.Point point = GetPoint(sensor, joint.Position, canvas.Height, canvas.Width);

                // Créer un cercle à la position du joint
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Colors.Green);
                ellipse.Width = size;
                ellipse.Height = size;

                // Positionner le cercle sur l'élément de dessin Canvas
                Canvas.SetLeft(ellipse, point.X - size / 2);
                Canvas.SetTop(ellipse, point.Y - size / 2);

                // Ajouter le cercle à l'élément de dessin Canvas
                canvas.Children.Add(ellipse);
            }
        }

        /// <summary>
        /// Retourne le point x,y d'un joint par rapport à la taille d'un canvas. 
        /// J'ai permis de dépasser le canvas car je trouvais ça drole :-)
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="position"></param>
        /// <param name="iCanvasHeight"></param>
        /// <param name="iCanvasWidth"></param>
        /// <returns></returns>
        public System.Windows.Point GetPoint(KinectSensor sensor, CameraSpacePoint position, double iCanvasHeight, double iCanvasWidth)
        {
            System.Windows.Point point = new System.Windows.Point();

            DepthSpacePoint depthPoint = sensor.CoordinateMapper.MapCameraPointToDepthSpace(position);
            point.X = float.IsInfinity(depthPoint.X) ? 0.0 : depthPoint.X;
            point.Y = float.IsInfinity(depthPoint.Y) ? 0.0 : depthPoint.Y;

            // La Kinect pour Xbox One utilise également le SDK 2 de Microsoft, et sa résolution de profondeur est de 512x424 pixels.
            //// Ainsi, la résolution de la carte de profondeur pour la Kinect pour Xbox One est également de 512x424 pixels.
            point.X = point.X / 512 * iCanvasHeight;
            point.Y = point.Y / 424 * iCanvasWidth;

            return point;
        }


        /// <summary>
        /// Charger la figure de danse en cours.
        /// </summary>
        private void ChargerFigure()
        {
            BitmapImage imgValue;
            bool bResultat;

            if (_positionEnCours > CstApplication.NBFIGURE)
                _positionEnCours = 1;

            if (_positionEnCours < 1)
                _positionEnCours = CstApplication.NBFIGURE;

            lblFigureEnCours.Content = _positionEnCours.ToString();

            bResultat = _dicImgFigure.TryGetValue("fig" + _positionEnCours, out imgValue);
            if (bResultat == true)
                picPositionAFaire.Source = imgValue;

        }


        /// <summary>
        /// Fermeture de la fenêtre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picRetour_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Change le curseur lorsque le curseur est sur l'image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picRetour_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Change le curseur lorsque le curseur est sur l'image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picRetour_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// Lorsqu'on appuie sur le bouton suivant ou précédent, modifier la figure en conséquence.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClickChangerFigure_Click(object sender, RoutedEventArgs e)
        {
            Control bouton = (Control)sender;

            if (bouton.Name == "btnSuivant")
                _positionEnCours++;
            else if (bouton.Name == "btnPrecedent")
                _positionEnCours--;

            ChargerFigure();
        }


        /// <summary>
        /// Apprentissage avec la position obtenu à partir de la Kinect versus l'image affichée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApprendre_Click(object sender, RoutedEventArgs e)
        {
            //Ajouter du code ICI

        }


    }
}
