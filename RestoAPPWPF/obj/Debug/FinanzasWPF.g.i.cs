﻿#pragma checksum "..\..\FinanzasWPF.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CB5845203AC7DB5C45050CB5E41711F37B5EF83797C61D5813C60401173EA2CB"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome.WPF;
using FontAwesome.WPF.Converters;
using RestoAPPWPF;
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


namespace RestoAPPWPF {
    
    
    /// <summary>
    /// FinanzasWPF
    /// </summary>
    public partial class FinanzasWPF : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\FinanzasWPF.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGenerarReporteIngresos;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\FinanzasWPF.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grListar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\FinanzasWPF.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBuscar;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\FinanzasWPF.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtgridListaFinanzas;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\FinanzasWPF.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrar;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\FinanzasWPF.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnminimizar;
        
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
            System.Uri resourceLocater = new System.Uri("/RestoAPPWPF;component/finanzaswpf.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FinanzasWPF.xaml"
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
            
            #line 10 "..\..\FinanzasWPF.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnGenerarReporteIngresos = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\FinanzasWPF.xaml"
            this.btnGenerarReporteIngresos.Click += new System.Windows.RoutedEventHandler(this.btnIrReportes_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.grListar = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.btnBuscar = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\FinanzasWPF.xaml"
            this.btnBuscar.Click += new System.Windows.RoutedEventHandler(this.btnBuscar_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dtgridListaFinanzas = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.btnCerrar = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\FinanzasWPF.xaml"
            this.btnCerrar.Click += new System.Windows.RoutedEventHandler(this.btnCerrar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnminimizar = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\FinanzasWPF.xaml"
            this.btnminimizar.Click += new System.Windows.RoutedEventHandler(this.btnMinimizar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

