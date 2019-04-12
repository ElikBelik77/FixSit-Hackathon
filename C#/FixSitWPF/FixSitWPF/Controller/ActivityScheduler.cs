using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace FixSitWPF.Controller
{
    public delegate void ScheduleEvent(IActivity activity);

    /// <summary>
    /// Class for scheduling activities
    /// </summary>
    public class ActivityScheduler
    {
        /// <summary>
        /// Occurs when [on a schedule happens]
        /// </summary>
        public event ScheduleEvent OnScheduleEvent;

        #region Member Variables
        private Timer _ScheduleTimer;
        private Dictionary<IActivity,int> _Activities;
        private Dictionary<IActivity, int> _RemainingActivities;
        private int _IdleTime = 0;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the activities.
        /// </summary>
        /// <value>
        /// The activities.
        /// </value>
        public Dictionary<IActivity,int> Activities
        {
            get { return _Activities; }
            set { _Activities = value; }
        }

        /// <summary>
        /// Gets or sets the schedule timer.
        /// </summary>
        /// <value>
        /// The schedule timer.
        /// </value>
        public Timer ScheduleTimer
        {
            get { return _ScheduleTimer; }
            set { _ScheduleTimer = value; }
        }
        #endregion

        #region Constructors
        public ActivityScheduler(Dictionary<IActivity, int> activties)
        {

            _Activities = activties;
            _ScheduleTimer = new Timer();
            _ScheduleTimer.Elapsed += _ScheduleTimer_Elapsed;
            _ScheduleTimer.Interval = 1000;
            _RemainingActivities = new Dictionary<IActivity, int>();
            foreach(IActivity activ in _Activities.Keys)
            {
                _RemainingActivities.Add(activ, _Activities[activ]);
            }
        }
        #endregion

        #region Scheduling Functions        
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _ScheduleTimer.Start();
        }

        public void UpdateTimeInterval(string identifier, int newTime)
        {
            IActivity activity = _Activities.Keys.Where(x => x.GetIdentifier() == identifier).ToList()[0];
            int oldTime = _Activities[activity];
            _Activities[activity] = newTime;

            _RemainingActivities[activity] = newTime;


        }
        
        private void _ScheduleTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _IdleTime += 1;
            if(_RemainingActivities.First().Value <= _IdleTime)
            {
                _RemainingActivities.First().Key.OnFinish += Activity_OnFinish;
                _ScheduleTimer.Stop();
                System.Threading.Thread activityThread = new System.Threading.Thread(() => _RemainingActivities.First().Key.Start());
                activityThread.Start();
            }
        }
        
        public void Resume()
        {
            _ScheduleTimer.Start();
        }

        public void Pause()
        {
            _ScheduleTimer.Stop();
        }


        private void Activity_OnFinish(IActivity sender)
        {
            _IdleTime = 0;
            _RemainingActivities.Remove(sender);
            if (_RemainingActivities.Count == 0)
            {
                foreach (IActivity activ in _Activities.Keys)
                {
                    _RemainingActivities.Add(activ, _Activities[activ]);
                }
            }
            _ScheduleTimer.Start();
        }

        /// <summary>
        /// Raises the on scehdule event.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected virtual void RaiseOnScehduleEvent(IActivity activity)
        {
            OnScheduleEvent?.Invoke(activity);
        }
        #endregion
    }
}
