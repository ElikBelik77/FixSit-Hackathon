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
    class QuitButton : System.Windows.Controls.Button, MenuButton
    {
        
        

        public QuitButton()
        {
           
            
            this.Content = "quit";
            this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#168C64"));
        }

        public void ShowContent(Grid grid)
        {
            
        }
        

       

        
    }
}
