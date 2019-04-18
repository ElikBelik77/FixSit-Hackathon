using FixSitWPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FixSitWPF.Properties;
using Application = System.Windows.Application;

namespace FixSitWPF.Activities
{
    /// <inheritdoc />
    /// <summary>
    /// Activity that instructs the user to do some exercises.
    /// </summary>
    /// <seealso cref="T:FixSitWPF.Controller.IActivity" />
    public class ExerciseActivity : IActivity
    {
        
        public delegate void ExerciseEventArgs(List<string> paths);
        public event ExerciseEventArgs OnExerciseStart;
        public event ActivityEventArgs OnFinish;

        #region Member Variables

        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public Controller.Controller Controller { get; set; }

        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ExerciseActivity"/> class.
        /// </summary>
        /// <param name="c">The controller.</param>
        public ExerciseActivity(Controller.Controller c)
        {
            Controller = c;
        }
        #endregion

        #region IActivity Functions        
        /// <inheritdoc />
        /// <summary>
        /// Gets the priority of the
        /// </summary>
        /// <returns></returns>
        public string GetIdentifier()
        {
            return "exercise";
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the priority of the activity.
        /// </summary>
        /// <returns></returns>
        public int GetPriority()
        {
            return 0;
        }

        /// <inheritdoc />
        /// <summary>
        /// Starts the activity.
        /// </summary>
        public void Start()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                NotifyIcon notification = new NotifyIcon()
                {
                    Visible = true,
                    Icon = System.Drawing.SystemIcons.Information,
                    BalloonTipTitle = Resources.ApplicationName,
                    BalloonTipText = Resources.ExercisesPopupText,
                };
                notification.ShowBalloonTip(5000);
                notification.BalloonTipClicked += (sender, e) =>
                    {


                        Random rnd = new Random();
                        const int itemsNeeded = 4;
                        List<string> gifPaths = new List<string>(itemsNeeded);
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
                        
                        string gif1 = pathToResources + numbers[0].ToString() + ".gif";
                        string gif2 = pathToResources + numbers[1].ToString() + ".gif";
                        string gif3 = pathToResources + numbers[2].ToString() + ".gif";
                        string gif4 = pathToResources + numbers[3].ToString() + ".gif";
                        gifPaths.Add(gif1);
                        gifPaths.Add(gif2);
                        gifPaths.Add(gif3);
                        gifPaths.Add(gif4);
                        OnExerciseStart?.Invoke(gifPaths);
                        //DO SOMETHING
                    };
            });
            OnFinish?.Invoke(this);
        }

        /// <inheritdoc />
        /// <summary>
        /// Stops the activity.
        /// </summary>
        public void Stop()
        {

        }
        #endregion
    }
}
