using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixSitWPF.Models
{
    public class SettingsModel 
    {
        
        #region Member Variables
        private double _PostureTimeInterval;
        private double _ExerciseTimeInterval;
        private double _SoundPower;
        #endregion

        #region Constructors
        
        public SettingsModel()
        {

        }
        #endregion


        #region Properties
        public double SoundPowers
        {
            get { return _SoundPower; }
            set { _SoundPower = value; }
        }

        public double ExerciseTimedoubleerval
        {
            get { return _ExerciseTimeInterval; }
            set { _ExerciseTimeInterval = value; }
        }

        public double Posturedoubleerval
        {
            get { return _PostureTimeInterval; }
            set { _PostureTimeInterval = value; }
        }
        #endregion
    }
}
