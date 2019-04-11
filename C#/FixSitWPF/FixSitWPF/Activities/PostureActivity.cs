using FixSitWPF.Controller;
using FixSitWPF.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixSitWPF.Extensions;
using System.Windows.Forms;

namespace FixSitWPF.Activities
{
    public class PostureActivity : IActivity
    {
        #region Member Variables
        private DataClient _Client;
        #endregion

        #region Properties
        public DataClient Client
        {
            get { return _Client; }
            set { _Client = value; }
        }
        #endregion


        public event ActivityEventArgs OnFinish;

        public string GetIdentifier()
        {
            return "posture";
        }

        public int GetPriority()
        {
            return 0;
        }

        public void Start()
        {
            //Dictionary<string, string> request = new Dictionary<string, string>()
            //{
            //    {"request","posture_status" }
            //};
            //JToken response = _Client.GetResponse(request.ToJson());
            var notification = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                Icon = System.Drawing.SystemIcons.Information,
                // optional - BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                // optional - BalloonTipTitle = "My Title",
                BalloonTipText = "FIX YOUR POSTURE YOU ARE NOT SITTING WELL!!!!!!!",
            };
            
            // Display for 5 seconds.
            notification.ShowBalloonTip(5000);

//            Thread.Sleep(10000);

            // The notification should be disposed when you don't need it anymore,
            // but doing so will immediately close the balloon if it's visible.
            notification.Dispose();
            OnFinish?.Invoke(this);
        }

        public void Stop()
        {
            
        }
    }
}
