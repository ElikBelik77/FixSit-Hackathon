using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FixSitWPF.Controller
{
    public delegate void ScheduleEvent(IActivity activity);

    /// <summary>
    /// Class for scheduling activities
    /// </summary>
    public class ActivityScheduler
    {
        #region Member Variables

        private readonly Dictionary<IActivity, int> _RemainingActivities;
        private int _IdleTime;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the activities.
        /// </summary>
        /// <value>
        /// The activities.
        /// </value>
        public Dictionary<IActivity,int> Activities { get; set; }

        /// <summary>
        /// Gets or sets the schedule timer.
        /// </summary>
        /// <value>
        /// The schedule timer.
        /// </value>
        public Timer ScheduleTimer { get; set; }

        #endregion

        #region Constructors
        public ActivityScheduler(Dictionary<IActivity, int> activities)
        {

            Activities = activities;
            ScheduleTimer = new Timer();
            ScheduleTimer.Elapsed += ScheduleTimer_Elapsed;
            ScheduleTimer.Interval = 1000;
            _RemainingActivities = new Dictionary<IActivity, int>();
            foreach(IActivity activity in Activities.Keys)
            {
                _RemainingActivities.Add(activity, Activities[activity]);
            }
        }
        #endregion

        #region Scheduling Functions        
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            ScheduleTimer.Start();
        }

        public void UpdateTimeInterval(string identifier, int newTime)
        {
            IActivity activity = Activities.Keys.Where(x => x.GetIdentifier() == identifier).ToList()[0];
            Activities[activity] = newTime;
            _RemainingActivities[activity] = newTime;
        }

        private void ScheduleTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _IdleTime += 1;
            if (_RemainingActivities.First().Value > _IdleTime) return;

            _RemainingActivities.First().Key.OnFinish += Activity_OnFinish;
            ScheduleTimer.Stop();
            System.Threading.Thread activityThread = new System.Threading.Thread(() => _RemainingActivities.First().Key.Start());
            activityThread.Start();
        }
        
        public void Resume()
        {
            ScheduleTimer.Start();
        }

        public void Pause()
        {
            ScheduleTimer.Stop();
        }


        private void Activity_OnFinish(IActivity sender)
        {
            _IdleTime = 0;
            _RemainingActivities.Remove(sender);
            if (_RemainingActivities.Count == 0)
            {
                foreach (IActivity activ in Activities.Keys)
                {
                    _RemainingActivities.Add(activ, Activities[activ]);
                }
            }
            ScheduleTimer.Start();
        }

        #endregion
    }
}
