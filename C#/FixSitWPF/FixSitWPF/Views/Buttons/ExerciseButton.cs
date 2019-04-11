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
    class ExerciseButton : System.Windows.Controls.Button, MenuButton
    {
        Grid _ContentGrid;
        public ExerciseButton(Grid _ContentGrid)
        {
            this._ContentGrid = _ContentGrid;
            this.Content = "exercise";
            this.Click += ExerciseButton_Click;
            this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#168C64"));

        }

        private void ExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            ShowContent(_ContentGrid);
        }

        public void ShowContent(Grid grid)
        {
            FixSitWPF.Views.Contents.ExerciseContent content = new FixSitWPF.Views.Contents.ExerciseContent();
            grid.Children.Add(content);
            Grid.SetColumn(content, 0);
            Grid.SetRow(content, 0);

        }
    }
}
