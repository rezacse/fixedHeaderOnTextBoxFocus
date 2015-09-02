using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using fixedHeaderOnTextBoxFocus.Resources;
using System.Windows.Media;
using System.Windows.Data;

namespace fixedHeaderOnTextBoxFocus
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static double newValue = 0.0;
        public static readonly DependencyProperty TranslateYProperty = DependencyProperty.Register("TranslateY", typeof(double), typeof(MainPage), new PropertyMetadata(0d, OnRenderXPropertyChanged));
        
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += BindToKeyboardFocus;
        }

        private void BindToKeyboardFocus(object sender, RoutedEventArgs e)
        {
            PhoneApplicationFrame frame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (frame != null)
            {
                var group = frame.RenderTransform as TransformGroup;
                if (group != null)
                {
                    var translate = group.Children[0] as TranslateTransform;
                    var translateYBinding = new Binding("Y");
                    translateYBinding.Source = translate;
                    SetBinding(TranslateYProperty, translateYBinding);
                }
            }
        }
        
        public double TranslateY
        {
            get { return (double)GetValue(TranslateYProperty); }
            set { SetValue(TranslateYProperty, value); }
        }

        private static void OnRenderXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((double)e.NewValue <= newValue)
                ((MainPage)d).UpdateTopMargin((double)e.NewValue);
            newValue = (double)e.NewValue;
        }
        
        private void UpdateTopMargin(double translateY)
        {
            LayoutRoot.Margin = new Thickness(0, -translateY, 0, 0);
        }
            
        private void TextBox_OnLostFocus(object sender, RoutedEventArgs e)
        { 
            LayoutRoot.Margin = new Thickness();
        }
    }
}