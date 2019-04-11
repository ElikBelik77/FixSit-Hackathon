﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace FixSitWPF.Networking
{
    public class DataClient
    {
        private Socket _Socket;
        private Thread _CommunicationThread;
        private int _Port;
        private string _IP;

        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }

        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        public Thread CommunicationThread
        {
            get { return _CommunicationThread; }
            set { _CommunicationThread = value; }
        }
          

        public DataClient(string ip, int port)
        {
            _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _IP = ip;
            _Port = port;
        }

        private void SendRequest(JToken request)
        {
            string data = request.ToString().Length.ToString().PadLeft(5, '0') + request.ToString();
            _Socket.Send(Encoding.UTF8.GetBytes(data));
        }

        private JObject ReadResponse()
        {
            byte[] messageLengthBuffer = new byte[5];
            _Socket.Receive(messageLengthBuffer);
            int messageLength = int.Parse(Encoding.UTF8.GetString(messageLengthBuffer));
            byte[] messageBuffer = new byte[messageLength];
            _Socket.Receive(messageBuffer);
            string message = Encoding.UTF8.GetString(messageBuffer);
            return (JObject)JsonConvert.DeserializeObject(message);
        }
        public JObject GetResponse(JToken request)
        {
            SendRequest(request);
            return ReadResponse();
        }


        

        public Socket Socket
        {
            get { return _Socket; }
            set { _Socket = value; }
        }


    }
}