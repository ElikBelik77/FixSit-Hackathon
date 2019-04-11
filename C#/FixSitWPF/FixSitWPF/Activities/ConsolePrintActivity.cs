using FixSitWPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixSitWPF.Activities
{
    public class ConsolePrintActivity : IActivity
    {
        public event ActivityEventArgs OnFinish;
        private string _str;
        public ConsolePrintActivity(string str)
        {
            _str = str;
        }
        public string GetIdentifier()
        {
            return "Console";
        }

        public int GetPriority()
        {
            return 0;
        }

        public void Start()
        {
            Console.WriteLine(_str);
            OnFinish?.Invoke(this);
        }

        public void Stop()
        {
            
        }
    }
}
