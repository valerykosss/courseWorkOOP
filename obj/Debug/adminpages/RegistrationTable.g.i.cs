﻿#pragma checksum "..\..\..\adminpages\RegistrationTable.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FBFD4D73856792830C0031D98B3FFCEBD67DA8A59A00983DECB5F62ACAA9B048"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CLINICS.adminpages;
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


namespace CLINICS.adminpages {
    
    
    /// <summary>
    /// RegistrationTable
    /// </summary>
    public partial class RegistrationTable : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DateIDCombobox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ClientIDCombobox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DoctorServiceIDCombobox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StatusCombobox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button edit;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delete;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\adminpages\RegistrationTable.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RegistrationDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/CLINICS;component/adminpages/registrationtable.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\adminpages\RegistrationTable.xaml"
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
            this.DateIDCombobox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 23 "..\..\..\adminpages\RegistrationTable.xaml"
            this.DateIDCombobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DateIDCombobox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ClientIDCombobox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.DoctorServiceIDCombobox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.StatusCombobox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.add = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\adminpages\RegistrationTable.xaml"
            this.add.Click += new System.Windows.RoutedEventHandler(this.add_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.edit = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\adminpages\RegistrationTable.xaml"
            this.edit.Click += new System.Windows.RoutedEventHandler(this.edit_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.save = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\adminpages\RegistrationTable.xaml"
            this.save.Click += new System.Windows.RoutedEventHandler(this.save_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.delete = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\adminpages\RegistrationTable.xaml"
            this.delete.Click += new System.Windows.RoutedEventHandler(this.delete_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.RegistrationDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

