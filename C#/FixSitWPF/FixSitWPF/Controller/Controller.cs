using FixSitWPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void CreatePythonModel()
        {
            string[] splitDirData = Environment.CurrentDirectory.Split(new[] { @"\" }, StringSplitOptions.None);
            string pythonModelPath = String.Join("/",splitDirData.Take(splitDirData.Length-5))+ @"/Python/main.py";
            Process.Start(pythonModelPath);
        }
    }
}
