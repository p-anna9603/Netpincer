﻿#pragma checksum "..\..\newMenu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DCA2E3CD9DA27CB253FDE2524F422E393A47C74682382218006808633B032E0F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RestaurantClient;
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


namespace RestaurantClient {
    
    
    /// <summary>
    /// newMenu
    /// </summary>
    public partial class newMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\newMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock errorText;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\newMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox categoryNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\newMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ImageButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\newMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Submit;
        
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
            System.Uri resourceLocater = new System.Uri("/RestaurantClient;component/newmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\newMenu.xaml"
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
            
            #line 8 "..\..\newMenu.xaml"
            ((RestaurantClient.newMenu)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.windowClosing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.errorText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.categoryNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ImageButton = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\newMenu.xaml"
            this.ImageButton.Click += new System.Windows.RoutedEventHandler(this.imageUpButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Submit = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\newMenu.xaml"
            this.Submit.Click += new System.Windows.RoutedEventHandler(this.addButton_Click);
            
            #line default
            #line hidden
            
            #line 44 "..\..\newMenu.xaml"
            this.Submit.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.addButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

