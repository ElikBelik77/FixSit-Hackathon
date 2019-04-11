using FixSitWPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixSitWPF.Activities
{
    public class ExerciseActivity : IActivity
    {
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
            
        }

        public void Stop()
        {
            
        }
    }
}
