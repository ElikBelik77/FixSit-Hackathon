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
using System.ComponentModel;
using System.Timers;
using WpfAnimatedGif;

namespace FixSitWPF.Views.Contents
{
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
            _Paths = gifs;
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
            this.GifView.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path, UriKind.Absolute);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(this.GifView, image);
        }
        private void Next_Button(object sender, RoutedEventArgs e)
        {
            ShowGifs(_Paths);
        }

    }

}
