using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;


namespace FixSitWPF.Controller
{
    public delegate void ScheduleEvent(IActivity activity);
    public class ActivityScheduler
    {
        public event ScheduleEvent OnScheduleEvent;

        private Timer _ScheduleTimer;
        private Dictionary<IActivity,int> _Activities;
        private Dictionary<IActivity, int> _RemainingActivities;
        public Dictionary<IActivity,int> Activities
        {
            get { return _Activities; }
            set { _Activities = value; }
        }



        public Timer ScheduleTimer
        {
            get { return _ScheduleTimer; }
            set { _ScheduleTimer = value; }
        }


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

        public void Start()
        {
            _ScheduleTimer.Start();
        }

        private int _IdleTime = 0;
        private void _ScheduleTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _IdleTime += 1;
            if(_RemainingActivities.First().Value == _IdleTime)
            {
                _RemainingActivities.First().Key.OnFinish += Activity_OnFinish;
                _ScheduleTimer.Stop();
                System.Threading.Thread activityThread = new System.Threading.Thread(() => _RemainingActivities.First().Key.Start());
                activityThread.Start();
            }
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

        protected virtual void RaiseOnScehduleEvent(IActivity activity)
        {
            OnScheduleEvent?.Invoke(activity);
        }
    }
}
