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
        public function2 onPlay;

        public PlayerRemoteController()
        {
            mainClient = new MainClient("PlayerRemoteController", onDataRecieved);
        }
        //==================Client====/

        void onDataRecieved(DataCodex code)
        {
            switch (code.get("call"))
            {
                case "play":                    
                    onPlay(code);
                    break;
                case "stop":
                    onStop();
                    break;
                case "Pause":
                    onPause();
                    break;
                case "Resume":
                    onResume();
                    break;
                case "DecreaseSpeed":
                    onDecreaseSpeed();
                    break;
                case "IncreaseSpeed":
                    onIncreaseSpeed();
                    break;
                case "Rewind":
                    onRewind();
                    break;
                case "FastForward":
                    onFastForward();
                    break;
                case "ShowSubtitles":
                    onShowSubtitles();
                    break;
                case "HideSubtitles":
                    onHideSubtitles();
                    break;
            }
        }
        //=======toDataSent
        public void Play(DataCodex code)
        {
            code.setIdentifier("PlayerRemoteController");
            code.put("call", "play");
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
            code.put("call", "Pause");
            mainClient.sentData(code);
        }

        public void Resume()
        {
            DataCodex code = new DataCodex("PlayerRemoteController");
            code.put("call", "Resume");
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
    }
}
