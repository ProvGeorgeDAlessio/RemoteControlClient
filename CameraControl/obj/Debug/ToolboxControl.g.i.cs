﻿#pragma checksum "..\..\ToolboxControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5036158C8B85B71908F94001751CA5A6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace CameraControl {
    
    
    /// <summary>
    /// ToolboxControl
    /// </summary>
    public partial class ToolboxControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\ToolboxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button left;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\ToolboxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button right;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ToolboxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button up;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\ToolboxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button down;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ToolboxControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button home;
        
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
            System.Uri resourceLocater = new System.Uri("/CameraControl;component/toolboxcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ToolboxControl.xaml"
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
            this.left = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\ToolboxControl.xaml"
            this.left.Click += new System.Windows.RoutedEventHandler(this.left_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.right = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\ToolboxControl.xaml"
            this.right.Click += new System.Windows.RoutedEventHandler(this.right_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.up = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\ToolboxControl.xaml"
            this.up.Click += new System.Windows.RoutedEventHandler(this.up_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.down = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\ToolboxControl.xaml"
            this.down.Click += new System.Windows.RoutedEventHandler(this.down_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.home = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\ToolboxControl.xaml"
            this.home.Click += new System.Windows.RoutedEventHandler(this.home_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

