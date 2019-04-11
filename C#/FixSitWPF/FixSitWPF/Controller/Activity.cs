using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixSitWPF.Controller
{

    public delegate void ActivityEventArgs(IActivity sender);
    public interface IActivity
    {
        void Start();
        void Stop();
        event ActivityEventArgs OnFinish;
        int GetPriority();
        string GetIdentifier();
    }
}
