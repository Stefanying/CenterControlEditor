﻿#pragma checksum "..\..\..\Controls\UserOrder.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FABD38B70D9344F35BAB2310004C51ED"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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


namespace CenterControlEditor.Controls {
    
    
    /// <summary>
    /// UserOrder
    /// </summary>
    public partial class UserOrder : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Controls\UserOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewbox OrderHourContainer;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Controls\UserOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label OrderHour;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Controls\UserOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewbox OrderMinuContainer;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Controls\UserOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label OrderMinu;
        
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
            System.Uri resourceLocater = new System.Uri("/CenterControlEditor;component/controls/userorder.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\UserOrder.xaml"
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
            
            #line 7 "..\..\..\Controls\UserOrder.xaml"
            ((CenterControlEditor.Controls.UserOrder)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.SelectThis);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\Controls\UserOrder.xaml"
            ((CenterControlEditor.Controls.UserOrder)(target)).MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.EditThis);
            
            #line default
            #line hidden
            return;
            case 2:
            this.OrderHourContainer = ((System.Windows.Controls.Viewbox)(target));
            return;
            case 3:
            this.OrderHour = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.OrderMinuContainer = ((System.Windows.Controls.Viewbox)(target));
            return;
            case 5:
            this.OrderMinu = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

