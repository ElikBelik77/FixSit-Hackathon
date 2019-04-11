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
    class SettingsButton : System.Windows.Controls.Button, MenuButton
    {
        Grid _ContentGrid;
        public SettingsButton(Grid _ContentGrid)
        {
            this._ContentGrid = _ContentGrid;
            this.Content = "settings";
            this.Click += SettingsButton_Click;
            this.Background = new SolidColorBrush(Colors.Orange);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowContent(_ContentGrid);
        }

        public void ShowContent(Grid grid)
        {
            FixSitWPF.Views.Contents.SettingsContent content = new FixSitWPF.Views.Contents.SettingsContent();
            grid.Children.Add(content);
            Grid.SetColumn(content, 0);
            Grid.SetRow(content, 0);

        }
    }
}
