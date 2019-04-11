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
    class StatsButton : System.Windows.Controls.Button, MenuButton
    {
        Grid _ContentGrid;
        public StatsButton(Grid _ContentGrid)
        {
            this._ContentGrid = _ContentGrid;
            this.Content = "stats";
            this.Click += StatsButton_Click;
            this.Background = new SolidColorBrush(Colors.Blue);
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowContent(_ContentGrid);
        }

        public void ShowContent(Grid grid)
        {
            FixSitWPF.Views.Contents.StatsContent content = new FixSitWPF.Views.Contents.StatsContent();
            grid.Children.Add(content);
            Grid.SetColumn(content, 0);
            Grid.SetRow(content, 0);

        }
    }
}
