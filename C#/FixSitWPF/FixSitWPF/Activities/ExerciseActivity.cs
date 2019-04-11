using FixSitWPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FixSitWPF.Activities
{
    public class ExerciseActivity : IActivity
    {

        public delegate void ExerciseEventArgs(List<string> paths);
        public event ExerciseEventArgs OnExerciseStart;
        public event ActivityEventArgs OnFinish;
        private Controller.Controller _Controller;

        public Controller.Controller Controller
        {
            get { return _Controller; }
            set { _Controller = value; }
        }

        public ExerciseActivity(Controller.Controller c)
        {
            _Controller = c;
        }

        public string GetIdentifier()
        {
            return "exercise";
        }

        public int GetPriority()
        {
            return 0;
        }

        public void Start()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Random rnd = new Random();
                int itemsNeeded = 3;
                List<string> gifs = new List<string>(itemsNeeded);
                List<int> numbers = new List<int>(itemsNeeded);
                List<int> availableNumbers = new List<int>();
                int n;
                for(int i =0; i < 5; i ++)
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
                OnExerciseStart?.Invoke(gifs);

            });
        }

        public void Stop()
        {
            
        }
    }
}
