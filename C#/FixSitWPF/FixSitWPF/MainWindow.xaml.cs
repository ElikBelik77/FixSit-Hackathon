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
            _NotifyIcon = new System.Windows.Forms.NotifyIcon();
            _NotifyIcon.Icon = System.Drawing.SystemIcons.Application;
            _NotifyIcon.Click += _NotifyIcon_Click;
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.FixSitButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.WebCamButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.ExerciseButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.StatsButton(Display));
            AppMenu.AddButton(new FixSitWPF.Views.Buttons.SettingsButton(Display));
            FixSitWPF.Views.Buttons.QuitButton quit = new FixSitWPF.Views.Buttons.QuitButton();
            AppMenu.AddButton(quit);
            quit.Click += Quit_Click;
            
            
            
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
