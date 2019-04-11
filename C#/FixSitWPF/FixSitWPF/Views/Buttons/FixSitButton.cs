using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace FixSitWPF.Views.Buttons
{
    class FixSitButton : System.Windows.Controls.Button, MenuButton
    {
        Grid content_grid;
        public FixSitButton(Grid content_grid)
        {
            this.content_grid = content_grid;
            this.Content = "fixsit";
            this.Click += FixSitButton_Click;
            this.Background = new SolidColorBrush(Colors.Yellow);
        }

        private void FixSitButton_Click(object sender, RoutedEventArgs e)
        {
            ShowContent(content_grid);
        }

        public void ShowContent(Grid grid)
        {
            FixSitWPF.Views.Contents.FixSitContent content = new FixSitWPF.Views.Contents.FixSitContent();
            grid.Children.Add(content);
            Grid.SetColumn(content, 0);
            Grid.SetRow(content, 0);

        }
    }
}
