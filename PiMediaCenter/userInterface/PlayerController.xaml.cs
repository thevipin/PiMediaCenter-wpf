using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using PiMediaCenter.Casting;

namespace PiMediaCenter.userInterface
{
    /// <summary>
    /// Interaction logic for PlayerController.xaml
    /// </summary>
    public partial class PlayerController : UserControl
    {
        Network.PlayerRemoteController remoter;
        bool IsPause = false;
        bool IsIncrease = false;
        bool Is2x = false;
        public delegate void fun(string name);
        public fun onNameChange; 
        public PlayerController()
        {
            InitializeComponent();
            
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                remoter = new Network.PlayerRemoteController();
                remoter.onSentInfo += onSentInfo;
                remoter.onStop += onStop;
                remoter.getInfo();
            }
            catch { }
        }
        public void onStop()
        {
            Dispatcher.Invoke(() =>
            {
                try { onNameChange("Nothing Playing"); } catch { }
                IsPause = true;
            });
        }
        public void onSentInfo(PiMediaCenter.Network.Comm.DataCodex code)
        {
            //Dispatcher.Invoke(() =>
            //{
                //lblName.Content = code.get("name");
                try { onNameChange(code.get("name")); } catch { }
                IsPause = (code.get("IsPause") == "true");
            //});
        }

        private void btnPlaynPause_Click(object sender, RoutedEventArgs e)
        {
            if (IsPause)            
                remoter.Resume();            
            else
                remoter.Pause();
            IsPause = !IsPause;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            remoter.Stop();
            if (VLC.IsCasting)
                VLC.Stop();
        }

        private void btnRewind_Click(object sender, RoutedEventArgs e)
        {
            if (!VLC.IsCasting)
                remoter.DirectWrite("4");           
        }

        private void btnFastForward_Click(object sender, RoutedEventArgs e)
        {
            if (!VLC.IsCasting)
                remoter.DirectWrite("6");
        }

        private void btnPlaylistPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (VLC.IsCasting)
                VLC.PlayPrevious();
        }

        private void btnPlaylistNext_Click(object sender, RoutedEventArgs e)
        {
            if (VLC.IsCasting)
                VLC.PlayNext();
        }

        private void btnDecreaseSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (VLC.IsCasting)
                return;
            if (Is2x)
            {
                remoter.Rewind();
                Is2x = false;
            }
            else
            {
                remoter.DecreaseSpeed();
                IsIncrease = true;
            }
        }


        private void btnSubTitle_Click(object sender, RoutedEventArgs e)
        {            
            remoter.ShowSubtitles();           
        }

        private void btnSubHide_Click(object sender, RoutedEventArgs e)
        {
            remoter.HideSubtitles();
        }

        private void btnIncreaseSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (VLC.IsCasting)
                return;
            //
            if (IsIncrease)
            {
                remoter.IncreaseSpeed();
                IsIncrease = false;
            }
            else
            {
                remoter.FastForward();
                Is2x = true;
            }
            //remoter.Seek10For();
        }

        

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
