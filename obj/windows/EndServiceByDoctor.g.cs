﻿#pragma checksum "..\..\..\windows\EndServiceByDoctor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6F52977C5013D4939BAF7B3BF245AE8B16DCBC56F48A2F5DAA435F1A74A5D0EC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CLINICS.windows;
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


namespace CLINICS.windows {
    
    
    /// <summary>
    /// EndServiceByDoctor
    /// </summary>
    public partial class EndServiceByDoctor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\windows\EndServiceByDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock clickText1;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\windows\EndServiceByDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock clickText2;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\windows\EndServiceByDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button renewOperationStatus;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\windows\EndServiceByDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock successText;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\windows\EndServiceByDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button okShowWithNewStatus;
        
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
            System.Uri resourceLocater = new System.Uri("/CLINICS;component/windows/endservicebydoctor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\windows\EndServiceByDoctor.xaml"
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
            this.clickText1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.clickText2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.renewOperationStatus = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\windows\EndServiceByDoctor.xaml"
            this.renewOperationStatus.Click += new System.Windows.RoutedEventHandler(this.renewOperationStatus_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.successText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.okShowWithNewStatus = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\windows\EndServiceByDoctor.xaml"
            this.okShowWithNewStatus.Click += new System.Windows.RoutedEventHandler(this.okShowWithNewStatus_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
