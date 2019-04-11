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
using System.Windows.Shell;
using System.Windows.Threading;

namespace FixSitWPF.Activities
{
    public class PostureActivity : IActivity
    {
        #region Member Variables
        private DataClient _Client;
        #endregion

        public PostureActivity(DataClient client)
        {
            _Client = client;
        }

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
            Dictionary<string, string> request = new Dictionary<string, string>()
            {
                {"request","posture_status" }
            };
            JToken response = _Client.GetResponse(request.ToJson());
            //string balloonTipText = "Your posture is not good!";
            string balloonTipText = (response["answer"].ToString() == "bad" ? "Your posture is not good" + Environment.NewLine
                + "Click here to fix it" : "Your posture is good!");
            if (response["answer"].ToString() == "bad")
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    var notification = new System.Windows.Forms.NotifyIcon()
                    {
                        Visible = true,
                        Icon = System.Drawing.SystemIcons.Information,
                        // BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                        BalloonTipTitle = "FixSit",
                        BalloonTipText = balloonTipText,
                    };

                    notification.BalloonTipClicked += (sender, e) =>
                   {
                       Console.WriteLine("click");
                       //DO SOMETHING
                   };
                    notification.ShowBalloonTip(5000);
                });
               
            }

            //REMOVE THIS !
            OnFinish?.Invoke(this);
        }

        private void Notification_BalloonTipClicked(object sender, EventArgs e)
        {
            Console.WriteLine("click");
        }

        public void Stop()
        {
            
        }
    }
}
