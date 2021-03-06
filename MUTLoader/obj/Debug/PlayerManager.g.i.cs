﻿#pragma checksum "..\..\PlayerManager.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E2274B714BDD69CD1630025B12FCCF7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
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


namespace MUTLoader {
    
    
    /// <summary>
    /// PlayerManager
    /// </summary>
    public partial class PlayerManager : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OVRBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IDBox;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PriceBox;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddPlayerButton;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ModifyPlayerButton;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemovePlayerButton;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ViewPlayersButton;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExecuteButton;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StopButton;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DelayButton;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HelpButton;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\PlayerManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TextButton;
        
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
            System.Uri resourceLocater = new System.Uri("/MUTLoader;component/playermanager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PlayerManager.xaml"
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
            
            #line 6 "..\..\PlayerManager.xaml"
            ((MUTLoader.PlayerManager)(target)).Closed += new System.EventHandler(this.PlayerManager_OnClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.OVRBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.NameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.IDBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.PriceBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.AddPlayerButton = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\PlayerManager.xaml"
            this.AddPlayerButton.Click += new System.Windows.RoutedEventHandler(this.AddPlayerButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ModifyPlayerButton = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\PlayerManager.xaml"
            this.ModifyPlayerButton.Click += new System.Windows.RoutedEventHandler(this.ModifyPlayerButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.RemovePlayerButton = ((System.Windows.Controls.Button)(target));
            
            #line 92 "..\..\PlayerManager.xaml"
            this.RemovePlayerButton.Click += new System.Windows.RoutedEventHandler(this.RemovePlayerButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ViewPlayersButton = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\PlayerManager.xaml"
            this.ViewPlayersButton.Click += new System.Windows.RoutedEventHandler(this.ViewPlayersButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ExecuteButton = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\PlayerManager.xaml"
            this.ExecuteButton.Click += new System.Windows.RoutedEventHandler(this.ExecuteButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.StopButton = ((System.Windows.Controls.Button)(target));
            
            #line 122 "..\..\PlayerManager.xaml"
            this.StopButton.Click += new System.Windows.RoutedEventHandler(this.StopButton_OnClickButtonClick);
            
            #line default
            #line hidden
            return;
            case 12:
            this.DelayButton = ((System.Windows.Controls.Button)(target));
            
            #line 152 "..\..\PlayerManager.xaml"
            this.DelayButton.Click += new System.Windows.RoutedEventHandler(this.DelayButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 13:
            this.HelpButton = ((System.Windows.Controls.Button)(target));
            
            #line 159 "..\..\PlayerManager.xaml"
            this.HelpButton.Click += new System.Windows.RoutedEventHandler(this.HelpButtonClick);
            
            #line default
            #line hidden
            return;
            case 14:
            this.TextButton = ((System.Windows.Controls.Button)(target));
            
            #line 166 "..\..\PlayerManager.xaml"
            this.TextButton.Click += new System.Windows.RoutedEventHandler(this.TextButton_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

