using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace FixSitWPF.Views.Contents
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Interaction logic for ExerciseContent.xaml
    /// </summary>
    public partial class ExerciseContent : UserControl
    {
        private int _CurrentGif;
        private List<string> _Paths;
        public ExerciseContent()
        {
            InitializeComponent();
            _CurrentGif = 0;
        }

        

        public void ShowGifs(List<string> paths)
        {
            _Paths = paths;
            if (_CurrentGif == paths.Count)
            {
                _CurrentGif = 0;
            }
            ShowGif(paths[_CurrentGif]);
            _CurrentGif++; 

        }

        private void ShowGif(string path)
        {
            GifView.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path, UriKind.Absolute);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(GifView, image);
        }
        private void Next_Button(object sender, RoutedEventArgs e)
        {
            ShowGifs(_Paths);
        }

    }

}
