using FixSitWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixSitWPF.Controller
{
    public class Controller
    {
        #region Member Variables
        private SettingsModel _SettingsModel;
        private ActivityScheduler _ActivityScheduler;

        public ActivityScheduler ActivityScheduler
        {
            get { return _ActivityScheduler; }
            set { _ActivityScheduler = value; }
        }

        #endregion

        #region Properties
        public SettingsModel SettingsModel
        {
            get { return _SettingsModel; }
            set { _SettingsModel = value; }
        }
        #endregion

        #region Constructors
        public Controller()
        {
            _SettingsModel = new SettingsModel();
            //_ActivityScheduler = new ActivityScheduler(SettingsModel);

        }
        #endregion
    }
}
