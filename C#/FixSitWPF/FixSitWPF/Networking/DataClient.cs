﻿using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FixSitWPF.Networking
{
    public class DataClient
    {
        #region Member Variables
        private readonly int _PrefixLength = 10;
        private Socket _Socket;

        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the communication thread.
        /// </summary>
        /// <value>
        /// The communication thread.
        /// </value>
        public Thread CommunicationThread { get; set; }

        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="DataClient"/> class.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public DataClient(string ip, int port)
        {
            IP = ip;
            Port = port;
        }
        #endregion

        #region Communication        
        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="request">The request.</param>
        private void SendRequest(string request)
        {
            string data = request.Length.ToString().PadLeft(_PrefixLength, '0') + request;
            _Socket.Send(Encoding.UTF8.GetBytes(data));
        }
        
        private JObject ReadResponse()
        {
            string message = "";
            byte[] messageLengthBuffer = new byte[10];
            _Socket.Receive(messageLengthBuffer);
            int messageLength = int.Parse(Encoding.UTF8.GetString(messageLengthBuffer));

            int count = messageLength / 5000;
            byte[] messageBuffer;
            while (count > 0)
            {
                messageBuffer = new byte[5000];
                _Socket.Receive(messageBuffer);
                Thread.Sleep(500);

                message += Encoding.UTF8.GetString(messageBuffer);
                count--;
            }
            messageBuffer = new byte[messageLength - 5000 * (messageLength / 5000)];
            _Socket.Receive(messageBuffer);
            
            
            message += Encoding.UTF8.GetString(messageBuffer);
            return (JObject)JsonConvert.DeserializeObject(message);
        }

        /// <summary>
        /// Gets the response for the given request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response for the given json request</returns>
        public JObject GetResponse(string request)
        {
            _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _Socket.Connect(new IPEndPoint(IPAddress.Parse(IP), Port));
            SendRequest(request);
            JObject response = ReadResponse();
            _Socket.Close();
            return response;
        }
        #endregion
    }
}
