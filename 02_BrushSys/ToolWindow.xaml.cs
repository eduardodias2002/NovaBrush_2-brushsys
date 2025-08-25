﻿using _NovaBrush;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NovaBrush._02_BrushSys
{
    public partial class ToolWindow : UserControl
    {
        private bool _isDragging = false;
        private Point _dragStartScreenPoint;
        private Point _elementStartPosition;

        public ToolWindow()
        {
            InitializeComponent();
        }

        // When holding the tip
        private void Wnd_ToolWindowTip_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Mouse.Capture(this, CaptureMode.Element);

                _dragStartScreenPoint = e.GetPosition(Application.Current.MainWindow);
                _elementStartPosition = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));

                _isDragging = true;
                e.Handled = true;
            }
        }

        // When moving while holding the tip
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isDragging)
            {
                Point currentScreenPoint = e.GetPosition(Application.Current.MainWindow);

                double offsetX = currentScreenPoint.X - _dragStartScreenPoint.X;
                double offsetY = currentScreenPoint.Y - _dragStartScreenPoint.Y;

                double newLeft = _elementStartPosition.X + offsetX;
                double newTop = _elementStartPosition.Y + offsetY;

                double maxWidth = Application.Current.MainWindow.ActualWidth - this.ActualWidth;
                double maxHeight = Application.Current.MainWindow.ActualHeight - this.ActualHeight;

                newLeft = Math.Clamp(newLeft, 0, maxWidth);
                newTop = Math.Clamp(newTop, 0, maxHeight);

                Canvas.SetLeft(this, newLeft);
                Canvas.SetTop(this, newTop);

            }
        }

        // Disable when left mouse releases
        protected override void OnMouseUp(MouseButtonEventArgs e){
            _isDragging = false;
            Mouse.Capture(null);
            e.Handled = true;
        }

        // Grabs tag as a string, set it
        private void ToolButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement button)
            {
                string toolName = button.Tag.ToString(); ;
                if (Globals.Tools.ContainsKey(toolName)){
                    Globals.CurrentTool = Globals.Tools[toolName];
                    HighlightActiveTool(button);
                }
            }
        }

        private void HighlightActiveTool(FrameworkElement activeButton)
        {
            if (activeButton.Parent is Panel panel)
            {
                foreach (var child in panel.Children.OfType<FrameworkElement>())
                {
                    if (child == activeButton)
                    {
                        child.Opacity = 1.0; // Full bright
                        child.Effect = new DropShadowEffect
                        {
                            Color = Colors.White,
                            ShadowDepth = 0
                        };
                    }
                    else
                    {
                        child.Opacity = 1.0; // Dimmed
                        child.Effect = null;
                    }
                }
            }
        }

    }
}
