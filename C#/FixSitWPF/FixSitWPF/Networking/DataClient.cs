using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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


        public string GetResponse(string request)
        {
            _Socket.Connect(new IPEndPoint(IPAddress.Parse(_IP), _Port));
            _Socket.Send(Encoding.UTF8.GetBytes("hello"));
            byte[] message = new byte[5];
            _Socket.Receive(message);
            return Encoding.UTF8.GetString(message);
        }


        

        public Socket Socket
        {
            get { return _Socket; }
            set { _Socket = value; }
        }


    }
}
