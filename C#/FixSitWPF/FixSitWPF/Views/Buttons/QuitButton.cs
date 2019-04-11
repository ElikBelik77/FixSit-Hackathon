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
    class QuitButton : System.Windows.Controls.Button, MenuButton
    {
        

        public QuitButton()
        {
           
            this.Click += QuitButton_Click;
            this.Content = "quit";
            this.Background = new SolidColorBrush(Colors.Pink);
        }

        public void ShowContent(Grid grid)
        {
            throw new NotImplementedException();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        
    }
}
