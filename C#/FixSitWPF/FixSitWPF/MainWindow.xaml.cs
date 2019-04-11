using FixSitWPF.Activities;
using FixSitWPF.Controller;
using MahApps.Metro.Controls;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FixSitWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.FixSitButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.WebCamButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.ExerciseButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.StatsButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.SettingsButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.QuitButton());
            ActivityScheduler sche = new ActivityScheduler(new Dictionary<IActivity, int>()
            {
                {new PostureActivity(),10 }
            });
            sche.Start();

            Controller.Controller c = new Controller.Controller();
            c.CreatePythonModel();
            
            
        }


        
    }
}
