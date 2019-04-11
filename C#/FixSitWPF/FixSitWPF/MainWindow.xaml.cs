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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FixSitWPF.Views.Contents;
using FixSitWPF.Views;

namespace FixSitWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private System.Windows.Forms.NotifyIcon _NotifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            FixSitWPF.Controller.Controller controller = new FixSitWPF.Controller.Controller();
            _NotifyIcon = new System.Windows.Forms.NotifyIcon();
            _NotifyIcon.Icon = System.Drawing.SystemIcons.Application;
            _NotifyIcon.Click += _NotifyIcon_Click;
            FixSitButton.Click += (sender, e) =>
            {
                SetContent(new FixSitContent());
            };

            WebcamButton.Click += (sender, e) => {
                SetContent(new WebCamContent());
            };

            StatisticsButton.Click += (sender, e) => {
                SetContent(new StatsContent());
            };

            ExerciseButton.Click += (sender, e) =>
            {
                SetContent(new ExerciseContent());
            };

            SettingsButton.Click += (sender, e) =>
            {
                SetContent(new SettingsContent(controller.SettingsModel));
            };
            
            QuitButton.Click += Quit_Click;

            //FixSitWPF.Controller.Controller c = new Controller.Controller();
            //c.CreatePythonModel();

        }
        private void SetContent(System.Windows.Controls.UserControl control)
        {
            Display.Children.Clear();
            Display.Children.Add(control);
            
        }

        private void _NotifyIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.Show();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                
                _NotifyIcon.Visible = false;
            }

            else if (WindowState.Normal== this.WindowState)
            {
                _NotifyIcon.Visible = true;

                this.Hide();
            }
        }
    }
}
