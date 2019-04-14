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
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace FixSitWPF.Views
{
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
