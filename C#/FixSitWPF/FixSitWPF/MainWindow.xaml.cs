using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FixSitWPF.Views.Contents;
using MahApps.Metro.Controls;

namespace FixSitWPF
{
    /// <inheritdoc cref="MetroWindow"/>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Member Variable
        private readonly System.Windows.Forms.NotifyIcon _NotifyIcon;

        #endregion

        #region Properties   

        /// <summary>
        /// Gets or sets the content of the exercise.
        /// </summary>
        /// <value>
        /// The content of the exercise.
        /// </value>
        public ExerciseContent ExerciseContent { get; set; }

        /// <summary>
        /// Gets or sets the content of the webcam.
        /// </summary>
        /// <value>
        /// The content of the webcam.
        /// </value>
        public WebCamContent WebcamContent { get; set; }

        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            WebcamContent = new WebCamContent();
            FixSitContent fixSitContent = new FixSitContent();
            ExerciseContent = new ExerciseContent();
            SetContent(fixSitContent);
            FixSitWPF.Controller.Controller controller = new FixSitWPF.Controller.Controller(this);

            _NotifyIcon = new System.Windows.Forms.NotifyIcon {Icon = System.Drawing.SystemIcons.Application};
            _NotifyIcon.Click += _NotifyIcon_Click;
            FixSitButton.Click += (sender, e) =>
            {
                SetContent(fixSitContent);
            };

            WebcamButton.Click += (sender, e) => {
                SetContent(WebcamContent);
            };

            

            ExerciseButton.Click += (sender, e) =>
            {
                Random rnd = new Random();
                int itemsNeeded = 4;
                List<string> gifs = new List<string>(itemsNeeded);
                List<int> numbers = new List<int>(itemsNeeded);
                List<int> availableNumbers = new List<int>();
                for (int i = 0; i < 9; i++)
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
                string pathToResources = string.Join("/", splitDirData.Take(splitDirData.Length - 2)) + "/Views/Resources/exercise";
                Console.WriteLine(pathToResources);
                string gif1 = pathToResources + numbers[0].ToString() + ".gif";
                string gif2  = pathToResources + numbers[1].ToString() + ".gif";
                string gif3 = pathToResources + numbers[2].ToString() + ".gif";
                string gif4 = pathToResources + numbers[3].ToString() + ".gif";
                gifs.Add(gif1);
                gifs.Add(gif2);
                gifs.Add(gif3);
                gifs.Add(gif4);
                ExerciseContent.ShowGifs(gifs);
                SetContent(ExerciseContent);
            };

            SettingsButton.Click += (sender, e) =>
            {
                SetContent(new SettingsContent(controller.SettingsModel));
            };
            
            QuitButton.Click += Quit_Click;

            controller.CreatePythonModel();

        }
        #endregion

        public delegate void FocusEvent();
        public event FocusEvent OnMinimize;
        public event FocusEvent OnMaximize;

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
            WindowState = WindowState.Normal;
            Show();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Minimized:
                    _NotifyIcon.Visible = false;
                    OnMaximize?.Invoke();
                    break;
                case WindowState.Normal:
                    _NotifyIcon.Visible = true;
                    OnMinimize?.Invoke();
                    Hide();
                    break;
            }
        }
    }
}
