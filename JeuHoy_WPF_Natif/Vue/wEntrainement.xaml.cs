﻿using JeuHoy_WPF_Natif.Modele;
using JeuHoy_WPF_Natif.Presentation;
using JeuHoy_WPF_Natif.Vue;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class wEntrainement : Window, IwEntrainement
    {
        #region Constants
        public static readonly double DPI = 96.0;
        public static readonly PixelFormat FORMAT = PixelFormats.Bgra32;
        #endregion

        #region Champs 
        private PresentateurwEntrainement _presentateurwEntrainement;

        private Dictionary<string, BitmapImage> _dicImgFigure = new Dictionary<string, BitmapImage>();
        private JouerSon _son = new JouerSon();
        private int _positionEnCours = 1;
        private EventArgs Event = new EventArgs();


        private KinectSensor _kinectSensor = null;
        private WriteableBitmap _bitmap = null;
        private ushort[] _picFrameData = null;
        private byte[] _picPixels = null;

        private SkeletonData _skeletonData = new SkeletonData();
        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        public wEntrainement()
        {
            InitializeComponent();

            _presentateurwEntrainement = new PresentateurwEntrainement(this);

            _kinectSensor = KinectSensor.GetDefault();
            if (_kinectSensor != null)
            {
                _kinectSensor.Open();
                _kinectSensor.IsAvailableChanged += _kinectSensor_IsAvailableChanged; // change title to connected kinect
                SetupDisplay();

                MultiSourceFrameReader multi = _kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color);
                BodyFrameReader bodyFrameReader = _kinectSensor.BodyFrameSource.OpenReader();

                multi.MultiSourceFrameArrived += Multi_MultiSourceFrameArrived;
                bodyFrameReader.FrameArrived += BodyFrameReader_FrameArrived;
            }

            for (int i = 1; i <= CstApplication.NBFIGURE; i++)
            {
                Uri uriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"./HoyContent/fig" + i + ".png", UriKind.Absolute);
                _dicImgFigure.Add("fig" + i, new BitmapImage(uriSource));
            }

            lblNbPositions.Content = "/ " + CstApplication.NBFIGURE.ToString();
            ChargerFigure();
            _son.JouerSonAsync(@"./Presentation/HoyContent/hoy.wav");

            ChargerModel();
        }

        public string Console { get => txtConsole.Text; set => txtConsole.Text = value; }
        string IwEntrainement.NomFichier => "DataPerceptron.txt";

        public SkeletonData Data { get => _skeletonData; set => _skeletonData = value; }
        public SkeletonData CurrentData { get => _skeletonData; }

        public event EventHandler FermetureEvt;
        public event EventHandler LectureFichierEvt;
        public event EventHandler EcritureFichierEvt;
        public event EventHandler EntrainementEvt;
        public event EventHandler TestEvt;

        /// <summary>
        /// arriver de frame de la kinect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            pDessinSquelette.Children.Clear();
            _skeletonData.Clear();

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    Body[] bodies = new Body[CstApplication.SKELETONCOUNT];

                    bodyFrame.GetAndRefreshBodyData(bodies);

                    foreach (Body body in bodies)
                    {
                        if (body.IsTracked)
                        {

                            if (!_skeletonData.ContainsKey(_positionEnCours))
                                _skeletonData.AddBody(_positionEnCours, new Dictionary<JointType, List<Point>>());

                            foreach (Joint joint in body.Joints.Values)
                            {
                                if (joint.TrackingState == TrackingState.Tracked)
                                {
                                    Point point;

                                    point = GetPoint(_kinectSensor, joint.Position, pDessinSquelette.ActualHeight, pDessinSquelette.ActualWidth);

                                    if (!_skeletonData.ContainsKey(_positionEnCours, joint.JointType))
                                        _skeletonData.AddJoint(_positionEnCours, joint.JointType, new List<Point>());
                                    _skeletonData.AddPoint(_positionEnCours, joint.JointType, point);

                                }
                            }
                            DessinerSquelette(body, _kinectSensor);
                        }
                    }
                    TestEvt(this, Event);
                }
            }
            
        }

        /// <summary>
        /// Arrivée de frame de la kinect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Multi_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            MultiSourceFrame sourceFrame = e.FrameReference.AcquireFrame();
            if (sourceFrame == null)
                return;

            using (ColorFrame colorFrame = sourceFrame.ColorFrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                    ShowColorFrame(colorFrame);
            }
        }

        /// <summary>
        /// Affiche le frame de couleur de la kinect
        /// </summary>
        /// <param name="colorFrame"></param>
        private void ShowColorFrame(ColorFrame colorFrame)
        {
            if (colorFrame != null)
            {
                FrameDescription frameDescription = colorFrame.FrameDescription;
                if (_bitmap == null)
                {
                    _bitmap = new WriteableBitmap(frameDescription.Width, frameDescription.Height, DPI, DPI, FORMAT, null);
                }

                colorFrame.CopyConvertedFrameDataToArray(_picPixels, ColorImageFormat.Bgra);
                RenderPixelArray(_picPixels, colorFrame.FrameDescription);
            }
        }

        /// <summary>
        /// Affiche le frame de couleur de la kinect
        /// </summary>
        /// <param name="pixels"></param>
        /// <param name="currentFrameDescription"></param>
        private void RenderPixelArray(byte[] pixels, FrameDescription currentFrameDescription)
        {
            _bitmap.Lock();
            _bitmap.WritePixels(new Int32Rect(0, 0, currentFrameDescription.Width, currentFrameDescription.Height), pixels, currentFrameDescription.Width * 4, 0);
            _bitmap.Unlock();
            picKinect.Source = _bitmap;
        }

        /// <summary>
        /// Setup de l'affichage
        /// </summary>
        private void SetupDisplay()
        {
            // COLOR display
            FrameDescription frameDescription;
            frameDescription = _kinectSensor.ColorFrameSource.FrameDescription;
            _picPixels = new byte[frameDescription.Width * frameDescription.Height * 4];
            _picFrameData = new ushort[frameDescription.Width * frameDescription.Height];
        }

        /// <summary>
        /// Lorsque le sensor Kinect est connecté ou déconnecté, changer le titre de la fenêtre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _kinectSensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            this.Title = "Kinect XBox One [2.0] - " + (e.IsAvailable ? "Connecté" : "Déconnecté");
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
                    DrawBone(body, JointType.Head, JointType.Neck, sensor);
                    DrawBone(body, JointType.Neck, JointType.SpineShoulder, sensor);
                    DrawBone(body, JointType.SpineShoulder, JointType.SpineMid, sensor);
                    DrawBone(body, JointType.SpineMid, JointType.SpineBase, sensor);
                    DrawBone(body, JointType.SpineShoulder, JointType.ShoulderRight, sensor);
                    DrawBone(body, JointType.SpineShoulder, JointType.ShoulderLeft, sensor);
                    DrawBone(body, JointType.SpineBase, JointType.HipRight, sensor);
                    DrawBone(body, JointType.SpineBase, JointType.HipLeft, sensor);
                    DrawBone(body, JointType.ShoulderRight, JointType.ElbowRight, sensor);
                    DrawBone(body, JointType.ElbowRight, JointType.WristRight, sensor);
                    DrawBone(body, JointType.WristRight, JointType.HandRight, sensor);
                    DrawBone(body, JointType.ShoulderLeft, JointType.ElbowLeft, sensor);
                    DrawBone(body, JointType.ElbowLeft, JointType.WristLeft, sensor);
                    DrawBone(body, JointType.WristLeft, JointType.HandLeft, sensor);
                    DrawBone(body, JointType.HipRight, JointType.KneeRight, sensor);
                    DrawBone(body, JointType.KneeRight, JointType.AnkleRight, sensor);
                    DrawBone(body, JointType.AnkleRight, JointType.FootRight, sensor);
                    DrawBone(body, JointType.HipLeft, JointType.KneeLeft, sensor);
                    DrawBone(body, JointType.KneeLeft, JointType.AnkleLeft, sensor);
                    DrawBone(body, JointType.AnkleLeft, JointType.FootLeft, sensor);

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
        /// Dessine un bone d'un squelette d'un senseur Kinect sur le canvas passé en paramètre
        /// </summary>
        /// <param name="body"></param>
        /// <param name="jointType1"></param>
        /// <param name="jointType2"></param>
        /// <param name="sensor"></param>
        private void DrawBone(Body body, JointType jointType1, JointType jointType2, KinectSensor sensor)
        {
            Joint joint1 = body.Joints[jointType1];
            Joint joint2 = body.Joints[jointType2];

            if (joint1.Position.X != 0 && joint1.Position.Y != 0 && joint2.Position.X != 0 && joint2.Position.Y != 0)
            {
                System.Windows.Point point1 = GetPoint(sensor, joint1.Position, pDessinSquelette.ActualHeight, pDessinSquelette.ActualWidth);
                System.Windows.Point point2 = GetPoint(sensor, joint2.Position, pDessinSquelette.ActualHeight, pDessinSquelette.ActualWidth);

                Line bone = new Line();
                bone.Stroke = new SolidColorBrush(Colors.Green);
                bone.StrokeThickness = 10;
                bone.X1 = point1.X;
                bone.Y1 = point1.Y;
                bone.X2 = point2.X;
                bone.Y2 = point2.Y;

                pDessinSquelette.Children.Add(bone);
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
                System.Windows.Point point = GetPoint(sensor, joint.Position, canvas.ActualHeight, canvas.ActualWidth);

                Ellipse ellipse = new Ellipse();
                ellipse.Width = size;
                ellipse.Height = size;

                ellipse.Fill = new SolidColorBrush(Colors.LightGreen);
                Canvas.SetLeft(ellipse, point.X - size / 2);
                Canvas.SetTop(ellipse, point.Y - size / 2);
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
        public System.Windows.Point GetPoint(KinectSensor sensor, CameraSpacePoint position, double canvasHeight, double canvasWidth)
        {
            System.Windows.Point point = new System.Windows.Point();

            DepthSpacePoint depthPoint = sensor.CoordinateMapper.MapCameraPointToDepthSpace(position);
            point.X = float.IsInfinity(depthPoint.X) ? 0.0 : depthPoint.X;
            point.Y = float.IsInfinity(depthPoint.Y) ? 0.0 : depthPoint.Y;

            point.X = point.X / 512 * canvasWidth;
            point.Y = point.Y / 424 * canvasHeight;

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
            Close();
        }

        /// <summary>
        /// Change le curseur lorsque le curseur est sur l'image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picRetour_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
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
            EcritureFichierEvt(this, Event);
            LectureFichierEvt(this, Event);
            EntrainementEvt(this, Event);
        }

        /// <summary>
        /// Charger le modèle de danse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChargerModel_Click(object sender, RoutedEventArgs e)
        {
            ChargerModel();
        }
        /// <summary>
        /// Charger le modèle de danse.
        /// </summary>
        private void ChargerModel()
        {
            LectureFichierEvt(this, Event);
            EntrainementEvt(this, Event);
        }
    }
}
