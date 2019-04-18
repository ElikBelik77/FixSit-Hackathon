using FixSitWPF.Activities;
using FixSitWPF.Models;
using FixSitWPF.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace FixSitWPF.Controller
{
    /// <summary>
    /// Class for the application logic.
    /// </summary>
    public class Controller
    {
        #region Member Variables

        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the window.
        /// </summary>
        /// <value>
        /// The window.
        /// </value>
        public MainWindow Window { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public DataClient Client { get; set; }

        /// <summary>
        /// Gets or sets the activity scheduler.
        /// </summary>
        /// <value>
        /// The activity scheduler.
        /// </value>
        public ActivityScheduler ActivityScheduler { get; set; }

        /// <summary>
        /// Gets or sets the settings model.
        /// </summary>
        /// <value>
        /// The settings model.
        /// </value>
        public SettingsModel SettingsModel { get; set; }

        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="win">The main window controlled by this instance</param>
        public Controller(MainWindow win)
        {
            SettingsModel = new SettingsModel();
            SettingsModel.PropertyChanged += _SettingsModel_PropertyChanged;
            Client = new DataClient("127.0.0.1", 10000);
            PostureActivity poseActivity = new PostureActivity(Client);
            ExerciseActivity execActivity = new ExerciseActivity(this);
            execActivity.OnExerciseStart += (paths) =>
            {

                win.Show();
                win.SetContent(win.ExerciseContent);
                win.ExerciseContent.ShowGifs(paths);
                ActivityScheduler.Pause();
            };

            poseActivity.OnImageUpdate += PoseActivity_OnImageUpdate;
            ActivityScheduler = new ActivityScheduler(new Dictionary<IActivity, int>()
            {
                { poseActivity, 15},
                {execActivity,30}
            });
            ActivityScheduler.Start();

            Window = win;
            Window.OnMaximize += Window_OnMaximize;
            Window.OnMinimize += Window_OnMinimize;
            Window.Closing += _Window_Closing;
        }

        private void _Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ActivityScheduler.Pause();
                _Process.Kill();
                Environment.Exit(0);
            }
            catch
            {
                // ignored
            }
        }

        private void _SettingsModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PostureTimeInterval":
                    ActivityScheduler.UpdateTimeInterval("posture", ((SettingsModel) sender).PostureTimeInterval);
                    break;
                case "ExerciseTimeInterval":
                    ActivityScheduler.UpdateTimeInterval("exercise", ((SettingsModel) sender).ExerciseTimeInterval);
                    break;
            }
        }

        private void Window_OnMinimize() => ActivityScheduler.Resume();

        private void Window_OnMaximize() => ActivityScheduler.Pause();

        #endregion

        private void PoseActivity_OnImageUpdate(Image image, string description)
        {
            ActivityScheduler.Pause();
            Window.WebcamContent.Image = ConvertDrawingImageToWpfImage(image);
            Window.WebcamContent.Description = description;
            Window.SetContent(Window.WebcamContent);
            Window.Show();
        }

        private static System.Windows.Controls.Image ConvertDrawingImageToWpfImage(Image gdiImg)
        {
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();

            //convert System.Drawing.Image to WPF image
            Bitmap bmp = new Bitmap(gdiImg);
            IntPtr hBitmap = bmp.GetHbitmap();
            System.Windows.Media.ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            img.Source = wpfBitmap;
            img.Stretch = System.Windows.Media.Stretch.Fill;
            return img;
        }
        private Process _Process;
        /// <summary>
        /// Creates the python model.
        /// </summary>
        public void CreatePythonModel()
        {
            string[] splitDirData = Environment.CurrentDirectory.Split(new[] { @"\" }, StringSplitOptions.None);
            string pythonModelPath = string.Join("/", splitDirData.Take(splitDirData.Length - 5)) + @"/Python/main.py";

            _Process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "python.exe",
                    Arguments = pythonModelPath,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                }
            };
            _Process.Start();
        }



        
    }
}
