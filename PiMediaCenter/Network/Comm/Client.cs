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
using System.Windows;
using System.Windows.Threading;

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
        private Connected ConnectionSuccess;
        private ConnFailed ConnectionFailed;

        private void Threading()
        {           
            try
            {
                while (true)
                {
                    SharedPreferance preferance = new SharedPreferance("Server");
                    socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IpAddress = preferance.get("ServerIpAddress", "192.168.1.122");
                    PortAddress = preferance.get("ServerPortAddress", "8082");
                    IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(IpAddress), Int32.Parse(PortAddress));
                    socketClient.Connect(serverAddress);
                    ConnectionSuccess();
                    Application.Current.Exit += Current_Exit;                    
                    if (socketClient.Connected)
                    {
                        NetworkStream networkStream = new NetworkStream(socketClient);
                        StreamReader reader = new StreamReader(networkStream);
                        while (socketClient.Connected)
                        {
                            if (socketClient.Available > 0)
                            {

                                DataCodex codex = new DataCodex();
                                string data = reader.ReadLine();
                                codex.putCodex(data);
                                dataRecieved(codex);

                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Debug.Print("\n----*** Vipin ***---\nError :\n" + e.StackTrace);
                try { ConnectionFailed(e); } catch { }
            }
        }

        public Client(Connected onConnection, ConnFailed onConnFailed, DataRecieved onDataRecieved)
        {

            ConnectionSuccess = onConnection;
            dataRecieved = onDataRecieved;
            ConnectionFailed = onConnFailed;
            thread = new Thread(Threading);
            thread.Start();

        }
        private void Current_Exit(object sender, ExitEventArgs e)
        {            
            DisconnectAll();
        }

        public void sent(DataCodex code)
        {
            try
            {
                if (thread.IsAlive == false)
                {
                    try
                    {
                        thread.Start();
                        Thread.Sleep(100);
                    }
                    catch { }
                }
                if (socketClient == null)
                    return;
                if (socketClient.Connected)
                {
                    string data = code.getCodex();
                    NetworkStream networkStream = new NetworkStream(socketClient);
                    StreamWriter writer = new StreamWriter(networkStream);
                    writer.WriteLine(data);
                    writer.Flush();
                }
                else
                {
                    MessageBox.Show("No Connection");
                }
            }
            catch { }
        }
        public void DisconnectAll()
        {
            thread.Abort();
            socketClient.Close();           
        }
    }
}
