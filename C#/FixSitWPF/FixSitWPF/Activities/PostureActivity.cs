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
using System.Drawing;
using System.IO;

namespace FixSitWPF.Activities
{
    public class PostureActivity : IActivity
    {
        public delegate void WebcamImageEventArgs(Image image, string description);
        public event WebcamImageEventArgs OnImageUpdate;
        #region Member Variables
        private DataClient _Client;
        #endregion

        #region Constructors
        public PostureActivity(DataClient client)
        {
            _Client = client;
        }
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
            bool displayImage = false;
            Dictionary<string, string> request = new Dictionary<string, string>()
            {
                {"request","posture_status" }
            };
            JToken response = _Client.GetResponse(request.ToJson());
            string balloonTipText = "";
            if (response["answer"].ToString() == "bad")
            {
                balloonTipText = "Your posture is not good" + Environment.NewLine
                + "Click here to fix it";
                displayImage = true;
            }
            else if (response["answer"].ToString() == "image_error" || response["answer"].ToString() == "camera_error")
            {
                balloonTipText = "FixSit can't evaluate your posture";
            }
            else
            {
                balloonTipText = "You are sitting good, well done !";
            }

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
                    if (displayImage)
                    {
                        notification.BalloonTipClicked += (sender, e) =>
                        {    
                           Image modelImage = LoadImage(response["image"].ToString());
                           OnImageUpdate?.Invoke(modelImage, response["image"].ToString());
                           //DO SOMETHING
                        };
                    }
                    notification.ShowBalloonTip(5000);
                });
               
            }

            //REMOVE THIS !
        }


        public void Stop()
        {
            
        }

        private Image LoadImage(string base64)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(base64);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
    }
}
