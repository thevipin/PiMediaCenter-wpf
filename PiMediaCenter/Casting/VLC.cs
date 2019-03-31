using PiMediaCenter.Network;
using PiMediaCenter.Network.Comm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;

namespace PiMediaCenter.Casting
{
    static class VLC
    {
        static Process VlcProcess;
        public static bool IsCasting;
        static PlayerRemoteController remoter = new PlayerRemoteController();
        static string VlcCode = ":sout=#rtp{sdp=rtsp://:8554/PiCastStream} :sout-keep  -I dummy --dummy-quiet";
        static string SrcStreamCode = "rtsp://192.168.1.121:8554/PiCastStream";
        static List<string> playList = new List<string>();
        static int currentIndex=-1;
        public static int CurrentIndex
        {
            set
            {
                currentIndex = value;
                try { onCurrentPlayChanges(value); } catch { }
            }
            get
            {
                return currentIndex;
            }
        }
        public delegate void fun(int index);
        public static fun onCurrentPlayChanges;
        public static List<string> PlayList
        {
            set
            {
                playList = value;
            }
            get
            {
                return playList;
            }
        }
        static  VLC()
        {
            string FilePath = Path.GetFullPath(@"vlc\vlc.exe");            
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("File Not found in location " + FilePath, "vlc.exe Not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (VlcProcess == null)
                {
                    VlcProcess = new Process();
                    VlcProcess.StartInfo.FileName = FilePath;
                    VlcProcess.StartInfo.RedirectStandardOutput = true;
                    VlcProcess.StartInfo.UseShellExecute = false;
                    VlcProcess.StartInfo.CreateNoWindow = true;
                    VlcProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    VlcProcess.Exited += (object sender, EventArgs e) => { VLC.IsCasting = false; };
                    IsCasting = false;
                    SharedPreferance pre = new SharedPreferance("VLC");
                    VlcCode = pre.get("VlcCode", VlcCode);
                    SrcStreamCode = pre.get("SrcStreamCode", SrcStreamCode);
                    remoter.onStop += () => { Stop(); };
                    Application.Current.Exit += Current_Exit;
                }
            }
        }
        
        private static void Current_Exit(object sender, ExitEventArgs e)
        {
            //throw new NotImplementedException();
            Stop();
        }
        public static void Play(string src)
        {
            playList.Add(src);
            PlayPlayList();
        }
        public static void PlayNext()
        {
            if((playList.Count>(CurrentIndex+1))&&(CurrentIndex>=0))
            {
                PlayPlayList(++CurrentIndex);
            }            
        }
        public static void PlayPrevious()
        {
            if(CurrentIndex>=0)
            {
                PlayPlayList(--CurrentIndex);
            }
        }
        public static void PlayPlayList(int Index=0)
        {
            if(playList.Count==0)
            {
                MessageBox.Show("Playlist is Empty" );
            }
            string src = playList[Index];
            try
            {
                if (!VlcProcess.HasExited)
                    VlcProcess.Kill();
            }
            catch { }
            //;
            Uri uri = new Uri(src);
            VlcProcess.StartInfo.Arguments = uri.AbsoluteUri + " "+ VlcCode;            
            VlcProcess.Start();            
            IsCasting = true;
            DataCodex code = new DataCodex();
            code.put("src", SrcStreamCode);
            string Name = (new FileInfo(src)).Name;
            code.put("name", Name);
            //VlcProcess.WaitForInputIdle(6000);
            remoter.Play(code);
            /*new Thread(() =>
            {
                VlcProcess.WaitForExit();
                VLC.IsCasting = false;
            }).Start();*/
            //VlcProcess
        }
        
        public static void Stop()
        {
            try
            {
                if (!VlcProcess.HasExited)
                    VlcProcess.Kill();
                VLC.IsCasting = false;
                CurrentIndex = -1;
            }
            catch { }
            
        }
    }
}
