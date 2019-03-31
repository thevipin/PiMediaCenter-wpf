using System;
using System.Collections.Generic;
using PiMediaCenter.Network.Comm;
using System.Diagnostics;

namespace PiMediaCenter.Network
{
    class MainClient
    {
        private static List<string> ListIdentifier = new List<string>();
        public delegate void DataReciever(DataCodex code);
        private static List<DataReciever> ListDataReciever = new List<DataReciever>();
        private static Client client = new Client(onConnected, onConnFailed, onDataRecieved);
        static void onConnected()
        {

        }
        static private void onConnFailed(Exception e)
        {
            //throw new NotImplementedException();
            Debug.Print("From MainClient 'Connection Failed'\n" + e.StackTrace);
        }
        static void onDataRecieved(DataCodex code)
        {
            ListDataReciever[ListIdentifier.IndexOf(code.getIdentifier())](code);
        }

        string identfier;
        DataReciever dataRecievered;

        public MainClient(string Identfier, DataReciever onDataReciever)
        {
            identfier = Identfier;
            dataRecievered = onDataReciever;
            ListIdentifier.Add(identfier);
            ListDataReciever.Add(dataRecievered);
        }
        public static void Kill()
        {            
            client.DisconnectAll();
        }
        ~MainClient()
        {
            ListIdentifier.Remove(identfier);
            ListDataReciever.Remove(dataRecievered);
            client.DisconnectAll();
        }
        public void sentData(DataCodex code)
        {
            code.setIdentifier(identfier);
            client.sent(code);
        }
    }
}
