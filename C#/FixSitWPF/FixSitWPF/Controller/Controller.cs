using FixSitWPF.Activities;
using FixSitWPF.Models;
using FixSitWPF.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace FixSitWPF.Controller
{
    /// <summary>
    /// Class for the application logic.
    /// </summary>
    public class Controller
    {
        #region Member Variables
        private SettingsModel _SettingsModel;
        private ActivityScheduler _ActivityScheduler;
        private DataClient _Client;
        private MainWindow _Window;


        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the window.
        /// </summary>
        /// <value>
        /// The window.
        /// </value>
        public MainWindow Window
        {
            get { return _Window; }
            set { _Window = value; }
        }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public DataClient Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        /// <summary>
        /// Gets or sets the activity scheduler.
        /// </summary>
        /// <value>
        /// The activity scheduler.
        /// </value>
        public ActivityScheduler ActivityScheduler
        {
            get { return _ActivityScheduler; }
            set { _ActivityScheduler = value; }
        }

        /// <summary>
        /// Gets or sets the settings model.
        /// </summary>
        /// <value>
        /// The settings model.
        /// </value>
        public SettingsModel SettingsModel
        {
            get { return _SettingsModel; }
            set { _SettingsModel = value; }
        }
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="win">The main window controlled by this instance</param>
        public Controller(MainWindow win)
        {
            _SettingsModel = new SettingsModel();
            _Client = new DataClient("127.0.0.1", 10000);
            PostureActivity poseActivity = new PostureActivity(_Client);
            ExerciseActivity execActivity = new ExerciseActivity(this);
            execActivity.OnExerciseStart += (paths) =>
            {
                win.SetContent(win._ExerciseContent);
                win._ExerciseContent.ShowGifs(paths);
            };
            
            poseActivity.OnImageUpdate += PoseActivity_OnImageUpdate;
            _ActivityScheduler = new ActivityScheduler(new Dictionary<IActivity, int>()
            {
                //{ poseActivity, 1 }
                {execActivity,5}
            });
            //_ActivityScheduler.Start();
            _Window = win;
        }

        




        #endregion
        private void PoseActivity_OnImageUpdate(Image image, string description)
        {
            this._Window.WebcamContent.Image = ConvertDrawingImageToWPFImage(image);
            this._Window.WebcamContent.Description = description;
        }

        private System.Windows.Controls.Image ConvertDrawingImageToWPFImage(System.Drawing.Image gdiImg)
        {
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();

            //convert System.Drawing.Image to WPF image
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(gdiImg);
            IntPtr hBitmap = bmp.GetHbitmap();
            System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            img.Source = WpfBitmap;
            img.Width = 500;
            img.Height = 600;
            img.Stretch = System.Windows.Media.Stretch.Fill;
            return img;
        }

        /// <summary>
        /// Creates the python model.
        /// </summary>
        public void CreatePythonModel()
        {
            string[] splitDirData = Environment.CurrentDirectory.Split(new[] { @"\" }, StringSplitOptions.None);
            string pythonModelPath = String.Join("/",splitDirData.Take(splitDirData.Length-5))+ @"/Python/main.py";
            Process.Start(pythonModelPath);
        }
        
    }
}
