using Microsoft.Win32;
using PiMediaCenter.Casting;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PiMediaCenter.userInterface
{
    /// <summary>
    /// Interaction logic for RemoterPlayerPage.xaml
    /// </summary>
    public partial class RemoterPlayerPage : UserControl
    {
        List<PlayListElementPage> Element = new List<PlayListElementPage>();
        public RemoterPlayerPage()
        {
            InitializeComponent();
            VLC.onCurrentPlayChanges += VlcPlayListChanged;
            playerContoller.onNameChange += onNameChanged;
        }

        public void onNameChanged(string name)
        {
            Dispatcher.Invoke(() =>
            {
                labelName.Content = name;
            });
        }

        private void buttonAddFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFilesDialog = new OpenFileDialog();
            openFilesDialog.Multiselect = true;
            openFilesDialog.Filter = "Video|*.mkv;*.vob;*.avi;*.mov;*mp4|Audio|*.mp3;*.ogg;*.aac| Other|*.*";
            if (openFilesDialog.ShowDialog() == true)
            {
                string[] files=openFilesDialog.FileNames;
                AddElements(files);
            }            
        }

        private void AllHolder_Drop(object sender, DragEventArgs e)
        {
            panelContainer_Drop(sender, e);
        }
        public void AddElements(string[] files)
        {
            for(int i = 0; i < files.Length; i++)
            {
                PlayListElementPage item = new PlayListElementPage(i, files[i]);
                item.onCLickPlay += onElementPlay_Click;
                item.onCLickRemove += onElementRemove_Click;
                panelContainer.Children.Add(item);                
                Element.Add(item);
                VLC.PlayList.Add(files[i]);
            }            
        }
        public void VlcPlayListChanged(int index)
        {

        }
        public void onElementPlay_Click(int index)
        {
            PiMediaCenter.Network.PlayerRemoteController con = new Network.PlayerRemoteController();
            con.Stop();            
            VLC.PlayPlayList(index);
        }
        public void onElementRemove_Click(int index)
        {
            if(index==VLC.CurrentIndex)
            {
                VLC.Stop();
            }
            VLC.PlayList.RemoveAt(index);
            panelContainer.Children.RemoveAt(index);
            Element.RemoveAt(index);
        }

        private void panelContainer_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<string> list = new List<string>();
            foreach(string file in files)
            {
                FileInfo info = new FileInfo(file);
                if(info.Extension== ".mkv"|| info.Extension == ".vob" || info.Extension == ".avi" || info.Extension == ".mov" || info.Extension == ".mp4")//".mp3"".ogg"".aac")
                {
                    list.Add(file);
                }
            }            
            AddElements(list.ToArray());
        }
    }
}