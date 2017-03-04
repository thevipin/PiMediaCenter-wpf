using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace PiMediaCenter.Network.Comm
{
    class Client
    {
        private Socket socketClient;
        //private NetworkStream networkStream;
        private string IpAddress;
        private string PortAddress;
        private Thread thread;
        public delegate void Connected();
        public delegate void ConnFailed(Exception e);        
        public delegate void DataRecieved(DataCodex code);
        private DataRecieved dataRecieved;        
        private void Threading()
        {
            if (socketClient.Connected)
            {                
                while (socketClient.Connected)
                {
                    if (socketClient.Available > 0)
                    {
                        NetworkStream networkStream = new NetworkStream(socketClient);
                        StreamReader reader = new StreamReader(networkStream);
                        DataCodex codex = new DataCodex();
                        //codex.Socketindex = Socketindex;
                        string data = reader.ReadLine();
                        codex.putCodex(data);
                        dataRecieved(codex);
                    }
                }
            }
        }

        public Client(Connected onConnection, ConnFailed onConnFailed, DataRecieved onDataRecieved)
        {
            
            try
            {
                SharedPreferance preferance = new SharedPreferance("Server");
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IpAddress = preferance.get("ServerIpAddress", "192.168.1.122");
                PortAddress = preferance.get("ServerPortAddress", "8082");
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IpAddress), Int32.Parse(PortAddress));
                socketClient.Connect(serverAddress);
                onConnection();                                
                dataRecieved = onDataRecieved;
                thread = new Thread(Threading);
            }
            catch(Exception e)
            {
                Debug.Print("\n----*** Vipin ***---\nError :\n"+e.StackTrace);
                onConnFailed(e);
            }
        }
        
        
        public void sent(DataCodex code)
        {
            string data = code.getCodex();
            NetworkStream networkStream = new NetworkStream(socketClient);
            StreamWriter writer = new StreamWriter(networkStream);
            writer.WriteLine(data);
            writer.Flush();
        }
    }
}
