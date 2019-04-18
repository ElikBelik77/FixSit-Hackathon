using FixSitWPF.Controller;
using FixSitWPF.Networking;
using System;
using System.Collections.Generic;
using FixSitWPF.Extensions;
using System.Drawing;
using System.IO;
using FixSitWPF.Properties;
using Newtonsoft.Json.Linq;

namespace FixSitWPF.Activities
{
    public class PostureActivity : IActivity
    {
        public delegate void WebcamImageEventArgs(Image image, string description);
        public event WebcamImageEventArgs OnImageUpdate;

        #region Member Variables

        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public PostureActivity(DataClient client)
        {
            Client = client;
        }
        #endregion

        #region Properties
        public DataClient Client { get; set; }

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
            JToken response = Client.GetResponse(request.ToJson());
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
                balloonTipText = "Your posture is perfect!";
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var notification = new System.Windows.Forms.NotifyIcon()
                {
                    Visible = true,
                    Icon = SystemIcons.Information,
                        // BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                        BalloonTipTitle = Resources.ApplicationName,
                    BalloonTipText = balloonTipText,
                };
                if (displayImage)
                {
                    notification.BalloonTipClicked += (sender, e) =>
                    {
                        Image modelImage = LoadImage(response["image"].ToString());
                        OnImageUpdate?.Invoke(modelImage, response["description"].ToString());
                            //DO SOMETHING
                        };
                }
                notification.ShowBalloonTip(5000);
            });

            OnFinish?.Invoke(this);
            //REMOVE THIS !
        }

        public void Stop()
        {
            
        }

        private static Image LoadImage(string base64)
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
