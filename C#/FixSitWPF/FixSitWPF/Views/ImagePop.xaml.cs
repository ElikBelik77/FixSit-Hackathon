using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using WpfAnimatedGif;

namespace FixSitWPF.Views
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Interaction logic for ImagePop.xaml
    /// </summary>
    public partial class ImagePop : MetroWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePop"/> class.
        /// </summary>
        public ImagePop()
        {
            InitializeComponent();
            Grid.SetColumnSpan(cv, 1);
            Grid.SetColumn(cv, 1);
            string[] splitDirData = Environment.CurrentDirectory.Split(new[] { @"\" }, StringSplitOptions.None);
            string pathToResources = string.Join("/", splitDirData.Take(splitDirData.Length - 2)) + "/Views/Resources/fixback.gif";
            ImageView.Source = new BitmapImage(new Uri(pathToResources, UriKind.Absolute));
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(pathToResources, UriKind.Absolute);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(ImageView, image);

        }
    }
}
