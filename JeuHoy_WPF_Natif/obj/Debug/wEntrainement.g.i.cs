﻿#pragma checksum "..\..\wEntrainement.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BF9C53163091F5C7759F8E30F1D72079AB9F8B1B5D298F922D31BA28BAD867CA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using JeuHoy_WPF;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace JeuHoy_WPF {
    
    
    /// <summary>
    /// wEntrainement
    /// </summary>
    public partial class wEntrainement : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas pDessinSquelette;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image picKinect;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtConsole;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image picPositionAFaire;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSuivant;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrecedent;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnApprendre;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgRetour;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFigureEnCours;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\wEntrainement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNbPositions;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/JeuHoy_WPF_Natif;component/wentrainement.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\wEntrainement.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.pDessinSquelette = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.picKinect = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.txtConsole = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.picPositionAFaire = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.btnSuivant = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\wEntrainement.xaml"
            this.btnSuivant.Click += new System.Windows.RoutedEventHandler(this.btnClickChangerFigure_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnPrecedent = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\wEntrainement.xaml"
            this.btnPrecedent.Click += new System.Windows.RoutedEventHandler(this.btnClickChangerFigure_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnApprendre = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\wEntrainement.xaml"
            this.btnApprendre.Click += new System.Windows.RoutedEventHandler(this.btnApprendre_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.imgRetour = ((System.Windows.Controls.Image)(target));
            
            #line 43 "..\..\wEntrainement.xaml"
            this.imgRetour.MouseEnter += new System.Windows.Input.MouseEventHandler(this.picRetour_MouseHover);
            
            #line default
            #line hidden
            
            #line 43 "..\..\wEntrainement.xaml"
            this.imgRetour.MouseLeave += new System.Windows.Input.MouseEventHandler(this.picRetour_MouseLeave);
            
            #line default
            #line hidden
            
            #line 43 "..\..\wEntrainement.xaml"
            this.imgRetour.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.picRetour_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lblFigureEnCours = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.lblNbPositions = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
