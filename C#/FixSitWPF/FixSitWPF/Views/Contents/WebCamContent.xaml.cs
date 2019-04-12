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
using WpfAnimatedGif;

namespace FixSitWPF.Views.Contents
{
    /// <summary>
    /// Interaction logic for WebCamContent.xaml
    /// </summary>
    public partial class WebCamContent : UserControl
    {
        #region Member Variables
        private string _Description;
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="WebCamContent"/> class.
        /// </summary>
        public WebCamContent()
        {
            InitializeComponent();
            ShowMeButton.Click += ShowMeButton_Click;
        }

        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                DescriptionLabel.Content = value;
            }
        }

        /// <summary>
        /// Sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public Image Image
        {
            set
            {
                ImageView.Source = value.Source;
            }
        }
        #endregion

        private ImageSource _PrevSource;
        private void ShowMeButton_Click(object sender, RoutedEventArgs e)
        {
            string[] splitDirData = Environment.CurrentDirectory.Split(new[] { @"\" }, StringSplitOptions.None);
            string pathToResources = String.Join("/", splitDirData.Take(splitDirData.Length - 2)) + "/Views/Resources/fixback.gif";
            ImageView.Source = new BitmapImage(new Uri(pathToResources, UriKind.Absolute));
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(pathToResources, UriKind.Absolute);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(this.ImageView, image);
        }
    }
}
