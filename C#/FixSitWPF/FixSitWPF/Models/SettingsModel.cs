using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace FixSitWPF.Models
{
    public class SettingsModel : INotifyPropertyChanged
    {
        
        #region Member Variables
        private int _PostureTimeInterval;
        private int _ExerciseTimeInterval;
        #endregion

        #region Constructors
        public event PropertyChangedEventHandler PropertyChanged;
        public SettingsModel()
        {
            _PostureTimeInterval = 15;
            _ExerciseTimeInterval = 30;
        }
        #endregion

        #region Properties
        public int ExerciseTimeInterval
        {
            get { return _ExerciseTimeInterval; }
            set { _ExerciseTimeInterval = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExerciseTimeInterval"));
            }
        }

        public int PostureTimeInterval
        {
            get { return _PostureTimeInterval; }
            set { _PostureTimeInterval = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PostureTimeInterval"));
            }
        }
        #endregion
    }
}
