﻿#pragma checksum "..\..\..\pages\ChooseDoctorListView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A189E814CF731E420EB850AF98A5E6F9DEC453889923A31DBF19ED03440A419F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CLINICS;
using CLINICS.pages;
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


namespace CLINICS.pages {
    
    
    /// <summary>
    /// ChooseDoctorListView
    /// </summary>
    public partial class ChooseDoctorListView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 85 "..\..\..\pages\ChooseDoctorListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseWindow;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\pages\ChooseDoctorListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock inCaseNullDocs;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\pages\ChooseDoctorListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView DoctorsItemsControl;
        
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
            System.Uri resourceLocater = new System.Uri("/CLINICS;component/pages/choosedoctorlistview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\pages\ChooseDoctorListView.xaml"
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
            this.CloseWindow = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\pages\ChooseDoctorListView.xaml"
            this.CloseWindow.Click += new System.Windows.RoutedEventHandler(this.CloseWindow_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.inCaseNullDocs = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.DoctorsItemsControl = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

