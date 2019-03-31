using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiMediaCenter.Network.Comm;

namespace PiMediaCenter.Network
{
    class PlayerRemoteController
    {
        MainClient mainClient;
        public delegate void function();
        public delegate void function2(DataCodex code);
        public function onStop, onPause, onResume, onDecreaseSpeed, onIncreaseSpeed, onRewind, onFastForward, onShowSubtitles, onHideSubtitles;
        public function2 onPlay,onSentInfo;

        public PlayerRemoteController()
        {
            mainClient = new MainClient("PlayerRemoteController", onDataRecieved);
        }
        //==================Client====/

        void onDataRecieved(DataCodex code)
        {
            try
            {
                switch (code.get("call").ToLower())
                {
                    case "play":
                        onPlay(code);
                        break;
                    case "stop":
                        onStop();
                        break;
                    case "pause":
                        onPause();
                        break;
                    case "resume":
                        onResume();
                        break;
                    case "decreasespeed":
                        onDecreaseSpeed();
                        break;
                    case "increasespeed":
                        onIncreaseSpeed();
                        break;
                    case "rewind":
                        onRewind();
                        break;
                    case "fastForward":
                        onFastForward();
                        break;
                    case "showSubtitles":
                        onShowSubtitles();
                        break;
                    case "hideSubtitles":
                        onHideSubtitles();
                        break;
                    case "sentinfo":
                        onSentInfo(code);
                        break;
                }
            }
            catch { }
        }
        //=======toDataSent
        public void Play(DataCodex code)
        {
            code.setIdentifier("PlayerRemoteController");
            code.put("call", "play");
            mainClient.sentData(code);
        }
        public void getInfo()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "sendinfo");
            mainClient.sentData(code);
        }

        public void Stop()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "stop");
            mainClient.sentData(code);
        }
        public void Pause()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "pause");
            mainClient.sentData(code);
        }

        public void Resume()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "resume");
            mainClient.sentData(code);
        }
        public void DecreaseSpeed()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "DecreaseSpeed");
            mainClient.sentData(code);
        }
        public void IncreaseSpeed()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "IncreaseSpeed");
            mainClient.sentData(code);
        }
        public void Rewind()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "Rewind");
            mainClient.sentData(code);
        }
        public void FastForward()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "FastForward");
            mainClient.sentData(code);
        }
        public void ShowSubtitles()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "ShowSubtitles");
            mainClient.sentData(code);
        }
        public void HideSubtitles()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "HideSubtitles");
            mainClient.sentData(code);
        }
        public void DirectWrite(string value)
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "directwrite");
            code.put("char", value);
            mainClient.sentData(code);
        }
        public void Seek10Back()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "seek10back");           
            mainClient.sentData(code);
        }
        public void Seek10For()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "seek10for");            
            mainClient.sentData(code);
        }
    }
}
