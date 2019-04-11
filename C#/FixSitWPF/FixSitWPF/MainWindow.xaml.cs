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
        #region Member Variable
        private System.Windows.Forms.NotifyIcon _NotifyIcon;
        private WebCamContent _WebcamContent;
        private FixSitContent _FixSitContent;
        public ExerciseContent _ExerciseContent;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the content of the webcam.
        /// </summary>
        /// <value>
        /// The content of the webcam.
        /// </value>
        public WebCamContent WebcamContent
        {
            get { return _WebcamContent; }
            set { _WebcamContent = value; }
        }
        #endregion


        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _WebcamContent = new WebCamContent();
            _FixSitContent = new FixSitContent();
            _ExerciseContent = new ExerciseContent();
            SetContent(_FixSitContent);
            FixSitWPF.Controller.Controller controller = new FixSitWPF.Controller.Controller(this);

            _NotifyIcon = new System.Windows.Forms.NotifyIcon();
            _NotifyIcon.Icon = System.Drawing.SystemIcons.Application;
            _NotifyIcon.Click += _NotifyIcon_Click;
            FixSitButton.Click += (sender, e) =>
            {
                SetContent(_FixSitContent);
            };

            WebcamButton.Click += (sender, e) => {
                SetContent(_WebcamContent);
            };

            

            ExerciseButton.Click += (sender, e) =>
            {
                Random rnd = new Random();
                int itemsNeeded = 3;
                List<string> gifs = new List<string>(itemsNeeded);
                List<int> numbers = new List<int>(itemsNeeded);
                List<int> availableNumbers = new List<int>();
                int n;
                for (int i = 0; i < 5; i++)
                {
                    availableNumbers.Add(i + 1);
                }
                for (int i = 0; i < itemsNeeded; i++)
                {
                    int index = rnd.Next(0, availableNumbers.Count);
                    numbers.Add(availableNumbers[index]);
                    availableNumbers.Remove(numbers[i]);
                }

                string[] splitDirData = Environment.CurrentDirectory.Split(new[] { @"\" }, StringSplitOptions.None);
                string pathToResources = String.Join("/", splitDirData.Take(splitDirData.Length - 2)) + "/Views/Resources/exercise";
                Console.WriteLine(pathToResources);
                string Sgif1 = pathToResources + numbers[0].ToString() + ".gif";
                string Sgif2 = pathToResources + numbers[1].ToString() + ".gif";
                string Sgif3 = pathToResources + numbers[2].ToString() + ".gif";
                gifs.Add(Sgif1);
                gifs.Add(Sgif2);
                gifs.Add(Sgif3);
                _ExerciseContent.ShowGifs(gifs);
                SetContent(_ExerciseContent);
            };

            SettingsButton.Click += (sender, e) =>
            {
                SetContent(new SettingsContent(controller.SettingsModel));
            };
            
            QuitButton.Click += Quit_Click;

            controller.CreatePythonModel();

        }
        #endregion


        /// <summary>
        /// Sets the content of the main part of the main window.
        /// </summary>
        /// <param name="control">The control.</param>
        public void SetContent(System.Windows.Controls.UserControl control)
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

            else if (WindowState.Normal == this.WindowState)
            {
                _NotifyIcon.Visible = true;
                this.Hide();
            }
        }
    }
}
