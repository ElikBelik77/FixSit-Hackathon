using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FixSitWPF.Views.Buttons
{
    class WebCamButton : System.Windows.Controls.Button, MenuButton
    {
        private Grid _ContentGrid;
        public WebCamButton(Grid _ContentGrid)
        {
            this._ContentGrid = _ContentGrid;
            this.Content = "webcam";
            this.Click += WebCamButton_Click;
            this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#168C64"));
        }
        private void WebCamButton_Click(object sender, RoutedEventArgs e)
        {
            ShowContent(_ContentGrid);
        }

        public void ShowContent(Grid grid)
        {
            FixSitWPF.Views.Contents.WebCamContent content = new FixSitWPF.Views.Contents.WebCamContent();
            grid.Children.Add(content);
            Grid.SetColumn(content, 0);
            Grid.SetRow(content, 0);

        }

        
    }
}
